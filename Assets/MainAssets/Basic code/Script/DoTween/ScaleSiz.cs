using UnityEngine;
using DG.Tweening;

public class ScaleSiz : MonoBehaviour
{

    [HideInInspector] public MainLine MainLine;
    public Vector3 NewSca = Vector3.zero;
    public float waittime = 0;
    public float needtime = 0;
    private bool Done = false;
    private Vector3 StartSca;
    private bool CheckedRot;

    void Start()
    {
        MainLine = GameObject.FindObjectOfType<MainLine>();
        StartSca = this.transform.localScale;
    }

    void Update()
    {
        if (MainLine.GetComponent<MainLine>().start == true)
        {
            if (MainLine.GetComponent<MainLine>().start_audio.time >= waittime)
            {

                if (MainLine.GetComponent<MainLine>().start_audio.time <= (waittime + needtime))
                {
                    if (Done == false)
                    {
                        this.transform.DOScale(NewSca, needtime);
                        Done = true;
                    }
                }
                else
                {
                    Done = false;
                    this.transform.localScale = NewSca;
                }
            }
            else
            {
                Done = false;
                if (CheckedRot == false)
                {
                    CheckedRot = true;
                    this.transform.localScale = StartSca;
                }
            }
        }
        else
        {
            CheckedRot = false;
        }
        if (Done == true)
        {
            this.enabled = false;
        }
    }
}

