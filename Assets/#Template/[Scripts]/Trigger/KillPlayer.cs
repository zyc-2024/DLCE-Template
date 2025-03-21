using DancingLineFanmade.Level;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DancingLineFanmade.Trigger
{
    public enum DieReason
    {
        Hit,
        Drowned,
        Border
    }

    [DisallowMultipleComponent, RequireComponent(typeof(Collider))]
    public class KillPlayer : MonoBehaviour
    {
        [SerializeField, EnumToggleButtons] private DieReason reason = DieReason.Drowned;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !Player.Instance.noDeath && LevelManager.GameState == GameStatus.Playing)
                LevelManager.PlayerDeath(Player.Instance, reason, Resources.Load<GameObject>("Prefabs/Remain"));
        }
    }
}