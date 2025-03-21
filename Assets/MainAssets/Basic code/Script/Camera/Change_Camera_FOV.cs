using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Change_Camera_FOV : MonoBehaviour
{
    public Camera Camera;
    public float NeedTime;
    
  [Range(1,179)]  public float FOV = 60;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="line")
        {
            Camera.DOFieldOfView(FOV, NeedTime);
        }
    }
}
