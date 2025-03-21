//使用方法，把脚本放到Main Camera（相机）上
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour {

	public KeyCode PlayAgainKey;
	void Update () {
		if (Input.GetKeyDown (PlayAgainKey)){
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
}
