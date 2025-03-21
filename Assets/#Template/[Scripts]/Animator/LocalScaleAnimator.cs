using DancingLineFanmade.Level;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DancingLineFanmade.Animator
{
    public class LocalScaleAnimator : AnimatorBase
    {
        [SerializeField] private Vector3 scale = Vector3.one;

        private void Start()
        {
            switch (transformType)
            {
                case TransformType.New:
                    finalTransform = scale;
                    break;
                case TransformType.Add:
                    finalTransform = originalTransform + scale;
                    break;
            }
            InitTransform(AnimatorType.Scale);
            if (timeTrigger) InitTime();
        }

        private void Update()
        {
            if (!finished && LevelManager.GameState == GameStatus.Playing && AudioManager.Time > triggerTime && timeTrigger) Trigger();
        }

        public void Trigger()
        {
            TriggerAnimator(AnimatorType.Scale);
        }

#if UNITY_EDITOR
        [Button("Get original scale", ButtonSizes.Large), HorizontalGroup("0")]
        private void GetOriginalScale()
        {
            originalTransform = transform.localScale;
        }

        [Button("Set as original scale", ButtonSizes.Large), HorizontalGroup("0")]
        private void SetOriginalScale()
        {
            transform.localScale = originalTransform;
        }

        [Button("Get new scale", ButtonSizes.Large), HorizontalGroup("1")]
        private void GetNewScale()
        {
            switch (transformType)
            {
                case TransformType.New:
                    scale = transform.localScale;
                    break;
                case TransformType.Add:
                    scale = transform.localScale - originalTransform;
                    break;
            }
        }

        [Button("Set as new scale", ButtonSizes.Large), HorizontalGroup("1")]
        private void SetNewScale()
        {
            switch (transformType)
            {
                case TransformType.New:
                    transform.localScale = scale;
                    break;
                case TransformType.Add:
                    transform.localScale = originalTransform + scale;
                    break;
            }
        }
#endif
    }
}