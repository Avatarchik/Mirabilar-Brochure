using UnityEngine;
using System.Collections;

public class Focus : MonoBehaviour {

	bool autoFocusSet ;


	// Use this for initialization
	void Start () {
		autoFocusSet = false;
	}
	

	
	void Awake()
	{
			autoFocusSet = false;
	}

	void Update()
	{
		if (Time.time > 1f && !autoFocusSet)
		{
			autoFocusSet = enableAutoFocus();
		}
	}

	public static bool enableAutoFocus()
	{
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		
		AndroidJavaClass metaioSDKAndroid = new AndroidJavaClass("com.metaio.sdk.jni.IMetaioSDKAndroid"); 
		object[] args = {currentActivity};
		AndroidJavaObject camera = metaioSDKAndroid.CallStatic<AndroidJavaObject>("getCamera", args);
		
		if (camera != null)
		{
			AndroidJavaObject cameraParameters = camera.Call<AndroidJavaObject>("getParameters");
			object[] focusMode = {cameraParameters.GetStatic<string>("FOCUS_MODE_CONTINUOUS_PICTURE")};
			cameraParameters.Call("setFocusMode", focusMode);
			object[] newParameters = {cameraParameters};
			camera.Call("setParameters", newParameters);
			return true;
		}
		else
		{
			Debug.LogError("metaioSDK.enableAutoFocus: Camera not available");
			return false;
		}
	}

	void OnGUI() 
	{
		if (GUI.Button (new Rect (Screen.width - 100, Screen.height - 100, 100, 100), "Focus"))
		{
						autoFocusSet = false;
		}
	}
}
