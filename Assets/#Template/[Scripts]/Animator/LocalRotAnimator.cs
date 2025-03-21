using DG.Tweening;
using DancingLineFanmade.Level;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DancingLineFanmade.Animator
{
    public class LocalRotAnimator : AnimatorBase
    {
        [SerializeField] private Vector3 rotation = Vector3.zero;
        [SerializeField] private RotateMode rotateMode = RotateMode.Fast;

        private void Start()
        {
            switch (transformType)
            {
                case TransformType.New:
                    finalTransform = rotation;
                    break;
                case TransformType.Add:
                    finalTransform = originalTransform + rotation;
                    break;
            }
            InitTransform(AnimatorType.Rotation);
            if (timeTrigger) InitTime();
        }

        private void Update()
        {
            if (!finished && LevelManager.GameState == GameStatus.Playing && AudioManager.Time > triggerTime && timeTrigger) Trigger();
        }

        public void Trigger()
        {
            TriggerAnimator(AnimatorType.Rotation, rotateMode);
        }

#if UNITY_EDITOR
        [Button("Get original rotation", ButtonSizes.Large), HorizontalGroup("0")]
        private void GetOriginalRot()
        {
            originalTransform = transform.localEulerAngles;
        }

        [Button("Set as original rotation", ButtonSizes.Large), HorizontalGroup("0")]
        private void SetOriginalRot()
        {
            transform.localEulerAngles = originalTransform;
        }
#endif
    }
}