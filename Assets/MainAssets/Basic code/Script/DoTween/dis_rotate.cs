using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class dis_rotate : MonoBehaviour
{
public float distance,needtime;
public Vector3 start_rot,end_rot;
private MainLine Line;
private bool OK=false;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.eulerAngles=start_rot;
        Line=GameObject.FindGameObjectWithTag("line").GetComponent<MainLine>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(Line.transform.position,this.transform.position)<=distance&&!OK)
        {
            OK=true;
            transform.DORotate(end_rot,needtime);
        }
    }
}
