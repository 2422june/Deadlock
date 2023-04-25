using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static GameManager _game;
    public static SoundManager _sound;
    public static TrainFinder _trainFinder;

    T Init<T>() where T : ManagerBase
    {
        T manager = gameObject.AddComponent<T>();
        manager.Init();

        return manager;
    }

    void Awake()
    {
        //_game = Init<GameManager>();
        //_trainFinder = Init<TrainFinder>();
        //_train = Init<TrainController>();
        //_sound = Init<SoundManager>();
    }
}
