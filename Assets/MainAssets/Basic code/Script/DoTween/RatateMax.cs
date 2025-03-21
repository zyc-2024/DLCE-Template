using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class RatateMax : MonoBehaviour
{
    public GameObject 动画物体;//动画物体
    [Serializable]
    public class Max
    {
        public Vector3 Rot;//角度
        public float PosTime;//移动时间
        public float WaitTime;//移动空隙延时间
    }
    public Max[] Rotate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line")
        {
            Sequence sequence = DOTween.Sequence();
            for (int i = 0; i < Rotate.Length; i++)
            {
                sequence.Append(动画物体.transform.DORotate(Rotate[i].Rot, Rotate[i].PosTime));
                sequence.AppendInterval(Rotate[i].WaitTime);
            }
        }
    }
}
