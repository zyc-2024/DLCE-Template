using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynMusicDelay : MonoBehaviour
{
    [HideInInspector]public MainLine line;
    public static float delay_second;
    private bool OK;
    // Start is called before the first frame update
    void Start()
    {
        line=GameObject.FindObjectOfType<MainLine>();
    }

    // Update is called once per frame
    void Update()
    {
        if(line.start && !OK)
        {
            if(delay_second<=0)
            {
                line.enabled=false;
                Invoke("SetTime",Mathf.Abs(delay_second));
            }
            if(delay_second>0)
            {
                line.start_audio.Pause();
                Invoke("SetAudio",delay_second);
            }
            OK=true;
        }
    }
    void SetTime()
    {
        line.enabled=true;
    }
    void SetAudio()
    {
        line.start_audio.Play();
    }
}
