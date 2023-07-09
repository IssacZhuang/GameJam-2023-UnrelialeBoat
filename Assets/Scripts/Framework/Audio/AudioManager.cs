using System;
using System.Collections.Generic;

using Vocore;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> BGMList = new List<AudioSource>();
    // getter of BGMList
    public List<AudioSource> GetBGMList()
    {
        return BGMList;
    }
    public void ChangeBGM(){
        BGMList[0].Stop();
        BGMList[1].Stop();
    }
    private class AudioInstance
    {
        public float lifeTime;
        public AudioSource source;
    }

    private List<AudioInstance> _audios = new List<AudioInstance>();
    private List<AudioInstance> _pendingRemove = new List<AudioInstance>();

    void Awake()
    {
        Current.AudioManager = this;
    }

    void OnCreate(){
        BGMList[0].Play();
        BGMList[1].Play();
    }
    void Update()
    {
        for (int i = _audios.Count - 1; i >= 0; i--)
        {
            var audio = _audios[i];
            audio.lifeTime -= Time.deltaTime;
            if (audio.lifeTime <= 0)
            {
                _pendingRemove.Add(audio);
            }
        }

        foreach (var audio in _pendingRemove)
        {
            _audios.Remove(audio);
            Destroy(audio.source);
        }
        _pendingRemove.Clear();
    }

    public void PlayAsync(string addressableId, float volume = 1.0f)
    {
        Content.LoadAudioAsync(addressableId, (audioClip) =>
        {
            if (audioClip == null)
            {
                Debug.LogError("找不到音频：" + addressableId);
                return;
            }

            Play(audioClip, volume);
        });
    }

    public void Play(AudioClip audioClip, float volume = 1.0f)
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        var audio = new AudioInstance();
        audio.source = audioSource;
        audio.lifeTime = audioClip.length;
        _audios.Add(audio);
    }
}
