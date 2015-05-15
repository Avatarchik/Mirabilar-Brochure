using UnityEngine;
using System.Collections;

public class TouchCard : MonoBehaviour {

	public GameObject[] carte;
	public Texture[] txCard;
	public AudioClip[] audioCards;
	public Texture retroTx;
	public int Counter = 0;
	public bool win = false;
	public AudioClip applauso;
	public AudioClip tamburi;
	public bool blockCouroutine = false ;

	private GameObject level1;
	private GameObject selectGO;
	private LayerMask layerMask = 1<<4;
	private RaycastHit hit;
	private int maxTwoCounter = 0;
	private AudioSource sourceAudioCard;
	private GestCallBack callBack;


	public int unita = 0;
	public int decine = 0;


	// Use this for initialization
	void Start () {
		callBack = GameObject.Find("GUI").GetComponent<GestCallBack> ();
		level1 = transform.gameObject;
		turnCards ();
		sourceAudioCard = gameObject.GetComponent<AudioSource> ();



		if (gameObject.name == "Level1")
			RandomLvl1 ();

		if (gameObject.name == "Level2")
			RandomLvl2 ();

		if (gameObject.name == "Level3")
			RandomLvl3 ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (level1.activeSelf && Input.touchCount > 0 && !win && callBack.cos == 4) 
		{
			handleTouches();
		}

	}

