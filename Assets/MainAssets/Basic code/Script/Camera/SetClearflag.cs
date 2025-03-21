using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetClearflag : MonoBehaviour
{
    public Camera cam;
    public enum flags { Skybox,SoidColor};
    public flags NewFlag;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "line")
        {
            if(NewFlag == flags.Skybox)
            {
                cam.clearFlags = CameraClearFlags.Skybox;
            }
            if (NewFlag == flags.SoidColor)
            {
                cam.clearFlags = CameraClearFlags.SolidColor;
            }

            
        }
    }
}
