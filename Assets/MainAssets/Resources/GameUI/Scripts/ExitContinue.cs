using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitContinue : MonoBehaviour
{

[HideInInspector]	public MainLine MainLine;
	public GameObject ContinueUI;

    private void Start()
    {
        MainLine = GameObject.FindObjectOfType<MainLine>();
    }
    public void click ()
    {
		MainLine.GetComponent<MainLine>().OverTab.SetActive (true);
		MainLine.GetComponent<MainLine>().OverTab.GetComponent<OverMenu> ().GameOver (false, MainLine.GetComponent<MainLine>().NowPercentage, MainLine.GetComponent<MainLine>().PickDiamondCount, MainLine.GetComponent<MainLine>().GetPickCrownCount());
		ContinueUI.SetActive (false);
	}
}
