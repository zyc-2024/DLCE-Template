#if UNITY_EDITOR
using UnityEditor;
#endif
using DG.Tweening;
using DancingLineFanmade.Trigger;
using UnityEngine;
using DancingLineFanmade.UI;

namespace DancingLineFanmade.Level
{
    public enum GameStatus
    {
        Waiting,
        Playing,
        Moving,
        Died,
        Completed
    }

    public enum Direction
    {
        First,
        Second
    }

    public static class LevelManager
    {
        private static GameStatus gameState = GameStatus.Waiting;
        public static GameStatus GameState
        {
            get => gameState;
            set => gameState = value;
        }

        public static bool Clicked
        {
            get => Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return);
        }

        public static Vector3 PlayerPosition
        {
            get => Player.Instance.transform.position;
            set => Player.Instance.transform.position = value;
        }

        public static Vector3 CameraPosition
        {
            get => CameraFollower.Instance.transform.position;
            set => CameraFollower.Instance.transform.position = value;
        }

        private static GameObject dieCubes { get; set; }
        private static Tween trackFadeOut { get; set; }

        public static Vector3 Convert(this Vector3 vector3, bool positive = true)
        {
            int x = (int)Mathf.Abs(Mathf.Floor(vector3.x / 360));
            int y = (int)Mathf.Abs(Mathf.Floor(vector3.y / 360));
            int z = (int)Mathf.Abs(Mathf.Floor(vector3.z / 360));
            return positive ? new Vector3(vector3.x + 360 * x, vector3.y + 360 * y, vector3.z + 360 * z) : new Vector3(vector3.x - 360 * x, vector3.y - 360 * y, vector3.z - 360 * z);
        }

        public static void DialogBox(string title, string message, string ok, bool stopPlaying)
        {
#if UNITY_EDITOR
            if (EditorUtility.DisplayDialog(title, message, ok)) if (stopPlaying) EditorApplication.isPlaying = false;
#endif
        }

        public static void PlayerDeath(Player player, DieReason reason, GameObject cubes = null, Collision collision = null)
        {
            trackFadeOut = AudioManager.FadeOut(0f, 10f);
            CameraFollower.Instance.Kill();
            player.allowTurn = false;
            switch (reason)
            {
                case DieReason.Hit:
                    GameState = GameStatus.Died;
                    AudioManager.PlayClip(Resources.Load<AudioClip>("Audios/Hit"));
                    dieCubes = Object.Instantiate(cubes, player.transform.position, player.transform.rotation);
                    dieCubes?.GetComponent<PlayerCubes>().Play(collision);
                    break;
                case DieReason.Drowned:
                    GameState = GameStatus.Moving;
                    AudioManager.PlayClip(Resources.Load<AudioClip>("Audios/Drowned"));
                    break;
                case DieReason.Border:
                    GameState = GameStatus.Moving;
                    break;
            }
            player.percentage = (int)(player.track.time / player.track.clip.length * 100f);
            GameOver(false);
        }

        public static void GameOver(bool complete)
        {
            float percentage = complete ? 1f : AudioManager.Progress;

            if (GameState == GameStatus.Died || GameState == GameStatus.Completed || GameState == GameStatus.Moving)
                LevelUI.Instance.Invoke(percentage, Player.Instance.blockCount);
        }

        public static void InitPlayerPosition(Player player, Vector3 position, bool changeDirection, Direction direction = Direction.First)
        {
            PlayerPosition = position;
            CameraPosition = position;
            if (changeDirection)
            {
                switch (direction)
                {
                    case Direction.First:
                        player.transform.eulerAngles = player.firstDirection;
                        break;
                    case Direction.Second:
                        player.transform.eulerAngles = player.secondDirection;
                        break;
                }
            }
        }
    }
}