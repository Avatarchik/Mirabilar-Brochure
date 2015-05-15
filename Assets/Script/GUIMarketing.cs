using UnityEngine;
using System.Collections;

public class GUIMarketing : MonoBehaviour {

	public GUIStyle exitStyle;
	public GUIStyle infoStyle;
	public GUIStyle cameraStyle;
	public GUIStyle downloadStyle;
	public GUIStyle neveStyle;
	public GUIStyle pioggiaStyle;
	public GUIStyle soleStyle;
	public GUIStyle carrello;
	public GUIStyle frecciaDx;
	public GUIStyle FrecciaSx;
	public GUIStyle chiudiFinestra;

	public Texture txLampadaRossa;
	public Texture txLampadaBlu;
	public Texture txLampadaVerde;
	public Texture txScarpaRossa;
	public Texture txScarpaBlu;
	public Texture txScarpaVerde;
	public Texture txOcchialiRossa;
	public Texture txOcchialiBlu;
	public Texture txOcchialiVerde;
	public Texture manina;
	public Texture txDescLampada;
	public Texture txDescOcchiali;
	public Texture txDescScarpa;
	public Texture txTitoloLampada;
	public Texture txTitoloOcchiali;
	public Texture txTitoloScarpa;
	public Texture txSfondoNero;
	public Texture txCambiaColore;
	public Texture txCompraProdotto;
	public Texture txGuardaLeInfo;
	public Texture txScreenShot;

	
	private  float buttonXSX ;
	private  float buttonXDX ;
	private  float buttonXSXExit;
	private  float buttonXDXInfo;
	private  float butXSX ;
	private  float butXDX ;
	private  float butXSXExit;
	private  float butXDXInfo;
	private  float fManina;

	private bool endMove = true;
	private bool freeUpdate = false;
	private bool moveManina = false;
	private bool freeManina = false;
	private bool descrizione = false;
	private bool infoGUI = false;



	private GestCallBack callBack;
	

	private TouchMarketing touchMark;


	
	float SizeFactor;
	
	// Use this for initialization
	void Start () {
		SizeFactor = GUIUtilities.SizeFactor;
		callBack = GameObject.Find("GUI").GetComponent<GestCallBack> ();

		GameObject goTouch;
		goTouch = GameObject.Find ("TouchMark");
		touchMark = goTouch.GetComponent<TouchMarketing> ();

		fManina = 400 * SizeFactor;

		buttonXSX = 50 * SizeFactor;
		buttonXDX = Screen.width - 150 * SizeFactor;
		buttonXDXInfo = Screen.width - 140 * SizeFactor;
		buttonXSXExit = 60 * SizeFactor;
		
		butXSX = -50 * SizeFactor;
		butXDX = Screen.width ;
		butXDXInfo = Screen.width ;
		butXSXExit = -60 * SizeFactor;


	}
	
	// Update is called once per frame
	void Update () {
		if (endMove && callBack.cos == 7) 
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
			freeManina = false;
		}

