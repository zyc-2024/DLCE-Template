using UnityEngine;
using DG.Tweening;

public class Crown : MonoBehaviour
{

    [HideInInspector] public MainLine MainLine;
	private MainLine MainLineCom;
	private Vector3 Resurrection_position = Vector3.zero;
	private Vector3 Resurrection_FoWard;
	private float Resurrection_Audio_Time = 0;
    private int Resurrection_Percentage = 0;
    public CrownIcon crown_icon;
	public GameObject CrownEffect;
	private bool picked = false;
	private GameObject TempEffect;
	[HideInInspector]public bool used = false;
    [HideInInspector] public bool kill_crown = false;

    void Start ()
    {
        crown_icon.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0);
        MainLine = GameObject.FindObjectOfType<MainLine>();
		MainLineCom = MainLine.GetComponent<MainLine> ();
	}
    void Update()
    {

        this.gameObject.transform.Rotate(0, 1, 0);
    }
         

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line")
        {
            Resurrection_position = MainLine.transform.position;
            Resurrection_Audio_Time = MainLine.GetComponent<AudioSource>().time;
            Resurrection_FoWard = MainLine.GetComponent<MainLine>().NowForward;
            Resurrection_Percentage = MainLine.GetComponent<MainLine>().NowPercentage;
        }

		if (other.GetComponent<MainLine> () != null && picked == false)
        {
            crown_icon.GetComponent<MeshRenderer>().material.DOFade(1, 2);
			if (MainLineCom.Crown1 == null)
            {
				MainLineCom.Crown1 = this.gameObject;
			} else if (MainLineCom.Crown2 == null)
            {
				MainLineCom.Crown2 = this.gameObject;
			} else
            {
				MainLineCom.Crown3 = this.gameObject;
			}
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;
            TempEffect = Instantiate (CrownEffect, this.transform.position, Quaternion.Euler(Vector3.zero));
			TempEffect.transform.DOMove (crown_icon.transform.position, 3f, false);
			Destroy (TempEffect, 3f);
			picked = true;
            MainLine.GetComponent<MainLine>().PickCrown++;
            MainLine.PC++;
		}
    }

	public void Easter ()
    {
        if (kill_crown == false)
        {
            MainLine.GetComponent<MainLine>().PickCrown--;
            MainLine.PC--;
            kill_crown = true;
        }
        MainLine.GetComponent<MainLine>().PickDiamondCount = 0;
        MainLine.PickCrown = MainLine.PC;
        used = true;
        crown_icon.GetComponent<MeshRenderer>().material.DOFade(0, 1);
    	MainLineCom.NowPercentage = Resurrection_Percentage;
		MainLineCom.Way.Enqueue (MainLineCom.LineBody);
		MainLineCom.LineBody = null;
		int TailCound = MainLineCom.Way.Count;
		for (int i = 0; i < TailCound; i++)
        {
			Destroy (MainLineCom.Way.Dequeue ());
		}
		MainLine.GetComponent<Rigidbody> ().isKinematic = false;
		MainLine.transform.position = Resurrection_position;
		MainLineCom.NowForward = Resurrection_FoWard;
		MainLineCom.transform.localEulerAngles = Resurrection_FoWard;
        MainLineCom.Over = false;
		MainLineCom.start = false;
		MainLineCom.Is_Stop = true;
        if (MainLineCom.start_audio)
        {
            MainLineCom.start_audio.GetComponent<AudioSource>().time = Resurrection_Audio_Time;
        }
	}
}
