using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObjMat : MonoBehaviour{
    public Material Mat;
    public GameObject[] Gameobjects;
    public MainLine line;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag==line .tag )
        {
            for (int i = 0; i < Gameobjects.Length; i++) 
            {
                Gameobjects[i].GetComponent<MeshRenderer>().material = Mat;
            }
        }

        
    }
}
