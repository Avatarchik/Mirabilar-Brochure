using UnityEngine;
using System.Collections;

public class GUIManualistica : MonoBehaviour {

	public GUIStyle exitStyle;
	public GUIStyle infoStyle;
	public GUIStyle cameraStyle;
	public GUIStyle downloadStyle;
	public GUIStyle neveStyle;
	public GUIStyle pioggiaStyle;
	public GUIStyle soleStyle;
	public GUIStyle esploso;

	public Texture txSfondoNero;
	public Texture txComeFunziona;
	public Texture txScaricaScheda;
	public Texture txScreenShot;

	private  float buttonXSX ;
	private  float buttonXDX ;
	private  float buttonXSXExit;
	private  float buttonXDXInfo;
	private  float butXSX ;
	private  float butXDX ;
	private  float butXSXExit;
	private  float butXDXInfo;
	private bool endMove = true;
	private bool freeUpdate = false;
	private bool infoGUI = false;
	private GestCallBack callBack;
	private GameObject scocca;
	private GameObject tagPala;

	float SizeFactor;

	// Use this for initialization
	void Start () {
		SizeFactor = GUIUtilities.SizeFactor;
		callBack = GameObject.Find("GUI").GetComponent<GestCallBack> ();

		buttonXSX = 50 * SizeFactor;
		buttonXDX = Screen.width - 150 * SizeFactor;
		buttonXDXInfo = Screen.width - 140 * SizeFactor;
		buttonXSXExit = 60 * SizeFactor;

		butXSX = -50 * SizeFactor;
		butXDX = Screen.width ;
		butXDXInfo = Screen.width ;
		butXSXExit = -60 * SizeFactor;

		scocca = GameObject.Find("scocca");
		//MetaioSDKUnity.setTrackingConfigurationFromAssets ("Manualistica/trackingManualistica.xml");

		tagPala = GameObject.Find ("Tag");
		tagPala.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (endMove && callBack.cos == 6) 
		{
			endMove = false;
			StartCoroutine ( moveButtonDX () );
			StartCoroutine ( moveButtonSX () );
			StartCoroutine ( moveButtonSXExit () );
			StartCoroutine ( moveButtonDXInfo () );
			freeUpdate = true;
		}

		if (callBack.cos == 0 && freeUpdate) 
		{
			endMove = true;
			butXSX = -100 * SizeFactor;
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

	private IEnumerator moveButtonSX () {
		

		while (butXSX<buttonXSX && !endMove) {
			butXSX+=10;
			yield return new WaitForSeconds(0.0001f);
		}
		
		yield return 0;
	}

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

	private IEnumerator infoGo () {
		
		
		infoGUI = true;
		yield return new WaitForSeconds(3f);
		infoGUI = false;
		
		
		yield return 0;
	}

	private IEnumerator photoGo ()
	{
		#if UNITY_ANDROID
		
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



		if (callBack.cos == 6) 
		{

			if (infoGUI)
			{
				GUI.DrawTexture (new Rect (0,0,Screen.width,Screen.height), txSfondoNero);
				
				GUI.DrawTexture (new Rect ( buttonXSX+120*SizeFactor,
				                           270*SizeFactor,
				                           250*SizeFactor,
				                           40*SizeFactor), txScaricaScheda);
				
				GUI.DrawTexture (new Rect (buttonXSX + 120 * SizeFactor,
				                           375 * SizeFactor,
				                           250 * SizeFactor,
				                           70 * SizeFactor), txComeFunziona);
				
				GUI.DrawTexture(new Rect (buttonXDX-220*SizeFactor,
				                          330*SizeFactor,
				                          200*SizeFactor,
				                          40 * SizeFactor),txScreenShot);
			}

//						if (GUI.Button (new Rect (butXSXExit,
//		                          			60 * SizeFactor,
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

						if (GUI.Button (new Rect (butXSX,
		                          360 * SizeFactor,
		                          100 * SizeFactor,
		                          100 * SizeFactor), "", esploso)) {
								//Debug.Log("Clicked the button!");
							if (scocca.activeSelf)
							{
								scocca.SetActive(false);
								tagPala.SetActive(true);
							}
							else
							{
								scocca.SetActive(true);
								tagPala.SetActive(false);
							}
						}

						if (GUI.Button (new Rect (butXDXInfo,
		                          60 * SizeFactor,
		                          80 * SizeFactor,
		                          80 * SizeFactor), "", infoStyle)) {
								//Debug.Log("Clicked the button!");
								if (!infoGUI)
									StartCoroutine(infoGo());
						}

						if (GUI.Button (new Rect (butXSX,
		                          240 * SizeFactor,
		                          100 * SizeFactor,
		                          100 * SizeFactor), "", downloadStyle)) {
								//Debug.Log("Clicked the button!");
								Application.OpenURL("http://www.it-energy.it/public/filemedia/26/Scheda%20tecnica%20turbina%2060%20kW.pdf");
						}

						if (GUI.Button (new Rect (butXDX,
		                          300 * SizeFactor,
		                          100 * SizeFactor,
		                          100 * SizeFactor), "", cameraStyle)) {
								//Debug.Log("Clicked the button!");
							StartCoroutine(photoGo());
						}

						
				}
		//GUI.Label (new Rect (50, 50, 50, 50), "" + callBack.cos + " " + butXDX + " " + butXSX);



		}
}
