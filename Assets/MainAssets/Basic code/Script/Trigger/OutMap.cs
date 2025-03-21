using UnityEngine;

public class OutMap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<MainLine>() && other.tag == "line")
        {
            other.GetComponent<MainLine>().Over = true;
            other.GetComponent<MainLine>().Is_Stop = true;
            other.GetComponent<MainLine>().GameOver(false, true);
        }
    }
}
