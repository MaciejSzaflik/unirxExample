using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UniRx.Triggers;

public class BulletHit
{ 
    public GameObject hit;
    public Bullet bullet;

    public BulletHit(GameObject hit, Bullet bullet)
    {
        this.hit = hit;
        this.bullet = bullet;
    }
}

public class Bullet : MonoBehaviour {

    public int timeOfLife = 1;
    private Shooter shooter;
    private IDisposable destroy;
    private IDisposable collision;

    public void Init(Shooter shooter)
    {
        this.shooter = shooter;
        this.gameObject.SetActive(true);
        this.GetComponent<Collider>().enabled = true;
        this.GetComponent<Movement>().Init();

        destroy = Observable
            .Timer(TimeSpan.FromSeconds(timeOfLife))
            .Subscribe(v => HandleBullet())
            .AddTo(this);

        var observable = this.gameObject.AddOrGetComponent<ObservableCollisionTrigger>();
        collision = observable.OnCollisionEnterAsObservable()
            .Where(col => col.gameObject.tag.Equals("Enemy"))
            .Subscribe(col =>
            {
                HandleBullet();
                DeathHandler.Instance.OnBulletCollision(new BulletHit(col.gameObject, this));
            });
            
    }

    public void HandleBullet()
    {
        if (destroy != null)
            destroy.Dispose();

        collision.Dispose();

        this.gameObject.SetActive(false);
        this.GetComponent<Collider>().enabled = false;
        this.GetComponent<Movement>().Stop();
        shooter.innactive.Push(this.gameObject);
    }
}
