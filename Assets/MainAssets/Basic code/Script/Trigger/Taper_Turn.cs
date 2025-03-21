using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taper_Turn : MonoBehaviour
{
  [HideInInspector]  public MainLine Line;
    private bool ok;
    // Start is called before the first frame update
    void Start()
    {
        Line = GameObject.FindObjectOfType<MainLine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.GetComponent<Taper_Turn>().enabled == false)
        {
        }
        else if (ok == false && other.GetComponent<MainLine>() != null)
        { 
            Line.GetComponent<MainLine>().TurnBlock();
            ok = true;
            Instantiate(this.gameObject.GetComponent<Tapers>().PlayEffect, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
        
    }
}
