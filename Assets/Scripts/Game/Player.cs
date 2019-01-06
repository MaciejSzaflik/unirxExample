using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Player : MonoBehaviour {

    public SimpleMovement movement;
    private Transform target;

	void Start () {
        InputReader.Instance.leftClickStream.Subscribe(
            OnClick);

        target = new GameObject("PlayerTaget").transform;
        target.position = this.transform.position;
        movement.target = target;
	}

    void OnClick(long time)
    {
        InputReader.GetWorldPoint((hit) =>
        {
            target.position = new Vector3(
               hit.x,
               transform.position.y,
               hit.z
           );
        });
    }

}
