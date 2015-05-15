using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using metaio;

public class GestCallBack : MonoBehaviour //: metaioCallback
{
	public int cos = 0;
	public int cc = 0;


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

//	void OnGUI () 
//	{
//		//GUI.Label (new Rect (50, 50, 100, 100), "" + cos + "   " + cc + " " );
//	}
}
