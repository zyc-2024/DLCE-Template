using DancingLineFanmade.Level;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DancingLineFanmade.Animator
{
    public class LocalPosAnimator : AnimatorBase
    {
        [SerializeField] private Vector3 position = Vector3.zero;

        private void Start()
        {
            switch (transformType)
            {
                case TransformType.New:
                    finalTransform = position;
                    break;
                case TransformType.Add:
                    finalTransform = originalTransform + position;
                    break;
            }
            InitTransform(AnimatorType.Position);
            if (timeTrigger) InitTime();
        }

        private void Update()
        {
            if (!finished && LevelManager.GameState == GameStatus.Playing && AudioManager.Time > triggerTime && timeTrigger) Trigger();
        }

        public void Trigger()
        {
            TriggerAnimator(AnimatorType.Position);
        }

#if UNITY_EDITOR
        [Button("Get original position", ButtonSizes.Large), HorizontalGroup("0")]
        private void GetOriginalPos()
        {
            originalTransform = transform.localPosition;
        }

        [Button("Set as original position", ButtonSizes.Large), HorizontalGroup("0")]
        private void SetOriginalPos()
        {
            transform.localPosition = originalTransform;
        }

        [Button("Get new position", ButtonSizes.Large), HorizontalGroup("1")]
        private void GetNewPos()
        {
            switch (transformType)
            {
                case TransformType.New:
                    position = transform.localPosition;
                    break;
                case TransformType.Add:
                    position = transform.localPosition - originalTransform;
                    break;
            }
        }

        [Button("Set as new position", ButtonSizes.Large), HorizontalGroup("1")]
        private void SetNewPos()
        {
            switch (transformType)
            {
                case TransformType.New:
                    transform.localPosition = position;
                    break;
                case TransformType.Add:
                    transform.localPosition = originalTransform + position;
                    break;
            }
        }
#endif
    }
}