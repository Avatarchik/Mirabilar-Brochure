using UnityEngine;
using System.Collections;
using UnityEngine.Cloud.Analytics;

public class UnityAnalyticsIntegration : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
		const string projectId = "76dec981-15df-4ff1-9f12-8459a5c0c84c";
		UnityAnalytics.StartSDK (projectId);
		
	}
	
}