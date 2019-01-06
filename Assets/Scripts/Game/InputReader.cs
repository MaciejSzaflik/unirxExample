using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class InputReader : Singleton<InputReader> {

    public IObservable<long> leftClickStream;
    public IObservable<long> rightClickStream;
    public IObservable<long> rightHoldStream;

    public Vector3 inputPosition
    {
        get { return Input.mousePosition; }
    }

    void Awake()
    {
        leftClickStream = Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0));
        rightClickStream = Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(1));
        rightHoldStream = Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButton(1));
    }

    public static void GetWorldPoint(System.Action<Vector3> onHit)
    {
        var worldPoint = CameraCache.main.ScreenToWorldPoint(
               InputReader.Instance.inputPosition);

        var hit = new RaycastHit();
        var ray = new Ray(worldPoint, CameraCache.main.gameObject.transform.forward);
        if (Physics.Raycast(ray, out hit, 10000))
        {
            onHit(hit.point);
        }
    }

}
