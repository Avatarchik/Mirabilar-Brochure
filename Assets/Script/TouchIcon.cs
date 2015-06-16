using UnityEngine;
using System.Collections;

using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class TouchIcon : MonoBehaviour
{

    public Texture textBirilli;
    public Texture textRisto;
    public Texture textMuseo;
    public Texture textShop;

    public GUIStyle exitStyle;
    public GUIStyle frecciaSx;
    public GUIStyle frecciaDx;
    public GUIStyle immGallery;
    public GUIStyle invio;
    public GUIStyle textField;
    public GUIStyle ticket;


    public Texture2D[] gallery;

    public Texture rectBack1;
    public Texture rectFront1;
    public Texture rectBack2;
    public Texture rectFront2;
    public Texture rectBack3;
    public Texture rectBack4;
	public Texture mailInviata;
	public Texture mailNonInviata;

    public Texture rectFrontMetro;
    public Texture rectFrontSconto;
    public Texture rectFrontMagazzino;
    public Texture sconto;

    public Texture museum;
    public Texture moderArt;
    public Texture recensioneMuseo;
	public Texture museumVideo;


    private GameObject iconRisto;
    private GameObject iconMuseo;
    private GameObject iconShop;

    public int changeGui = 0;
    private int countGallery = 0;
	private int countNextGallery = 0;

    private string stringMail = "";
    //private string error = "";

    private Rect rectGallery;
	private Rect cutGallery;
	private Rect rectNextImmGallery;
	private Rect cutNextGallery;
	private Rect rectNextImmDxGallery;
	private Rect rectVideoMuseo;

    private Vector2 position = Vector2.zero;

	#if UNITY_ANDROID
    private NaturalOrientation natOrient = new NaturalOrientation();
	#endif

    private bool anim = false;
    private bool isTablet = false;
	private bool invioAnimImm = false;
	private bool nonInviatoAnim = false;
	private bool selectImmGallery = false;
	private bool blockImmGallery = false;
	private bool nextImmGallery = false;
	private bool nextImmDxGallery = false;
	private bool viewImm = true;

	private GestCallBack callBack;

    // variables for ray tracing
    private RaycastHit hit;
    private LayerMask layerMask = 1 << 4;

    private bool selected = false;
    private string iconHitName;

    float SizeFactor = 0;
	private float xButtonSxMarginGallery = 0;
	private float xButtonDxMarginGallery = 0;

    // Use this for initialization
    void Start()
    {
        SizeFactor = GUIUtilities.SizeFactor;
		callBack = gameObject.GetComponent<GestCallBack> ();

		#if UNITY_ANDROID
        isTablet = natOrient.IsTablet();
		#endif

		xButtonSxMarginGallery = (isTablet) ? Screen.width / 2 - 390 * SizeFactor + 40 * SizeFactor
			: 20 * SizeFactor + 50 * SizeFactor;

		xButtonDxMarginGallery = (isTablet) ? Screen.width / 2 + 350 * SizeFactor : 
			Screen.width - 70 * SizeFactor;

        iconRisto = GameObject.Find("Risto");
        iconMuseo = GameObject.Find("Museo");
        iconShop = GameObject.Find("Shop");


        rectGallery = new Rect(Screen.width / 2 - 350 * SizeFactor, Screen.height / 2 - 175 * SizeFactor,
                                700 * SizeFactor, 390 * SizeFactor);

		cutGallery = new Rect (0,0,1,1);

		cutNextGallery = new Rect (0,0,0.01f,1);

		rectNextImmGallery = new Rect(Screen.width / 2 - 350 * SizeFactor, Screen.height / 2 - 175 * SizeFactor,
		                              7 * SizeFactor, 390 * SizeFactor);

		rectNextImmDxGallery = new Rect(Screen.width / 2 - 350 * SizeFactor + 700 * SizeFactor, Screen.height / 2 - 175 * SizeFactor,
		                                7 * SizeFactor, 390 * SizeFactor);

		rectVideoMuseo = new Rect (Screen.width / 2 - 350 * SizeFactor,
		                           Screen.height / 2 - 175 * SizeFactor,
		                           700 * SizeFactor,
		                           270 * SizeFactor);
    }

    // Update is called once per frame
    void Update()
    {

        if (callBack.cos==1)
        {
            if (Input.touchCount > 0)
                // if tracking, then evaluate touch point
                handleTouches();
            else
            {
                selected = false;
                iconHitName = "";
            }
        }

		if (changeGui == 1)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Began && rectVideoMuseo.Contains(Input.GetTouch(0).position))
				Handheld.PlayFullScreenMovie ("TurismoCultura/1913 - 'Dynamism of a Soccer Player' by Umberto Boccioni.mp4",Color.black,FullScreenMovieControlMode.CancelOnInput);
		}

        if (changeGui == 2)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && rectGallery.Contains(Input.GetTouch(0).position))
			{
                position = Input.GetTouch(0).position;
				selectImmGallery = true;
			}

            else if (Input.GetTouch(0).phase == TouchPhase.Moved 
			         && selectImmGallery )
			{
				//rectGallery.x += (Input.GetTouch(0).position.x - position.x) / 5  ;

				if (rectNextImmGallery.width >= 700*SizeFactor ||
				    rectNextImmDxGallery.width <= -700*SizeFactor)
					viewImm = false;

				if (rectGallery.x >= xButtonSxMarginGallery && rectGallery.xMax <= xButtonDxMarginGallery
				    )
					rectGallery = new Rect (rectGallery.x+Input.GetTouch(0).deltaPosition.x *2,
				                        rectGallery.y,
										rectGallery.width,rectGallery.height
				     ); 

				if (rectGallery.x <= xButtonSxMarginGallery && rectGallery.width > 10 
				    && cutGallery.width > 0)
				{
					rectGallery.width += Input.GetTouch(0).deltaPosition.x * 5;
					cutGallery.width += Input.GetTouch(0).deltaPosition.x/200;

					if (cutGallery.width >= 1)
					{
						cutGallery.width = 1;
						rectGallery.width = 700 * SizeFactor;
						rectGallery.x = xButtonSxMarginGallery + 1;
					}

				}



				if (rectGallery.xMax >= xButtonDxMarginGallery)
					blockImmGallery = true;

				if (blockImmGallery && Input.GetTouch(0).deltaPosition.x < 0)
				{
					rectGallery.width -= Input.GetTouch(0).deltaPosition.x * 5;
					cutGallery.width -= Input.GetTouch(0).deltaPosition.x/200;
					rectGallery.x += Input.GetTouch(0).deltaPosition.x *2;

					if (cutGallery.width >= 1 && rectGallery.width >= 700 * SizeFactor)
					{
						cutGallery.width = 1;
						rectGallery.width = 700 * SizeFactor;
						//rectGallery.x = xButtonDxMarginGallery - 701 * SizeFactor;
						blockImmGallery = false;
					}
				}

				if (rectGallery.xMax >= xButtonDxMarginGallery && rectGallery.width > 0 
				    && cutGallery.width > 0 && Input.GetTouch(0).deltaPosition.x > 0 )
				{
					rectGallery.width -= Input.GetTouch(0).deltaPosition.x * 5;
					cutGallery.width -= Input.GetTouch(0).deltaPosition.x/200;
					//rectGallery.x += Input.GetTouch(0).deltaPosition.x *2;

//					if (cutGallery.width >= 1)
//					{
//						cutGallery.width = 1;
//						rectGallery.width = 700 * SizeFactor;
//						rectGallery.x = xButtonDxMarginGallery - 1;
//						blockImmGallery = false;
//					}
					
				}





					if (/*position.x - Input.GetTouch(0).position.x < -100 */
				    	rectGallery.x >	Screen.width / 2 - 350 * SizeFactor + 100 * SizeFactor)
					{
						
//						if ( (cutNextGallery.width >= 1 ||
//						         rectNextImmGallery.width >= 700 * SizeFactor)
//					    && Input.GetTouch(0).deltaPosition.x < 0)
//						{
////							rectNextImmGallery.width = 700 * SizeFactor;
////							cutNextGallery.width = 1;
//							rectNextImmGallery.width += Input.GetTouch(0).deltaPosition.x * 5;
//								cutNextGallery.width += Input.GetTouch(0).deltaPosition.x/200;
//						}

						nextImmGallery = true;
						//rectNextImmGallery.xMax = xButtonDxMarginGallery - 1;
						if ( rectNextImmGallery.width <= 700 * SizeFactor 
						    && cutNextGallery.width <= 1 )
						{
								rectNextImmGallery.width += Input.GetTouch(0).deltaPosition.x * 4 * SizeFactor;
								cutNextGallery.width += Input.GetTouch(0).deltaPosition.x/200;
						}

						if (rectNextImmGallery.width <= 0 * SizeFactor 
						    && cutNextGallery.width <= 0)
							nextImmGallery = false;

						if (countGallery == 0)
							countNextGallery = gallery.Length - 1;
						else
							countNextGallery = countGallery - 1;
					}
								
					else 
						nextImmGallery = false;

				if (/*position.x - Input.GetTouch(0).position.x < -100 */
				    rectGallery.xMax <	Screen.width / 2 - 350 * SizeFactor + 600 * SizeFactor 
				    )
				{
					
//					if ( (cutNextGallery.width <= -1 ||
//					      rectNextImmDxGallery.width <= -700 * SizeFactor)
//					    && Input.GetTouch(0).deltaPosition.x > 0)
//					{
//						//							rectNextImmGallery.width = 700 * SizeFactor;
//						//							cutNextGallery.width = 1;
//						rectNextImmGallery.width += Input.GetTouch(0).deltaPosition.x * 5;
//						cutNextGallery.width += Input.GetTouch(0).deltaPosition.x/200;
//					}
					
					nextImmDxGallery = true;
					//rectNextImmGallery.xMax = xButtonDxMarginGallery - 1;
					if ( rectNextImmDxGallery.width >= -700 * SizeFactor 
					    && cutNextGallery.width >= -1 )
					{
						rectNextImmDxGallery.width += Input.GetTouch(0).deltaPosition.x * 4 * SizeFactor;
						cutNextGallery.width += Input.GetTouch(0).deltaPosition.x/200;
					}
					
//					if (rectNextImmDxGallery.width <= 0 * SizeFactor 
//					    && cutNextGallery.width <= 0)
//						nextImmDxGallery = false;
					
					if (countGallery == gallery.Length - 1)
						countNextGallery = 0;
					else
						countNextGallery = countGallery + 1;
				}
				
				else 
					nextImmDxGallery = false;



			}

			else if (Input.GetTouch(0).phase == TouchPhase.Ended && selectImmGallery)
			{
				selectImmGallery = false;
				nextImmGallery = false;
				nextImmDxGallery = false;
				viewImm = true;

                if (position.x - Input.GetTouch(0).position.x > 150)
                {
                    if (countGallery == gallery.Length - 1)
                    {
                        countGallery = 0;

                        //immGallery.normal.background = gallery[countGallery];
                    }
                    else
                    {
                        countGallery++;

                        //immGallery.normal.background = gallery[countGallery];
                    }
                }
                else if (position.x - Input.GetTouch(0).position.x < -150)
                {
                    if (countGallery == 0)
                    {
                        countGallery = gallery.Length - 1;

                        //immGallery.normal.background = gallery[countGallery];
                    }
                    else
                    {
                        countGallery--;

                        //immGallery.normal.background = gallery[countGallery];
                    }
                }
				rectGallery = new Rect(Screen.width / 2 - 350 * SizeFactor, Screen.height / 2 - 175 * SizeFactor,
				                       700 * SizeFactor, 390 * SizeFactor);
				cutGallery = new Rect (0,0,1,1);
				blockImmGallery = false;

				cutNextGallery = new Rect (0,0,0.01f,1);
				
				rectNextImmGallery = new Rect(Screen.width / 2 - 350 * SizeFactor, Screen.height / 2 - 175 * SizeFactor,
				                              7 * SizeFactor, 390 * SizeFactor);

				rectNextImmDxGallery = new Rect(Screen.width / 2 - 350 * SizeFactor + 700 * SizeFactor, Screen.height / 2 - 175 * SizeFactor,
				                                7 * SizeFactor, 390 * SizeFactor);
			}
        }

    }

    private void handleTouches()
    {
        // if there's touch points and the phase is began-->try to select the geometry
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && changeGui == 0)
            iconHitName = selectGameObject(Input.GetTouch(0).position);

        // if the touch point is moving and metaio man is selected
        //		if(Input.GetTouch(0).phase == TouchPhase.Moved && selected)
        //			moveGameObject(Input.GetTouch(0).position);
        else if (Input.GetTouch(0).phase == TouchPhase.Ended && selected && !anim && changeGui == 0)
        // deselect metaio man if touch has ended
        {

            if (iconHitName == iconMuseo.name)
            {
                StartCoroutine(animBirillo(iconMuseo));
                changeGui = 1;

            }

            else if (iconHitName == iconRisto.name)
            {
                StartCoroutine(animBirillo(iconRisto));
                changeGui = 2;

            }

            else if (iconHitName == iconShop.name)
            {
                StartCoroutine(animBirillo(iconShop));
                changeGui = 3;

            }

            selected = false;
        }
    }

    public string selectGameObject(Vector2 position)
    {
        // get a ray from the touch point
        Ray ray = Camera.main.ScreenPointToRay(position);

        // layer mask, metaio man is on layer 4
        layerMask = 1 << 4;

        // cast a ray on layer 4, if metaio man has been hit
        if (Physics.Raycast(ray, out hit, 5000, layerMask) && hit.collider.gameObject.name == "Risto"
            || hit.collider.gameObject.name == "Museo" || hit.collider.gameObject.name == "Shop")
        {
            // metaio man is touched --> select it
            selected = true;

            // swith to layer 8, find the initial touch point on the plane and use it as a reference
            layerMask = 1 << 8;
            //			if (Physics.Raycast(ray, out hit, 5000, layerMask))
            //			{
            //				// record the offset of the touch point and the object position point
            //				//offset = hit.point - metaioMan.transform.position;
            //			}
            return hit.collider.gameObject.name;
        }
        else
        {
            // not hit, don't select
            selected = false;
            return "";
        }
    }

    private IEnumerator animBirillo(GameObject iconHitAnim)
    {

        anim = true;

        iconShop.renderer.material.mainTexture = textShop;
//        iconShop.transform.localScale = new Vector3(8f, 1f, 8f);
        iconMuseo.renderer.material.mainTexture = textMuseo;
//        iconMuseo.transform.localScale = new Vector3(8f, 1f, 8f);
       iconRisto.renderer.material.mainTexture = textRisto;
//        iconRisto.transform.localScale = new Vector3(8f, 1f, 8f);

        iconHitAnim.renderer.material.mainTexture = textBirilli;
//        iconHitAnim.transform.localScale = new Vector3(6f, 1f, 1f);
//        yield return new WaitForSeconds(0.04f);
//        iconHitAnim.transform.localScale = new Vector3(6f, 1f, 2f);
//        yield return new WaitForSeconds(0.04f);
//        iconHitAnim.transform.localScale = new Vector3(6f, 1f, 3f);
//        yield return new WaitForSeconds(0.04f);
//        iconHitAnim.transform.localScale = new Vector3(6f, 1f, 4f);
//        yield return new WaitForSeconds(0.04f);
//        iconHitAnim.transform.localScale = new Vector3(6f, 1f, 5f);
//        yield return new WaitForSeconds(0.04f);
//        iconHitAnim.transform.localScale = new Vector3(6f, 1f, 6f);
//        yield return new WaitForSeconds(0.04f);
//        iconHitAnim.transform.localScale = new Vector3(6f, 1f, 7f);
//        yield return new WaitForSeconds(0.04f);
//        iconHitAnim.transform.localScale = new Vector3(6f, 1f, 8f);
//        yield return new WaitForSeconds(0.04f);
//        iconHitAnim.transform.localScale = new Vector3(6f, 1f, 9f);
//        yield return new WaitForSeconds(0.04f);
//        iconHitAnim.transform.localScale = new Vector3(7f, 1f, 9f);
//        yield return new WaitForSeconds(0.1f);
//        iconHitAnim.transform.localScale = new Vector3(8f, 1f, 9f);
//        yield return new WaitForSeconds(0.1f);
//        iconHitAnim.transform.localScale = new Vector3(7f, 1f, 9f);
//        yield return new WaitForSeconds(0.1f);
//        iconHitAnim.transform.localScale = new Vector3(6f, 1f, 9f);

        anim = false;

        yield return 0;
    }

	private IEnumerator AnimInvioMail ()
	{
		yield return new WaitForSeconds(3f);
		invioAnimImm = false;
	}

	private IEnumerator AnimNonInvioMail ()
	{
		yield return new WaitForSeconds(3f);
		nonInviatoAnim = false;
	}

    public void OnGUI()
    {
//		GUI.DrawTextureWithTexCoords (new Rect(Screen.width / 2 - 350 * SizeFactor, Screen.height / 2 - 180 * SizeFactor,
//		                                       700 * SizeFactor, 400 * SizeFactor),gallery[0],new Rect (0,0,1,1));
	
//        GUI.TextField(new Rect(Screen.width - 200,
//                                                           0,
//                                                           200,
//                                                           200), "Selected:" + selected + "  " + selected.ToString()
//                      + " " + iconHitName + "   " + error + " " + SizeFactor
//                                                  , 100);
        if (isTablet)
        {
			IsTabletGUI ();        
        }

		else if (!isTablet)
		{
			IsPhoneGUI ();
		}

		if (invioAnimImm && changeGui != 0) 
		{
			GUI.DrawTexture(new Rect(Screen.width / 2 - 400 * SizeFactor,
			                         Screen.height / 2 - 40 * SizeFactor,
			                         800 * SizeFactor,
			                         80 * SizeFactor), mailInviata);
		}

		if (nonInviatoAnim && changeGui != 0) 
		{
			GUI.DrawTexture(new Rect(Screen.width / 2 - 400 * SizeFactor,
			                         Screen.height / 2 - 40 * SizeFactor,
			                         800 * SizeFactor,
			                         80 * SizeFactor), mailNonInviata);
		}

    }

	private void IsTabletGUI () 
	{

		if (changeGui == 2)
		{
			GUI.DrawTexture(new Rect(Screen.width / 2 - 400 * SizeFactor,
			                         Screen.height / 2 - 180 * SizeFactor,
			                         800 * SizeFactor,
			                         400 * SizeFactor), rectBack3);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 400 * SizeFactor,
			                         30 * SizeFactor,
			                         800 * SizeFactor,
			                         80 * SizeFactor), rectBack1);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 400 * SizeFactor,
			                         Screen.height - 72 * SizeFactor,
			                         800 * SizeFactor,
			                         40 * SizeFactor), rectBack2);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 350 * SizeFactor,
			                         50 * SizeFactor,
			                         700 * SizeFactor,
			                         50 * SizeFactor), rectFront1);
			
			if (GUI.Button(new Rect(Screen.width / 2 - 390 * SizeFactor,
			                        35 * SizeFactor,
			                        40 * SizeFactor,
			                        40 * SizeFactor), "", exitStyle))
			{
				changeGui = 0;
			}
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 380 * SizeFactor,
			                         Screen.height - 65 * SizeFactor,
			                         400 * SizeFactor,
			                         25 * SizeFactor), rectFront2);
			
			//GUI.Box(rectGallery, "", immGallery);

			if (viewImm)
				GUI.DrawTextureWithTexCoords(rectGallery,gallery[countGallery], cutGallery);
			
			if (nextImmGallery)
				GUI.DrawTextureWithTexCoords(rectNextImmGallery,gallery[countNextGallery], cutNextGallery);
			
			if (nextImmDxGallery)
				GUI.DrawTextureWithTexCoords(rectNextImmDxGallery,gallery[countNextGallery], cutNextGallery);
			
			if (GUI.Button(new Rect(Screen.width / 2 - 390 * SizeFactor,
			                        Screen.height / 2,
			                        40 * SizeFactor,
			                        40 * SizeFactor), "", frecciaSx))
			{
				if (countGallery == 0)
				{
					countGallery = gallery.Length - 1;
					immGallery.normal.background = gallery[countGallery];
				}
				else
				{
					countGallery--;
					immGallery.normal.background = gallery[countGallery];
				}
			}
			
			if (GUI.Button(new Rect(Screen.width / 2 + 350 * SizeFactor,
			                        Screen.height / 2,
			                        40 * SizeFactor,
			                        40 * SizeFactor), "", frecciaDx))
			{
				if (countGallery == gallery.Length - 1)
				{
					countGallery = 0;
					immGallery.normal.background = gallery[countGallery];
				}
				else
				{
					countGallery++;
					immGallery.normal.background = gallery[countGallery];
				}
			}
			
		}
		
		if (changeGui == 3)
		{
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 400 * SizeFactor,
			                         Screen.height / 2 - 180 * SizeFactor,
			                         800 * SizeFactor,
			                         320 * SizeFactor), rectBack3);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 400 * SizeFactor,
			                         30 * SizeFactor,
			                         800 * SizeFactor,
			                         80 * SizeFactor), rectBack1);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 400 * SizeFactor,
			                         Screen.height - 72 * SizeFactor,
			                         800 * SizeFactor,
			                         40 * SizeFactor), rectBack2);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 350 * SizeFactor,
			                         50 * SizeFactor,
			                         700 * SizeFactor,
			                         50 * SizeFactor), rectFrontMetro);
			
			if (GUI.Button(new Rect(Screen.width / 2 - 390 * SizeFactor,
			                        35 * SizeFactor,
			                        40 * SizeFactor,
			                        40 * SizeFactor), "", exitStyle))
			{
				changeGui = 0;
			}
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 380 * SizeFactor,
			                         Screen.height - 65 * SizeFactor,
			                         500 * SizeFactor,
			                         30 * SizeFactor), rectFrontMagazzino);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 275 * SizeFactor,
			                         Screen.height / 2 - 170 * SizeFactor,
			                         550 * SizeFactor,
			                         300 * SizeFactor), sconto);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 400 * SizeFactor,
			                         Screen.height - 150 * SizeFactor,
			                         800 * SizeFactor,
			                         70 * SizeFactor), rectBack1);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 380 * SizeFactor,
			                         Screen.height - 145 * SizeFactor,
			                         650 * SizeFactor,
			                         40 * SizeFactor), rectFrontSconto);
			
			stringMail = GUI.TextField(new Rect(Screen.width / 2 - 270 * SizeFactor,
			                                    Screen.height - 120 * SizeFactor,
			                                    350 * SizeFactor,
			                                    30 * SizeFactor), stringMail, textField);
			
			if (GUI.Button(new Rect(Screen.width / 2 + 100 * SizeFactor,
			                        Screen.height - 120 * SizeFactor,
			                        130 * SizeFactor,
			                        30 * SizeFactor), "", invio))
			{
				//Application.OpenURL("mailto:"+stringMail+"?subject=Email&body=from Unity");
				
				try
				{
//					MailMessage mail = new MailMessage();
//					mail.From = new MailAddress(stringMail);
//					mail.To.Add(stringMail);
//					mail.Subject = "Test Mail";
//					mail.Body = "This is for testing SMTP mail from GMAIL";
//					SmtpClient smtpServer = new SmtpClient("smtp.aruba.it");
//					smtpServer.Port = 465;
//					smtpServer.Credentials = new System.Net.NetworkCredential("coupon@mirabilar.com", "4tVC6jqk5ytt") as ICredentialsByHost;
//					smtpServer.EnableSsl = true;
//					ServicePointManager.ServerCertificateValidationCallback =
//						delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
//					{
//						return true;
//					};
//					smtpServer.Send(mail);

					System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
					message.To.Add(stringMail);
					message.Subject = "Mirabilar Coupon";
					message.From = new System.Net.Mail.MailAddress("coupon@mirabilar.com");
					message.Body = BodyMirabilarMail.bodyMirabilar();
					message.IsBodyHtml=true;
					System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.mirabilar.com");
					smtp.Port = 25;
					// if you need user/pass login
					smtp.Credentials  = new NetworkCredential("coupon@mirabilar.com","4tVC6jqk5ytt");
					smtp.Send(message);

					invioAnimImm = true;
					StartCoroutine(AnimInvioMail());
				}
				catch (System.Exception e)
				{
					//error = e.ToString();
					invioAnimImm = false;
					nonInviatoAnim = true;
					StartCoroutine(AnimNonInvioMail());
				}
				
			}
			
		}
		
		if (changeGui == 1)
		{
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 400 * SizeFactor,
			                         Screen.height / 2 - 180 * SizeFactor,
			                         800 * SizeFactor,
			                         280 * SizeFactor), rectBack3);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 400 * SizeFactor,
			                         30 * SizeFactor,
			                         800 * SizeFactor,
			                         80 * SizeFactor), rectBack1);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 400 * SizeFactor,
			                         Screen.height - 72 * SizeFactor,
			                         800 * SizeFactor,
			                         40 * SizeFactor), rectBack2);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 300 * SizeFactor,
			                         40 * SizeFactor,
			                         600 * SizeFactor,
			                         60 * SizeFactor), museum);

			GUI.DrawTexture(rectVideoMuseo, museumVideo);
			
			if (GUI.Button(new Rect(Screen.width / 2 - 390 * SizeFactor,
			                        35 * SizeFactor,
			                        40 * SizeFactor,
			                        40 * SizeFactor), "", exitStyle))
			{
				changeGui = 0;
			}
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 380 * SizeFactor,
			                         Screen.height - 65 * SizeFactor,
			                         600 * SizeFactor,
			                         55 * SizeFactor), recensioneMuseo);
			
			
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 400 * SizeFactor,
			                         Screen.height - 190 * SizeFactor,
			                         800 * SizeFactor,
			                         110 * SizeFactor), rectBack4);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 380 * SizeFactor,
			                         Screen.height - 180 * SizeFactor,
			                         750 * SizeFactor,
			                         90 * SizeFactor), moderArt);
			
			
			if (GUI.Button(new Rect(Screen.width / 2 - 115 * SizeFactor,
			                        Screen.height - 110 * SizeFactor,
			                        230 * SizeFactor,
			                        25 * SizeFactor), "", ticket))
			{
				Application.OpenURL("http://www.moma.org/visit/calendar/tickets");
			}
		}

	}
	//PHONE GUI
	private void IsPhoneGUI ()
	{
		if (changeGui == 2)
		{
			GUI.DrawTexture(new Rect(10 * SizeFactor,
			                         Screen.height / 2 - 200 * SizeFactor,
			                         Screen.width  - 20 * SizeFactor,
			                         440 * SizeFactor), rectBack3);
			
			GUI.DrawTexture(new Rect(10 * SizeFactor,
			                         10 * SizeFactor,
			                         Screen.width  - 20 * SizeFactor,
			                         80 * SizeFactor), rectBack1);
			
			GUI.DrawTexture(new Rect(10 * SizeFactor,
			                         Screen.height - 50 * SizeFactor,
			                         Screen.width  - 20 * SizeFactor,
			                         40 * SizeFactor), rectBack2);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 400 * SizeFactor,
			                         20 * SizeFactor,
			                         800 * SizeFactor,
			                         50 * SizeFactor), rectFront1);
			
			if (GUI.Button(new Rect(20 * SizeFactor,
			                        25 * SizeFactor,
			                        50 * SizeFactor,
			                        50 * SizeFactor), "", exitStyle))
			{
				changeGui = 0;
			}
			
			GUI.DrawTexture(new Rect(20 * SizeFactor,
			                         Screen.height - 45 * SizeFactor,
			                         400 * SizeFactor,
			                         25 * SizeFactor), rectFront2);
			
			//GUI.Box(rectGallery, "", immGallery);
			if (viewImm)
				GUI.DrawTextureWithTexCoords(rectGallery,gallery[countGallery], cutGallery);

			if (nextImmGallery)
				GUI.DrawTextureWithTexCoords(rectNextImmGallery,gallery[countNextGallery], cutNextGallery);

			if (nextImmDxGallery)
				GUI.DrawTextureWithTexCoords(rectNextImmDxGallery,gallery[countNextGallery], cutNextGallery);
			
			if (GUI.Button(new Rect(20 * SizeFactor,
			                        Screen.height / 2,
			                        50 * SizeFactor,
			                        50 * SizeFactor), "", frecciaSx))
			{
				if (countGallery == 0)
				{
					countGallery = gallery.Length - 1;
					immGallery.normal.background = gallery[countGallery];
				}
				else
				{
					countGallery--;
					immGallery.normal.background = gallery[countGallery];
				}
			}
			
			if (GUI.Button(new Rect(Screen.width - 70 * SizeFactor,
			                        Screen.height / 2,
			                        50 * SizeFactor,
			                        50 * SizeFactor), "", frecciaDx))
			{
				if (countGallery == gallery.Length - 1)
				{
					countGallery = 0;
					immGallery.normal.background = gallery[countGallery];
				}
				else
				{
					countGallery++;
					immGallery.normal.background = gallery[countGallery];
				}
			}
			
		}
		
		if (changeGui == 3)
		{
			
			GUI.DrawTexture(new Rect(10 * SizeFactor,
			                         Screen.height / 2 - 200 * SizeFactor,
			                         Screen.width  - 20 * SizeFactor,
			                         360 * SizeFactor), rectBack3);
			
			GUI.DrawTexture(new Rect(10 * SizeFactor,
			                         10 * SizeFactor,
			                         Screen.width  - 20 * SizeFactor,
			                         80 * SizeFactor), rectBack1);
			
			GUI.DrawTexture(new Rect(10 * SizeFactor,
			                         Screen.height - 50 * SizeFactor,
			                         Screen.width  - 20 * SizeFactor,
			                         40 * SizeFactor), rectBack2);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 400 * SizeFactor,
			                         20 * SizeFactor,
			                         800 * SizeFactor,
			                         50 * SizeFactor), rectFrontMetro);
			
			if (GUI.Button(new Rect(20 * SizeFactor,
			                        25 * SizeFactor,
			                        50 * SizeFactor,
			                        50 * SizeFactor), "", exitStyle))
			{
				changeGui = 0;
			}
			
			GUI.DrawTexture(new Rect(20 * SizeFactor,
			                         Screen.height - 43 * SizeFactor,
			                         600 * SizeFactor,
			                         30 * SizeFactor), rectFrontMagazzino);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 350 * SizeFactor,
			                         Screen.height / 2 - 195 * SizeFactor,
			                         700 * SizeFactor,
			                         350 * SizeFactor), sconto);
			
			GUI.DrawTexture(new Rect(10 * SizeFactor,
			                         Screen.height - 130 * SizeFactor,
			                         Screen.width  - 20 * SizeFactor,
			                         70 * SizeFactor), rectBack1);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 335 * SizeFactor,
			                         Screen.height - 130 * SizeFactor,
			                         650 * SizeFactor,
			                         50 * SizeFactor), rectFrontSconto);
			
			stringMail = GUI.TextField(new Rect(Screen.width / 2 - 260 * SizeFactor,
			                                    Screen.height - 100 * SizeFactor,
			                                    350 * SizeFactor,
			                                    35 * SizeFactor), stringMail, textField);
			
			if (GUI.Button(new Rect(Screen.width / 2 + 110 * SizeFactor,
			                        Screen.height - 100 * SizeFactor,
			                        150 * SizeFactor,
			                        35 * SizeFactor), "", invio))
			{
				//Application.OpenURL("mailto:"+stringMail+"?subject=Email&body=from Unity");
				
				try
				{
//					MailMessage mail = new MailMessage();
//					mail.From = new MailAddress(stringMail);
//					mail.To.Add(stringMail);
//					mail.Subject = "Test Mail";
//					mail.Body = "This is for testing SMTP mail from GMAIL";
//					SmtpClient smtpServer = new SmtpClient("smtp.aruba.it");
//					smtpServer.Port = 465;
//					smtpServer.Credentials = new System.Net.NetworkCredential("coupon@mirabilar.com", "4tVC6jqk5ytt") as ICredentialsByHost;
//					smtpServer.EnableSsl = true;
//					ServicePointManager.ServerCertificateValidationCallback =
//						delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
//					{
//						return true;
//					};
//					smtpServer.Send(mail);

					System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
					message.To.Add(stringMail);
					message.Subject = "This is the Subject line";
					message.From = new System.Net.Mail.MailAddress("coupon@mirabilar.com");
					message.Body = "This is the message body";
					System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.mirabilar.com");
					smtp.Port = 25;
					// if you need user/pass login
					smtp.Credentials  = new NetworkCredential("coupon@mirabilar.com","4tVC6jqk5ytt");
					smtp.Send(message);

					invioAnimImm = true;
					StartCoroutine(AnimInvioMail());
				}
				catch (System.Exception e)
				{
					//error = e.ToString();
					invioAnimImm = false;
					nonInviatoAnim = true;
					StartCoroutine(AnimNonInvioMail());
				}
				
			}
			
		}
		
		if (changeGui == 1)
		{
			
			GUI.DrawTexture(new Rect(10 * SizeFactor,
			                         Screen.height / 2 - 200 * SizeFactor,
			                         Screen.width  - 20 * SizeFactor,
			                         320 * SizeFactor), rectBack3);
			
			GUI.DrawTexture(new Rect(10 * SizeFactor,
			                         10 * SizeFactor,
			                         Screen.width  - 20 * SizeFactor,
			                         80 * SizeFactor), rectBack1);
			
			GUI.DrawTexture(new Rect(10 * SizeFactor,
			                         Screen.height - 50 * SizeFactor,
			                         Screen.width  - 20 * SizeFactor,
			                         40 * SizeFactor), rectBack2);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 300 * SizeFactor,
			                         20 * SizeFactor,
			                         600 * SizeFactor,
			                         60 * SizeFactor), museum);

			GUI.DrawTexture(rectVideoMuseo, museumVideo);
			
			if (GUI.Button(new Rect(20 * SizeFactor,
			                        20 * SizeFactor,
			                        50 * SizeFactor,
			                        50 * SizeFactor), "", exitStyle))
			{
				changeGui = 0;
			}
			
			GUI.DrawTexture(new Rect(20 * SizeFactor,
			                         Screen.height - 45 * SizeFactor,
			                         600 * SizeFactor,
			                         55 * SizeFactor), recensioneMuseo);
			
			
			
			GUI.DrawTexture(new Rect(10 * SizeFactor,
			                         Screen.height - 170 * SizeFactor,
			                         Screen.width  - 20 * SizeFactor,
			                         110 * SizeFactor), rectBack4);
			
			GUI.DrawTexture(new Rect(Screen.width / 2 - 380 * SizeFactor,
			                         Screen.height - 160 * SizeFactor,
			                         800 * SizeFactor,
			                         90 * SizeFactor), moderArt);
			
			
			if (GUI.Button(new Rect(Screen.width / 2 - 115 * SizeFactor,
			                        Screen.height - 90 * SizeFactor,
			                        230 * SizeFactor,
			                        25 * SizeFactor), "", ticket))
			{
				Application.OpenURL("http://www.moma.org/visit/calendar/tickets");
			}
		}
	}

}
