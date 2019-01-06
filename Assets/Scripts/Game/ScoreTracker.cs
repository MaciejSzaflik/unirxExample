using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ScoreTracker : Singleton<ScoreTracker> {

    public ReactiveProperty<int> enemiesKilled = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> damageTaken = new ReactiveProperty<int>(0);

    void Start()
    {
        DeathHandler.Instance.bulletStream
            .Subscribe(hit => enemiesKilled.Value++);
    }
	
}
