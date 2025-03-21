using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeTrigger : MonoBehaviour {
	public CameraShake CameraShake;
	public float seconds  = 0f;//震动持续秒数
	public float quake = 0.2f;//震动系数

	// Use this for initialization
	public void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other) {
		if (other.tag == "line"){
		CameraShake.ShakeFor (seconds, quake);
		}
	}
}