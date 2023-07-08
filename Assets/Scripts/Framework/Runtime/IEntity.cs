public interface IEntity:IEventReciever
{
    void Spawn(Map map);
    void Despawn();
    void OnCreate();
    void OnUpdate();
    void OnTick();
}
