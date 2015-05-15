using UnityEngine;
using System.Collections;

public class TouchMarketing : MonoBehaviour {

	public int counter = 0;

	private GameObject lampada;
	private GameObject occhiali;
	private GameObject scarpa;
	private GameObject goStrike;

	private Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);
	private Vector3 memoryPosition = Vector3.zero;
	private Vector2 position = Vector2.zero;
	private GestCallBack callBack;
	// variables for ray tracing
	private RaycastHit hit;
	private LayerMask layerMask = 1<<12;
	
	private bool selected = false;
	private bool blockSwipe = false;
	private string debug = "";



	// Use this for initialization
	void Start () {
		callBack = GameObject.Find("GUI").GetComponent<GestCallBack> ();
		lampada = GameObject.Find ("Lampada Eclisse");
		occhiali = GameObject.Find ("Occhiali");
		scarpa = GameObject.Find ("Scarpa");
		occhiali.SetActive (false);
		scarpa.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
		if ((lampada.activeSelf || occhiali.activeSelf || scarpa.activeSelf)&&callBack.cos == 7)
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
			selectGameObject(Input.GetTouch(0).position);
			if (selected)
				position = Input.GetTouch(0).position;
		}

		if(Input.GetTouch(0).phase == TouchPhase.Moved && selected)
		{
			moveGameObject(Input.GetTouch(0).position);
			distanceSwipe();
		}

		// if the touch point is moving and metaio man is selected
		if(Input.GetTouch(0).phase == TouchPhase.Ended && selected)
		{
			swipe();
			goStrike.transform.localPosition = memoryPosition;
			//trasparenceGameObject(goStrike,0f);
			selected = false;
		}


	}

	private void swipe ()
	{
		if (position.x - Input.GetTouch(0).position.x > 200)
		{
			switch (counter) 
			{
				case 0:
					lampada.SetActive(false);
					scarpa.SetActive(true);
					occhiali.SetActive(false);
					counter++;
					break;
				case 1:
					lampada.SetActive(false);
					scarpa.SetActive(false);
					occhiali.SetActive(true);
					counter++;
					break;
				case 2:
					lampada.SetActive(true);
					scarpa.SetActive(false);
					occhiali.SetActive(false);
					counter = 0;
					break;
			}
		}
		else if (position.x - Input.GetTouch(0).position.x < -200)
		{
			switch (counter) 
			{
			case 0:
				lampada.SetActive(false);
				scarpa.SetActive(false);
				occhiali.SetActive(true);
				counter = 2;
				break;
			case 1:
				lampada.SetActive(true);
				scarpa.SetActive(false);
				occhiali.SetActive(false);
				counter--;
				break;
			case 2:
				lampada.SetActive(false);
				scarpa.SetActive(true);
				occhiali.SetActive(false);
				counter--;
				break;
			}
		}

	}

	private void distanceSwipe ()
	{
		if (position.x - Input.GetTouch(0).position.x > 200 && !blockSwipe)
		{
			switch (counter) 
			{
				case 0:			
					if (!scarpa.activeSelf)
					scarpa.SetActive(true) ;	
					blockSwipe = true;
					break;
				case 1:
					if (!occhiali.activeSelf)
					occhiali.SetActive(true);
					blockSwipe = true;
					break;
				case 2:
					if (!lampada.activeSelf)
					lampada.SetActive(true);
					blockSwipe = true;
					break;
			}
		}

		else if (position.x - Input.GetTouch(0).position.x < -200 && !blockSwipe)
		{
			switch (counter) 
			{
			case 0:
				if (!occhiali.activeSelf)
				occhiali.SetActive(true);
				blockSwipe = true;
				break;
			case 1:
				if (!lampada.activeSelf)
				lampada.SetActive(true);
				blockSwipe = true;
				break;
			case 2:
				if (!scarpa.activeSelf)
				scarpa.SetActive(true);
				blockSwipe = true;
				break;
			}
		}

		else if (position.x > Input.GetTouch(0).position.x  - 200 &&
		         position.x < Input.GetTouch(0).position.x  + 200)
		{
			switch (counter) 
			{
			case 0:
				scarpa.SetActive(false);
				occhiali.SetActive(false);
				//lampada.SetActive(true);
				break;
			case 1:
				occhiali.SetActive(false);
				lampada.SetActive(false);
				//scarpa.SetActive(true);
				break;
			case 2:
				lampada.SetActive(false);
				scarpa.SetActive(false);
				//occhiali.SetActive(true);
				break;
			}
			blockSwipe = false;
		}

	}
	

	private void selectGameObject(Vector2 position)
	{
		// get a ray from the touch point
		Ray ray = Camera.main.ScreenPointToRay(position);

		// layer mask, metaio man is on layer 4
		layerMask = 1<<12;
		
		// cast a ray on layer 4, if metaio man has been hit
		if (Physics.Raycast(ray, out hit, 5000, layerMask) && hit.collider.name != "Plane" )
		{
			goStrike = hit.collider.transform.parent.gameObject;
			memoryPosition = hit.collider.transform.localPosition;
			//trasparenceGameObject(goStrike,0.1f); //GUARDA
			debug = hit.collider.name;
			// metaio man is touched --> select it
			selected = true;
			
			// swith to layer 8, find the initial touch point on the plane and use it as a reference
			layerMask = 1<<8;
			if (Physics.Raycast(ray, out hit, 5000, layerMask))
			{
				// record the offset of the touch point and the object position point
				offset = hit.point - goStrike.transform.position;
			}
		}
		else
			// not hit, don't select
			selected = false;
	}

	public void moveGameObject(Vector2 position)
	{
		// cast a ray on layer 8 (the plane) to calculate the hit position of the plane
		Ray ray = Camera.main.ScreenPointToRay(position);
		layerMask = 1<<8;
		if(Physics.Raycast(ray, out hit, 5000, layerMask) && hit.collider.gameObject.name == "Plane")
		{
			// move the metaio man to the intersect of the ray and the plane, offset should be accounted for
			goStrike.transform.position = new Vector3 (hit.point.x - offset.x,
			                              goStrike.transform.position.y,
			                              goStrike.transform.position.z);
		}
	}

	public void trasparenceGameObject (GameObject go, float a)
	{
		//Transform[] ts = go.GetComponentsInChildren<Transform> ();



		foreach (Transform t in go.transform) 
		{
			//if (t.gameObject.activeSelf)

				try 
				{
					Color cl = t.gameObject.renderer.material.color;
					cl.a = a;
					t.gameObject.renderer.material.color = cl;
					//t.gameObject.renderer.material.shader = Shader.Find("Transparent/Diffuse");

				
			}
				catch
				{

				}
		}
	}



	public void FrecciaSinistra ()
	{
		switch (counter) 
		{
		case 0:
			lampada.SetActive(false);
			scarpa.SetActive(false);
			occhiali.SetActive(true);
			counter = 2;
			break;
		case 1:
			lampada.SetActive(true);
			scarpa.SetActive(false);
			occhiali.SetActive(false);
			counter--;
			break;
		case 2:
			lampada.SetActive(false);
			scarpa.SetActive(true);
			occhiali.SetActive(false);
			counter--;
			break;
		}
	}
	
	public void FrecciaDestra ()
	{
		switch (counter) 
		{
		case 0:
			lampada.SetActive(false);
			scarpa.SetActive(true);
			occhiali.SetActive(false);
			counter++;
			break;
		case 1:
			lampada.SetActive(false);
			scarpa.SetActive(false);
			occhiali.SetActive(true);
			counter++;
			break;
		case 2:
			lampada.SetActive(true);
			scarpa.SetActive(false);
			occhiali.SetActive(false);
			counter = 0;
			break;
		}
	}
	
	void OnGUI() 
	{
		//GUI.Label (new Rect (200, 100, 50, 50), "" + selected + "   " + debug);
	}
}
