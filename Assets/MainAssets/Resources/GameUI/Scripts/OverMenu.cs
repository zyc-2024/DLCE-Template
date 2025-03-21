using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OverMenu : MonoBehaviour
{
    public MainLine line;
    public Text LevelNameZh_CNLabel;
    public Text DiamondLabel;
    public RawImage CrownIcon1;
    public RawImage CrownIcon2;
    public RawImage CrownIcon3;
    public RawImage PerCrownIcon1;
    public RawImage PerCrownIcon2;
    public RawImage PerCrownIcon3;
    [HideInInspector] public static bool perfact = false;
    public Texture NoLightCrown, LightCrown, NoLightPerCrown, LightPerCrown;
    public Image ProgressImage;
    public Text ProgressText;
    public Image PerfectImage;

    private void Start()
    {
        PerfectImage.gameObject.SetActive(false);
    }

    public void GameOver(bool win, int Percentage, int PickDimaondCount, int PickCrownCount)
    {
        ProgressImage.DOFillAmount(float.Parse(Percentage.ToString()) / 100, 2f);
        ProgressText.text = Percentage.ToString() + "%";
        DiamondLabel.text = line.PickDiamondCount.ToString() + "/10";

    }
    private void Update()
    {
        if (perfact)
        {
            showPcrown();
        }
        if (!perfact)
        {
            showNcrown();
        }
        if (line.NowPercentage >= 100 && line.PickDiamondCount >= 10 && line.PickCrown >= 3)
        {
            perfact = true;
            PerfectImage.gameObject.SetActive(true);
        }
        if (line.NowPercentage < 100 && line.PickDiamondCount < 10 && line.PickCrown < 3)
        {
            perfact = false;
            PerfectImage.gameObject.SetActive(false);
        }
    }
    public void showNcrown()
    {
        PerCrownIcon1.gameObject.SetActive(false);
        PerCrownIcon2.gameObject.SetActive(false);
        PerCrownIcon3.gameObject.SetActive(false);

        CrownIcon1.gameObject.SetActive(true);
        CrownIcon2.gameObject.SetActive(true);
        CrownIcon3.gameObject.SetActive(true);

        if (line.PickCrown <= 0)
        {
            CrownIcon1.texture = NoLightCrown;
            CrownIcon2.texture = NoLightCrown;
            CrownIcon3.texture = NoLightCrown;
        }
        if (line.PickCrown == 1)
        {
            CrownIcon1.texture = LightCrown;
            CrownIcon2.texture = NoLightCrown;
            CrownIcon3.texture = NoLightCrown;
        }
        if (line.PickCrown == 2)
        {
            CrownIcon1.texture = LightCrown;
            CrownIcon2.texture = LightCrown;
            CrownIcon3.texture = NoLightCrown;
        }
        if (line.PickCrown >= 3)
        {
            CrownIcon1.texture = LightCrown;
            CrownIcon2.texture = LightCrown;
            CrownIcon3.texture = LightCrown;
        }
    }

    public void showPcrown()
    {
        PerCrownIcon1.gameObject.SetActive(true);
        PerCrownIcon2.gameObject.SetActive(true);
        PerCrownIcon3.gameObject.SetActive(true);

        CrownIcon1.gameObject.SetActive(false);
        CrownIcon2.gameObject.SetActive(false);
        CrownIcon3.gameObject.SetActive(false);

        if (line.PickCrown <= 0)
        {
            PerCrownIcon1.texture = NoLightPerCrown;
            PerCrownIcon2.texture = NoLightPerCrown;
            PerCrownIcon3.texture = NoLightPerCrown;
        }
        if (line.PickCrown == 1)
        {
            PerCrownIcon1.texture = LightPerCrown;
            PerCrownIcon2.texture = NoLightPerCrown;
            PerCrownIcon3.texture = NoLightPerCrown;
        }
        if (line.PickCrown == 2)
        {
            PerCrownIcon1.texture = LightPerCrown;
            PerCrownIcon2.texture = LightPerCrown;
            PerCrownIcon3.texture = NoLightPerCrown;
        }
        if (line.PickCrown >= 3)
        {
            PerCrownIcon1.texture = LightPerCrown;
            PerCrownIcon2.texture = LightPerCrown;
            PerCrownIcon3.texture = LightPerCrown;
        }
    }
}