		if (callBack.cos == 7 && !freeManina)
		{
			freeManina = true;
			StartCoroutine ( AnimManina () );
		}

		
	}

	private IEnumerator AnimManina () {

		yield return new WaitForSeconds (0.5f);

		moveManina = true;

		while (fManina<Screen.width - 400 * SizeFactor ) {
			fManina+=10f;
			yield return new WaitForSeconds(0.0001f);
		}

		fManina = 400 * SizeFactor;
		yield return new WaitForSeconds (0.1f);

		while (fManina<Screen.width - 400 * SizeFactor ) {
			fManina+=10f;
			yield return new WaitForSeconds(0.0001f);
		}

		fManina = 400 * SizeFactor;

		moveManina = false;
		
		yield return 0;
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

	private void changeRed ()
	{
		if (touchMark.counter == 0)
		{
			GameObject goBaseLampada = GameObject.Find("lampada_inferiore1");
			//GameObject goCappelloLampada = GameObject.Find("cappello_lampada");
			goBaseLampada.renderer.material.mainTexture = txLampadaRossa;
			//goCappelloLampada.renderer.material.mainTexture = txLampadaRossa;
		}
		else if (touchMark.counter == 1)
		{
			GameObject goScarpaTessuto = GameObject.Find("scarpa:scarpa_tessuto");
			GameObject goScarpaLinguetta = GameObject.Find("soletta:linguetta");
			goScarpaLinguetta.renderer.material.mainTexture = txScarpaRossa;
			goScarpaTessuto.renderer.material.mainTexture = txScarpaRossa;
		}
		else if (touchMark.counter == 2)
		{
			GameObject goOcchialiAstina = GameObject.Find("occhiali_astina");
			GameObject goOcchialiAstinaUno = GameObject.Find("occhiali_astina1");
			GameObject goOcchialiLenti = GameObject.Find("occhiali_lenti");
			goOcchialiAstina.renderer.material.mainTexture = txOcchialiRossa;
			goOcchialiAstinaUno.renderer.material.mainTexture = txOcchialiRossa;
			goOcchialiLenti.renderer.material.mainTexture = txOcchialiRossa;
		}
	}

	private void changeBlu ()
	{
		if (touchMark.counter == 0)
		{
			GameObject goBaseLampada = GameObject.Find("lampada_inferiore1");
			//GameObject goCappelloLampada = GameObject.Find("cappello_lampada");
			goBaseLampada.renderer.material.mainTexture = txLampadaBlu;
			//goCappelloLampada.renderer.material.mainTexture = txLampadaBlu;
		}
		else if (touchMark.counter == 1)
		{
			GameObject goScarpaTessuto = GameObject.Find("scarpa:scarpa_tessuto");
			goScarpaTessuto.renderer.material.mainTexture = txScarpaBlu;
			GameObject goScarpaLinguetta = GameObject.Find("soletta:linguetta");
			goScarpaLinguetta.renderer.material.mainTexture = txScarpaBlu;
		}
		else if (touchMark.counter == 2)
		{
			GameObject goOcchialiAstina = GameObject.Find("occhiali_astina");
			GameObject goOcchialiAstinaUno = GameObject.Find("occhiali_astina1");
			GameObject goOcchialiLenti = GameObject.Find("occhiali_lenti");
			goOcchialiAstina.renderer.material.mainTexture = txOcchialiBlu;
			goOcchialiAstinaUno.renderer.material.mainTexture = txOcchialiBlu;
			goOcchialiLenti.renderer.material.mainTexture = txOcchialiBlu;
		}
	}

	private void changeGreen ()
	{
		if (touchMark.counter == 0)
		{
			GameObject goBaseLampada = GameObject.Find("lampada_inferiore1");
			//GameObject goCappelloLampada = GameObject.Find("cappello_lampada");
			goBaseLampada.renderer.material.mainTexture = txLampadaVerde;
			//goCappelloLampada.renderer.material.mainTexture = txLampadaVerde;
		}
		else if (touchMark.counter == 1)
		{
			GameObject goScarpaTessuto = GameObject.Find("scarpa:scarpa_tessuto");
			goScarpaTessuto.renderer.material.mainTexture = txScarpaVerde;
			GameObject goScarpaLinguetta = GameObject.Find("soletta:linguetta");
			goScarpaLinguetta.renderer.material.mainTexture = txScarpaVerde;
		}
		else if (touchMark.counter == 2)
		{
			GameObject goOcchialiAstina = GameObject.Find("occhiali_astina");
			GameObject goOcchialiAstinaUno = GameObject.Find("occhiali_astina1");
			GameObject goOcchialiLenti = GameObject.Find("occhiali_lenti");
			goOcchialiAstina.renderer.material.mainTexture = txOcchialiVerde;
			goOcchialiAstinaUno.renderer.material.mainTexture = txOcchialiVerde;
			goOcchialiLenti.renderer.material.mainTexture = txOcchialiVerde;
		}
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



		if (callBack.cos == 7 && !descrizione) 
		{

			if (touchMark.counter == 0)
				GUI.DrawTexture(new Rect (Screen.width/2-150*SizeFactor,60 * SizeFactor,300*SizeFactor,60*SizeFactor)
				                ,txTitoloLampada);
			
			if (touchMark.counter == 1)
				GUI.DrawTexture(new Rect (Screen.width/2-150*SizeFactor,60 * SizeFactor,300*SizeFactor,60*SizeFactor)
				                ,txTitoloScarpa);
			
			if (touchMark.counter == 2)
				GUI.DrawTexture(new Rect (Screen.width/2-150*SizeFactor,60 * SizeFactor,300*SizeFactor,60*SizeFactor)
				                ,txTitoloOcchiali);

			if (infoGUI)
			{


				GUI.DrawTexture (new Rect (0,0,Screen.width,Screen.height), txSfondoNero);
				
				
				GUI.DrawTexture(new Rect (buttonXSX+120*SizeFactor,
				                          195*SizeFactor,
				                          250*SizeFactor,
				                          70 * SizeFactor),txCambiaColore);
				
				GUI.DrawTexture (new Rect ( buttonXSX+120*SizeFactor,
				                           315*SizeFactor,
				                           250*SizeFactor,
				                           70*SizeFactor), txCambiaColore);
				
				GUI.DrawTexture (new Rect (buttonXSX + 120 * SizeFactor,
				                           435 * SizeFactor,
				                           250 * SizeFactor,
				                           70 * SizeFactor), txCambiaColore);
				
				GUI.DrawTexture(new Rect (buttonXDX-220*SizeFactor,
				                          195*SizeFactor,
				                          200*SizeFactor,
				                          70 * SizeFactor),txGuardaLeInfo);
				
				GUI.DrawTexture(new Rect (buttonXDX-200*SizeFactor,
				                          330*SizeFactor,
				                          180*SizeFactor,
				                          40 * SizeFactor),txScreenShot);
				
				GUI.DrawTexture(new Rect (buttonXDX-270*SizeFactor,
				                          450*SizeFactor,
				                          250*SizeFactor,
				                          40 * SizeFactor),txCompraProdotto);
			}

			if (moveManina)
				GUI.DrawTexture (new Rect (fManina,
				                           Screen.height/2 - 75 * SizeFactor,
				                           150 * SizeFactor,
				                           150 * SizeFactor),manina);

//			if (GUI.Button (new Rect (butXSXExit,
//			                          60 * SizeFactor,
//			                          80 * SizeFactor,
//			                          80 * SizeFactor), "", exitStyle)) {
//				//Debug.Log("Clicked the button!");
//			}
			
			if (GUI.Button (new Rect (butXSX,
			                          180 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", soleStyle)) {
				//verde
				changeGreen();
			}
			
			if (GUI.Button (new Rect (butXSX,
			                          300 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", pioggiaStyle)) {
				//Rosso

				changeRed();
			}
			
			if (GUI.Button (new Rect (butXSX,
			                          420 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", neveStyle)) {
				//Blu
				changeBlu();
			}
			
			if (GUI.Button (new Rect (butXDXInfo,
			                          60 * SizeFactor,
			                          80 * SizeFactor,
			                          80 * SizeFactor), "", infoStyle)) {
				//Debug.Log("Clicked the button!");
				if (!infoGUI)
					StartCoroutine(infoGo());
			}
			
			if (GUI.Button (new Rect (butXDX,
			                          180 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", downloadStyle)) {
				//Debug.Log("Clicked the button!");
				descrizione = true;
			}
			
			if (GUI.Button (new Rect (butXDX,
			                          300 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", cameraStyle)) {
				//Debug.Log("Clicked the button!");
				StartCoroutine(photoGo());
			}

			if (GUI.Button (new Rect (butXDX,
			                          420 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", carrello)) {
				//Debug.Log("Clicked the button!");
				if (touchMark.counter == 0)
					Application.OpenURL("http://www.amazon.it/s/ref=nb_sb_noss_1?__mk_it_IT=%C3%85M%C3%85%C5%BD%C3%95%C3%91&url=search-alias%3Daps&field-keywords=eclisse+artemide");
				
				if (touchMark.counter == 1)
					Application.OpenURL("http://www.converse.com/#ui­tabs­2");
				
				if (touchMark.counter == 2)
					Application.OpenURL("http://www.ray-ban.com/italy/personalizza/rb-2140-original-wayfarer-occhiali-da-sole?icmp=int-RB:undefined:RemixClp:StartWithClassic:Customize");
			}

//			if (GUI.Button (new Rect (Screen.width - 270 * SizeFactor,
//			                          Screen.height/2 - 50* SizeFactor,
//			                          100 * SizeFactor,
//			                          100 * SizeFactor), "", frecciaDx)) {
//				//Debug.Log("Clicked the button!");
//				touchMark.FrecciaDestra();
//			}
//			
//			if (GUI.Button (new Rect (170 * SizeFactor,
//			                          Screen.height/2 - 50* SizeFactor,
//			                          100 * SizeFactor,
//			                          100 * SizeFactor), "", FrecciaSx)) {
//				//Debug.Log("Clicked the button!");
//				touchMark.FrecciaSinistra();
//			}
			




		}

		if (descrizione)
		{
			if (touchMark.counter == 0)
				GUI.DrawTexture(new Rect (Screen.width/2-350*SizeFactor,Screen.height/2-200*SizeFactor,700*SizeFactor,400*SizeFactor)
				                ,txDescLampada);
			
			if (touchMark.counter == 1)
				GUI.DrawTexture(new Rect (Screen.width/2-350*SizeFactor,Screen.height/2-200*SizeFactor,700*SizeFactor,400*SizeFactor)
				                ,txDescScarpa);
			
			if (touchMark.counter == 2)
				GUI.DrawTexture(new Rect (Screen.width/2-350*SizeFactor,Screen.height/2-200*SizeFactor,700*SizeFactor,400*SizeFactor)
				                ,txDescOcchiali);
			
			if (GUI.Button(new Rect(Screen.width/2-320*SizeFactor,Screen.height/2-170*SizeFactor,50*SizeFactor,50*SizeFactor),"",chiudiFinestra))
				descrizione = false;
			
		}

		//GUI.Label (new Rect (50, 50, 50, 50), "" + callBack.cos + " " + butXDX + " " + butXSX);
		

		
	}


	
}
