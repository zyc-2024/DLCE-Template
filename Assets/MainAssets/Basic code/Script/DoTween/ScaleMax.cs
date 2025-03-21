using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ScaleMax : MonoBehaviour
{
    public GameObject 动画物体;//动画物体
    [Serializable]
    public class Max
    {
        public Vector3 Scale;//大小
        public float PosTime;//移动时间
        public float WaitTime;//移动空隙延时间
    }
    public Max[] Sca;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line")
        {
            Sequence sequence = DOTween.Sequence();
            for (int i = 0; i < Sca.Length; i++)
            {
                sequence.Append(动画物体.transform.DOScale(Sca[i].Scale, Sca[i].PosTime));
                sequence.AppendInterval(Sca[i].WaitTime);
            }
        }
    }
}
