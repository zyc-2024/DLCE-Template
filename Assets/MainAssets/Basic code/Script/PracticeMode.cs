using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeMode : MonoBehaviour
{
    public GameObject testcube;
    public KeyCode makekey=KeyCode.S;
    public KeyCode tpkey=KeyCode.W;
    public KeyCode Destorykey=KeyCode.D;
    private MainLine line;
    private Vector3 position;
    private Vector3 rotation;
    private Stack<GameObject> CP=new Stack<GameObject>(0);
    private float music;
    private Vector3 nowlinedir;
    // Start is called before the first frame update
    void Start()
    {
        testcube.tag="line";
        line=this.GetComponent<MainLine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (line.start)
        {
            if (Input.GetKeyDown(makekey))
            {
                CP.Push(Instantiate(testcube, line.transform.position, line.transform.rotation));
                CP.Peek().transform.localScale = new Vector3(1, 1, 1);
                music = line.start_audio.time;
            }
            if (Input.GetKeyDown(tpkey) && CP.Count != 0)
            {
                deltailing();
                line.transform.position = CP.Peek().transform.position;
                line.transform.eulerAngles = CP.Peek().transform.eulerAngles;
                line.TurnBlock();
                line.start_audio.time = music;
            }
            if (Input.GetKeyDown(Destorykey) && CP.Count != 0)
            {
                Destroy(CP.Peek());
                CP.Pop();
            }
        }
    }
    public void deltailing()
    {
        int count = line.Way.Count;
        for (int i = 0; i < count; i++)
        {
            Destroy(line.Way.Dequeue());
        }
    }
}
