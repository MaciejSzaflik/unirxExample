using System.Collections;
using System.Collections.Generic;
using UniRx;
using System;
using UnityEngine;

public class Shooter : MonoBehaviour {

    public GameObject bullet;
    public int milliseconsds = 100;

    public Stack<GameObject> innactive;

	void Start () {
        innactive = new Stack<GameObject>();

        InputReader.Instance.rightHoldStream
            .Sample(TimeSpan.FromMilliseconds(milliseconsds))
            .Subscribe(v => {
                Spawn();
                })
            .AddTo(this);
	}

    void Spawn()
    {
        InputReader.GetWorldPoint(pos => {
            var spawned = GetNext();

            pos.y = this.transform.position.y;

            var dir = (pos - this.transform.position).normalized;

            spawned.transform.position = this.transform.position + dir * 2;
            spawned.transform.forward = dir;

            var bulletNew = spawned.AddOrGetComponent<Bullet>();
            bulletNew.Init(this);
        });
    }

    GameObject GetNext()
    {
        if (innactive.Count > 0)
            return innactive.Pop();
        else
            return Instantiate<GameObject>(bullet);
    }
}