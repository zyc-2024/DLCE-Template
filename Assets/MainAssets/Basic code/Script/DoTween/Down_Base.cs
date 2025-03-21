using UnityEngine;
using DG.Tweening;

public class Down_Base : MonoBehaviour
{
    private Vector3 Downer;
    private MainLine line;
    [HideInInspector] public Vector3 DownRot = new Vector3(0, -90, 0);
    public float DownY, DownTimes;
    private bool caner = true;

    void Start()
    {
        Downer = new Vector3(this.transform.position.x, this.transform.position.y - DownY, this.transform.position.z);
        line = GameObject.FindObjectOfType<MainLine>();
        line.GetComponent<MainLine>().enabled = false;
        line.transform.parent = this.transform;
    }

    void Update()
    {
        if (caner)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                caner = false;
                this.gameObject.transform.DOMove(Downer, DownTimes);
                line.gameObject.transform.DORotate(DownRot, DownTimes);
                Destroy(Instantiate(line.GetComponent<MainLine>().jump_effect, new Vector3(line.transform.position.x, line.transform.position.y + line.GetComponent<MainLine>().Jump_Effect_Deviation, line.transform.position.z), line.transform.rotation), 1f);
                Invoke("ending", DownTimes);
            }
        }

    }
    public void ending()
    {
        line.transform.eulerAngles = line.GetComponent<MainLine>().NowForward;
        line.GetComponent<MainLine>().enabled = true;
        this.enabled = false;
        this.transform.DetachChildren();
    }

}
