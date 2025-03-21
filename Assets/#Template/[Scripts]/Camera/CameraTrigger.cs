using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace DancingLineFanmade.Level
{
    public class CameraTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent onFinished = new UnityEvent();
        [SerializeField] private Vector3 offset = Vector3.zero;
        [SerializeField] private Vector3 rotation = new Vector3(54f, 45f, 0f);
        [SerializeField] private Vector3 scale = Vector3.one;
        [SerializeField] private bool follow = true;
        [SerializeField, MinValue(0f)] private float duration = 2f;
        [SerializeField] private Ease ease = Ease.InOutSine;
        [SerializeField] private RotateMode mode = RotateMode.FastBeyond360;
        [SerializeField] private bool triggered = true;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && triggered)
            {
                CameraFollower.Instance.follow = follow;
                CameraFollower.Instance.Trigger(offset, rotation, scale, duration, ease, mode, onFinished);
            }
        }

        public void Trigger()
        {
            if (!triggered)
            {
                CameraFollower.Instance.follow = follow;
                CameraFollower.Instance.Trigger(offset, rotation, scale, duration, ease, mode, onFinished);
            }
        }
    }
}