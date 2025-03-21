using UnityEngine;
using UnityEngine.Events;

namespace DancingLineFanmade.Trigger
{
    [DisallowMultipleComponent, RequireComponent(typeof(Collider))]
    public class EventTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent onTriggerEnter = new UnityEvent();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) onTriggerEnter.Invoke();
        }
    }
}