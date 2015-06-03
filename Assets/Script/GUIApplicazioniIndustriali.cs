using UnityEngine;
using System.Collections;

public class GUIApplicazioniIndustriali : MonoBehaviour {

	public GUIStyle exitStyle;
	public GUIStyle infoStyle;
	public GUIStyle cameraStyle;
	public GUIStyle tourStyle;
	public GUIStyle tubiStyle;
	public GUIStyle mattoniStyle;
	public GUIStyle palazzoStyle;
	public GUIStyle alberiStyle;

	public Texture txSfondoNero;
	public Texture txAmbiente;
	public Texture txEdificio;
	public Texture txImpianti;
	public Texture txStruttura;
	public Texture txScreenShot;
	public Texture txVirtualTour;
	
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

	private GameObject goBase;
	private GameObject goBaseSenzaTx;
	private GameObject goGrattacieloStruttura;
	private GameObject goImpianti;
	private GameObject goStrutturaEsterna;
	
	private float SizeFactor;

	public string albumName = "Mirabilar";
	bool isScreenShotSave;

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

		goBase = GameObject.Find("baseM");
		goStrutturaEsterna = GameObject.Find("Struttura_esterna");
		goBaseSenzaTx = GameObject.Find ("basi_noTexture");
		goGrattacieloStruttura = GameObject.Find ("grattacielo_struttura");
		goImpianti = GameObject.Find ("grattacielo_impianti+struttura prova");

		goStrutturaEsterna.SetActive(true);
		goBase.SetActive(true);
		goGrattacieloStruttura.SetActive(false);
		goImpianti.SetActive(false);
		goBaseSenzaTx.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (endMove && callBack.cos == 3) 
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
		//butXSXExit = 60 * SizeFactor;
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

	void ScreenShotStatus(bool status)
	{
		isScreenShotSave = status;
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




		if (callBack.cos == 3) 
		{

			if (infoGUI)
			{
				GUI.DrawTexture (new Rect (0,0,Screen.width,Screen.height), txSfondoNero);
				
				GUI.DrawTexture(new Rect (buttonXSX+120*SizeFactor,
				                          190*SizeFactor,
				                          200*SizeFactor,
				                          40 * SizeFactor),txEdificio);
				
				GUI.DrawTexture (new Rect ( buttonXSX+120*SizeFactor,
				                           285*SizeFactor,
				                           250*SizeFactor,
				                           70*SizeFactor), txStruttura);
				
				GUI.DrawTexture (new Rect (buttonXSX + 120 * SizeFactor,
				                           395 * SizeFactor,
				                           250 * SizeFactor,
				                           70 * SizeFactor), txImpianti);
				
				GUI.DrawTexture (new Rect (buttonXSX + 120 * SizeFactor,
				                           505 * SizeFactor,
				                           250 * SizeFactor,
				                           70 * SizeFactor), txAmbiente);
				
				GUI.DrawTexture(new Rect (buttonXDX-320*SizeFactor,
				                          270*SizeFactor,
				                          300*SizeFactor,
				                          40 * SizeFactor),txVirtualTour);
				
				GUI.DrawTexture(new Rect (buttonXDX-220*SizeFactor,
				                          390*SizeFactor,
				                          200*SizeFactor,
				                          40 * SizeFactor),txScreenShot);
			}
			
//			if (GUI.Button (new Rect (butXSXExit,
//			                          60 * SizeFactor,
//			                          80 * SizeFactor,
//			                          80 * SizeFactor), "", exitStyle)) {
//				//Debug.Log("Clicked the button!");
//			}
			
			if (GUI.Button (new Rect (butXSX,
			                          160 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", palazzoStyle)) {
				//Debug.Log("Clicked the button!");
				goStrutturaEsterna.SetActive(true);
				goBase.SetActive(true);
				goGrattacieloStruttura.SetActive(false);
				goImpianti.SetActive(false);
				goBaseSenzaTx.SetActive(false);
			}
			
			if (GUI.Button (new Rect (butXSX,
			                          270 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", mattoniStyle)) {
				//Debug.Log("Clicked the button!");
				goStrutturaEsterna.SetActive(false);
				goImpianti.SetActive(false);
				goGrattacieloStruttura.SetActive(true);

			}
			
			if (GUI.Button (new Rect (butXSX,
			                          380 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", tubiStyle)) {
				//Debug.Log("Clicked the button!");
				goStrutturaEsterna.SetActive(false);
				goGrattacieloStruttura.SetActive(false);
				goImpianti.SetActive(true);
			}

			if (GUI.Button (new Rect (butXSX,
			                          490 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", alberiStyle)) {
				//Debug.Log("Clicked the button!");
				if (goBase.activeSelf)
				{
					goBase.SetActive(false);
					goBaseSenzaTx.SetActive(true);
				}
				else
				{
					goBase.SetActive(true);
					goBaseSenzaTx.SetActive(false);
				}

			}
			
			if (GUI.Button (new Rect (butXDXInfo,
			                          60 * SizeFactor,
			                          80 * SizeFactor,
			                          80 * SizeFactor), "", infoStyle)) {
				//Debug.Log("Clicked the button!");
				if (!infoGUI)
					StartCoroutine ( infoGo () );
			}
			
			if (GUI.Button (new Rect (butXDX,
			                          240 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", tourStyle)) {
				//Debug.Log("Clicked the button!");
				Application.LoadLevel("Tour");
			}
			
			if (GUI.Button (new Rect (butXDX,
			                          360 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", cameraStyle)) {
				//Debug.Log("Clicked the button!");
				StartCoroutine(photoGo());

				#if UNITY_IOS

				for (int i= 0; i < 2; i++)
				{
					isScreenShotSave = false;
					StartCoroutine(ScreenShotBridge.SaveScreenShot(albumName,ScreenShotStatus));	
				}
				#endif

			}
			
			
		}
//		GUI.Label (new Rect (50, 50, 50, 50), "" + callBack.cos + " " + butXDX + " " + butXSX);
		
		
		
	}

}
