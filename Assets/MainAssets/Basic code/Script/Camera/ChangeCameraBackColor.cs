using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeCameraBackColor : MonoBehaviour
{

    public Camera Camera;
    public Color BackColor;
    public float ChangeTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("line"))
        {
            DOTween.To(() => Camera.backgroundColor, x => Camera.backgroundColor = x, BackColor, ChangeTime);
        }
    }
}
