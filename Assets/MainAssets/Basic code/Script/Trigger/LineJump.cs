using UnityEngine;

public class LineJump : MonoBehaviour
{
    public float JumpPower;
    [HideInInspector] public GameObject OnlyJumpObject;

    private void Start()
    {
        OnlyJumpObject = GameObject.FindGameObjectWithTag("line");
    }

    void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "line")
        {
            if (Other.gameObject == OnlyJumpObject)
            {
                OnlyJumpObject.GetComponent<Rigidbody>().AddForce(Vector3.up * JumpPower, ForceMode.VelocityChange);
            }
        }
    }
}

