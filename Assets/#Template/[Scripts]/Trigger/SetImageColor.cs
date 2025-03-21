using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DancingLineFanmade.Trigger
{
    [RequireComponent(typeof(Collider))]
    public class SetImageColor : MonoBehaviour
    {
        [SerializeField, TableList] private List<SingleImage> images = new List<SingleImage>();
        [SerializeField] private float duration = 2f;
        [SerializeField] private Ease ease = Ease.InOutSine;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) foreach (SingleImage s in images) s.SetColor(duration, ease);
        }
    }

    [Serializable]
    public class SingleImage
    {
        public Image image;
        public Color color = Color.white;

        private List<Tween> tweens = new List<Tween>();

        internal void SetColor(float duration, Ease ease)
        {
            tweens.Add(image.DOColor(color, duration).SetEase(ease));
        }
    }
}