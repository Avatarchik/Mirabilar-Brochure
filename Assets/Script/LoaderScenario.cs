using UnityEngine;
using System.Collections;
//using System.Threading;

public class LoaderScenario : MonoBehaviour {

	public Texture mirabilar;

	public Texture2D emptyProgressBar; 
	public Texture2D fullProgressBar; 
	private AsyncOperation async = null;

//	public Texture loadingUno;
//	public Texture loadingDue;
//	public Texture loadingTre;
//	public Texture loadingQuattro;
//	public Texture loadingCinque;
//	public Texture loadingSei;
//	public Texture loadingSette;
//	public Texture loadingOtto;

	private Texture comodo;
	private float SizeFactor = 1;
	private bool stopThread = false;

	// Use this for initialization
	void Start () {

		//comodo = loadingUno;
		SizeFactor = GUIUtilities.SizeFactor;
		//StartCoroutine (AnimCaricamento());
//		Thread trd = new Thread(new ThreadStart(AnimCaricamentoThread));
//		trd.IsBackground = true;
//		trd.Start();
		StartCoroutine (LoadALevel());

	}
	
	// Update is called once per frame
	void Update () {

	}

	private IEnumerator LoadALevel() {
		async = Application.LoadLevelAsync ("Brochure Vuforia 2");
		yield return async;
	}

//	private IEnumerator AnimCaricamento ()
//	{
//		while (Application.isLoadingLevel)
//		{
//			yield return new WaitForSeconds(0.2f);
//			comodo = loadingDue;
//			yield return new WaitForSeconds(0.2f);
//			comodo = loadingTre;
//			yield return new WaitForSeconds(0.2f);
//			comodo = loadingQuattro;
//			yield return new WaitForSeconds(0.2f);
//			comodo = loadingCinque;
//			yield return new WaitForSeconds(0.2f);
//			comodo = loadingSei;
//			yield return new WaitForSeconds(0.2f);
//			comodo = loadingSette;
//			yield return new WaitForSeconds(0.2f);
//			comodo = loadingOtto;
//			yield return new WaitForSeconds(0.2f);
//			comodo = loadingUno;
//		}
//
//		yield return 0;
//	}

//	private void AnimCaricamentoThread ()
//	{
//		while (Application.isLoadingLevel)
//		{
//
//			Thread.Sleep(200);
//			comodo = loadingTre;
//			Thread.Sleep(200);
//			comodo = loadingQuattro;
//			Thread.Sleep(200);
//			comodo = loadingCinque;
//			Thread.Sleep(200);
//			comodo = loadingSei;
//			Thread.Sleep(200);
//			comodo = loadingSette;
//			Thread.Sleep(200);
//			comodo = loadingOtto;
//			Thread.Sleep(200);
//			comodo = loadingUno;
//		}
//		
//
//	}

	void OnGUI() 
	{
//		if (Application.isLoadingLevel)
//		{
//		GUI.DrawTexture (new Rect (0,0,Screen.width,Screen.height), mirabilar);
//
//		GUI.DrawTexture (new Rect (Screen.width/2-60*SizeFactor,
//		                           Screen.height/2+30*SizeFactor,
//		                           120*SizeFactor,
//		                           120*SizeFactor), comodo);
//		}
		GUI.DrawTexture (new Rect (0,0,Screen.width,Screen.height), mirabilar);

		if (async != null) {
			GUI.DrawTexture(new Rect(Screen.width/2-150*SizeFactor,
			                     Screen.height/2+30*SizeFactor, 300*SizeFactor, 30*SizeFactor), emptyProgressBar);

			GUI.DrawTexture(new Rect(Screen.width/2-150*SizeFactor,
			                     Screen.height/2+30*SizeFactor,
			                     (300 * async.progress)*SizeFactor, 30*SizeFactor), fullProgressBar);
		}

	}
}
