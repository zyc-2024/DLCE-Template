using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBody_Trigger : MonoBehaviour
{
    public Rigidbody OBJ;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="line")
        {
            if (OBJ.useGravity == false)
                OBJ.useGravity = true;
            else
                OBJ.useGravity = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
