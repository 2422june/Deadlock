using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static GameManager _game;
    public static SoundManager _sound;
    public static TrainFinder _train;

    T Init<T>() where T : ManagerBase
    {
        T manager = gameObject.AddComponent<T>();
        manager.Init();

        return manager;
    }

    void Awake()
    {
        _game = Init<GameManager>();
        _train = Init<TrainFinder>();
        _sound = Init<SoundManager>();
    }
}
