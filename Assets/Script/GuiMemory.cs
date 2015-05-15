using UnityEngine;
using System.Collections;
using metaio;

public class GuiMemory : MonoBehaviour {

	public GameObject Level1;
	public GameObject Level2;
	public GameObject Level3;
	public GUIStyle buttonTextStyle;
	public GUIStyle exitStyle;
	public GUIStyle infoStyle;
	public GUIStyle Level1Button;
	public GUIStyle Level2Button;
	public GUIStyle Level3Button;
	public GUIStyle cameraStyle;

	public Texture txSfondoNero;
	public Texture txlvl1;
	public Texture txlvl2;
	public Texture txlvl3;
	public Texture txRicominciaPartita;
	public Texture txScreenShot;

	public Texture winTx;
	public Texture Num0Tx;
	public Texture Num1Tx;
	public Texture Num2Tx;
	public Texture Num3Tx;
	public Texture Num4Tx;
	public Texture Num5Tx;
	public Texture Num6Tx;
	public Texture Num7Tx;
	public Texture Num8Tx;
	public Texture Num9Tx;

	private GestCallBack callBack;
	private TouchCard touchCardGO;
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
	private string error = "";


	float SizeFactor;

	// Use this for initialization
	void Start () {
		//Level1.SetActive (false);
		try
		{
		Level2.SetActive (false);
		Level3.SetActive (false);
		SizeFactor = GUIUtilities.SizeFactor;
		touchCardGO = Level1.GetComponent<TouchCard> ();
		//MetaioSDKUnity.setTrackingConfigurationFromAssets ("Memory/tracking.xml");
		//callBack = gameObject.GetComponent<GestCallBack> ();
		callBack = GameObject.Find("GUI").GetComponent<GestCallBack> ();

		buttonXSX = 50 * SizeFactor;
		buttonXDX = Screen.width - 150 * SizeFactor;
		buttonXDXInfo = Screen.width - 140 * SizeFactor;
		buttonXSXExit = 60 * SizeFactor;
		
		butXSX = -50 * SizeFactor;
		butXDX = Screen.width ;
		butXDXInfo = Screen.width ;
		butXSXExit = -60 * SizeFactor;

		}
		catch (System.Exception er)
		{
			error = er.ToString();
		}
	}
	
	// Update is called once per frame
	void Update () {


		if (endMove && callBack.cos == 4) 
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
		Application.OpenURL (Application.persistentDataPath+"/"+namePhoto);
		
		AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass refreshGallery = new AndroidJavaClass ("com.mirabilar.refreshgallery.GalleryRefresh");
		
		AndroidJavaObject joString = new AndroidJavaObject("java.lang.String",Application.persistentDataPath+"/"+namePhoto); 
		refreshGallery.CallStatic("NewRefreshG",new object[2]{jo,joString});				
		

		#endif
		
		
		yield return 0;
	}

