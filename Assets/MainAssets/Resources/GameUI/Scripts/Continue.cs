using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{

	public GameObject ContinueUI;
    [HideInInspector] public MainLine MainLine;
    private void Start()
    {
        MainLine = GameObject.FindObjectOfType<MainLine>();
    }
    public void Click ()
    {
		MainLine.GetComponent<MainLine>().GoToEaster ();
		ContinueUI.SetActive (false);
    }
}
