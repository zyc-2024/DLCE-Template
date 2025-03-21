// To user: Sorry,the code is so long!

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainLine : MonoBehaviour
{
    public GameObject tail;
    public GameObject die_effect;
    public AudioClip die_sound;
    public AudioSource start_audio;
    public GameObject jump_effect;
    [HideInInspector] public MainLine Other_Line;
    public GameObject Camera;
   [HideInInspector] public Material LineMaterial,DiamondMaterial;

    //[Header("这个关卡的线和钻石的颜色")]
    public Color ThisLevelLineColor = new Color(0, 1, 0, 1);
    public Color ThisLevelDiamondColor = new Color(0.3921569f, 0.7843137f, 1, 1);

    //[Header("线开始时要播放和线死亡时要停止的帧动画")]
    public Animator[] Start_AniObj;
    public Animator[] Die_animator;

    private int HitCount = 0;
    [HideInInspector] public bool isFall = true;
    public bool ifStart { get; private set; }

    public Vector3 NowForward;
    public Vector3 Block_Foward1;
    public Vector3 Block_Foward2;

    public float Speed;
    [HideInInspector] public float Jump_Effect_Deviation = -0.9f;
    public int NowPercentage = 0;

    [HideInInspector]public Queue<GameObject> Way = new Queue<GameObject>();
	private GameObject Jump_Effect_temp;

    public bool No_Clip = false;
    [HideInInspector] public bool Is_Stop = false;
    [HideInInspector] public bool fly = false;
    [HideInInspector] public bool start = false;
    [HideInInspector]public bool Can_Tail = true;
    [HideInInspector] public bool Over = true;
    public bool canuse = true;

    [Header("如果不做关卡内UI的话那下面三个不用选")]
    public Text PercentageOnUI;
    public GameObject OverTab;
    public GameObject ContinueTab;


    [Header("如果不做选关UI的话那下面三个不用填写")]
    public string PerSet;
    public string DiaSet;
    public string CroSet;

    [HideInInspector]public GameObject LineBody;
	[HideInInspector]public GameObject LastLineBody;
	[HideInInspector]public bool Pause = false;
	[HideInInspector]public bool keydown = false;
	[HideInInspector]public GameObject Crown1;
	[HideInInspector]public GameObject Crown2;
	[HideInInspector]public GameObject Crown3;
    [HideInInspector] public int PickDiamondCount = 0;
    [HideInInspector] public int PickCrown = 0;
    [HideInInspector] public int PC = 0;
    [HideInInspector]public bool Win = false;

	void Start ()
    {
        LineBody = tail;
        for (int i = 0; i < Start_AniObj.Length; i++)
        {
            Start_AniObj[i].enabled = false;
        }
        ifStart = false;
		NowForward = Block_Foward1;
		this.transform.localEulerAngles = NowForward;
		if (PercentageOnUI)
		{
			InvokeRepeating ("setPercentage", 0f, start_audio.GetComponent<AudioSource> ().clip.length / 100);
		}
        LineMaterial.color = ThisLevelLineColor;
        DiamondMaterial.color = ThisLevelDiamondColor;
        Other_Line = this.gameObject.GetComponent<MainLine>();
	}
	public void GameOver(bool win, bool stop)
    {
        if (win != true)
            {
            for(int i=0;i<Die_animator.Length;i++)
            {
                Die_animator[i].speed = 0;
            }
			Over = true;
			Destroy(Instantiate (die_effect, this.transform.position, this.transform.rotation), 10f);
			AudioSource.PlayClipAtPoint (die_sound, this.transform.position);
			if (start_audio)
            {
				float Time = start_audio.time;
                start_audio.Pause ();
                start_audio.time = Time;
			}
			if (Other_Line.Over == false)
            {
				Other_Line.GameOver (win, stop);
			}
			Is_Stop = stop;
			this.GetComponent<Rigidbody> ().isKinematic = stop;
			if (Crown1 != null)
            {
				if (ContinueTab != null)
                {
					ContinueTab.GetComponent<CanvasGroup> ().alpha = 0;
					ContinueTab.SetActive (true);
					DOTween.To (() => ContinueTab.GetComponent<CanvasGroup> ().alpha, x => ContinueTab.GetComponent<CanvasGroup> ().alpha = x, 1, 2);
				}
			}
            else
            {
				if (OverTab != null)
                {
					OverTab.GetComponent<CanvasGroup> ().alpha = 0;
					OverTab.SetActive (true);
					DOTween.To (() => OverTab.GetComponent<CanvasGroup> ().alpha, x => OverTab.GetComponent<CanvasGroup> ().alpha = x, 1, 2);
					OverTab.GetComponent<OverMenu> ().GameOver (false, NowPercentage, PickDiamondCount, GetPickCrownCount ());
				}
			}
		}
        else
        {
            if (PlayerPrefs.GetInt(CroSet) < PickCrown)
            {
                PlayerPrefs.SetInt(CroSet, PickCrown);
            }
            if (PlayerPrefs.GetInt(PerSet) < NowPercentage)
            {
                PlayerPrefs.SetInt(PerSet, NowPercentage);
            }
            if (PlayerPrefs.GetInt(DiaSet) < PickDiamondCount)
            {
                PlayerPrefs.SetInt(DiaSet, PickDiamondCount);
            }
            Win = true;
			Is_Stop = true;
			OverTab.GetComponent<CanvasGroup> ().alpha = 0;
			OverTab.SetActive (true);
			DOTween.To (() => OverTab.GetComponent<CanvasGroup> ().alpha, x => OverTab.GetComponent<CanvasGroup> ().alpha = x, 1, 2);
			OverTab.GetComponent<OverMenu> ().GameOver (true, NowPercentage, PickDiamondCount, GetPickCrownCount());
        }
	}
	void Update ()
    {
        if (!Over && !Is_Stop && start)
        {
            if (isGrounded() == true || fly == true)
            {
                if (canuse == true)
                {
                    if ((Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space)) && keydown == false)
                    {
                        keydown = true;
                        TurnBlock();
                    }
                    else if (!(Input.GetMouseButton(0) && keydown == true))
                    {
                        keydown = false;
                    }
                }
            }
            else
            {
                if (LineBody != null)
                {
                    Way.Enqueue(LineBody);
                    LineBody = null;
                }
            }
            this.transform.Translate(Vector3.forward * Speed * 5f * Time.deltaTime, Space.Self);
            if (LineBody != null && !TransporterWith.OK)
            {
                LineBody.transform.localScale = new Vector3(LineBody.transform.localScale.x, LineBody.transform.localScale.y, LineBody.transform.localScale.z + 5f * Speed * Time.deltaTime);
                LineBody.transform.Translate(Vector3.forward * 2.5f * Speed * Time.deltaTime, Space.Self);
            }
        }
        if (start == false)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 0; i < Start_AniObj.Length; i++)
                {
                    Start_AniObj[i].enabled = true;
                }
                keydown = true;
                start = true;
                Over = false;
                Is_Stop = false;
                Can_Tail = true;
                CreateLineBody();
                if (start_audio)
                {
                    start_audio.Play();
                }
            }
        }
        if(Over && Is_Stop)
        {
            if (PlayerPrefs.GetInt(PerSet) < NowPercentage)
            {
                PlayerPrefs.SetInt(PerSet, NowPercentage);
            }
            if (PlayerPrefs.GetInt(DiaSet) < PickDiamondCount)
            {
                PlayerPrefs.SetInt(DiaSet, PickDiamondCount);
            }
            PickCrown = 0;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (Over == false)
        {
            if (No_Clip == true)
            {
                HitCount++;
                if (jump_effect && isFall == true && start)
                {
                    Jump_Effect_temp = Instantiate(jump_effect, new Vector3(this.transform.position.x, this.transform.position.y - this.transform.lossyScale.y / 2 + 0.2f, this.transform.position.z), this.transform.rotation);
                    Destroy(Jump_Effect_temp, 0.8f);
                }
            }
            else
            {
                if (collision.collider.tag == "Wall")
                {
                    GameOver(false, true);
                }
                else
                {
                    if (jump_effect && isFall == true && start)
                    {
                        Jump_Effect_temp = Instantiate(jump_effect, new Vector3(this.transform.position.x, this.transform.position.y - this.transform.lossyScale.y / 2 + 0.2f, this.transform.position.z), this.transform.rotation);
                        Destroy(Jump_Effect_temp, 0.8f);
                    }
                }
            }
            if (start)
            {
                CreateLineBody();
            }
        }
        isFall = false;
    }
	void OnCollisionExit(Collision collision)
    {
		isFall = !isGrounded ();
	}

	public void TurnBlock()
    {
		if (NowForward == Block_Foward1)
        {
			NowForward = Block_Foward2;
		}
        else
        {
			NowForward = Block_Foward1;
		}
		this.transform.eulerAngles = NowForward;
		CreateLineBody ();
	}

	public void CreateLineBody()
    {
        LineBody = Instantiate(tail, this.transform.position, this.transform.rotation);
        if (LineBody != null)
        {
            LastLineBody = LineBody;
        }
        if (LastLineBody != null)
        {
            Way.Enqueue(LastLineBody);
        }
        if (Can_Tail)
        {
            LineBody.SetActive(true);
        }
        else
        {
            LineBody.SetActive(false);
        }
	}

    public void setPercentage()
    {
        PercentageOnUI.GetComponent<Text>().text = NowPercentage + "%";
        if (start && Over != true && NowPercentage < 100)
        {
            NowPercentage = NowPercentage + 1;
        }
        if (start && Win)
        {
            NowPercentage = 100;
        }
    }

	public bool isGrounded ()
    {
		return Physics.Raycast (this.transform.position, Vector3.down, this.transform.localScale.y / 2 + 0.1f);
	}

	public int GetPickCrownCount ()
    {
		int PickCrownCount = 0;
		if (Crown3 != null && Crown3.GetComponent<Crown> ().used == false)
        {
			PickCrownCount++;
		}
		if (Crown2 != null && Crown2.GetComponent<Crown> ().used == false)
        {
			PickCrownCount++;
		}
		if (Crown1 != null && Crown1.GetComponent<Crown> ().used == false)
        {
			PickCrownCount++;
		}
		return PickCrownCount;
	}
	
	public void GoToEaster ()
    {
		if (Crown3 != null)
        {
			Crown3.GetComponent<Crown> ().Easter ();
		}
        else if (Crown2 != null)
        {
			Crown2.GetComponent<Crown> ().Easter ();
		}
        else
        {
			Crown1.GetComponent<Crown> ().Easter ();
		}
	}
}