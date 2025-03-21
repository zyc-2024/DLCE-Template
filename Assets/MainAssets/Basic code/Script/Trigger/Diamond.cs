using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{

    [HideInInspector] public MainLine MainLine;
    public GameObject remains;
	public GameObject PickDiamondEffect;

    private void Start()
    {
        MainLine = GameObject.FindObjectOfType<MainLine>();
    }
    void OnTriggerEnter(Collider other)
    {
		if (other.GetComponent<MainLine> () != null)
        {
			MainLine.GetComponent<MainLine>().PickDiamondCount++;
            Destroy(Instantiate(remains, this.transform.position, this.transform.rotation),8);
			Destroy (Instantiate(PickDiamondEffect, this.transform.position, Quaternion.Euler(Vector3.zero)), 8);
			Destroy (this.gameObject);
		}
    }
    void Update()
    {
        this.gameObject.transform.Rotate(0, 1, 0);
    }
}
