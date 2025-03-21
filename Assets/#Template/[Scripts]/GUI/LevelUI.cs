using DancingLineFanmade.Level;
using DG.Tweening;
using MaxLine2.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DancingLineFanmade.UI
{
    public class LevelUI : MonoBehaviour
    {
        public static LevelUI Instance { get; private set; }

        [SerializeField] private Text title;
        [SerializeField] private Text percentage;
        [SerializeField] private Text block;
        [SerializeField] private Image background;
        [SerializeField] private RectTransform barFill;
        [SerializeField] private RectTransform moveUpPart;
        [SerializeField] private RectTransform moveDownPart;
        [SerializeField] private List<Button> buttons = new List<Button>();

        private Player player;

        private void Awake()
        {
            Instance = this;
            player = Player.Instance;

            moveUpPart.anchoredPosition = new Vector2(0f, -250f);
            moveDownPart.anchoredPosition = new Vector2(0f, 430f);
            background.color = Color.clear;
            foreach (Button b in buttons) b.interactable = false;
        }

        internal void Invoke(float percent, int blockCount)
        {
            moveUpPart.DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.OutSine);
            moveDownPart.DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.OutSine);
            background.DOFade(0.64f, 0.5f).SetEase(Ease.OutSine).OnComplete(() => { foreach (Button b in buttons) b.interactable = true; });
            barFill.sizeDelta = new Vector2(10f, 18f) + new Vector2(480f * percent, 0f);
            percentage.text = ((int)(percent * 100f)).ToString() + "%";
            block.text = blockCount + "/10";
            title.text = player.levelData.levelTitle;
        }

        public void ReloadScene()
        {
            if (LoadingPage.Instance) LoadingPage.Instance.Load(SceneManager.GetActiveScene().name);
            else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}