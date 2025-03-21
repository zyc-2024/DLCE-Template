using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public int LoadSceneID;
    public bool Self;

    public void Click ()
    {
        if (Self == true)
        {
            SceneManager.LoadScene(Application.loadedLevel);
        }
        else
        {
            SceneManager.LoadSceneAsync(LoadSceneID);
        }
	}
}
