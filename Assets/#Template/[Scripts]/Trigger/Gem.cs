using DancingLineFanmade.Level;
using UnityEngine;

namespace DancingLineFanmade.Trigger
{
    [DisallowMultipleComponent, RequireComponent(typeof(Collider), typeof(MeshRenderer))]
    public class Gem : MonoBehaviour
    {
        [SerializeField] private bool fake = false;

        private GameObject effectPrefab;
        private GameObject effect;
        private bool got = false;

        private MeshRenderer MeshRenderer
        {
            get => GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            MeshRenderer.enabled = true;
            effectPrefab = Resources.Load<GameObject>("Prefabs/GetGem");
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 60f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !got && !fake)
            {
                got = true;
                MeshRenderer.enabled = false;
                if (QualitySettings.GetQualityLevel() > 0) effect = Instantiate(effectPrefab, transform.position, Quaternion.Euler(-90, 0, 0));
                Player.Instance.blockCount++;
            }
        }
    }
}