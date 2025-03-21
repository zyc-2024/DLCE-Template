using System.Collections.Generic;
using System.Linq;
using DancingLineFanmade.Level;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DancingLineFanmade.Guidance
{
    [DisallowMultipleComponent]
    public class ScoreReader : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private TextAsset score;
        [SerializeField] private float offset;

        [SerializeField] internal List<float> hitTime;

        private readonly List<string> hit1 = new List<string>();
        private readonly List<List<string>> hit2 = new List<List<string>>();

#if UNITY_EDITOR
        private void ReadScore()
        {
            hit1.Clear();
            hit2.Clear();
            hitTime.Clear();

            if (score == null)
            {
                Debug.LogError("未选择谱面数据文件");
                return;
            }

            foreach (var VARIABLE in score.text.Split('\n'))
            {
                hit1.Add(VARIABLE.Trim());
            }

            var index = hit1.IndexOf("[HitObjects]");
            hit1.RemoveRange(0, index + 1);
            hit1.RemoveAll(text => text == string.Empty);

            foreach (var VARIABLE in hit1)
            {
                hit2.Add(VARIABLE.Split(',').ToList());
            }

            foreach (var VARIABLE in hit2)
            {
                hitTime.Add(int.Parse(VARIABLE[2]) / 1000f + offset);
            }
        }

        [Button("Create Guide Taps By Score", ButtonSizes.Large)]
        private void Create()
        {
            ReadScore();

            if (hitTime.Count <= 0) return;
            var boxPrefab = Resources.Load<GameObject>("Prefabs/GuidanceBox");
            var startPos = player.startPosition;
            var firstDir = player.firstDirection;
            var secondDir = player.secondDirection;
            var speed = player.levelData.speed;
            var hitParent = new GameObject("GuideBoxHolder-ScoreCreated");
            var count = 1;

            var boxes = new List<GameObject>();
            var firstBox = Instantiate(boxPrefab, startPos - new Vector3(0f, 0.45f, 0f),
                Quaternion.Euler(90, firstDir.y, 0));
            firstBox.GetComponent<GuidanceBox>().canBeTriggered = false;
            firstBox.transform.parent = hitParent.transform;
            boxes.Add(firstBox);

            for (var i = 0; i < hitTime.Count; i++)
            {
                var focusedBox = Instantiate(boxPrefab, hitParent.transform, true);
                if (boxes.Count > 0) focusedBox.transform.position = boxes[^1].transform.position;
                else focusedBox.transform.position = startPos - new Vector3(0f, 0.45f, 0f);

                focusedBox.transform.eulerAngles = (count % 2) switch
                {
                    1 => new Vector3(90, firstDir.y, 0),
                    0 => new Vector3(90, secondDir.y, 0),
                    _ => focusedBox.transform.eulerAngles
                };

                focusedBox.transform.Translate(
                    i == 0
                        ? new Vector3(0, hitTime[i] * speed, 0)
                        : new Vector3(0, (hitTime[i] - hitTime[i - 1]) * speed, 0), Space.Self);

                boxes.Add(focusedBox);
                count++;
            }

            for (var i = 0; i < boxes.Count; i++)
            {
                boxes[i].transform.eulerAngles = ((i + 1) % 2) switch
                {
                    1 => new Vector3(90, firstDir.y, 0),
                    0 => new Vector3(90, secondDir.y, 0),
                    _ => boxes[i].transform.eulerAngles
                };
            }
        }

        [Button("Reload Hit Time", ButtonSizes.Large)]
        private void ReloadHits()
        {
            ReadScore();
        }
#endif
    }
}