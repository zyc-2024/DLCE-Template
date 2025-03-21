using UnityEngine;

public class LevelMassage : MonoBehaviour
{
    [HideInInspector] public MenuSettings Menu;
    public GameObject ThisLevelCrownModel1, ThisLevelCrownModel2, ThisLevelCrownModel3;
    public GameObject ThisLevelPerfactCrownHolder;
    public AudioClip ThisLevelSound;
    public Color ThisLevelCameraBackColor = new Color(0, 0, 0, 1);
    public string LevelNames;
    public int SceneId;
    public string ThisLevelPerGeter, ThisLevelDiamondGeter, ThisLevelCrownGeter;

    void Start()
    {
        Menu = GameObject.FindObjectOfType<MenuSettings>();
    }
}
