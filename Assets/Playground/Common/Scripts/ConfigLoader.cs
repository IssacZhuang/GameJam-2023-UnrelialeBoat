using System.Collections;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;

using Vocore;
using Config;

public static class ConfigLoader
{
    public static DemoConfig LoadDemoConfig(TextAsset config, string name)
    {

        DemoConfig result = null;

        try
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(config.text)))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                XmlNode node = doc.SelectSingleNode($"Config/DemoConfig[name = '{name}']");

                result = XmlParser.ObjectFromXml<DemoConfig>(node);

            }
        }
        catch (XmlException e)
        {
            Debug.LogError($"Error parsing config: {e.Message}");
        }

        return result;
    }
}
