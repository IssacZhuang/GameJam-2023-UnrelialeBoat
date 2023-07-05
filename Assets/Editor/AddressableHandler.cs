using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;

public class AddressableHandler : AssetPostprocessor
{
    public struct ResourceFolderInfo
    {
        public string path;
        public string prefix;
        public string[] extesions;

        public bool Match(string importedAsset)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            if (!importedAsset.Contains(path))
            {
                return false;
            }

            if (extesions == null || extesions.Length == 0)
            {
                return false;
            }
            foreach (var extesion in extesions)
            {
                if (importedAsset.EndsWith(extesion))
                {
                    return true;
                }
            }
            return false;
        }

        public static ResourceFolderInfo Create(string path, string prefix, params string[] extesions)
        {
            return new ResourceFolderInfo()
            {
                path = path,
                prefix = prefix,
                extesions = extesions
            };
        }
    }

    public static readonly ResourceFolderInfo InfoAnimation = ResourceFolderInfo.Create("Assets/Resources/Animations", "Animation_", ".anim");
    public static readonly ResourceFolderInfo InfoSound = ResourceFolderInfo.Create("Assets/Resources/Sounds", "Sound_", ".mp3", ".wav", ".ogg");
    public static readonly ResourceFolderInfo InfoPrefab = ResourceFolderInfo.Create("Assets/Resources/Prefabs", "Prefab_", ".prefab");
    public static readonly ResourceFolderInfo InfoMaterial = ResourceFolderInfo.Create("Assets/Resources/Materials", "Material_", ".mat");
    public static readonly ResourceFolderInfo InfoTexture = ResourceFolderInfo.Create("Assets/Resources/Textures", "Texture_", ".png", ".jpg", ".jpeg", ".tga", ".bmp", ".psd", ".gif", ".exr", ".iff", ".pict", ".pic", ".hdr", ".tiff");
    public static readonly ResourceFolderInfo InfoShader = ResourceFolderInfo.Create("Assets/Resources/Shaders", "Shader_", ".shader");

    private static StringBuilder _strBuilder = new StringBuilder();
    public static AddressableAssetSettings Settings
    {
        get
        {
            return AddressableAssetSettingsDefaultObject.GetSettings(true);
        }
    }

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
    {
        //remove group not in used except Default
        var groups = Settings.groups;

        for (int i = groups.Count - 1; i >= 0; i--)
        {
            var group = groups[i];
            if (group.IsDefaultGroup())
            {
                continue;
            }

            List<AddressableAssetEntry> entrieToRmove = new List<AddressableAssetEntry>();

            foreach (var entry in group.entries)
            {
                if (!File.Exists(entry.AssetPath))
                {
                    entrieToRmove.Add(entry);
                }
            }

            foreach (var entry in entrieToRmove)
            {
                group.RemoveAssetEntry(entry);
                LogEntryRemoved(group, entry);
            }


            if (group.entries.Count == 0)
            {
                Settings.RemoveGroup(group);
            }
        }

        foreach (var importedAsset in importedAssets)
        {
            try
            {
                if (InfoAnimation.Match(importedAsset))
                {
                    HandleAsset<AnimationClip>(importedAsset, InfoAnimation);
                }
                else if(InfoMaterial.Match(importedAsset))
                {
                    HandleAsset<Material>(importedAsset, InfoMaterial);
                }
                else if(InfoPrefab.Match(importedAsset))
                {
                    HandleAsset<GameObject>(importedAsset, InfoPrefab);
                }
                else if(InfoShader.Match(importedAsset))
                {
                    HandleAsset<Shader>(importedAsset, InfoShader);
                }
                else if(InfoSound.Match(importedAsset))
                {
                    HandleAsset<AudioClip>(importedAsset, InfoSound);
                }
                else if(InfoTexture.Match(importedAsset))
                {
                    HandleAsset<Texture>(importedAsset, InfoTexture);
                } 
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        try
        {
            DatabaseLoader.Load();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    static void HandleAsset<T>(string importedAsset, ResourceFolderInfo info) where T : UnityEngine.Object
    {
        T asset = AssetDatabase.LoadAssetAtPath<T>(importedAsset);
        if (asset == null)
        {
            return;
        }

        string guid = AssetDatabase.AssetPathToGUID(importedAsset);

        AddressableAssetGroup group = FindOrCreateGroup(info, importedAsset);

        AddressableAssetEntry entry = Settings.CreateOrMoveEntry(guid, group);
        entry.address = asset.name;
        LogAddressableCreated(group, entry);
    }

    static AddressableAssetGroup FindOrCreateGroup(ResourceFolderInfo info, string importedAsset)
    {
        //find or create addressable group by it parent folder name
        string folderName = importedAsset.Substring(0, importedAsset.LastIndexOf('/'));
        string groupName = info.prefix + folderName.Substring(folderName.LastIndexOf('/') + 1);
        return Settings.FindGroup(groupName) ?? Settings.CreateGroup(groupName, false, false, false, null, typeof(BundledAssetGroupSchema));
    }

    static void LogAddressableCreated(AddressableAssetGroup group, AddressableAssetEntry entry)
    {
        _strBuilder.Clear();

        _strBuilder.Append("Addressable created! ");

        _strBuilder.Append(" Group: ");
        _strBuilder.Append("<color=#00ff00ff>");
        _strBuilder.Append(group.Name);
        _strBuilder.Append("</color>");

        _strBuilder.Append(", Entry: ");
        _strBuilder.Append("<color=#00ff00ff>");
        _strBuilder.Append(entry.address);
        _strBuilder.Append("</color>");

        _strBuilder.Append(", Path: ");
        _strBuilder.Append("<color=#00ff00ff>");
        _strBuilder.Append(entry.AssetPath);
        _strBuilder.Append("</color>");

        Debug.Log(_strBuilder.ToString());
    }

    static void LogEntryRemoved(AddressableAssetGroup group, AddressableAssetEntry entry)
    {
        //use red
        _strBuilder.Clear();

        _strBuilder.Append("Addressable removed! ");

        _strBuilder.Append(" Group: ");
        _strBuilder.Append("<color=#ff0000ff>");
        _strBuilder.Append(group.Name);
        _strBuilder.Append("</color>");

        _strBuilder.Append(", Entry: ");
        _strBuilder.Append("<color=#ff0000ff>");
        _strBuilder.Append(entry.address);
        _strBuilder.Append("</color>");

        _strBuilder.Append(", Path: ");
        _strBuilder.Append("<color=#ff0000ff>");
        _strBuilder.Append(entry.AssetPath);
        _strBuilder.Append("</color>");

        Debug.Log(_strBuilder.ToString());
    }

    [MenuItem("Tools/重新生成Addressables")]
    public static void ReimportFolder()
    {
        var groups = Settings.groups;
        for (int i = groups.Count - 1; i >= 0; i--)
        {
            var group = groups[i];
            if (group.IsDefaultGroup())
            {
                continue;
            }

            Settings.RemoveGroup(group);
        }

        AssetDatabase.StartAssetEditing();
        AssetDatabase.ImportAsset(InfoAnimation.path, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ImportRecursive);
        AssetDatabase.ImportAsset(InfoMaterial.path, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ImportRecursive);
        AssetDatabase.ImportAsset(InfoPrefab.path, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ImportRecursive);
        AssetDatabase.ImportAsset(InfoShader.path, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ImportRecursive);
        AssetDatabase.ImportAsset(InfoSound.path, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ImportRecursive);
        AssetDatabase.ImportAsset(InfoTexture.path, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ImportRecursive);
        AssetDatabase.StopAssetEditing();
        AssetDatabase.Refresh();
    }
}
