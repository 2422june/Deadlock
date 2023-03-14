using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    private AudioSource _bgmSource;
    private List<AudioSource> _seSources = new List<AudioSource>();

    private int _index;

    public void PlayBGM(AudioClip clip)
    {
        _bgmSource.clip = clip;
        _bgmSource.Play();
    }

    public void PlaySE(AudioClip clip)
    {
        SetSEIndex();
        _seSources[_index].clip = clip;
        _seSources[_index].Play();
    }

    void SetSEIndex()
    {
        int i;

        for (i = 0; i < _seSources.Count; i++)
        {
            if (!_seSources[_index].isPlaying)
            {
                _index = i;
                break;
            }
            _index = (_index + 1) % _seSources.Count;
        }

        if (i == _seSources.Count)
        {
            AddAudioSource();
        }
    }

    void AddAudioSource()
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();

        newSource.playOnAwake = false;
        newSource.loop = false;

        newSource.minDistance = 3; // 가장 큰소리가 나는 구간
        newSource.maxDistance = 10; // 이후로는 소리가 안 나는 구간

        _seSources.Add(newSource);
    }
}
