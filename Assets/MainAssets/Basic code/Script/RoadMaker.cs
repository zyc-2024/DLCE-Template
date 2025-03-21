using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMaker : MonoBehaviour {

	public GameObject cube;
	public float roadWidth;
	private MainLine MainLineCom;
	private GameObject road;

	// Use this for initialization
	void Start ()
    {
		MainLineCom = this.GetComponent<MainLine> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)||Input.GetKeyDown(KeyCode.Space) && MainLineCom.keydown == false)
        {
			road = Instantiate (cube, new Vector3(MainLineCom.LineBody.transform.position.x, MainLineCom.LineBody.transform.position.y - 1, MainLineCom.LineBody.transform.position.z), MainLineCom.LineBody.transform.rotation);
			road.transform.localScale = new Vector3 (MainLineCom.LineBody.transform.localScale.x + roadWidth, 1f, MainLineCom.LineBody.transform.localScale.z + roadWidth);
		}
	}
}
