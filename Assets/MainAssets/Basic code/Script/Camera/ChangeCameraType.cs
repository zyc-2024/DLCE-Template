using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum QWQ { Perspective, Orthographic };
public class ChangeCameraType : MonoBehaviour {

    public Camera Camera;
    
    public QWQ SelectType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line")
        {
            if (SelectType == QWQ.Perspective)
            {
                Camera.orthographic = false;
            }
            else
            {
                Camera.orthographic = true;
            }
        }
    }
}
