using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBase : MonoBehaviour
{
    protected AudioSource _bgmSource;
    protected List<AudioSource> _seSources = new List<AudioSource>();

    protected int index;

    public void PlayBGM(AudioClip clip)
    {
        _bgmSource.clip = clip;
        _bgmSource.Play();
    }

    public void PlaySE(AudioClip clip)
    {
        SetSEIndex();
        _seSources[index].clip = clip;
        _seSources[index].Play();
    }

    protected void SetSEIndex()
    {
        int i;

        for (i = 0; i < _seSources.Count; i++)
        {
            if (!_seSources[index].isPlaying)
            {
                index = i;
                break;
            }
            index = (index + 1) % _seSources.Count;
        }

        if (i == _seSources.Count)
        {
            AddAudioSource();
        }
    }

    protected void AddAudioSource()
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();

        newSource.playOnAwake = false;
        newSource.loop = false;

        newSource.minDistance = 3; // 가장 큰소리가 나는 구간
        newSource.maxDistance = 10; // 이후로는 소리가 안 나는 구간

        _seSources.Add(newSource);
    }
}
