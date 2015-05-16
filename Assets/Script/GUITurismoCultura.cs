using UnityEngine;
using System.Collections;
using System;

public class GUITurismoCultura : MonoBehaviour {

	public GUIStyle exitStyle;
	public GUIStyle infoStyle;
	public GUIStyle cameraStyle;
	public GUIStyle mappaStyle;
	//public GUIStyle neveStyle;
	//public GUIStyle pioggiaStyle;
	//public GUIStyle soleStyle;
	public Texture mappa;
	public Texture txSfondoNero;
	public Texture txMappa;
	public Texture txScreenShot;

	private int changeGUI= 0;
	//private  float buttonXSX ;
	private  float buttonXDX ;
	private  float buttonXSXExit;
	private  float buttonXDXInfo;
	//private  float butXSX ;
	private  float butXDX ;
	private  float butXSXExit;
	private  float butXDXInfo;

	private bool endMove = true;
	private bool freeUpdate = false;
	private bool infoGUI = false;

	private GestCallBack callBack;

	private TouchIcon goTouchIcon;

	private string sDebug = "";

	float SizeFactor;

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		SizeFactor = GUIUtilities.SizeFactor;
		callBack = gameObject.GetComponent<GestCallBack> ();

		goTouchIcon = gameObject.GetComponent<TouchIcon> ();

		//buttonXSX = 50 * SizeFactor;
		buttonXDX = Screen.width - 130 * SizeFactor;
		buttonXDXInfo = Screen.width - 120 * SizeFactor;
		buttonXSXExit = 40 * SizeFactor;

		//butXSX = -50 * SizeFactor;
		butXDX = Screen.width ;
		butXDXInfo = Screen.width ;
		butXSXExit = -60 * SizeFactor;
	}
	
	// Update is called once per frame
	void Update () {
		if (endMove && callBack.cos == 1) 
		{
			endMove = false;
			StartCoroutine ( moveButtonDX () );
			//StartCoroutine ( moveButtonSX () );
			StartCoroutine ( moveButtonSXExit () );
			StartCoroutine ( moveButtonDXInfo () );
			freeUpdate = true;
		}

		if (callBack.cos == 0 && freeUpdate) 
		{
			endMove = true;
			//butXSX = -100 * SizeFactor;
			butXDX = Screen.width ;
			butXDXInfo = Screen.width ;
			butXSXExit = -120 * SizeFactor;
			freeUpdate = false;
		}

	}

	private IEnumerator moveButtonDX () {


		  while (butXDX>buttonXDX && !endMove ) {
			butXDX-=10;
			yield return new WaitForSeconds(0.0001f);
		}

		
		yield return 0;
	}

