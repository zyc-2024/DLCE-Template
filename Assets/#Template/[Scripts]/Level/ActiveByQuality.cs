using Sirenix.OdinInspector;
using UnityEngine;

namespace DancingLineFanmade.Level
{
    public enum ActiveType
    {
        Display,
        Hide
    }

    public enum QualityLevel
    {
        Low,
        Medium,
        High
    }

    [DisallowMultipleComponent]
    public class ActiveByQuality : MonoBehaviour
    {
        [SerializeField, EnumToggleButtons, InfoBox("$message"), DisableInPlayMode]
        private ActiveType activeType = ActiveType.Hide;

        [SerializeField, EnumToggleButtons, DisableInPlayMode]
        private QualityLevel targetLevel = QualityLevel.Medium;

        private string message;

        internal void OnEnable()
        {
            var i = targetLevel switch
            {
                QualityLevel.Low => 0,
                QualityLevel.Medium => 1,
                QualityLevel.High => 2,
                _ => -1
            };
            if (activeType == ActiveType.Display)
                if (QualitySettings.GetQualityLevel() > i) gameObject.SetActive(true);
                else gameObject.SetActive(false);
            if (activeType == ActiveType.Hide)
                if (QualitySettings.GetQualityLevel() < i) gameObject.SetActive(false);
                else gameObject.SetActive(true);
        }

        private void OnValidate()
        {
            string text1;
            string text2;

            if (activeType == ActiveType.Display)
            {
                text1 = "��ʾ";
                text2 = "����";
            }
            else
            {
                text1 = "����";
                text2 = "����";
            }

            var text3 = targetLevel switch
            {
                QualityLevel.Low => "�ͻ���",
                QualityLevel.Medium => "�л���",
                QualityLevel.High => "�߻���",
                _ => "-"
            };

            message = "������" + text2 + text3 + "ʱ" + text1;
        }
    }
}