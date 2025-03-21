using DancingLineFanmade.Level;
using UnityEngine;

namespace DancingLineFanmade.Auto
{
    [DisallowMultipleComponent]
    public class AutoPlay : MonoBehaviour
    {
        private Transform playerTransform;
        private Transform selfTransform;
        private const float triggerDistance = 0.33f;
        private bool triggered;

        private float Distance => (selfTransform.position - playerTransform.position).sqrMagnitude;

        private void Start()
        {
            selfTransform = transform;
            playerTransform = Player.Instance.transform;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player") || !(Distance <= triggerDistance) || triggered) return;
            triggered = true;
            Player.Instance.Turn();
        }
    }
}