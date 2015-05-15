using UnityEngine;
using System.Collections;

public class NaturalOrientation  {

	public static int ORIENTATION_UNDEFINED = 0x00000000;
	public static int ORIENTATION_PORTRAIT = 0x00000001;
	public static int ORIENTATION_LANDSCAPE = 0x00000002;
	
	public static int ROTATION_0 	= 0x00000000;
	public static int ROTATION_180 	= 0x00000002;
	public static int ROTATION_270 	= 0x00000003;
	public static int ROTATION_90 	= 0x00000001;
	
	public static int PORTRAIT = 0;
	public static int PORTRAIT_UPSIDEDOWN = 1;
	public static int LANDSCAPE = 2;
	public static int LANDSCAPE_LEFT = 3;
	
	#if UNITY_ANDROID
	
	AndroidJavaObject mConfig;
	AndroidJavaObject mWindowManager;
	
	public NaturalOrientation ()
	{

	}
	
	//adapted from http://stackoverflow.com/questions/4553650/how-to-check-device-natural-default-orientation-on-android-i-e-get-landscape/4555528#4555528
	public bool IsTablet()
	{


		if ((mWindowManager == null) || (mConfig == null))
		{
//			using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").
//			       GetStatic<AndroidJavaObject>("currentActivity"))
//			{
//				mWindowManager = activity.Call<AndroidJavaObject>("getSystemService","window");
//				mConfig = activity.Call<AndroidJavaObject>("getResources").Call<AndroidJavaObject>("getConfiguration");
//			}
			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

				mWindowManager = activity.Call<AndroidJavaObject>("getSystemService","window");
				mConfig = activity.Call<AndroidJavaObject>("getResources").Call<AndroidJavaObject>("getConfiguration");

		}
		
		int lRotation = mWindowManager.Call<AndroidJavaObject>("getDefaultDisplay").Call<int>("getRotation");
		int dOrientation = mConfig.Get<int>("orientation");
		
		if( (((lRotation == ROTATION_0) || (lRotation == ROTATION_180)) && (dOrientation == ORIENTATION_LANDSCAPE)) ||
		   (((lRotation == ROTATION_90) || (lRotation == ROTATION_270)) && (dOrientation == ORIENTATION_PORTRAIT)))
		{
			return(true); //TABLET
		}     
		
		return (false); //PHONE
	} 
	
	#endif
}
