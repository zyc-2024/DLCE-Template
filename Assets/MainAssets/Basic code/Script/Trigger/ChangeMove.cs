using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMove : MonoBehaviour
{

    public MainLine MainLine;
    public Vector3 New_Block_Foward1;
    public Vector3 New_Block_Foward2;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line")
        {
            MainLine.Block_Foward1 = New_Block_Foward1;
            MainLine.Block_Foward2 = New_Block_Foward2;
        }
    }
}
