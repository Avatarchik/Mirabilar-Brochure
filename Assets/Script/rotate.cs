using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {
	
	public float  rotation_speed = 200;  
	public Vector3 direction = 	Vector3.forward; 
	private bool _stopRotation = false; 
	// Use this for initialization
	void Start () {
		transform.Rotate(direction * Random.Range(0f,120f));
		StartCoroutine(rotating()); 
	}
	
	IEnumerator rotating()
	{
		while(true)
		{
			if(false == _stopRotation)
			{
			transform.Rotate(direction * rotation_speed * Time.deltaTime);
			}
			yield return null; 
		}
	}
	
	public void setSpeed(float pwProduction)
	{
		rotation_speed = pwProduction; ; 
	}

	public void setZ (float z) {

		direction.z = z;

	}

	public float getSpeed()
	{
		return rotation_speed;
	}

	public void setY (float y) {
		
		direction.y = y;
		
	}
	
	void StopRotation()
	{
		_stopRotation = true; 
	}
	void RotationContinue()
	{
		_stopRotation = false; 
	}
}
