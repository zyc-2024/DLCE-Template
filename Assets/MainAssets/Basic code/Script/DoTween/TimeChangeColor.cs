using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimeChangeColor : MonoBehaviour
{
    private MainLine MainLine;
    public Material Material;
    public Color StartColor, EndColor;
    public float WaitTime, ChangeTime;
    // Start is called before the first frame update
    void Start()
    {
        MainLine = GameObject.FindObjectOfType<MainLine>();
        Material.color = StartColor;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(WaitTime);
        Material.DOColor(EndColor, ChangeTime);
    }
    void Update()
    {
       if(MainLine.start)
        {
            StartCoroutine(wait());
            this.enabled = false;
 
        }
    }
}
