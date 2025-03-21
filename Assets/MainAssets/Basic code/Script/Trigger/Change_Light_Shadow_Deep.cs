using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Change_Light_Shadow_Deep : MonoBehaviour
{
    public Light set;
    [Range(0, 1)] public float new_shadow_deep;
    public float times;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "line")
        {
            DOTween.To(() => set.shadowStrength, y => set.shadowStrength = y, new_shadow_deep, times);
        }
    }
}