	void OnGUI () {
		
//		if(GUIUtilities.ButtonWithText(new Rect(
//			Screen.width - 200*SizeFactor,
//			0,
//			200*SizeFactor,
//			100*SizeFactor),touchCardGO.Counter.ToString(),null,buttonTextStyle) ||	Input.GetKeyDown(KeyCode.Escape)) 
//		{
//
//		
//		}
//
//		GUI.Label (new Rect (50, 50, 50, 50), "" + callBack.cos + "   " + error + " " );



		if (callBack.cos == 4) 
		{

			if (infoGUI)
			{
				GUI.DrawTexture (new Rect (0,0,Screen.width,Screen.height), txSfondoNero);

				GUI.DrawTexture(new Rect (buttonXSX+120*SizeFactor,
				                          80*SizeFactor,
				                          250*SizeFactor,
				                          40 * SizeFactor),txRicominciaPartita);
				
				GUI.DrawTexture(new Rect (buttonXSX+120*SizeFactor,
				                          210*SizeFactor,
				                          300*SizeFactor,
				                          40 * SizeFactor),txlvl1);
				
				GUI.DrawTexture (new Rect ( buttonXSX+120*SizeFactor,
				                           330*SizeFactor,
				                           300*SizeFactor,
				                           40*SizeFactor), txlvl2);
				
				GUI.DrawTexture (new Rect (buttonXSX + 120 * SizeFactor,
				                           450 * SizeFactor,
				                           250 * SizeFactor,
				                           40 * SizeFactor), txlvl3);
				
				GUI.DrawTexture(new Rect (buttonXDX-220*SizeFactor,
				                          270*SizeFactor,
				                          200*SizeFactor,
				                          40 * SizeFactor),txScreenShot);
			}

			if (GUI.Button (new Rect (butXSXExit,
			                          60 * SizeFactor,
			                          80 * SizeFactor,
			                          80 * SizeFactor), "", exitStyle)) 
			{
				if (!touchCardGO.blockCouroutine)
				{

					if (Level1.activeSelf )
					{
						Level1.GetComponent<TouchCard>().resetTouchCard();
						Level1.GetComponent<TouchCard>().win = false;	
					}

					else if (Level2.activeSelf)
					{
						Level2.GetComponent<TouchCard>().resetTouchCard();
						Level2.GetComponent<TouchCard>().win = false;	
					}

					else if (Level3.activeSelf)
					{
						Level3.GetComponent<TouchCard>().resetTouchCard();
						Level3.GetComponent<TouchCard>().win = false;	
					}

				}
			}
			
			if (GUI.Button (new Rect (butXSX,
			                          180 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", Level1Button)) 
			{
				if (!touchCardGO.blockCouroutine)
				{
					Level1.SetActive(true);
					Level2.SetActive(false);
					Level3.SetActive(false);
					touchCardGO.unita = 0;
					touchCardGO.decine = 0;
					touchCardGO = Level1.GetComponent<TouchCard> ();
					Level1.GetComponent<TouchCard>().resetTouchCard();
					Level3.GetComponent<TouchCard>().win = false;
					Level2.GetComponent<TouchCard>().win = false;
					Level1.GetComponent<TouchCard>().win = false;	
				}
			}
			
			if (GUI.Button (new Rect (butXSX,
			                          300 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", Level2Button)) {
				if (!touchCardGO.blockCouroutine)
				{
					Level1.SetActive(false);
					Level2.SetActive(true);
					Level3.SetActive(false);
					touchCardGO.unita = 0;
					touchCardGO.decine = 0;
					touchCardGO = Level2.GetComponent<TouchCard> ();
					Level2.GetComponent<TouchCard>().resetTouchCard();
					Level3.GetComponent<TouchCard>().win = false;
					Level2.GetComponent<TouchCard>().win = false;
					Level1.GetComponent<TouchCard>().win = false;
				}
			}
			
			if (GUI.Button (new Rect (butXSX,
			                          420 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", Level3Button)) 
			{
				if (!touchCardGO.blockCouroutine)
				{
							Level1.SetActive(false);
							Level2.SetActive(false);
							Level3.SetActive(true);
							touchCardGO.unita = 0;
							touchCardGO.decine = 0;
							touchCardGO = Level3.GetComponent<TouchCard> ();
							Level3.GetComponent<TouchCard>().resetTouchCard();
							Level3.GetComponent<TouchCard>().win = false;
							Level2.GetComponent<TouchCard>().win = false;
							Level1.GetComponent<TouchCard>().win = false;
				}
			}
			
			if (GUI.Button (new Rect (butXDXInfo,
			                          60 * SizeFactor,
			                          80 * SizeFactor,
			                          80 * SizeFactor), "", infoStyle)) {
				//Debug.Log("Clicked the button!");
				if (!infoGUI)
					StartCoroutine (infoGo());
			}
			
			if (GUI.Button (new Rect (butXDX,
			                          240 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", cameraStyle)) {
				//Debug.Log("Clicked the button!");
				StartCoroutine(photoGo());
			}

			if (Level3.GetComponent<TouchCard>().win || Level2.GetComponent<TouchCard>().win || Level1.GetComponent<TouchCard>().win)
				GUI.DrawTexture(new Rect(Screen.width/2 - 150 * SizeFactor ,
				                         Screen.height/2 - 75 * SizeFactor,
				                         300 * SizeFactor,
				                         150 * SizeFactor), winTx);	

			if (touchCardGO.unita == 0)
				GUI.DrawTexture (new Rect (Screen.width/2 ,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num0Tx);

			else if (touchCardGO.unita == 1)
				GUI.DrawTexture (new Rect (Screen.width/2 ,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num1Tx);

			else if (touchCardGO.unita == 2)
				GUI.DrawTexture (new Rect (Screen.width/2 ,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num2Tx);
			
			else if (touchCardGO.unita == 3)
				GUI.DrawTexture (new Rect (Screen.width/2 ,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num3Tx);

			else if (touchCardGO.unita == 4)
				GUI.DrawTexture (new Rect (Screen.width/2 ,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num4Tx);

			else if (touchCardGO.unita == 5)
				GUI.DrawTexture (new Rect (Screen.width/2 ,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num5Tx);

			else if (touchCardGO.unita == 6)
				GUI.DrawTexture (new Rect (Screen.width/2 ,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num6Tx);

			else if (touchCardGO.unita == 7)
				GUI.DrawTexture (new Rect (Screen.width/2 ,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num7Tx);

			else if (touchCardGO.unita == 8)
				GUI.DrawTexture (new Rect (Screen.width/2 ,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num8Tx);

			else if (touchCardGO.unita == 9)
				GUI.DrawTexture (new Rect (Screen.width/2 ,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num9Tx);

			if (touchCardGO.decine == 0)
				GUI.DrawTexture (new Rect (Screen.width/2 - 120 * SizeFactor ,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num0Tx);

			else if (touchCardGO.decine == 1)
				GUI.DrawTexture (new Rect (Screen.width/2 - 120 * SizeFactor,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num1Tx);
			
			else if (touchCardGO.decine == 2)
				GUI.DrawTexture (new Rect (Screen.width/2 - 120 * SizeFactor,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num2Tx);
			
			else if (touchCardGO.decine == 3)
				GUI.DrawTexture (new Rect (Screen.width/2 - 120 * SizeFactor,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num3Tx);
			
			else if (touchCardGO.decine == 4)
				GUI.DrawTexture (new Rect (Screen.width/2 - 120 * SizeFactor,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num4Tx);
			
			else if (touchCardGO.decine == 5)
				GUI.DrawTexture (new Rect (Screen.width/2 - 120 * SizeFactor,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num5Tx);
			
			else if (touchCardGO.decine == 6)
				GUI.DrawTexture (new Rect (Screen.width/2 - 120 * SizeFactor,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num6Tx);
			
			else if (touchCardGO.decine == 7)
				GUI.DrawTexture (new Rect (Screen.width/2 - 120 * SizeFactor,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num7Tx);
			
			else if (touchCardGO.decine == 8)
				GUI.DrawTexture (new Rect (Screen.width/2 - 120 * SizeFactor,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num8Tx);
			
			else if (touchCardGO.decine == 9)
				GUI.DrawTexture (new Rect (Screen.width/2 - 120 * SizeFactor,
				                           0,
				                           120 * SizeFactor,
				                           120 * SizeFactor),Num9Tx);


		}









	}

}
