public class BulletConfig : BaseConfig
{
    public string prefab;
    public float speed;
    public float lifeTime;
}


public class GunConfig : BaseConfig
{
    public int magzineSize;
    public BulletConfig bullet;
}
