using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : Movement {

    int speed = 200;

	void Start () {
        Init();
        
    }

    public override void Init()
    {
        SetVelocity(this.transform.forward.normalized * speed);
    }

    void SetVelocity(Vector3 value)
    {
        var rigid = GetComponent<Rigidbody>();
        rigid.velocity = value;
    }

    public override void Stop()
    {
        SetVelocity(Vector3.zero);
    }
	
}
