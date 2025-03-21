using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Line_Remains_And_Tail : MonoBehaviour
{
    public MainLine line;
    public GameObject new_remains, new_tails;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "line")
        {
            line.die_effect = new_remains;
            line.tail = new_tails;
        }
    }
}
