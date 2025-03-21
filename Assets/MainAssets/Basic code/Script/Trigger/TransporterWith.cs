using UnityEngine;
using DG.Tweening;

public class TransporterWith : MonoBehaviour
{
    public GameObject Transporter;
    public Vector3 offset=new Vector3(0,-1,0);
    public float TurnTimes,WithObjectTimes;
    private MainLine line;
    [HideInInspector]public static bool OK;

    void Start()
    {
        line = GameObject.FindObjectOfType<MainLine>();
    }

    void Update()
    {
        if(OK)
        {
            Transporter.transform.DOMove(line.transform.position+offset,0.01f);
            
            Transporter.transform.DORotate(line.transform.eulerAngles,TurnTimes);
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="line")
        {
            OK=true;
            line.Can_Tail = false;
            Invoke("setok",WithObjectTimes);

        }
    }
    void setok()
    {
        OK=false;
        line.Can_Tail = true;
        line.CreateLineBody();
    }

}
