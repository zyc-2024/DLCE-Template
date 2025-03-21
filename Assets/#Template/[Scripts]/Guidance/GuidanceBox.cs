using DancingLineFanmade.Level;
using UnityEngine;

namespace DancingLineFanmade.Guidance
{
    [DisallowMultipleComponent]
    public class GuidanceBox : MonoBehaviour
    {
        [SerializeField] private float triggerDistance;
        [SerializeField] private float appearDistance;
        [SerializeField] internal bool canBeTriggered = true;
        [SerializeField] internal bool haveLine = true;

        private SpriteRenderer spriteRenderer;
        private bool used = false;
        private GameObject triggerEffect;

        public SpriteRenderer Renderer
        {
            get
            {
                if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
                return spriteRenderer;
            }
        }

        private float Distance
        {
            get => (transform.position - Player.Instance.transform.position).magnitude;
        }

        public void SetColor(Color color)
        {
            Renderer.color = color;
        }

        private void Start()
        {
            triggerEffect = Resources.Load<GameObject>("Prefabs/Triggered");
            if (Distance > appearDistance) Disappear(false);
        }

        private void Update()
        {
            if (!used && Distance <= appearDistance) Appear(false);
            if (LevelManager.Clicked && !used && Distance <= triggerDistance && canBeTriggered) Trigger();
        }

        private void Trigger()
        {
            used = true;
            Disappear(true);
            Destroy(Instantiate(triggerEffect, transform.position, Quaternion.Euler(Vector3.zero)), 1f);
        }

        private void Appear(bool onlyBox)
        {
            SpriteRenderer[] renderers = transform.GetComponentsInChildren<SpriteRenderer>();
            if (!onlyBox) foreach (SpriteRenderer r in renderers) r.enabled = true;
            else Renderer.enabled = true;
        }

        private void Disappear(bool onlyBox)
        {
            SpriteRenderer[] renderers = transform.GetComponentsInChildren<SpriteRenderer>();
            if (!onlyBox) foreach (SpriteRenderer r in renderers) r.enabled = false;
            else Renderer.enabled = false;
        }
    }
}