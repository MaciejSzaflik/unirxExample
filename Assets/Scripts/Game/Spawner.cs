using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject toSpawn;
    public int spawnLimit;
    public int spawnTime = 1000;
    private IDisposable update;

    private HashSet<GameObject> setOfObjects;
    void Start ()
    {
        setOfObjects = new HashSet<GameObject>();
        update = Observable.Interval(TimeSpan.FromMilliseconds(spawnTime))
            .Where(v => setOfObjects.Count <= spawnLimit)
            .Subscribe(v => Spawn())
            .AddTo(this);

        DeathHandler.Instance.bulletStream
            .Where(hit => setOfObjects.Contains(hit.hit))
            .Subscribe(hit => Die(hit.hit))
            .AddTo(this);   
    }

    void Spawn()
    {
        var spawned = Instantiate<GameObject>(toSpawn);
        spawned.transform.position = this.transform.position;
        setOfObjects.Add(spawned);
    }

    void Die(GameObject obj)
    {
        setOfObjects.Remove(obj);
        Destroy(obj);
    }

    void OnDestroy()
    {
        update.Dispose();
    }
}
