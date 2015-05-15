using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIEditoria : MonoBehaviour {

//	GameObject moviePlane;
//	metaioMovieTexture movieTexture;

	public GUIStyle exitStyle;
	public GUIStyle infoStyle;
	public GUIStyle cameraStyle;
	public GUIStyle downloadStyle;
	public GUIStyle neveStyle;
	public GUIStyle pioggiaStyle;
	public GUIStyle soleStyle;
	public GUIStyle condividiVideo;
	public GUIStyle chiudiFinestraStyle;

	public VideoPlaybackBehaviour myVideo;

	public Texture playTx;
	public Texture pauseTx;
	public Texture biografria;
	public Texture txConfividiFacebook;
	public Texture txBiografiaAutore;
	public Texture txScreenShot;
	public Texture txCompra;
	public Texture txSfondoNero;
	//public MovieTexture movie;
	private Rect rectPlayPause;
	
	private  float buttonXSX ;
	private  float buttonXDX ;
	private  float buttonXSXExit;
	private  float buttonXDXInfo;
	private  float butXSX ;
	private  float butXDX ;
	private  float butXSXExit;
	private  float butXDXInfo;
	private float timer = 0;
	private bool endMove = true;
	private bool freeUpdate = false;
	private bool stop = false;
	private bool guiPlay = false;
	private bool guiPause = false;
	private bool condividi = false;
	private bool freeTimer = false;
	private bool biografia = false;
	private bool infoGUI = false;
	private GestCallBack callBack;
	private float SizeFactor;

	private string debug = "";


	// variables for ray tracing
	private RaycastHit hit;
	private LayerMask layerMask = 1<<4;
	
	private bool selected = false;
	
	// Use this for initialization
	void Start () {
	
		FB.Init(OnInitComplete, OnHideUnity);

//		moviePlane = GameObject.Find("moviePlane");
//		movieTexture = moviePlane.GetComponent<metaioMovieTexture>();
//		movieTexture.play(false);

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

		rectPlayPause = new Rect (Screen.width/2 - 50*SizeFactor, Screen.height/2 - 50*SizeFactor,
		                          100 * SizeFactor, 100*SizeFactor);
		//movie.Play();

	}
	
	// Update is called once per frame
	void Update () {
		//movieTexture.play(true);
		if (endMove && callBack.cos == 5) 
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
	



//		if (callBack.cos == 5)
//		{
//
//		if (moviePlane.activeSelf && !biografia)
//		{
//			if (Input.touchCount > 0)
//				// if tracking, then evaluate touch point
//				handleTouches();
//			else
//				selected = false;
//		}
//
//		if (freeTimer)
//			timer += Time.deltaTime;
//		
//		if (timer>0.3
//			    && freeTimer)
//		{
//			freeTimer = false;
//			timer = 0;
//		}
//		else if (Input.touchCount > 0 && freeTimer && timer > 0.1f && !biografia)
//		{
//			Handheld.PlayFullScreenMovie ("Editoria/_39_Chiedi_alla_luna_39_Nathan_Filer_-_Booktrailer.3g2",Color.black,FullScreenMovieControlMode.CancelOnInput);
//			stop = true;
//			movieTexture.pause();
//		}
//
//		}

	}

	private void handleTouches()
	{
		// if there's touch points and the phase is began-->try to select the geometry
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			selectGameObject(Input.GetTouch(0).position);
		
		// if the touch point is moving and metaio man is selected
//		if(Input.GetTouch(0).phase == TouchPhase.Moved && selected)
//			moveGameObject(Input.GetTouch(0).position);
		else if(Input.GetTouch(0).phase == TouchPhase.Ended && selected)
		{
			// deselect metaio man if touch has ended
			selected = false;
			freeTimer = true;
		}
	}

	public void selectGameObject(Vector2 position)
	{
		// get a ray from the touch point
		Ray ray = Camera.main.ScreenPointToRay(position);
		
		// layer mask, metaio man is on layer 4
		layerMask = 1<<11;
		
		// cast a ray on layer 4, if metaio man has been hit
		if (Physics.Raycast(ray, out hit, 5000, layerMask) && hit.collider.gameObject.name == "moviePlane")
		{
			// metaio man is touched --> select it
			selected = true;

			if (!stop)
			{
				//movieTexture.pause();

				StartCoroutine (PlayPauseGui());
			}
				
			else
			{
				//movieTexture.play(false);

				StartCoroutine (PlayPauseGui());
			}

			// swith to layer 8, find the initial touch point on the plane and use it as a reference
			layerMask = 1<<11;
			if (Physics.Raycast(ray, out hit, 5000, layerMask))
			{
				// record the offset of the touch point and the object position point
				//offset = hit.point - metaioMan.transform.position;
			}
		}
		else
			// not hit, don't select
			selected = false;
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

	private IEnumerator PlayPauseGui ()
	{
		if (!stop)
		{
			stop = true;
			guiPause = true;
			yield return new WaitForSeconds(1f);
			guiPause = false;

		}
		else
		{
			stop = false;
			guiPlay = true;
			yield return new WaitForSeconds(1f);
			guiPlay = false;
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

		if (biografia)
		{
			GUI.DrawTexture(new Rect (Screen.width/2-350*SizeFactor,Screen.height/2-200*SizeFactor,700*SizeFactor,400*SizeFactor)
			                ,biografria);

			if (GUI.Button(new Rect(Screen.width/2-320*SizeFactor,Screen.height/2-170*SizeFactor,50*SizeFactor,50*SizeFactor),"",chiudiFinestraStyle))
				biografia = false;
		}




		if (callBack.cos == 5 && !biografia) 
		{

			if (infoGUI)
			{
				GUI.DrawTexture (new Rect (0,0,Screen.width,Screen.height), txSfondoNero);
				
				GUI.DrawTexture(new Rect (buttonXSX+120*SizeFactor,
				                          330*SizeFactor,
				                          250*SizeFactor,
				                          40 * SizeFactor),txConfividiFacebook);
				
				GUI.DrawTexture(new Rect (buttonXDX-220*SizeFactor,
				                          195*SizeFactor,
				                          200*SizeFactor,
				                          70 * SizeFactor),txBiografiaAutore);
				
				GUI.DrawTexture(new Rect (buttonXDX-200*SizeFactor,
				                          330*SizeFactor,
				                          180*SizeFactor,
				                          40 * SizeFactor),txScreenShot);
				
				GUI.DrawTexture(new Rect (buttonXDX-220*SizeFactor,
				                          450*SizeFactor,
				                          200*SizeFactor,
				                          40 * SizeFactor),txCompra);
			}
			
//			if (GUI.Button (new Rect (butXSXExit,
//			                          60 * SizeFactor,
//			                          80 * SizeFactor,
//			                          80 * SizeFactor), "", exitStyle)) {
//				//Debug.Log("Clicked the button!");
//			}
			
			if (GUI.Button (new Rect (butXDX,
			                          180 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", soleStyle)) {
				//Debug.Log("Clicked the button!"); BIOGRAFIA
				stop = true;
				//movieTexture.pause();
				myVideo.VideoPlayer.Pause();
				biografia = true;
			}
			
			if (GUI.Button (new Rect (butXDX,
			                          300 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", pioggiaStyle)) {
				//Debug.Log("Clicked the button!"); CAMERA
				StartCoroutine(photoGo());
			}
			
			if (GUI.Button (new Rect (butXDX,
			                          420 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", neveStyle)) {
				//Debug.Log("Clicked the button!");SHOPPING
				stop = true;
				//movieTexture.pause();
				myVideo.VideoPlayer.Pause();
				Application.OpenURL("http://www.lafeltrinelli.it/libri/nathan-filer/chiedi-alla-luna/9788807030437");
			}
			
			if (GUI.Button (new Rect (butXDXInfo,
			                          60 * SizeFactor,
			                          80 * SizeFactor,
			                          80 * SizeFactor), "", infoStyle)) {
				myVideo.VideoPlayer.Pause();
				if (!infoGUI)
					StartCoroutine (infoGo());

				//Debug.Log("Clicked the button!");
			}
			
//			if (GUI.Button (new Rect (butXSX,
//			                          240 * SizeFactor,
//			                          100 * SizeFactor,
//			                          100 * SizeFactor), "", downloadStyle)) {
//				//Debug.Log("Clicked the button!");
//			}
			
			if (GUI.Button (new Rect (butXSX,
			                          300 * SizeFactor,
			                          100 * SizeFactor,
			                          100 * SizeFactor), "", cameraStyle)) {
				//FACEBOOK
				try 
				{
					myVideo.VideoPlayer.Pause();
					stop = true;
					//movieTexture.pause();
					Dictionary<string, string[]> FeedProperties = null;
					FB.Login("public_profile,email,user_friends,publish_actions", LoginCallback);
					condividi = true;
					//FB.Login("publish_actions", LoginCallback);
//					FB.Feed(
//						toId: "",
//						link: "http://www.feltrinellieditore.it/opera/opera/chiedi-alla-luna/",
//						linkName: "",
//						linkCaption: "",
//						linkDescription: "",
//						picture: "",
//						mediaSource: "",
//						actionName: "",
//						actionLink: "",
//						reference: "",
//						properties: FeedProperties,
//						callback: Callback
//						);
				}
				catch (System.Exception e)
				{
					debug = e.Message;
					condividi = false;
				}
			}

			if (guiPlay)
				GUI.DrawTexture (rectPlayPause, playTx);
			
			if (guiPause)
				GUI.DrawTexture (rectPlayPause, pauseTx);
			
		}
		//GUI.Label (new Rect (50, 50, 1000, 50), "  " + callBack.cos + "       " + butXDX + " " + butXSX + "  " + debug);
		
//		if (GUI.Button (new Rect (200,200,50,50),"SPACCA"))
//		{
//			//FB.Login("public_profile,email,user_friends,publish_actions", LoginCallback);
//			//FB.Login("publish_actions", LoginCallback);
//								FB.Feed(
//									toId: "",
//									link: "http://www.feltrinellieditore.it/opera/opera/chiedi-alla-luna/",
//									linkName: "Chiedi alla luna",
//									linkCaption: "",
//									linkDescription: "",
//									picture: "",
//									mediaSource: "",
//									actionName: "",
//									actionLink: "",
//									reference: "",
//									properties: null,
//									callback: Callback
//									);
//		}

		if (condividi)
		{
			if (GUI.Button(new Rect(Screen.width/2-125*SizeFactor,Screen.height/2-25*SizeFactor,250*SizeFactor,50*SizeFactor),"",condividiVideo))
			{
				stop = true;
				//movieTexture.pause();
												FB.Feed(
													toId: "",
													link: "http://www.feltrinellieditore.it/opera/opera/chiedi-alla-luna/",
													linkName: "Chiedi alla luna",
													linkCaption: "",
													linkDescription: "",
													picture: "",
													mediaSource: "",
													actionName: "",
													actionLink: "",
													reference: "",
													properties: null,
													callback: Callback
													);
				condividi = false;
			}
		}

		//if (!guiMovie)
			//GUI.DrawTexture(new Rect(0, 0, Screen.width/2, Screen.height/2), movieTexture.renderer.material.mainTexture);
//		GUI.DrawTextureWithTexCoords (new Rect(0, Screen.height/2, Screen.width/2, -Screen.height/2), movieTexture.renderer.material.mainTexture,
//			                              new Rect (0,0,1,1));
	}

	private void OnInitComplete()
	{
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown);
	}

	void LoginCallback(FBResult result)
	{
		if (result.Error != null)
			debug += " Error Response:\n" + result.Error;
		else if (!FB.IsLoggedIn)
		{
			debug += " Login cancelled by Player";
		}
		else
		{
			debug += " Login was successful!";
		}
	}

	protected void Callback(FBResult result)
	{
//		lastResponseTexture = null;
//		// Some platforms return the empty string instead of null.
//		if (!String.IsNullOrEmpty (result.Error))
//		{
//			lastResponse = "Error Response:\n" + result.Error;
//		}
//		else if (!String.IsNullOrEmpty (result.Text))
//		{
//			lastResponse = "Success Response:\n" + result.Text;
//		}
//		else if (result.Texture != null)
//		{
//			lastResponseTexture = result.Texture;
//			lastResponse = "Success Response: texture\n";
//		}
//		else
//		{
//			lastResponse = "Empty Response\n";
//		}
	}
}
