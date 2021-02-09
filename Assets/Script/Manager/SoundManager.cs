using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }

    public void Play(string path, float delayTime = 0.0f, Sound type = Sound.Effect, float pitch = 1.0f, float volume = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, delayTime, type, pitch);
    }

    public void Play(AudioClip audioClip, float delayTime = 0.0f, Sound type = Sound.Effect, float pitch = 1.0f, float volume = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Sound.Bgm];
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            //Debug.Log(audioSource);
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Sound.Effect];
            audioSource.pitch = pitch;
        }
    }
    AudioClip GetOrAddAudioClip(string Name, Sound type = Sound.Effect)
    {
        //if (type == Sound.Bgm) folderName = "Bgm";
        //else if (type == Sound.Effect) folderName = "Effect";
        //path = $"Data/Sound/{folderName}/{path}";

        AudioClip audioClip = null;

        if (type == Sound.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>($"Data/Sound/Bgm/{Name}");
        }
        else
        {
            if (_audioClips.TryGetValue(Name, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>($"Data/Sound/Effect/{Name}");

                _audioClips.Add(Name, audioClip);
            }
        }

        if (audioClip == null)
        {
            //Debug.Log($"AudioClip Missing ! {Name}");
        }

        return audioClip;
    }
}
