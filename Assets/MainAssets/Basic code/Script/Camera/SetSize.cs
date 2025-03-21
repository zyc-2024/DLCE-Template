
using UnityEngine;
using DG.Tweening;
public class SetSize : MonoBehaviour
{
    public Camera Camera;
    public float newsize;
    public float needtime;
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
        if (other.tag == "line")
        {
            DOTween.To(() => Camera.orthographicSize, x => Camera.orthographicSize = x, newsize, needtime);
        }
    }
}
