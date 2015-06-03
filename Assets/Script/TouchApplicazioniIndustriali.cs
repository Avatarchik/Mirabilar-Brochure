using UnityEngine;
using System.Collections;

public class TouchApplicazioniIndustriali : MonoBehaviour {

	// variables for ray tracing
//	private RaycastHit hit;
//	private LayerMask layerMask = 1<<4;
//	
//	private bool selected = false;

	private Vector2 touchZeroPrevPos = Vector2.zero ;
	private Vector2 touchOnePrevPos = Vector2.zero;
	private float prevTouchDeltaMag = 0;
	private float touchDeltaMag = 0;
	private float deltaMagnitudeDiff = 0;
	private GestCallBack callBack;

	//private Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);

	// Use this for initialization
	void Start () {
	
		callBack = GameObject.Find("GUI").GetComponent<GestCallBack> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.touchCount == 2 && callBack.cos == 3)
		{
			touchZeroPrevPos = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;
			touchOnePrevPos = Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition;

			prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			touchDeltaMag = (Input.GetTouch(0).position - Input.GetTouch(1).position).magnitude;

			deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x + deltaMagnitudeDiff * -0.0001f,
			                                               gameObject.transform.localScale.y + deltaMagnitudeDiff * -0.0001f,
			                                               gameObject.transform.localScale.z + deltaMagnitudeDiff * -0.0001f);

		}

//		if (gameObject.activeSelf)
//		{
//			if (Input.touchCount > 0)
//				// if tracking, then evaluate touch point
//				handleTouches();
//			else
//			
//				selected = false;
//				
//			
//		}
	}

//	private void handleTouches()
//	{
//		// if there's touch points and the phase is began-->try to select the geometry
//		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
//			selectGameObject(Input.GetTouch(0).position);
//		
//		// if the touch point is moving and metaio man is selected
//		if(Input.GetTouch(0).phase == TouchPhase.Moved && selected && Input.touchCount > 1)
//			moveGameObject(Input.GetTouch(0).position);
//		else if(Input.GetTouch(0).phase == TouchPhase.Ended)
//		
//			// deselect metaio man if touch has ended
//			selected = false;
//
//		
//	}
//
//	public void selectGameObject(Vector2 position)
//	{
//		// get a ray from the touch point
//		Ray ray = Camera.main.ScreenPointToRay(position);
//		
//		// layer mask, metaio man is on layer 4
//		layerMask = 1<<4;
//		
//		// cast a ray on layer 4, if metaio man has been hit
//		if (Physics.Raycast(ray, out hit, 5000, layerMask) )
//		{
//			// metaio man is touched --> select it
//			selected = true;
//			
//			// swith to layer 8, find the initial touch point on the plane and use it as a reference
//			layerMask = 1<<8;
//			if (Physics.Raycast(ray, out hit, 5000, layerMask))
//			{
//				// record the offset of the touch point and the object position point
//				//offset = hit.point - metaioMan.transform.position;
//			}
//		}
//		else
//			// not hit, don't select
//			selected = false;
//	}
//
//	public void moveGameObject(Vector2 position)
//	{
//		// cast a ray on layer 8 (the plane) to calculate the hit position of the plane
//		Ray ray = Camera.main.ScreenPointToRay(position);
//		layerMask = 1<<8;
//		if(Physics.Raycast(ray, out hit, 5000, layerMask) && hit.collider.gameObject.name == "Plane")
//		{
//			// move the metaio man to the intersect of the ray and the plane, offset should be accounted for
//			//metaioMan.transform.position = hit.point - offset;
//			gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x + deltaMagnitudeDiff * 100f,
//			                                               gameObject.transform.localScale.y + deltaMagnitudeDiff * 100f,
//			                                               gameObject.transform.localScale.z + deltaMagnitudeDiff * 100f);
//		}
//	}

}
