using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using UnityEngine;

public class SoundManager : ManagerBase
{
    private List<AudioSource> _seSources = new List<AudioSource>();

    private int _index;
    private List<AudioClip> _se = new List<AudioClip>();


    public override void Init()
    {
        _index = 0;
        LoadAudios();
    }

    void LoadAudios()
    {
        int size = System.Enum.GetValues(typeof(Define.SE)).Length;
        for (; size > 0; size--)
        {
            _se.Add(Resources.Load<AudioClip>("Audios/"+((Define.SE)size).ToString()));
        }
    }

    public void PlaySE(Define.SE type)
    {
        PlaySE(_se[(int)type]);
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
            _index = _seSources.Count;
            AddAudioSource();
        }
    }

    void AddAudioSource()
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();

        newSource.playOnAwake = false;
        newSource.loop = false;
        newSource.volume = 1.0f;

        //newSource.minDistance = 3; // 가장 큰소리가 나는 구간
        //newSource.maxDistance = 10; // 이후로는 소리가 안 나는 구간

        _seSources.Add(newSource);
    }
}
