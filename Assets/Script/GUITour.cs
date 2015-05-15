using UnityEngine;
using System.Collections;

public class GUITour : MonoBehaviour {

	public GUIStyle exitStyle;
	public GUIStyle cameraStyle;
	public Texture swipe;
	private bool switchSwipe = false;
	
	private float SizeFactor;

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		SizeFactor = GUIUtilities.SizeFactor;
		StartCoroutine (swipeGo());

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private IEnumerator photoGo ()
	{
		
		
		string namePhoto  = "Mirabilar" + System.DateTime.Now.Day+System.DateTime.Now.Month + System.DateTime.Now.Year + System.DateTime.Now.Hour+ System.DateTime.Now.Minute + System.DateTime.Now.Second+".png";
		
		//String namePhoto  = "Mirabilar.png" ;
		//Application.CaptureScreenshot("/storage/emulated/0/DCIM/Prova.png");
		//Application.CaptureScreenshot(name +".png");
		Application.CaptureScreenshot(namePhoto);
		yield return new WaitForSeconds(3f);


			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaClass refreshGallery = new AndroidJavaClass ("com.mirabilar.refreshgallery.GalleryRefresh");
			
			AndroidJavaObject joString = new AndroidJavaObject("java.lang.String",Application.persistentDataPath+"/"+namePhoto); 
			refreshGallery.CallStatic("NewRefreshG",new object[2]{jo,joString});				
			
			Application.OpenURL (Application.persistentDataPath+"/"+namePhoto);

		
		
		yield return 0;
	}

	private IEnumerator swipeGo ()
	{
		switchSwipe = true;
		yield return new WaitForSeconds(0.5f);
		switchSwipe = false;
		yield return new WaitForSeconds(0.3f);
		switchSwipe = true;
		yield return new WaitForSeconds(0.5f);
		switchSwipe = false;
		yield return new WaitForSeconds(0.3f);
		switchSwipe = true;
		yield return new WaitForSeconds(0.5f);
		switchSwipe = false;


		yield return 0;
	}

	void OnGUI()
	{
		if (GUI.Button (new Rect (60 * SizeFactor,
		                          60 * SizeFactor,
		                          80 * SizeFactor,
		                          80 * SizeFactor), "", exitStyle)) {
			//Debug.Log("Clicked the button!");
			Application.LoadLevel("Vuoto");
		}

		if (GUI.Button (new Rect (Screen.width - 150 * SizeFactor,
		                          Screen.height / 2 - 50 * SizeFactor,
		                          100 * SizeFactor,
		                          100 * SizeFactor), "", cameraStyle)) {
			//Debug.Log("Clicked the button!");
			StartCoroutine(photoGo());
		}

		if (switchSwipe && !Input.gyro.enabled)
		{
			GUI.DrawTexture(new Rect(Screen.width/2-40*SizeFactor,
			                         Screen.height/2-100*SizeFactor,
			                         80*SizeFactor,200*SizeFactor),swipe);
		}
	}
}
