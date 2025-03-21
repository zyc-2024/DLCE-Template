using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class dis_scale : MonoBehaviour
{
    public Vector3 original_scale;
    public Vector3 start_scale;
    public float needtime;
    public float distance;
    private MainLine line;
    // Start is called before the first frame update
    void Start()
    {
        line=/*Game */GameObject.
        FindGameObjectWithTag("line").
        GetComponent<MainLine>();
        this.transform.localScale=start_scale;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(line.transform.position,this.transform
        .position)<= distance)
        {
            DOTween.To(()=>this.transform.localScale,x=>this.transform.localScale=x, original_scale,needtime);
        }
    }
}
