using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : Movement
{

    public Transform target;
    public float speed = 0.5f;
    public float rotSpeed = 0.05f;
    public float rotSpeedCloser = 0.8f;
    public float distSpeedChange = 5.0f;
    public float distStop = 1.0f;

    public bool jumpBack = true;
    public float jumpBackDist = 1;


	void Update () {
        if (target == null)
            return;

        var diffVec = target.position - this.transform.position;
        var directionVector = diffVec.normalized;
        var dist = diffVec.magnitude;

        this.transform.forward = Vector3.Slerp(
            this.transform.forward,
            directionVector,
            dist > distSpeedChange ? rotSpeed : rotSpeedCloser
            );

        if (dist > distStop)
        {
            this.transform.position += this.transform.forward * speed;
        }
        else if(jumpBack)
        {
            JumpBack();
        }
    }

    public void JumpBack()
    {
        this.transform.position += -this.transform.forward * jumpBackDist;
    }
}
