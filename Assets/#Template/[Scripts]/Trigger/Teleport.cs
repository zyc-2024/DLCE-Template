using DancingLineFanmade.Level;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DancingLineFanmade.Trigger
{
    internal enum TeleportType
    {
        Target,
        Position
    }

    [DisallowMultipleComponent, RequireComponent(typeof(Collider))]
    public class Teleport : MonoBehaviour
    {
        [SerializeField, EnumToggleButtons] private TeleportType type = TeleportType.Target;

        [SerializeField, HideIf("type", TeleportType.Position)]
        private Transform target;

        [SerializeField, HideIf("type", TeleportType.Target)]
        private Vector3 position = Vector3.zero;

        [SerializeField] private bool turn;
        [SerializeField, ShowIf("turn")] private Direction targetDirection = Direction.First;

        private void OnTriggerEnter(Collider other)
        {
            var final = type switch
            {
                TeleportType.Target => target.position,
                TeleportType.Position => position,
                _ => Vector3.zero
            };
            if (other.CompareTag("Player"))
                LevelManager.InitPlayerPosition(Player.Instance, final, turn, targetDirection);
        }
    }
}