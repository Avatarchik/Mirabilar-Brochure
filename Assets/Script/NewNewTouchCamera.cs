using UnityEngine;
using System.Collections;

public class NewNewTouchCamera : MonoBehaviour {

	private Vector2 worldStartPoint;
	
	void Update () {
		if (!Input.gyro.enabled)
		
		// only work with one touch
		if (Input.touchCount == 1) {
			Touch currentTouch = Input.GetTouch(0);
			
			if (currentTouch.phase == TouchPhase.Began) {
				//this.worldStartPoint = this.getWorldPoint(currentTouch.position);
			}
			
			if (currentTouch.phase == TouchPhase.Moved) {
				//Vector2 worldDelta = this.getWorldPoint(currentTouch.position) - this.worldStartPoint;

				gameObject.transform.eulerAngles= new Vector3(
					gameObject.transform.eulerAngles.x + (currentTouch.deltaPosition.y*0.6f),
					gameObject.transform.eulerAngles.y-(currentTouch.deltaPosition.x*0.6f),
					0
					);
			}
		}

	}
	
	// convert screen point to world point
	private Vector2 getWorldPoint (Vector2 screenPoint) {
		RaycastHit hit;
		Physics.Raycast(Camera.main.ScreenPointToRay(screenPoint), out hit);
		return hit.point;
	}
		
	void OnGUI()
	{
		//GUI.Box (new Rect (10,10,300,500),-worldDelta.x+"");
	}

	}

