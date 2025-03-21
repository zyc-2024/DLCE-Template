using DancingLineFanmade.Level;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DancingLineFanmade.Trigger
{
    [DisallowMultipleComponent, RequireComponent(typeof(Collider))]
    public class Jump : MonoBehaviour
    {
        [SerializeField, MinValue(0f)] internal float power = 500f;
        [SerializeField] private bool changeDirection;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (changeDirection) Player.Instance.Turn();
            Player.Rigidbody.AddForce(0, power * Player.Rigidbody.mass, 0, ForceMode.Force);
            Player.Instance.Events?.Invoke(7);
        }
    }
}