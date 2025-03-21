using UnityEngine;
using DG.Tweening;

public class Ending : MonoBehaviour
{

	private MainLine MainLine;
    private Ending_Trigger EndTrigger;
    public float Rate = 1;
	public float OpenNeedTime = 1;
	public float WinWaitTime = 1; 
	private Transform Ending_Left;
	private Transform Ending_Right;

	// Use this for initialization
	public void Start ()
    {
		Ending_Left = this.transform.Find ("Ending_Left").GetComponent<Transform> ();
		Ending_Right = this.transform.Find("Ending_Right").GetComponent<Transform>();
        EndTrigger = GameObject.FindObjectOfType<Ending_Trigger>();
        MainLine = GameObject.FindObjectOfType<MainLine>();
    }

	public void open()
    {
		MainLine.GetComponent<MainLine>().canuse = false;		
		if (MainLine.GetComponent<MainLine>().Camera.GetComponent<FollowCamera>())
        {
			MainLine.GetComponent<MainLine>().Camera.GetComponent<FollowCamera> ().enabled = false;
		}
        Invoke ("win", WinWaitTime);
    }
    public void doopen()
    {
        Ending_Left.DOLocalMoveZ(-0.1f * Rate, OpenNeedTime, false);
        Ending_Right.DOLocalMoveZ(0.1f * Rate, OpenNeedTime, false);
    }
	public void win()
    {
		MainLine.GetComponent<MainLine>().GameOver (true, true);
        EndTrigger.GetComponent<Ending_Trigger>().playsound();
	}
    
}
