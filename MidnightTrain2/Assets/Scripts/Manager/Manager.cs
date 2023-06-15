using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static GameManager _game;
    public static SoundManager _sound;
    public static FindManager _find;
    public static PathManager _path;
    public static Transform _player;

    T Init<T>() where T : ManagerBase
    {
        T manager = gameObject.AddComponent<T>();
        manager.Init();

        return manager;
    }

    void Awake()
    {
        _find = Init<FindManager>();
        _player = _find.FindObject("Player");

        _game = Init<GameManager>();
        _path = Init<PathManager>();
        //_trainFinder = Init<TrainFinder>();
        //_train = Init<TrainController>();
        //_sound = Init<SoundManager>();
    }
}
