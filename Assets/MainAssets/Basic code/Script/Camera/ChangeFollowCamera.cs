using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFollowCamera : MonoBehaviour
{
    public FollowCamera FollowCamera;
	public Vector3 pivotOffset = Vector3.zero;
	public bool ActiveOffset=true;
    public float targetX = 45f;
	public float targetY = 45f;
	public float targetZ = 0f;
	public float TargetDistance = 20f;
    public bool ActiveRot=true;
	public float SmoothTime = 1f;
    public float needtime = 1f;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line")
        {
			if (ActiveOffset)
				FollowCamera.pivotOffset = pivotOffset;
            if (ActiveRot)
                FollowCamera.targetX = targetX;
			    FollowCamera.targetY = targetY;
				FollowCamera.targetZ = targetZ;
				FollowCamera.TargetDistance = TargetDistance;
				FollowCamera.SmoothTime = SmoothTime;
				FollowCamera.needtime = needtime;
        }
        else
        {
			if (ActiveOffset)
				FollowCamera.pivotOffset = pivotOffset;
            if (ActiveRot)
				FollowCamera.targetX = targetX;
                FollowCamera.targetY = targetY;
				FollowCamera.targetZ = targetZ;
				FollowCamera.TargetDistance = TargetDistance;
				FollowCamera.SmoothTime = SmoothTime;
				FollowCamera.needtime = needtime;
        }
    }
}
