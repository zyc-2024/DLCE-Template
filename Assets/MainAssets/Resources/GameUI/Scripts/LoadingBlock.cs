using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LoadingBlock : MonoBehaviour
{
    public Color Color1;
    public Color Color2;
    public float Times;
    // Start is called before the first frame update
    void Start()
    {
        frist();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Image>().color == Color1)
        {
            second();
        }
        if (this.GetComponent<Image>().color == Color2)
        {
            frist();
        }
    }
    public void frist()
    {
        this.GetComponent<Image>().DOColor(Color1, Times);
    }
    public void second()
    {
        this.GetComponent<Image>().DOColor(Color2, Times);
    }

}
