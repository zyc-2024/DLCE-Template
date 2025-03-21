using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace DancingLineFanmade.Level
{
    [DisallowMultipleComponent]
    public class CameraFollower : MonoBehaviour
    {
        public static CameraFollower Instance { get; private set; }

        [SerializeField] private Transform target;
        [SerializeField] private Vector3 followSpeed = new Vector3(1.5f, 1.5f, 1.5f);

        [SerializeField] internal bool follow = true;
        [SerializeField] internal bool smooth = true;

        internal Transform rotator;
        internal Transform scale;

        private Tween offset;
        private Tween rotation;
        private Tween zoom;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            rotator = transform.GetChild(0);
            scale = rotator.GetChild(0);
        }

        private void Update()
        {
            Vector3 translation = target.position - transform.position;
            if (LevelManager.GameState == GameStatus.Playing && follow)
            {
                if (smooth) transform.Translate(new Vector3(translation.x * followSpeed.x * Time.deltaTime, translation.y * followSpeed.y * Time.deltaTime, translation.z * followSpeed.z * Time.deltaTime));
                else transform.position = target.position;
            }
        }

        internal void Trigger(Vector3 offset, Vector3 rotation, Vector3 scale, float duration, Ease ease, RotateMode mode, UnityEvent action = null)
        {
            SetOffset(offset, duration, ease);
            SetRotation(rotation, duration, mode, ease);
            SetScale(scale, duration, ease);
            this.rotation.OnComplete(() => action.Invoke());
        }

        internal void Kill()
        {
            offset?.Kill();
            rotation?.Kill();
            zoom?.Kill();
        }

        private void SetOffset(Vector3 offset, float duration, Ease ease = Ease.InOutSine)
        {
            if (this.offset != null)
            {
                this.offset.Kill();
                this.offset = null;
            }
            this.offset = rotator.DOLocalMove(offset, duration).SetEase(ease);
        }

        private void SetRotation(Vector3 rotation, float duration, RotateMode mode, Ease ease = Ease.InOutSine)
        {
            if (this.rotation != null)
            {
                this.rotation.Kill();
                this.rotation = null;
            }
            this.rotation = rotator.DOLocalRotate(rotation, duration, mode).SetEase(ease);
        }

        private void SetScale(Vector3 scale, float duration, Ease ease = Ease.InOutSine)
        {
            if (zoom != null)
            {
                zoom.Kill();
                zoom = null;
            }
            zoom = this.scale.DOScale(scale, duration).SetEase(ease);
        }
    }

    [Serializable]
    public class CameraSettings
    {
        public Vector3 offset;
        public Vector3 rotation;
        public Vector3 scale;
        public bool follow;

        internal CameraSettings GetCamera()
        {
            CameraSettings c = new CameraSettings();
            c.offset = CameraFollower.Instance.rotator.localPosition;
            c.rotation = CameraFollower.Instance.rotator.localEulerAngles;
            c.scale = CameraFollower.Instance.scale.localScale;
            c.follow = CameraFollower.Instance.follow;
            return c;
        }

        internal void SetCamera()
        {
            CameraFollower.Instance.rotator.localPosition = offset;
            CameraFollower.Instance.rotator.localEulerAngles = rotation;
            CameraFollower.Instance.scale.localScale = scale;
            CameraFollower.Instance.follow = follow;
        }
    }
}