using UnityEngine;

public class Quit : MonoBehaviour
{
public void click()
    {
        PlayerPrefs.Save();
        Application.Quit();
        Debug.Log("Quit The Game!");
    }
}
