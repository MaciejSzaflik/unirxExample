using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MainScreen : Singleton<MainScreen> {

    public Text playerDamage;
    public Text enemiesKilled;
    public Text log;

    public void Start()
    {
        ScoreTracker.Instance.enemiesKilled
            .SubscribeToText(enemiesKilled, (value) => string.Format("Killed: {0}", value));

        ScoreTracker.Instance.damageTaken
            .SubscribeToText(playerDamage, (value) => string.Format("Lost: {0}", value));

        ScoreTracker.Instance.enemiesKilled
            .Sample(System.TimeSpan.FromMilliseconds(1000))
            .Subscribe(i =>
            {
                log.gameObject.SetActive(true);
                Observable.Timer(System.TimeSpan.FromMilliseconds(300))
                    .Subscribe(l => log.gameObject.SetActive(false));
            });
    }

}
