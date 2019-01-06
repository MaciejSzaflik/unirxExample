using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class DeathHandler : Singleton<DeathHandler>
{
    public Subject<BulletHit> bulletCollision;

    public IObservable<BulletHit> bulletStream
    {
        get
        {
            return bulletCollision.AsObservable();
        }
    }

    public void OnBulletCollision(BulletHit bulletHit)
    {
        bulletCollision.OnNext(bulletHit);
    }

    void Awake()
    {
        bulletCollision = new Subject<BulletHit>();
    }
}
