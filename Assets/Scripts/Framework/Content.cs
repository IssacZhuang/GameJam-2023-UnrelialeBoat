using System;

using UnityEngine;
using UnityEngine.AddressableAssets;

public static class Content
{
    public static T GetConfig<T>(string name) where T : BaseConfig
    {
        return Database<T>.Get(name);
    }

    public static GameObject GetPrefab(string name)
    {
        return GetResrouce<GameObject>(name);
    }

    public static Texture2D GetTexture(string name)
    {
        return GetResrouce<Texture2D>(name);
    }

    public static Shader GetShader(string name)
    {
        return GetResrouce<Shader>(name);
    }

    public static Material GetMaterial(string name)
    {
        return GetResrouce<Material>(name);
    }

    public static AudioClip GetAudio(string name)
    {
        return GetResrouce<AudioClip>(name);
    }
    
    public static Sprite GetSprite(string name)
    {
        return GetResrouce<Sprite>(name);
    }

    public static void LoadPrefabAsync(string name, Action<GameObject> callback)
    {
        Addressables.LoadAssetAsync<GameObject>(name).Completed += (obj) =>
        {
            callback(obj.Result);
        };
    }

    public static void LoadTextureAsync(string name, Action<Texture2D> callback)
    {
        Addressables.LoadAssetAsync<Texture2D>(name).Completed += (obj) =>
        {
            callback(obj.Result);
        };
    }

    public static void LoadAudioAsync(string name, Action<AudioClip> callback)
    {
        Addressables.LoadAssetAsync<AudioClip>(name).Completed += (obj) =>
        {
            callback(obj.Result);
        };
    }

    public static T GetResrouce<T>(string name) where T : UnityEngine.Object
    {
        return Addressables.LoadAssetAsync<T>(name).WaitForCompletion();
    }
}
