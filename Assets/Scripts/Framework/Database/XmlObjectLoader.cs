using System;
using System.IO;
using System.Xml;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

using Vocore;

public class XmlConfigLoader
{
    private List<BaseConfig> _content = new List<BaseConfig>();
    private const string XPathRootNode = "Data";
    private const string RegexPattern = @"^[a-zA-Z0-9_]+$";

    public List<BaseConfig> Content
    {
        get
        {
            return _content;
        }
    }

    /// <summary>
    /// 加载/Config/目录下的所有xml文件, 并转换成C#对象
    /// </summary>
    public void Load()
    {
        Load(ConstGlobal.GetConfigDirectory());
    }

    /// <summary>
    /// 加载指定目录下的所有xml文件, 并转换成C#对象
    /// </summary>
    public void Load(string dicrectory)
    {
        _content.Clear();
        string[] files = Directory.GetFiles(dicrectory, "*.xml", SearchOption.AllDirectories);
        List<BaseConfig>[] content = new List<BaseConfig>[files.Length];
        for (int i = 0; i < files.Length; i++)
        {
            content[i] = new List<BaseConfig>();
        }

        Parallel.For(0, files.Length, i =>
        {
            LoadFile(files[i], content[i]);
        });

        foreach (var list in content)
        {
            _content.AddRange(list);
        }
    }


    private void LoadFile(string path, List<BaseConfig> content)
    {
        if (!File.Exists(path))
        {
            return;
        }

        string xmlContent = File.ReadAllText(path);

        try
        {
            //To xml document
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlContent);

            XmlNode rootNode = xmlDocument.SelectSingleNode(XPathRootNode);

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                try
                {
                    object obj = node.ParseToObject();

                    BaseConfig config = obj as BaseConfig;

                    if (config == null)
                    {
                        Debug.LogError(ErrorFormat.ObjectNotConfig(obj.GetType()));
                        continue;
                    }

                    if(config.name == null || config.name == "")
                    {
                        Debug.LogError(ErrorFormat.EmptyConfigName);
                        continue;
                    }

                    if(!Regex.IsMatch(config.name, RegexPattern))
                    {
                        Debug.LogError(ErrorFormat.InvalidConfigName(config.name));
                        continue;
                    }

                    content.Add(config);
                }
                catch (Exception e)
                {
                    Debug.LogError(ErrorFormat.InvalidXmlObject(xmlContent, e.ToString()));
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(ErrorFormat.InvalidXmlDocument(xmlContent, e.ToString()));
        }
    }


}
