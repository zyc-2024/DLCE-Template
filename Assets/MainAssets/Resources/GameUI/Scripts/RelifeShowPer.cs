using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelifeShowPer : MonoBehaviour
{
    [HideInInspector] public MainLine line;
    public Text shower;
    public Image bar;
    private string a;
    private float b;
    // Start is called before the first frame update
    void Start()
    {
        line = GameObject.FindObjectOfType<MainLine>();
    }

    // Update is called once per frame
    void Update()
    {
        b = float.Parse(line.GetComponent<MainLine>().NowPercentage.ToString());
        shower.text = line.GetComponent<MainLine>().NowPercentage.ToString() + "%";
        bar.fillAmount = b/100;
    }
}
