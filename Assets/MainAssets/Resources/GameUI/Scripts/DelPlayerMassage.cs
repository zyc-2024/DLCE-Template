using UnityEngine;

public class DelPlayerMassage : MonoBehaviour
{
    [HideInInspector] public MenuSettings core;

    private void Start()
    {
        core = GameObject.FindObjectOfType<MenuSettings>();
    }

    public void click()
    {
        PlayerPrefs.DeleteAll();
        core.NowPer = 0;
        core.NowDia = 0;
        core.NowCro = 0;
        Debug.Log("OK");
    }
}
