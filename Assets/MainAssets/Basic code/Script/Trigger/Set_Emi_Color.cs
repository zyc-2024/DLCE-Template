using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Emi_Color : MonoBehaviour
{
    public Material need_set_mat;
    public Color start,newer;
    //alled before the first frame update
    void Start()
    {
        need_set_mat.EnableKeyword("_EMISSION");
        need_set_mat.SetColor("_EmissionColor", start);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="line")
        {
            
                need_set_mat.EnableKeyword("_EMISSION");
                need_set_mat.SetColor("_EmissionColor",newer);
  
        }
    }
}
