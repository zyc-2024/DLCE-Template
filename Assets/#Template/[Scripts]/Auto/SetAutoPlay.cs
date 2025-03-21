using DancingLineFanmade.Level;
using UnityEngine;

namespace DancingLineFanmade.Auto
{
    [DisallowMultipleComponent]
    public class SetAutoPlay : MonoBehaviour
    {
        private bool active;

        public void SetAuto()
        {
            active = !active;
            if (!AutoPlayController.Instance || !AutoPlayController.Instance.holder) return;
            AutoPlayController.Instance.SetHolder(active);
            Player.Instance.disallowInput = active;
        }
    }
}