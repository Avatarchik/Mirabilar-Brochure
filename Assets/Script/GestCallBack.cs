using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using metaio;

public class GestCallBack : MonoBehaviour //: metaioCallback
{
	public int cos = 0;
	public int cc = 0;
	public Texture scansioneQuandro;
	public Texture scansioneScritta;
	private bool scritta = false;

	void Start () {
		
		StartCoroutine (LampeggioScritta());
		
	}

	private IEnumerator LampeggioScritta () {
		
		
		while (true) {

			if (cos==0)
			{
				yield return new WaitForSeconds(1f);
				scritta = true;
				yield return new WaitForSeconds(1f);
				scritta = false;
			}

			yield return 0;
		}
		
		
		yield return 0;
	}

//	override protected void onTrackingEvent (List<TrackingValues> trackingValues)
//	{
//		if (trackingValues.Count > 0)
//			foreach (TrackingValues tv in trackingValues) 
//			{
//			if (tv.state.isTrackingState()){
//				cos = tv.coordinateSystemID;
//				cc = trackingValues.Count;
//			}
//			else 
//				cos = 0;
//
//			}
//		else 
//		{
//			cc = 0;
//
//		}
//	}

	void OnGUI () 
	{
		if (cos == 0)
		{
			GUI.DrawTexture(new Rect (0,0,Screen.width,Screen.height), scansioneQuandro);

			if (scritta)
			{
				GUI.DrawTexture(new Rect (0,0,Screen.width,Screen.height), scansioneScritta);
			}

		}
	}
}
