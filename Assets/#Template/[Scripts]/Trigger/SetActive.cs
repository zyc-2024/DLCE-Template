using DancingLineFanmade.Level;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace DancingLineFanmade.Trigger
{
    [DisallowMultipleComponent]
    public class SetActive : MonoBehaviour
    {
        [SerializeField] private bool activeOnAwake = false;
        [SerializeField, TableList] internal List<SingleActive> actives = new List<SingleActive>();

        private void Start()
        {
            if (activeOnAwake) foreach (SingleActive s in actives) s.SetActive();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !activeOnAwake) foreach (SingleActive s in actives) s.SetActive();
        }
    }
}