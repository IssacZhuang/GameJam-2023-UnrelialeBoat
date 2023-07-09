using Vocore;

public class BulletConfig : BaseThingConfig
{
    public float speed;
    public float lifeTime;
}


public class GunConfig : BaseThingConfig
{
    public int magzineSize;
    public BulletConfig bullet;
}


public class InteractiveObjectConfig : BaseThingConfig
{
    public bool isKeyItem; // 是否为重要物品 (最后的门也算)
    public bool isDoor; // 是否为门 (普通门和重要门)
    public bool isKey; // 是否为钥匙 (唯一,用于打开最后的门)
    public float detectionRadius; // 物体的探测半径
    public string description;  // 非重要物品的描述,以及没有拿到钥匙时最后重要的门的描述

    public string dialogConfig; // 重要物品触发的对话
    public string audioID;  // 重要物品触发的事件ID
}