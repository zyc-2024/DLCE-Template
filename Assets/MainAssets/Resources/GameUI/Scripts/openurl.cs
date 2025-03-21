using UnityEngine;
using System.Collections;

public class openurl : MonoBehaviour {

public string url = "";

public void click()
{
Application.OpenURL(url);
}
}