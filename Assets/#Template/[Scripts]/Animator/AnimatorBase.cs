using DancingLineFanmade.Level;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace DancingLineFanmade.Animated
{
    public enum TransformType
    {
        New,
        Add
    }

    public enum AnimatorType
    {
        Position,
        Rotation,
        Scale
    }

    public class AnimatorBase : MonoBehaviour
    {
        private Transform selfTransform;

        [SerializeField] internal UnityEvent onAnimatorStart;
        [SerializeField] internal UnityEvent onAnimatorFinished;
        [SerializeField, EnumToggleButtons] protected TransformType transformType = TransformType.New;
        [SerializeField] protected bool triggeredByTime = true;
        [SerializeField, MinValue(0f)] protected float triggerTime;
        [SerializeField, MinValue(0f)] private float duration = 2f;
        [SerializeField] private bool offsetTime;
        [SerializeField] protected bool dontRevive;
        [SerializeField] private Ease ease = Ease.InOutSine;
        [SerializeField] protected Vector3 originalTransform = Vector3.zero;

        protected bool finished;
        protected Vector3 finalTransform = Vector3.zero;
        protected int index;

        private void OnEnable()
        {
            selfTransform = transform;
        }

        protected void TriggerAnimator(AnimatorType type, RotateMode rotateMode = RotateMode.Fast)
        {
            finished = true;
            onAnimatorStart.Invoke();
            Animator(type, rotateMode).OnComplete(() => onAnimatorFinished.Invoke());
        }

        protected void InitTransform(AnimatorType type)
        {
            switch (type)
            {
                case AnimatorType.Position:
                    selfTransform.localPosition = originalTransform;
                    break;
                case AnimatorType.Rotation:
                    selfTransform.localEulerAngles = originalTransform;
                    break;
                case AnimatorType.Scale:
                    selfTransform.localScale = originalTransform;
                    break;
            }
        }

        protected void InitTime()
        {
            triggerTime = offsetTime ? triggerTime - duration : triggerTime;
        }

        private Tween Animator(AnimatorType type, RotateMode rotateMode)
        {
            index = Player.Instance.Checkpoints.Count;

            return type switch
            {
                AnimatorType.Position => selfTransform.DOLocalMove(finalTransform, duration).SetEase(ease),
                AnimatorType.Rotation => selfTransform.DOLocalRotate(finalTransform, duration, rotateMode)
                    .SetEase(ease),
                AnimatorType.Scale => selfTransform.DOScale(finalTransform, duration).SetEase(ease),
                _ => null
            };
        }
    }
}