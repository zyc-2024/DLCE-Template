using UnityEngine;
using System.Collections;
 
public class CameraShake : MonoBehaviour {
 
	public static bool startShake = false;  //camera是否开始震动
	public static float seconds  = 0f;    //震动持续秒数
	public static bool started  = false;    //是否已经开始震动
	public static float quake = 0.2f;       //震动系数
 
	private Vector3 camPOS;  //camera的起始位置
	private Vector3 deltaPos = Vector3.zero;
 
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
	  if(startShake)
		{
			transform.localPosition -= deltaPos;
            deltaPos = Random.insideUnitSphere * (quake/5);
            transform.localPosition += deltaPos;
		}
 
		if(started)
		{
			StartCoroutine(WaitForSecond(seconds));
			started = false;
		}
	}
	/// <summary>
	/// 外部调用控制camera震动
	/// </summary>
	/// <param name="a">震动时间</param>
	/// <param name="b">震动幅度</param>
	public static void ShakeFor(float a ,float b)
	{
//		if (startShake)
//			return;
		seconds = a;
		started = true;
		startShake = true;
		quake = b;
	}
	IEnumerator WaitForSecond(float a)
	{
//		camPOS = transform.position;
 
		yield return new WaitForSeconds (a);
		startShake = false;
	}
 
}
