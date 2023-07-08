using System;

using UnityEngine;

public class Game : MonoBehaviour
{
    private static Game _instance;
    public static Game Instance
    {
        get
        {
            return _instance;
        }
    }

    private Map _currentMap;
    public Map CurrentMap
    {
        get
        {
            return _currentMap;
        }
    }

    [InspectorName("游戏配置文件")]
    public string gameConfig;

    public void LoadMap(MapConfig config)
    {
        if (_currentMap != null)
        {
            _currentMap.Destroy();
        }

        _currentMap = Map.CreateMap(config);
        _currentMap.Scene.transform.position = Vector3.zero;
    }

    public void RelaodMap()
    {
        if (_currentMap == null)
        {
            return;
        }

        MapConfig config = _currentMap.Config;

        _currentMap.Destroy();
        _currentMap = Map.CreateMap(config);
    }

    private void Start()
    {
        _instance = this;

        DatabaseLoader.Load();

        GameConfig config = Content.GetConfig<GameConfig>(gameConfig, true);
        Time.fixedDeltaTime = 1 / config.tickRate;

        LoadMap(config.defaultMap);
        MainMenu.PopMainMenu();
    }

    private void FixedUpdate()
    {
        if (_currentMap != null)
        {
            _currentMap.Tick();
        }
    }

    private void Update()
    {
        if (_currentMap != null)
        {
            _currentMap.Update();
        }
    }
}
