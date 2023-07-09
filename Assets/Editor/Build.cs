using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEngine;

public class Build : MonoBehaviour
{

    [MenuItem("Build/Windows")]
    public static void BuildForWindows()
    {
        BuildFor(BuildTarget.StandaloneWindows64);
    }

    [MenuItem("Build/Linux")]
    public static void BuildForLinux()
    {
        BuildFor(BuildTarget.StandaloneLinux64);
    }

    [MenuItem("Build/MacOS")]
    public static void BuildForMacOS()
    {
        BuildFor(BuildTarget.StandaloneOSX);
    }

    public static void BuildFor(BuildTarget platform)
    {
        //select path to build
        string path = EditorUtility.SaveFolderPanel("选择生成的路径: ", "", "");
        AddressableHandler.ReimportFolder();
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, Path.Combine(path, "Game/Game.exe"), platform, BuildOptions.None);
        

        string configPath = Path.Combine(path, "Game/Config");
        if (Directory.Exists(configPath))
        {
            Directory.Delete(configPath, true);
        }
        else
        {
            Directory.CreateDirectory(configPath);
        }
        string editorConfigPath = ConstGlobal.GetEditorConfigDirectory();
        string[] files = Directory.GetFiles(editorConfigPath, "*.xml", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            File.Copy(file, Path.Combine(configPath, Path.GetFileName(file)));
        }
    }
}