using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LRButton : MonoBehaviour
{
    public MenuSettings MenuSettings;
    [HideInInspector] public Trip Trip;
    [HideInInspector] public Setting Setting;
    public enum lr { Left,Right};
    public lr LeftOrRight;
    [HideInInspector]public bool canclick = true;

    private void Start()
    {
        Trip = GameObject.FindObjectOfType<Trip>();
        Setting = GameObject.FindObjectOfType<Setting>();
    }
    public void click()
    {
        //DOTween.KillAll();
        if (canclick)
        {
            if (LeftOrRight == lr.Left)
            {
                if (MenuSettings.NowLevelId > 0)
                {
                    MenuSettings.NowLevelId -= 1;
                    MenuSettings.LevelModelHolder.transform.DOMove(MenuSettings.LV3Next, 0.5f);
                }
            }
            else
            {
                if (MenuSettings.NowLevelId < MenuSettings.LevelInfos.Length - 1)
                {
                    MenuSettings.NowLevelId += 1;
                    MenuSettings.LevelModelHolder.transform.DOMove(MenuSettings.LV3Last, 0.5f);
                }
            }
           // MenuSettings.LevelInfos[MenuSettings.NowLevelId].CrownAniStopper();
            canclick = false;
            MenuSettings.PlayButton.GetComponent<Button>().enabled = false;
            StartCoroutine(clicks());
           // StartCoroutine(crownplay());
            MenuSettings.MainCamera.DOColor(MenuSettings.LevelInfos[MenuSettings.NowLevelId].ThisLevelCameraBackColor, 0.5f);
            Setting.GetComponent<Button>().enabled = false;
            Trip.GetComponent<Button>().enabled = false;          
        }
    }
    IEnumerator clicks()
    {
        yield return new WaitForSeconds(0.5f);
        canclick = true;
        MenuSettings.PlayButton.GetComponent<Button>().enabled = true;
        Setting.GetComponent<Button>().enabled = true;
        Trip.GetComponent<Button>().enabled = true;
        MenuSettings.AudioPlayer.Play();
    }
   // IEnumerator crownplay()
   // {
   //     yield return new WaitForSeconds(0.48f);
        //MenuSettings.LevelInfos[MenuSettings.NowLevelId].GetComponent<LevelMassage>().CrownAniPlayer();
   // }
}
