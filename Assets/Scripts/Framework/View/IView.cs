public interface IView: IEventReciever
{
    
    void Destroy();
    void Show();
    void Hide();
    void OnCreate();
    void OnUpdate();
    void OnTick();
}
