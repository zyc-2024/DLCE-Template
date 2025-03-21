using DancingLineFanmade.Level;
using DG.Tweening;
using UnityEngine;

namespace DancingLineFanmade.Trigger
{
    [DisallowMultipleComponent]
    public class Checkpoint : MonoBehaviour
    {
        private Transform rotator;
        private Transform frame;
        private Transform core;

        private void Start()
        {
            rotator = transform.Find("Rotator");
            frame = rotator.Find("Frame");
            core = rotator.Find("Core");

            rotator.localScale = Vector3.zero;
        }

        private void Update()
        {
            frame.Rotate(Vector3.up, Time.deltaTime * -45f);
            core.Rotate(Vector3.up, Time.deltaTime * 45f);
        }

        internal void EnterTrigger()
        {
            rotator.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        }
    }
}