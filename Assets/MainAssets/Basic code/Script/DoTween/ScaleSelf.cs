using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleSelf : MonoBehaviour
{
   [HideInInspector] public Vector3 scale = new Vector3(0,0,0);
  [HideInInspector]  public float waittime = 5,scaletime = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(waittime);
        this.transform.DOScale(scale, scaletime);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