//	private IEnumerator moveButtonSX () {
//		
//
//		while (butXSX<buttonXSX && !endMove) {
//			butXSX+=10;
//			yield return new WaitForSeconds(0.0001f);
//		}
//		
//		yield return 0;
//	}

	private IEnumerator moveButtonSXExit () {
		
		
		while (butXSXExit<buttonXSXExit && !endMove) {
			butXSXExit+=11;
			yield return new WaitForSeconds(0.0001f);
		}
		
		yield return 0;
	}

	private IEnumerator moveButtonDXInfo () {
		
		
		while (butXDXInfo>buttonXDXInfo && !endMove ) {
			butXDXInfo-=11;
			yield return new WaitForSeconds(0.0001f);
		}
		
		
		yield return 0;
	}

	#if UNITY_ANDROID

	class ScanCallback : AndroidJavaProxy
	{
		public ScanCallback() : base("android.media.MediaScannerConnection$OnScanCompletedListener") { }
		
		public void onScanCompleted(AndroidJavaObject path, AndroidJavaObject uri)
		{
			
		}
	}
	#endif

	private IEnumerator infoGo () {
		
		
		infoGUI = true;
		yield return new WaitForSeconds(3f);
		infoGUI = false;
		
		
		yield return 0;
	}

	private IEnumerator photoGo ()
	{
		#if UNITY_ANDROID
			
		String namePhoto  = "Mirabilar" + System.DateTime.Now.Day+System.DateTime.Now.Month + System.DateTime.Now.Year + System.DateTime.Now.Hour+ System.DateTime.Now.Minute + System.DateTime.Now.Second+".png";
			
			//String namePhoto  = "Mirabilar.png" ;
			//Application.CaptureScreenshot("/storage/emulated/0/DCIM/Prova.png");
			//Application.CaptureScreenshot(name +".png");
			Application.CaptureScreenshot(namePhoto);
			yield return new WaitForSeconds(3f);
		try 
		{
			//Application.OpenURL (Application.persistentDataPath + "/Prova.png");
			
			//								System.IO.File.Copy(Application.persistentDataPath + "/Prova.png", "/storage/emulated/0/DCIM/100ANDRO/Prova.png");
			//								System.IO.File.Copy(Application.persistentDataPath + "/Prova.png", "/storage/emulated/0/DCIM/CAMERA/Prova.png");			
			
			//								AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			//								AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			//								AndroidJavaClass classUri = new AndroidJavaClass("android.net.Uri");
			//								AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", new object[2]{"android.intent.action.MEDIA_MOUNTED", classUri.CallStatic<AndroidJavaObject>("parse", "file://" +  Application.persistentDataPath )});
			//								objActivity.Call ("sendBroadcast", objIntent);
			//MEDIASCANNERSCANFILE
			//								AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			//								AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			//								AndroidJavaClass classUri = new AndroidJavaClass("android.net.Uri");
			//								AndroidJavaObject classFile = new AndroidJavaObject ("java.io.File",Application.persistentDataPath + "/" + name+".png");								
			////								AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", new object[2]{"android.intent.action.MEDIA_SCANNER_SCAN_FILE", classUri.CallStatic<AndroidJavaObject>("fromFile", classFile )}); 
			//								AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", "android.intent.action.MEDIA_SCANNER_SCAN_FILE");
			//
			//								AndroidJavaObject objUri = classUri.CallStatic<AndroidJavaObject>("fromFile", classFile );
			//								objIntent.Call<AndroidJavaObject> ("setData",objUri);
			//
			//								objActivity.Call ("sendBroadcast", objIntent); 
			
			//								AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			//									AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			//									
			//									AndroidJavaObject path = new AndroidJavaObject("java.lang.String", "file://" + Application.persistentDataPath + "/" + name+".png");
			//									string [] pathArray = new string[] {Application.persistentDataPath};
			//									IntPtr pathArrayConvert = UnityEngine.AndroidJNIHelper.ConvertToJNIArray(pathArray);
			//									UnityEngine.jvalue [] arrayPathJValue  = new UnityEngine.jvalue [1];
			//									object[] objParams = new object[1];
			//									string[] myStrings = new string[1];
			//									myStrings [0] = Application.persistentDataPath + "/" + name+".png";
			//									objParams [0] = myStrings;
			//									arrayPathJValue[0].l = pathArrayConvert;
			//									//
			//									IntPtr clazz = UnityEngine.AndroidJNI.FindClass("java.lang.String");
			//									IntPtr methId = UnityEngine.AndroidJNI.GetMethodID(clazz, "<init>", "(Ljava/lang/String;)V");
			//									IntPtr strId = UnityEngine.AndroidJNI.NewStringUTF("Hello");
			//									IntPtr strObj = UnityEngine.AndroidJNI.NewObject(clazz, methId, new UnityEngine.jvalue[] { new UnityEngine.jvalue() { l = strId } });
			//									IntPtr objArray = UnityEngine.AndroidJNI.NewObjectArray(1, clazz, strObj);
			//									AndroidJavaObject [] prova = new AndroidJavaObject [1] ;
			//									prova [0] = new AndroidJavaObject("java.lang.String", Application.persistentDataPath + "/" + name+".png");
			//									//
			//									new AndroidJavaClass ("android.media.MediaScannerConnection").CallStatic("scanFile",objActivity,prova,null,null);
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaClass refreshGallery = new AndroidJavaClass ("com.mirabilar.refreshgallery.GalleryRefresh");
			
			AndroidJavaObject joString = new AndroidJavaObject("java.lang.String",Application.persistentDataPath+"/"+namePhoto); 
			refreshGallery.CallStatic("NewRefreshG",new object[2]{jo,joString});				

			Application.OpenURL (Application.persistentDataPath+"/"+namePhoto);
			
		}
		catch (System.Exception e)
		{
			sDebug = e.Message;
		}

		#endif

		yield return 0;
	}

	void OnGUI() {

//		if(GUIUtilities.ButtonWithText(new Rect(
//			Screen.width - 200*SizeFactor,
//			0,
//			100*SizeFactor,
//			100*SizeFactor),"",null,exitStyle) ||	Input.GetKeyDown(KeyCode.Escape)) 
//		{
//			
//			
//		}



		if (callBack.cos == 1 && goTouchIcon.changeGui == 0) 
		{

			if (infoGUI)
			{
				GUI.DrawTexture (new Rect (0,0,Screen.width,Screen.height), txSfondoNero);
				
				GUI.DrawTexture(new Rect (buttonXDX-270*SizeFactor,
				                          270*SizeFactor,
				                          250*SizeFactor,
				                          40 * SizeFactor),txMappa);
				
				GUI.DrawTexture(new Rect (buttonXDX-220*SizeFactor,
				                          390*SizeFactor,
				                          200*SizeFactor,
				                          40 * SizeFactor),txScreenShot);
			}

//						if (GUI.Button (new Rect (butXSXExit,
//		                          			20 * SizeFactor,
//		                          			80 * SizeFactor,
//		                          			80 * SizeFactor), "", exitStyle)) {
//								//Debug.Log("Clicked the button!");
//						}

//						if (GUI.Button (new Rect (butXSX,
//		                          180 * SizeFactor,
//		                          100 * SizeFactor,
//		                          100 * SizeFactor), "", soleStyle)) {
//								//Debug.Log("Clicked the button!");
//						}
//
//						if (GUI.Button (new Rect (butXSX,
//		                          300 * SizeFactor,
//		                          100 * SizeFactor,
//		                          100 * SizeFactor), "", pioggiaStyle)) {
//								//Debug.Log("Clicked the button!");
//						}
//
//						if (GUI.Button (new Rect (butXSX,
//		                          420 * SizeFactor,
//		                          100 * SizeFactor,
//		                          100 * SizeFactor), "", neveStyle)) {
//								//Debug.Log("Clicked the button!");
//						}

						if (GUI.Button (new Rect (butXDXInfo,
		                          20 * SizeFactor,
		                          80 * SizeFactor,
		                          80 * SizeFactor), "", infoStyle)) {
								
							if (!infoGUI)
								StartCoroutine(infoGo());
								//Debug.Log("Clicked the button!");
//								try {
////									AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
////									AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
////									AndroidJavaClass classMediaScanner = new AndroidJavaClass ("android.media.MediaScannerConnection");
////									AndroidJavaObject path = new AndroidJavaObject("java.lang.String", "file://" +  Application.persistentDataPath);
////									string [] pathArray = {"file://" +  Application.persistentDataPath};
////									IntPtr pathArrayConvert = AndroidJNIHelper.ConvertToJNIArray(pathArray);
////									classMediaScanner.CallStatic("scanFile",new object[4]{objActivity,null,null,new ScanCallback()});
//
//
//									AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//									AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
//									
//									AndroidJavaObject path = new AndroidJavaObject("java.lang.String", "file://" +  Application.persistentDataPath);
//									string [] pathArray = {"file://" +  Application.persistentDataPath};
//									IntPtr pathArrayConvert = AndroidJNIHelper.ConvertToJNIArray(pathArray);
//									
//									objActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
//					                                                       {
//									new AndroidJavaClass ("android.media.MediaScannerConnection").CallStatic("scanFile",new object[4]{objActivity,pathArrayConvert,null,new ScanCallback()});
//									}));
//								}
//								catch (System.Exception e)
//								{
//									sDebug = e.Message;
//								}
						}

						if (GUI.Button (new Rect (butXDX,
		                          240 * SizeFactor,
		                          100 * SizeFactor,
		                          100 * SizeFactor), "", mappaStyle)) 
						{
							changeGUI = 1;
							goTouchIcon.changeGui = 4;
						}

						if (GUI.Button (new Rect (butXDX,
		                          360 * SizeFactor,
		                          100 * SizeFactor,
		                          100 * SizeFactor), "", cameraStyle)) {
								//Debug.Log("Clicked the button!");
							StartCoroutine(photoGo());
						}

						
				}
		//GUI.Label (new Rect (50, 50, 300, 300), "" + callBack.cos + " "+ Application.dataPath + "  "+ Application.persistentDataPath + " " + sDebug);

		if (changeGUI == 1 && goTouchIcon.changeGui == 0 || goTouchIcon.changeGui == 4) 
		{
			mappaGUI ();
		}



		}

	void mappaGUI () 
	{
		GUI.DrawTexture(new Rect(10 * SizeFactor,
		                         Screen.height / 2 - 220 * SizeFactor,
		                         Screen.width  - 20 * SizeFactor,
		                         440 * SizeFactor), goTouchIcon.rectBack3);
		
		if (GUI.Button(new Rect(20 * SizeFactor,
		                        110 * SizeFactor,
		                        50 * SizeFactor,
		                        50 * SizeFactor), "", goTouchIcon.exitStyle))
		{
			changeGUI = 0;
			goTouchIcon.changeGui = 0;
		}
		
		GUI.DrawTexture (new Rect (Screen.width / 2 - 500 * SizeFactor, 
		                           Screen.height / 2 - 200 * SizeFactor,
		                           1000 * SizeFactor, 
		                           400 * SizeFactor), mappa);
	}

}
