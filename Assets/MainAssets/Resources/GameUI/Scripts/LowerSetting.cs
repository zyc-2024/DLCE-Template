using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowerSetting : MonoBehaviour
{
    public Trip Trip;
    public Setting Setting;
    public Vector2 To_Trip_Rect, To_Setting_Rect;
    [HideInInspector] public bool NowIsTrip = true, NowIsSetting;
    // Start is called before the first frame update
    void Start()
    {
        NowIsTrip = true;
        Trip.GetComponent<Button>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(NowIsTrip)
        {
            Trip.GetComponent<Image>().sprite = Trip.GetComponent<Trip>().OnSprite;
            Setting.GetComponent<Image>().sprite = Setting.GetComponent<Setting>().OffSprite;
            NowIsSetting = false;
            NowIsTrip = true;
        }
        if(NowIsSetting)
        {
            Trip.GetComponent<Image>().sprite = Trip.GetComponent<Trip>().OffSprite;
            Setting.GetComponent<Image>().sprite = Setting.GetComponent<Setting>().OnSprite;
            NowIsTrip = false;
            NowIsSetting = true;
        }
    }
}
