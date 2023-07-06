public interface IEntity
{
    void Spawn(Map map);
    void OnSpawn();
    void Despawn();
    void OnDespawn();
    void OnCreate();
    void OnUpdate();
    void OnTick();
}
