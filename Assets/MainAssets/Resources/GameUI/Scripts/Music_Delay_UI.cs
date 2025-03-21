using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music_Delay_UI : MonoBehaviour
{
    public Text SynText;
    private static int blast=0;
    // Start is called before the first frame update
    void Start()
    {
        Set();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 public   void Set()
    {
        if(blast==0)
        {
            SynText.text="0.00";
        }
        else if(blast>0)
        {
            SynText.text=blast/100+"."+blast/10%10+blast%10;
        }
        else SynText.text = "-" + (-blast / 100) + "." + (- blast / 10 % 10) + -blast % 10;
    }
  public  void ClickRight()
    {
        SynMusicDelay.delay_second+=0.01f;
        blast++;
        Set();
    }
 public   void ClickLeft()
    {
        SynMusicDelay.delay_second-=0.01f;
        blast--;
        Set();
    }
}
