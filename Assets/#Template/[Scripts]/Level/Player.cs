using DancingLineFanmade.Trigger;
using DancingLineFanmade.UI;
using DG.Tweening;
using MaxLine2.UI;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace DancingLineFanmade.Level
{
    [DisallowMultipleComponent, RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }
        public static Rigidbody Rigidbody { get; private set; }

        private GameObject tailPrefab;
        private GameObject cubesPrefab;
        private GameObject dustParticle;
        private GameObject uiPrefab;
        private GameObject startPrefab;
        private GameObject loadingPrefab;

        [Title("Data")]
        [Required("必须选填关卡数据文件")] public LevelData levelData;

        [Title("Settings")]
        public new Camera camera;
        public new Light light;
        public Vector3 startPosition = Vector3.zero;
        public Vector3 firstDirection = new Vector3(0, 90, 0);
        public Vector3 secondDirection = Vector3.zero;
        public int poolSize = 100;
        public Color debugTextColor = Color.black;
        public bool allowTurn = true;
        public bool noDeath = false;

        internal float speed { get; set; }
        internal AudioSource track { get; private set; }
        internal int blockCount { get; set; }
        internal int percentage { get; set; }
        internal UnityEvent onTurn { get; private set; }

        private Vector3 tailPosition;
        private Transform tail;
        private ObjectPool<Transform> tailPool = new ObjectPool<Transform>();
        private StartPage startPage;
        private bool debug = true;
        private bool loading = false;

        private float TailDistance
        {
            get => new Vector2(tailPosition.x - transform.position.x, tailPosition.z - transform.position.z).magnitude;
        }

        private bool previousFrameIsGrounded;
        private float groundedRayDistance = 0.05f;
        private ValueTuple<Vector3, Ray>[] groundedTestRays;
        private RaycastHit[] groundedTestResults = new RaycastHit[1];
        public bool Falling
        {
            get
            {
                for (int i = 0; i < groundedTestRays.Length; i++)
                {
                    groundedTestRays[i].Item2.origin = transform.position + transform.localRotation * groundedTestRays[i].Item1;
                    if (Physics.RaycastNonAlloc(groundedTestRays[i].Item2, groundedTestResults, groundedRayDistance + 0.1f, -257, QueryTriggerInteraction.Ignore) > 0)
                        return false;
                }
                return true;
            }
        }

        private void Awake()
        {
            if (!levelData)
            {
                Debug.LogError("无法获取关卡信息，请确保关卡数据文件（Level Data）填选正确且不为空");
                LevelManager.DialogBox("警告", "无法获取关卡信息，请确保关卡数据文件（Level Data）填选正确且不为空", "确定", true);
                return;
            }
            DOTween.Clear();
            Instance = this;
            Rigidbody = GetComponent<Rigidbody>();
            loading = false;
            onTurn = new UnityEvent();

            BoxCollider boxCollider = GetComponent<BoxCollider>();
            groundedTestRays = new ValueTuple<Vector3, Ray>[]
            {
                new ValueTuple<Vector3, Ray>(boxCollider.center - new Vector3(boxCollider.size.x / 2f, boxCollider.size.y / 2f - 0.1f, boxCollider.size.z / 2f), new Ray(Vector3.zero, transform.localRotation * Vector3.down)),
                new ValueTuple<Vector3, Ray>(boxCollider.center - new Vector3(boxCollider.size.x / -2f, boxCollider.size.y / 2f - 0.1f, boxCollider.size.z / 2f), new Ray(Vector3.zero, transform.localRotation * Vector3.down)),
                new ValueTuple<Vector3, Ray>(boxCollider.center - new Vector3(boxCollider.size.x / 2f, boxCollider.size.y / 2f - 0.1f, boxCollider.size.z / -2f), new Ray(Vector3.zero, transform.localRotation * Vector3.down)),
                new ValueTuple<Vector3, Ray>(boxCollider.center - new Vector3(boxCollider.size.x / -2f, boxCollider.size.y / 2f - 0.1f, boxCollider.size.z / -2f), new Ray(Vector3.zero, transform.localRotation * Vector3.down))
            };
            previousFrameIsGrounded = Falling;

            LoadingPage.Instance?.Fade(0f, 0.4f);
        }

        private void Start()
        {
            levelData.SetLevelData();
            firstDirection = firstDirection.Convert();
            secondDirection = secondDirection.Convert();
            tailPool.Size = poolSize;
            LevelManager.InitPlayerPosition(this, startPosition, false);
            tailPrefab = Resources.Load<GameObject>("Prefabs/Tail");
            cubesPrefab = Resources.Load<GameObject>("Prefabs/Remain");
            dustParticle = Resources.Load<GameObject>("Prefabs/Dust");
            uiPrefab = Resources.Load<GameObject>("Prefabs/LevelUI");
            startPrefab = Resources.Load<GameObject>("Prefabs/StartPage");
            loadingPrefab = Resources.Load<GameObject>("Prefabs/LoadingPage");

            transform.eulerAngles = firstDirection;
            LevelManager.GameState = GameStatus.Waiting;
            Instantiate(uiPrefab);
            startPage = Instantiate(startPrefab).GetComponent<StartPage>();
            if (!LoadingPage.Instance) DontDestroyOnLoad(Instantiate(loadingPrefab));
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.R) && !loading)
            {
                loading = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.C)) Debug.Log("Click time: " + track.time);
            if (Input.GetKeyDown(KeyCode.D)) debug = !debug;
