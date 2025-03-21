using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushrooms : MonoBehaviour {
    public GameObject MainObject;
    public float DownSOM =3f;
    public float distance = 10f;
    public float upSpeed = 0.03f;
    float  yy;
	// Use this for initialization
	void Start () {
        yy= this.transform.position.y;
        this.transform.position-= new Vector3(0,DownSOM,0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(MainObject.transform.position.x-this.transform.position.x)<distance&&Mathf.Abs(MainObject.transform.position.z-this.transform.position.z)<distance) {
            if (this.transform.position.y<=yy-upSpeed) {
                this.transform.position = this.transform.position+new Vector3(0,upSpeed,0);
            }
        }
	}
}
