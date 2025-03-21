using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace DancingLineFanmade.Level
{
    [CreateAssetMenu(fileName = "Level Data")]
    public class LevelData : ScriptableObject
    {
        public string levelTitle = "БъЬт";
        public AudioClip soundTrack;
        [MinValue(0f)] public int speed = 12;
        [MinValue(0f)] public float timeScale = 1f;
        public Vector3 gravity = new Vector3(0, -10, 0);
        [TableList] public List<SingleColor> colors = new List<SingleColor>();

        internal void SetLevelData()
        {
            Player.Instance.speed = speed;
            Time.timeScale = timeScale;
            Physics.gravity = gravity;
            foreach (SingleColor s in colors) s.SetColor();
        }

        [Button("Get Colors", ButtonSizes.Large), HorizontalGroup("Color")]
        private void GetColors()
        {
            foreach (SingleColor s in colors) s.GetColor();
        }

        [Button("Set Colors", ButtonSizes.Large), HorizontalGroup("Color")]
        private void SetColors()
        {
            foreach (SingleColor s in colors) s.SetColor();
        }
    }

    [Serializable]
    public class SingleColor
    {
        public Material material;
        public Color color = Color.white;

        private List<Tween> tweens = new List<Tween>();

        internal void GetColor()
        {
            color = material.color;
        }

        internal void SetColor()
        {
            material.color = color;
        }

        internal void SetColor(float duration, Ease ease)
        {
            tweens.Add(material.DOColor(color, duration).SetEase(ease));
        }
    }

    [Serializable]
    public class SingleActive
    {
        public GameObject gameObject;
        public bool active = false;

        internal void SetActive()
        {
            gameObject.SetActive(active);
        }
    }

    [Serializable]
    public class FogSettings
    {
        public bool useFog = true;
        public Color fogColor = Color.white;
        public float start = 25f;
        public float end = 120f;

        private List<Tween> tweens = new List<Tween>();

        public FogSettings()
        {
        }

        public FogSettings(Color fogColor, float start, float end)
        {
            useFog = true;
            this.fogColor = fogColor;
            this.start = start;
            this.end = end;
        }

        internal FogSettings GetFog()
        {
            FogSettings fog = new FogSettings();
            fog.useFog = RenderSettings.fog;
            fog.fogColor = RenderSettings.fogColor;
            fog.start = RenderSettings.fogStartDistance;
            fog.end = RenderSettings.fogEndDistance;
            return fog;
        }

        internal void SetFog(Camera camera)
        {
            RenderSettings.fog = useFog;
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogStartDistance = start;
            RenderSettings.fogEndDistance = end;
            camera.backgroundColor = fogColor;
        }

        internal void SetFog(Camera camera, float duration, Ease ease)
        {
            RenderSettings.fog = useFog;
            tweens.Add(DOTween.To(() => RenderSettings.fogColor, x => RenderSettings.fogColor = x, fogColor, duration).SetEase(ease));
            tweens.Add(DOTween.To(() => RenderSettings.fogStartDistance, x => RenderSettings.fogStartDistance = x, start, duration).SetEase(ease));
            tweens.Add(DOTween.To(() => RenderSettings.fogEndDistance, x => RenderSettings.fogEndDistance = x, end, duration).SetEase(ease));
            tweens.Add(DOTween.To(() => camera.backgroundColor, x => camera.backgroundColor = x, fogColor, duration).SetEase(ease));
        }
    }

    [Serializable]
    public class LightSettings
    {
        public Vector3 rotation = Vector3.zero;
        public Color color = Color.white;
        public float intensity = 1f;
        [Range(0f, 1f)] public float shadowStrength = 0.8f;

        private List<Tween> tweens = new List<Tween>();

        internal void SetLight(Light light, float duration, Ease ease)
        {
            tweens.Add(light.transform.DORotate(rotation, duration).SetEase(ease));
            tweens.Add(light.DOColor(color, duration).SetEase(ease));
            tweens.Add(light.DOIntensity(intensity, duration).SetEase(ease));
            tweens.Add(light.DOShadowStrength(shadowStrength, shadowStrength).SetEase(ease));
        }
    }

    public enum EnvironmentLightingType
    {
        Skybox,
        Color,
        Gradient
    }

    [Serializable]
    public class AmbientSettings
    {
        [EnumToggleButtons] public EnvironmentLightingType lightingType = EnvironmentLightingType.Color;
        [Range(0f, 8f), ShowIf("@lightingType == EnvironmentLightingType.Skybox")] public float intensity = 1f;
        [ShowIf("@lightingType == EnvironmentLightingType.Color")] public Color ambientColor = new Color(0.67f, 0.67f, 0.67f, 1f);
        [ShowIf("@lightingType == EnvironmentLightingType.Gradient")] public Color skyColor = new Color(0.67f, 0.67f, 0.67f, 1f);
        [ShowIf("@lightingType == EnvironmentLightingType.Gradient")] public Color equatorColor = new Color(0.114f, 0.125f, 0.133f, 1f);
        [ShowIf("@lightingType == EnvironmentLightingType.Gradient")] public Color groundColor = new Color(0.047f, 0.043f, 0.035f, 1f);

        private List<Tween> tweens = new List<Tween>();

        internal AmbientMode GetAmbientMode(EnvironmentLightingType type)
        {
            switch (type)
            {
                case EnvironmentLightingType.Skybox:
                    return AmbientMode.Skybox;
                case EnvironmentLightingType.Color:
                    return AmbientMode.Flat;
                case EnvironmentLightingType.Gradient:
                    return AmbientMode.Trilight;
                default:
                    return AmbientMode.Flat;
            }
        }

        internal EnvironmentLightingType GetEnvironmentLightingType(AmbientMode type)
        {
            switch (type)
            {
                case AmbientMode.Skybox:
                    return EnvironmentLightingType.Skybox;
                case AmbientMode.Flat:
                    return EnvironmentLightingType.Color;
                case AmbientMode.Trilight:
                    return EnvironmentLightingType.Gradient;
                default:
                    return EnvironmentLightingType.Color;
            }
        }

        internal void SetAmbient(float duration, Ease ease)
        {
            RenderSettings.ambientMode = GetAmbientMode(lightingType);
            switch (lightingType)
            {
                case EnvironmentLightingType.Skybox:
                    tweens.Add(DOTween.To(() => RenderSettings.ambientIntensity, x => RenderSettings.ambientIntensity = x, intensity, duration).SetEase(ease));
                    break;
                case EnvironmentLightingType.Color:
                    tweens.Add(DOTween.To(() => RenderSettings.ambientLight, x => RenderSettings.ambientLight = x, ambientColor, duration).SetEase(ease));
                    break;
                case EnvironmentLightingType.Gradient:
                    tweens.Add(DOTween.To(() => RenderSettings.ambientSkyColor, x => RenderSettings.ambientSkyColor = x, skyColor, duration).SetEase(ease));
                    tweens.Add(DOTween.To(() => RenderSettings.ambientEquatorColor, x => RenderSettings.ambientEquatorColor = x, equatorColor, duration).SetEase(ease));
                    tweens.Add(DOTween.To(() => RenderSettings.ambientGroundColor, x => RenderSettings.ambientGroundColor = x, groundColor, duration).SetEase(ease));
                    break;
            }
        }
    }

    public enum Percent
    {
        Ten,
        Twenty,
        Thirty,
        Forty,
        Fifty,
        Sixty,
        Seventy,
        Eighty,
        Ninety,
    }
}