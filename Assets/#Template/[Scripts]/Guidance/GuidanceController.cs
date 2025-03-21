using DancingLineFanmade.Level;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DancingLineFanmade.Guidance
{
    [DisallowMultipleComponent]
    public class GuidanceController : MonoBehaviour
    {
        [Title("Creating")]
        [SerializeField] private bool createBoxes = false;
        [SerializeField] private bool createLines = true;

        [Title("Settings")]
        [SerializeField] internal Transform boxHolder;
        [SerializeField] private Color guidanceBoxColor = Color.white;
        [SerializeField] private float lineGap = 0.2f;

        private GameObject boxPrefab;
        private GameObject linePrefab;
        private Transform holder;
        private int id = 0;
        private List<GuidanceBox> boxes = new List<GuidanceBox>();
        private bool started = false;
        private float forward;

        private void Awake()
        {
            id = 0;
            boxPrefab = Resources.Load<GameObject>("Prefabs/GuidanceBox");
            linePrefab = Resources.Load<GameObject>("Prefabs/GuidanceLine");
            if (createBoxes) holder = new GameObject("GuidanceBoxHolder").transform;
            if (boxHolder) boxes = boxHolder.GetComponentsInChildren<GuidanceBox>().ToList();
            foreach (GuidanceBox b in boxes) b.SetColor(guidanceBoxColor);
            if (createLines) GenerateLines();
        }

        private void Start()
        {
            if (createBoxes)
            {
                GameObject box = Instantiate(boxPrefab, Player.Instance.transform.position - new Vector3(0f, 0.45f, 0f), Quaternion.Euler(90, Player.Instance.firstDirection.y, 0));
                box.transform.parent = holder;
                box.name = "OriginalGuidanceBox ";
                box.GetComponent<GuidanceBox>().canBeTriggered = false;
            }
        }

        private void Update()
        {
            forward = Player.Instance.transform.eulerAngles.y == Player.Instance.firstDirection.y ? Player.Instance.secondDirection.y : Player.Instance.firstDirection.y;
            if (createBoxes && LevelManager.GameState == GameStatus.Playing && !started)
            {
                Player.Instance.onTurn.AddListener(() =>
                {
                    GameObject box = Instantiate(boxPrefab, Player.Instance.transform.position - new Vector3(0f, 0.45f, 0f), Quaternion.Euler(90, forward, 0));
                    box.transform.parent = holder;
                    box.name = "GuidanceBox " + id;
                    id++;
                });
                started = true;
            }
        }

        private void GenerateLines()
        {
            for (int a = 0; a < boxes.Count; a++)
            {
                Transform line;
                if (a + 1 < boxes.Count && boxes[a].haveLine)
                {
                    line = Instantiate(linePrefab, 0.5f * (boxes[a].transform.position + boxes[a + 1].transform.position), Quaternion.Euler(Vector3.zero)).transform;
                    line.GetComponent<SpriteRenderer>().color = guidanceBoxColor;
                    line.localScale = new Vector3(0.15f, (boxes[a + 1].transform.position - boxes[a].transform.position).magnitude - 0.5f * boxPrefab.transform.localScale.y - 2 * lineGap, 0.15f);
                    line.parent = boxes[a].transform;
                    line.localEulerAngles = Vector3.zero;
                    line.name = line.parent.name + " - Line";
                    if (line.transform.localScale.y <= 0f) Destroy(line.gameObject);
                }
            }
        }
    }
}