#endif
            if (allowTurn && !EventSystem.current.IsPointerOverGameObject())
            {
                switch (LevelManager.GameState)
                {
                    case GameStatus.Waiting:
                        if (LevelManager.Clicked && !Falling)
                        {
                            LevelManager.GameState = GameStatus.Playing;
                            track = AudioManager.PlayClip(levelData.soundTrack, 1f);
                            CreateTail();
                            startPage.Hide();
                            startPage = null;
                        }
                        break;
                    case GameStatus.Playing: if (LevelManager.Clicked && !Falling) Turn(); break;
                }
            }
            if (LevelManager.GameState == GameStatus.Playing || LevelManager.GameState == GameStatus.Moving)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
                if (tail && !Falling)
                {
                    tail.position = (tailPosition + transform.position) * 0.5f;
                    tail.localScale = new Vector3(tail.localScale.x, tail.localScale.y, TailDistance);
                    tail.position = new Vector3(tail.position.x, transform.position.y, tail.position.z);
                    tail.LookAt(transform);
                }
                if (previousFrameIsGrounded != Falling)
                {
                    previousFrameIsGrounded = Falling;
                    if (Falling) tail = null;
                    else
                    {
                        CreateTail();
                        Destroy(Instantiate(dustParticle, new Vector3(transform.localPosition.x, transform.localPosition.y - transform.lossyScale.y * 0.5f + 0.2f, transform.localPosition.z), Quaternion.Euler(90f, 0f, 0f)), 2f);
                    }
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Obstacle") && !noDeath && LevelManager.GameState == GameStatus.Playing) LevelManager.PlayerDeath(this, DieReason.Hit, cubesPrefab, collision);
        }

        internal void Turn()
        {
            transform.eulerAngles = transform.eulerAngles == firstDirection ? secondDirection : firstDirection;
            CreateTail();
            onTurn.Invoke();
        }

        private void CreateTail()
        {
            Quaternion now = Quaternion.Euler(transform.localEulerAngles);
            float offset = tailPrefab.transform.localScale.z * 0.5f;

            if (tail)
            {
                Quaternion last = Quaternion.Euler(tail.transform.localEulerAngles);
                float angle = Quaternion.Angle(last, now);
                if (angle >= 0f && angle <= 90f) offset = 0.5f * Mathf.Tan(Mathf.PI / 180f * angle * 0.5f);
                else offset = -0.5f * Mathf.Tan(Mathf.PI / 180f * ((180f - angle) * 0.5f));
                Vector3 end = tailPosition + last * Vector3.forward * (TailDistance + offset);
                tail.position = (tailPosition + end) * 0.5f;
                tail.position = new Vector3(tail.position.x, transform.position.y, tail.position.z);
                tail.localScale = new Vector3(tail.localScale.x, tail.localScale.y, Vector3.Distance(tailPosition, end));
                tail.LookAt(transform.position);
            }
            tailPosition = transform.position + now * Vector3.back * Mathf.Abs(offset);
            if (!tailPool.Full)
            {
                tail = Instantiate(tailPrefab, transform.position, transform.rotation).transform;
                tailPool.Add(tail);
            }
            else
            {
                tail = tailPool.First();
                tailPool.MoveToLast(tailPool.First());
            }
        }

#if UNITY_EDITOR
        private void OnGUI()
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = debugTextColor;
            style.fontSize = 25;
            int progress = track ? (int)(track.time / track.clip.length * 100f) : 0;

            if (debug)
            {
                GUI.Label(new Rect(10, 10, 120, 50), "关卡进度：" + progress + "%", style);
                GUI.Label(new Rect(10, 40, 120, 50), "游戏状态：" + LevelManager.GameState, style);
                GUI.Label(new Rect(10, 70, 120, 50), "线的坐标：" + transform.localPosition, style);
                GUI.Label(new Rect(10, 100, 120, 50), "线的朝向：" + transform.localEulerAngles, style);
                GUI.Label(new Rect(10, 130, 120, 50), "已获取方块数量：" + blockCount + "/10", style);
                GUI.Label(new Rect(10, 160, 120, 50), "相机偏移：" + CameraFollower.Instance.rotator.localPosition, style);
                GUI.Label(new Rect(10, 190, 120, 50), "相机角度：" + CameraFollower.Instance.rotator.localEulerAngles, style);
                GUI.Label(new Rect(10, 220, 120, 50), "相机缩放：" + CameraFollower.Instance.scale.localScale, style);
                GUI.Label(new Rect(10, 250, 120, 50), "视场大小：" + camera.fieldOfView, style);
            }
        }
#endif
    }
}