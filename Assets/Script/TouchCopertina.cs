using UnityEngine;
using System.Collections;

public class TouchCopertina : MonoBehaviour {

	public Texture txFacebook;
	public Texture txGoogle;
	public Texture txSito;
	private Texture comodo;

	private GameObject logo;
	private GameObject facebook;
	private GameObject google;
	private GameObject sito;

	private GestCallBack callBack;

	private string nameTouch = "";
	private string debug = "";

	private rotate logoRot;

	// variables for ray tracing
	private RaycastHit hit;
	private LayerMask layerMask = 1<<13;
	
	private bool selected = false;

	private Vector2 posXY = Vector2.zero;

	// Use this for initialization
	void Start () {
		callBack = GameObject.Find ("GUI").GetComponent<GestCallBack> ();
		logo = GameObject.Find ("Logo");
		facebook = GameObject.Find ("Facebook");
		google = GameObject.Find ("Google");
		sito = GameObject.Find ("Sito");

		logoRot = logo.GetComponent<rotate>();
	}
	
	// Update is called once per frame
	void Update () {
		if ((logo.activeSelf || facebook.activeSelf || google.activeSelf || sito.activeSelf)&&  callBack.cos == 2)
		{
			if (Input.touchCount > 0)
				// if tracking, then evaluate touch point
				handleTouches();
			else
				selected = false;
		}
	}

	private void handleTouches()
	{
		// if there's touch points and the phase is began-->try to select the geometry
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			nameTouch = selectGameObject(Input.GetTouch(0).position);

			if (selected)
			{
				if (nameTouch == "Facebook")
				{
					comodo = facebook.renderer.material.mainTexture;
					facebook.renderer.material.mainTexture = txFacebook;
				}

				else if (nameTouch == "Google")
				{
					comodo = google.renderer.material.mainTexture;
					google.renderer.material.mainTexture = txGoogle;
				}

				else if (nameTouch == "Sito")
				{
					comodo = sito.renderer.material.mainTexture;
					sito.renderer.material.mainTexture = txSito;
				}
				else if (nameTouch == "Logo1" || nameTouch == "Logo2")
					logoRot.setSpeed (0);

				posXY = Input.GetTouch (0).position;

			}
		}
		
		// if the touch point is moving and metaio man is selected
		//if(Input.GetTouch(0).phase == TouchPhase.Moved && selected)
			//moveGameObject(Input.GetTouch(0).position);
		if(Input.GetTouch(0).phase == TouchPhase.Ended )
		{
			if (nameTouch == "Facebook")
			{
				facebook.renderer.material.mainTexture = comodo;
				Application.OpenURL("http://www.facebook.com/mirabilar");
			}
			
			else if (nameTouch == "Google")
			{
				google.renderer.material.mainTexture = comodo;
				Application.OpenURL("http://www.Google.com/+Mirabilar");
			}
			
			else if (nameTouch == "Sito")
			{
				sito.renderer.material.mainTexture = comodo;
				Application.OpenURL("http://www.mirabilar.com");
			}

			if (Input.GetTouch(0).position.x < posXY.x && nameTouch == "Logo1" || nameTouch == "Logo2"){
				logoRot.setY(1f);
				StartCoroutine(turns());
			}
			
			if (Input.GetTouch(0).position.x > posXY.x && nameTouch == "Logo1" || nameTouch == "Logo2"){
				logoRot.setY(-1f);
				StartCoroutine(turns());
			}

			selected = false;
		}
	}

	IEnumerator turns()
	{
		logoRot.setSpeed (150f);
		
		while(logoRot.getSpeed() > 20f)
		{
			logoRot.setSpeed (logoRot.getSpeed() - 1);
			yield return new WaitForSeconds(0.13f);
		}
	}

	public string selectGameObject(Vector2 position)
	{
		// get a ray from the touch point
		Ray ray = Camera.main.ScreenPointToRay(position);
		
		// layer mask, metaio man is on layer 4
		layerMask = 1<<13;
		
		// cast a ray on layer 4, if metaio man has been hit
		if (Physics.Raycast(ray, out hit, 5000, layerMask))
		{
			// metaio man is touched --> select it
			selected = true;
			
			// swith to layer 8, find the initial touch point on the plane and use it as a reference
			//layerMask = 1<<8;
//			if (Physics.Raycast(ray, out hit, 5000, layerMask))
//			{
//				// record the offset of the touch point and the object position point
//				//offset = hit.point - metaioMan.transform.position;
//			}
			debug = hit.collider.gameObject.name;
			return hit.collider.gameObject.name;
		}
		else
		{
			// not hit, don't select

			selected = false;
			return "";
		}
	}

	void OnGUI() 
	{
		//GUI.Label (new Rect (200, 100, 50, 50), "" + selected + "   " + debug + "  " + nameTouch);
	}

}
