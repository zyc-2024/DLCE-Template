using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MovingPosMax : MonoBehaviour
{ 
    public GameObject 动画物体;//动画物体
    [Serializable]
    public class Max{
    public Vector3 Pos;//位置
    public float PosTime;//移动时间
    public float WaitTime;//移动空隙延时间
    }
    public Max[] Position;
    private void OnTriggerEnter(Collider other)
    {
    if (other.tag == "line")
    {
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < Position.Length; i++)
        {
            sequence.Append(动画物体.transform.DOMove(Position[i].Pos, Position[i].PosTime));
            sequence.AppendInterval(Position[i].WaitTime);
        }
    }
    }
}

