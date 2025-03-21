using UnityEngine;

public class Ending_Open : MonoBehaviour
{
    public Ending ending;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "line")
        {
            ending.doopen();
        }
    }
}
