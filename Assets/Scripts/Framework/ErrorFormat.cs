using System;

public static class ErrorFormat
{
    public static readonly string EmptyConfigName = TextColor.Red("配置的名称(<name/>字段)不能为空");
    public static readonly string NullConfig = TextColor.Red("尝试传入空的Config对象");
    public static string ConfigNotFound(Type type, string name)
    {
        return TextColor.Red("找不到配置: 类型" + type.ToString() + ", 名称" + name);
    }
    public static string DuplicatedConfigName(string name)
    {
        return TextColor.Red("配置的名称(<name/>字段)重复: " + name);
    }
    public static string InvalidConfigName(string name)
    {
        return TextColor.Red("配置的名称(<name/>字段)不合法, 只能包含字母、数字和下划线: " + name);
    }
    public static string ObjectNotConfig(Type type)
    {
        return TextColor.Red("对象未继承于BaseConfig, 类型: " + type.ToString());
    }
    public static string InvalidXmlObject(string xmlContent, string excpt)
    {
        return TextColor.Red("无法转换XML到C#对象: \n" + excpt + "\n XML文本：\n" + xmlContent);
    }

    public static string InvalidXmlDocument(string xmlContent, string excpt)
    {
        return TextColor.Red("无法读取XML文本: \n" + excpt + "\n XML文本：\n" + xmlContent);
    }
}
