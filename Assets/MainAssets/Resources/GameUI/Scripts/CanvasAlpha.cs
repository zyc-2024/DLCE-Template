﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CanvasAlpha : MonoBehaviour
{
    public CanvasGroup setting_canvas,ms_canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void to_setting()
    {
        setting_canvas.gameObject.SetActive(true);
        DOTween.To(() => setting_canvas.alpha, x => setting_canvas.alpha = x, 1, 0.3f);
        DOTween.To(() => ms_canvas.alpha, x => ms_canvas.alpha = x, 0, 0.3f);
        Invoke("msfalse", 0.3f);
    }
    void msfalse()
    {
        ms_canvas.gameObject.SetActive(false);
    }

    public void to_ms()
    {
        ms_canvas.gameObject.SetActive(true);
        DOTween.To(() => ms_canvas.alpha, x => ms_canvas.alpha = x, 1, 0.3f);
        DOTween.To(() => setting_canvas.alpha, x => setting_canvas.alpha = x, 0, 0.3f);
        Invoke("sfalse", 0.3f);
    }
    void sfalse()
    {
        setting_canvas.gameObject.SetActive(false);
    }

}
