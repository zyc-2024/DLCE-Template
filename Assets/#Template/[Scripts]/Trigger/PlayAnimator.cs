using System.Collections.Generic;
using UnityEngine;

namespace DancingLineFanmade.Trigger
{
    [DisallowMultipleComponent, RequireComponent(typeof(Collider))]
    public class PlayAnimator : MonoBehaviour
    {
        [SerializeField] private List<UnityEngine.Animator> animators = new List<UnityEngine.Animator>();

        private void Start()
        {
            foreach (UnityEngine.Animator animator in animators) animator.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) foreach (UnityEngine.Animator animator in animators) animator.enabled = true;
        }
    }
}