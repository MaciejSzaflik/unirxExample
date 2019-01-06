using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Enemy : MonoBehaviour {

    public GameObject deathParticle;

	void Start()
    {
        var movement = GetComponent<SimpleMovement>();
        movement.target = PlayerSupervisor.Instance.GetFirstPlayer().transform;

        this.gameObject.AddOrGetComponent<ObservableDestroyTrigger>()
            .OnDestroyAsObservable()
            .Subscribe(v => DeathEffects());

        this.gameObject.AddOrGetComponent<ObservableCollisionTrigger>()
            .OnTriggerEnterAsObservable()
            .Where(col => col.gameObject.tag.Equals("User"))
            .Subscribe(col => OnPlayerHit());
    }

    void OnPlayerHit()
    {
        var movement = GetComponent<SimpleMovement>();
        if (movement != null)
            movement.JumpBack();

        ScoreTracker.Instance.damageTaken.Value++;
        CameraShake.Instance.ShakeCamera(0.4f, 0.1f);
    }

    void DeathEffects()
    {
        CameraShake.Instance.ShakeCamera(0.1f, 0.07f);
        SpawnDeathParticle();
    }

    void SpawnDeathParticle()
    {
        if (deathParticle == null)
            return;
            
        var particle = Instantiate<GameObject>(deathParticle);
        particle.transform.position = this.transform.position;
        Observable.Timer(System.TimeSpan.FromMilliseconds(5000))
            .Subscribe(l => GameObject.Destroy(particle))
            .AddTo(particle);
    }
}
