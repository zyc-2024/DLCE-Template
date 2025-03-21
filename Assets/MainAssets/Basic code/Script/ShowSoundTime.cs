using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSoundTime : MonoBehaviour
{
    public MainLine line;
    public UnityEngine.UI.Text ShowText;

    void Update()
    {
        if (line.start == true)
        {
            ShowText.text = "NowTime:" + line.GetComponent<AudioSource>().time.ToString() + "\n" + "AudioLength:" + line.GetComponent<AudioSource>().clip.length.ToString();
        }
    }
}
