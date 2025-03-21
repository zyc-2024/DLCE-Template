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
        [SerializeField] private List<float> hits;

        private List<string> hitPoints = new List<string>();

        [Button("Create Guide Taps By Score", ButtonSizes.Large)]
        private void Create()
        {
            hitPoints.Clear();
            hits.Clear();

            if (score == null)
            {
                Debug.LogError("未选择谱面数据文件");
                return;
            }

            #region Read Score

            foreach (var VARIABLE in score.text.Split('\n'))
            {
                hitPoints.Add(VARIABLE.Trim());
            }

            var index = hitPoints.IndexOf("[HitObjects]");
            hitPoints.RemoveRange(0, index + 1);
            hitPoints.RemoveAll(text => text == string.Empty);

            var result = hitPoints.Select(VARIABLE => VARIABLE.Remove(0, 8))
                .Select(hit => hit.Remove(hit.Length - 13, 13)).ToList();
            hitPoints = result;
            foreach (var VARIABLE in hitPoints)
            {
                hits.Add(int.Parse(VARIABLE) / 1000f + offset);
            }

            #endregion

            #region Create Guide Boxes

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

            for (var i = 0; i < hits.Count; i++)
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
                        ? new Vector3(0, hits[i] * speed, 0)
                        : new Vector3(0, (hits[i] - hits[i - 1]) * speed, 0), Space.Self);

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

            #endregion
        }
    }
}