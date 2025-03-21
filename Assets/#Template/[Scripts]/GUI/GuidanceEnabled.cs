using UnityEngine;
using UnityEngine.UI;

namespace DancingLineFanmade.Guidance
{
    [DisallowMultipleComponent]
    public class GuidanceEnabled : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Image background;
        [SerializeField] private Sprite on;
        [SerializeField] private Sprite off;
        [SerializeField] private bool available;

        private GuidanceController controller;

        private void Start()
        {
            controller = FindObjectOfType<GuidanceController>();
            SetGuidance(available);

            if (controller.boxHolder) return;
            GetComponent<Button>().interactable = false;
            foreach (var i in GetComponentsInChildren<Image>())
            {
                i.enabled = false;
                i.raycastTarget = false;
            }

            background.enabled = false;
            background.raycastTarget = false;
        }

        public void OnClick()
        {
            available = !available;
            SetGuidance(available);
        }

        private void SetGuidance(bool n_available)
        {
            if (n_available)
            {
                image.sprite = on;
                if (controller.boxHolder) controller.boxHolder.gameObject.SetActive(true);
            }
            else
            {
                image.sprite = off;
                if (controller.boxHolder) controller.boxHolder.gameObject.SetActive(false);
            }
        }
    }
}