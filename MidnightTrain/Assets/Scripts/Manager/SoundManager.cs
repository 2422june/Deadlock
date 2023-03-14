using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SoundManager : ManagerBase
{
    private Speaker[] _speakers = new Speaker[2];

    private List<AudioClip> _bgm = new List<AudioClip>();
    private List<AudioClip> _se = new List<AudioClip>();


    public override void Init()
    {
        LoadAudios();
        FindSpeakers();
    }

    void LoadAudios()
    {
        int size = System.Enum.GetValues(typeof(Define.BGM)).Length;

        for (; size > 0; size--)
        {
            _bgm.Add(Resources.Load<AudioClip>(((Define.BGM)size).ToString()));
        }

        size = System.Enum.GetValues(typeof(Define.SE)).Length;
        for (; size > 0; size--)
        {
            _se.Add(Resources.Load<AudioClip>(((Define.SE)size).ToString()));
        }
    }

    void FindSpeakers()
    {
        for (int i = 1; i <= _speakers.Length; i++)
        {
            _speakers[i - 1] = Manager._train._speakers.Find($"speaker{i}").GetComponent<Speaker>();
        }
    }

    public void PlayBGM(Define.BGM type, int room = 0)
    {
        _speakers[room].PlayBGM(_bgm[(int)type - 1]);
    }

    public void PlaySE(Define.SE type, int room = 0)
    {
        _speakers[room].PlaySE(_se[(int)type - 1]);
    }
}