	private void handleTouches()
	{

		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began && !blockCouroutine) 
		{
			StartCoroutine ( selectGameObject (Input.GetTouch (0).position) );
//			

		}
		
		
		//if(Input.GetTouch(0).phase == TouchPhase.Moved && selected)
			//moveGameObject(Input.GetTouch(0).position);
		if (Input.GetTouch (0).phase == TouchPhase.Ended) 
		{

			//turnCards();
			//disabCards ("Card1","Card8");

		}
	}



	private IEnumerator selectGameObject(Vector2 position)
	{
		blockCouroutine = true;

		// get a ray from the touch point
		Ray ray = Camera.main.ScreenPointToRay(position);
		
		// layer mask, metaio man is on layer 4
		layerMask = 1<<10;
		

		
		// cast a ray on layer 4, if metaio man has been hit
		if (Physics.Raycast (ray, out hit, 5000, layerMask) && isCard (hit.collider.gameObject.name)) {
			// metaio man is touched --> select it



			if (!hit.collider.gameObject.GetComponent<CardValue>().girata && maxTwoCounter < 2)
			{


//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,170);
//				yield return new WaitForSeconds(0.1f);
//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,160);
//				yield return new WaitForSeconds(0.1f);
//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,150);
//				yield return new WaitForSeconds(0.1f);
//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,140);
//				yield return new WaitForSeconds(0.1f);
//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,130);
//				yield return new WaitForSeconds(0.1f);
//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,120);
//				yield return new WaitForSeconds(0.1f);
//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,110);
//				yield return new WaitForSeconds(0.1f);
//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,100);
//				changeTexture ( hit.collider.gameObject);
//				yield return new WaitForSeconds(0.1f);
//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,90);
//				yield return new WaitForSeconds(0.1f);
//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,80);
//				yield return new WaitForSeconds(0.1f);
//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,70);
//				yield return new WaitForSeconds(0.1f);
//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,60);
//				yield return new WaitForSeconds(0.1f);
//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,50);
//				yield return new WaitForSeconds(0.1f);
//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,40);
//				yield return new WaitForSeconds(0.1f);
//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,30);
//				yield return new WaitForSeconds(0.1f);
//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,20);
//				yield return new WaitForSeconds(0.1f);
//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,10);
//				yield return new WaitForSeconds(0.1f);
//				hit.collider.transform.localEulerAngles = new Vector3 (0,180,0);

				for (int i= 180; i>0; i-=5)
				{
					hit.collider.transform.localEulerAngles = new Vector3 (0,180,i);

					if (hit.collider.transform.localEulerAngles.z == 100)
						changeTexture ( hit.collider.gameObject);

					yield return null; 
				}

				hit.collider.transform.localEulerAngles = new Vector3 (0,180,0);



				if (maxTwoCounter == 0)
					selectGO = hit.collider.gameObject;

				maxTwoCounter++;
				hit.collider.gameObject.GetComponent<CardValue>().girata = true;


					

			}



			if (maxTwoCounter >= 2)
			{
				counterAdd ();

				yield return new WaitForSeconds(1.0f);

				Counter++;

				if (hit.collider.gameObject.GetComponent<CardValue>().value == selectGO.GetComponent<CardValue>().value)
					disabCards (hit.collider.gameObject.name, selectGO.name);

				else
				{
					//turnCards();
					for (int i= 0; i<=180; i+=4)
					{
						hit.collider.transform.localEulerAngles = new Vector3 (0,180,i);
						
						selectGO.transform.localEulerAngles = new Vector3 (0,180,i);

						if (i==180)
							turnCards();

						yield return null; 
					}
				}

				maxTwoCounter = 0;
			}

			// swith to layer 8, find the initial touch point on the plane and use it as a reference
			layerMask = 1 << 8;
			if (Physics.Raycast (ray, out hit, 5000, layerMask)) {
				// record the offset of the touch point and the object position point
				//offset = hit.point - level1.transform.position;
			}
			//return hit.collider.gameObject;
		} 
		else 
		{
			// not hit, don't select

			//return null;
			
		}

		blockCouroutine = false;

		win = isWin ();

		yield return 0;
	}

	private void turnCards ()
	{


		foreach (Transform child in transform)
		{
			child.renderer.material.mainTexture = retroTx;
			CardValue cv = child.GetComponent<CardValue>();
			cv.girata = false;
		}

	}

	private bool isWin ()
	{
		foreach (Transform child in transform)
		{
			if (child.gameObject.activeSelf)
				return false;
		}
		sourceAudioCard.clip = applauso;
		sourceAudioCard.Play();
		return true;

	}

	private void disabCards (string name1 , string name2)
	{
		foreach (Transform child in transform)
		{
			if (child.name == name1 || child.name == name2)
			{
				CardValue cv = child.GetComponent<CardValue>();
				cv.girata = false;
				child.gameObject.SetActive (false);
				sourceAudioCard.clip = tamburi;
				sourceAudioCard.Play();
			}
		}
	}

	private void counterAdd ()
	{
		if (unita == 9)
		{
			decine++;
			unita = 0;
		}
		else 
			unita++;

		if (decine == 9)
		{
			decine = 0;
		}


	}

	private void changeTexture ( GameObject cardGO) 
	{
		CardValue cv = cardGO.GetComponent<CardValue> ();

		if (carte [cv.pos].GetComponent<CardValue> ().value == 1)
		{
			cardGO.renderer.material.mainTexture = txCard [0]; 
			sourceAudioCard.clip = audioCards[0];
			sourceAudioCard.Play();
		}

		else if (carte [cv.pos].GetComponent<CardValue> ().value == 2)
		{
			cardGO.renderer.material.mainTexture = txCard [1]; 
			sourceAudioCard.clip = audioCards[1];
			sourceAudioCard.Play();
		}

		else if (carte [cv.pos].GetComponent<CardValue> ().value == 3)
		{
			cardGO.renderer.material.mainTexture = txCard [2];
			sourceAudioCard.clip = audioCards[2];
			sourceAudioCard.Play();
		}

		else if (carte [cv.pos].GetComponent<CardValue> ().value == 4)
		{
			cardGO.renderer.material.mainTexture = txCard [3];
			sourceAudioCard.clip = audioCards[3];
			sourceAudioCard.Play();
		}

		else if (carte [cv.pos].GetComponent<CardValue> ().value == 5)
		{
			cardGO.renderer.material.mainTexture = txCard [4];
			sourceAudioCard.clip = audioCards[4];
			sourceAudioCard.Play();
		}

		else if (carte [cv.pos].GetComponent<CardValue> ().value == 6)
		{
			cardGO.renderer.material.mainTexture = txCard [5];
			sourceAudioCard.clip = audioCards[5];
			sourceAudioCard.Play();
		}

		else if (carte [cv.pos].GetComponent<CardValue> ().value == 7)
		{
			cardGO.renderer.material.mainTexture = txCard [6];
			sourceAudioCard.clip = audioCards[6];
			sourceAudioCard.Play();
		}

		else if (carte [cv.pos].GetComponent<CardValue> ().value == 8)
		{
			cardGO.renderer.material.mainTexture = txCard [7];
			sourceAudioCard.clip = audioCards[7];
			sourceAudioCard.Play();
		}

		else if (carte [cv.pos].GetComponent<CardValue> ().value == 9)
		{
			cardGO.renderer.material.mainTexture = txCard [8];
			sourceAudioCard.clip = audioCards[8];
			sourceAudioCard.Play();
		}

		else if (carte [cv.pos].GetComponent<CardValue> ().value == 10)
		{
			cardGO.renderer.material.mainTexture = txCard [9];
			sourceAudioCard.clip = audioCards[9];
			sourceAudioCard.Play();
		}
	}

	private bool isCard (string name) 
	{


		switch (name) 
		{
		case "Card1": 
			return true;

		case "Card2":
			return true;

		case "Card3":
			return true;

		case "Card4":
			return true;

		case "Card5":
			return true;

		case "Card6":
			return true;

		case "Card7":
			return true;

		case "Card8":
			return true;
		
		case "Card9":
			return true;
			
		case "Card10":
			return true;

		case "Card11":
			return true;
						
		case "Card12":
			return true;

		case "Card13":
			return true;

		case "Card14":
			return true;

		case "Card15":
			return true;

		case "Card16":
			return true;

		case "Card17":
			return true;

		case "Card18":
			return true;

		case "Card19":
			return true;

		case "Card20":
			return true;

		}

		return false;
	}

	private void RandomLvl1 () 

	{
		int [] comodo  = new int [8] {1,1,2,2,3,3,4,4} ;


		for (int i=0; i<100; i++) 
		{
			int r1 = Random.Range(0,7);
			int r2 = Random.Range(0,7);
			int sw = 0;
			sw = comodo [r1];
			comodo [r1] = comodo [r2];
			comodo [r2] = sw;
		}
				

		for (int i=0; i<comodo.Length; i++) 
		{
			carte[i].GetComponent<CardValue>().value = comodo[i];
		}

	}

	private void RandomLvl2 () 
		
	{
		int [] comodo  = new int [12] {1,1,2,2,3,3,4,4,5,5,6,6} ;
		
		
		for (int i=0; i<100; i++) 
		{
			int r1 = Random.Range(0,11);
			int r2 = Random.Range(0,11);
			int sw = 0;
			sw = comodo [r1];
			comodo [r1] = comodo [r2];
			comodo [r2] = sw;
		}
		
		
		for (int i=0; i<comodo.Length; i++) 
		{
			carte[i].GetComponent<CardValue>().value = comodo[i];
		}
		
	}

	private void RandomLvl3 () 
		
	{
		int [] comodo  = new int [20] {1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9,10,10} ;
		
		
		for (int i=0; i<150; i++) 
		{
			int r1 = Random.Range(0,19);
			int r2 = Random.Range(0,19);
			int sw = 0;
			sw = comodo [r1];
			comodo [r1] = comodo [r2];
			comodo [r2] = sw;
		}
		
		
		for (int i=0; i<comodo.Length; i++) 
		{
			carte[i].GetComponent<CardValue>().value = comodo[i];
		}
		
	}

//	void OnGUI ()
//	{
//		GUI.TextField(new Rect(Screen.width - 200,
//		                       0,
//		                       200,
//		                       200), "Selected:"+selected+"  "+Counter
//		              , 100);	
//	}

	public void resetTouchCard () 
	{
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(true);
			child.renderer.material.mainTexture = retroTx ;
			CardValue cv = child.GetComponent<CardValue>();
			cv.girata = false;
			child.localEulerAngles = new Vector3 (0,180,180);
		}

		if (gameObject.name == "Level1")
			RandomLvl1 ();
		
		if (gameObject.name == "Level2")
			RandomLvl2 ();
		
		if (gameObject.name == "Level3")
			RandomLvl3 ();

		Counter = 0;
		maxTwoCounter = 0;
		decine = 0;
		unita = 0;

	}

}
