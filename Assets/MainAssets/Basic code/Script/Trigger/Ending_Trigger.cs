using UnityEngine;

public class Ending_Trigger : MonoBehaviour
{
    public Ending Ending;
    private MainLine Line;
    public GameObject crowns1, crowns2, crowns3;

    void Start()
    {
        Line = GameObject.FindObjectOfType<MainLine>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line")
        {
            Ending.open();
            Line.GetComponent<MainLine>().NowPercentage = 100;
        }
    }
    public void playsound()
    {
        if (Line.GetComponent<MainLine>().PickCrown == 1)
        {
            Instantiate(crowns1, this.transform.position, this.transform.rotation);
        }
        if (Line.GetComponent<MainLine>().PickCrown == 2)
        {
            Instantiate(crowns2, this.transform.position, this.transform.rotation);
        }
        if (Line.GetComponent<MainLine>().PickCrown >= 3)
        {
            Instantiate(crowns3, this.transform.position, this.transform.rotation);
        }
    }
}
