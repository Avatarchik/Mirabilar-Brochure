using UnityEngine;
using System.Collections;

public class FocusVuforia : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		Renderer [] rend= coll.gameObject.GetComponentsInChildren<Renderer>();
//		for (int i= 0; i<rend.Length; i++){
//			rend[i].material.color=Color.yellow;//cambia colore al laser intercettato
//		}
	}

	void Awake ()
	{
	
	}

	void OnApplicationPause()
	{

	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.touchCount > 0)
			//CameraDevice.Instance.SetFocusMode (CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
	}
}
