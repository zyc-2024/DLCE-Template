using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayLevel : MonoBehaviour
{
    public MenuSettings MenuCore;

    public void click()
    {
        SceneManager.LoadSceneAsync(MenuCore.SceneId);
    }
}
