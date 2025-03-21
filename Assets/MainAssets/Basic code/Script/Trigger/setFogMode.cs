using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setFogMode : MonoBehaviour
{
    public enum mod { Linear,Exponential, ExponentialSquared };
    public mod NewMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "line")
        {
            if(NewMode == mod.Linear)
            {
                RenderSettings.fogMode = FogMode.Linear;
            }
            if (NewMode == mod.Exponential)
            {
                RenderSettings.fogMode = FogMode.Exponential;
            }
            if (NewMode == mod.ExponentialSquared)
            {
                RenderSettings.fogMode = FogMode.ExponentialSquared;
            }

        }
    }
}
