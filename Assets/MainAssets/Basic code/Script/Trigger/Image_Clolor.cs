using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image_Clolor : MonoBehaviour
{
    public UnityEngine.UI.Image image;
    public Color newcolor;
    public float time;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "line")
        {
            image.DOColor(newcolor, time);
        }
    }
}
