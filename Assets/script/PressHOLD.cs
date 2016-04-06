using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PressHOLD : MonoBehaviour {

	public int WaitTime = 1;
	private float DownTime;
	private bool isHandled = false;

	public GameObject portal1;
	public GameObject portal2;
	public GameObject portal3;
	public GameObject portal4;
	public GameObject portal5;
	public GameObject portal6;

	public Text interactionText;
	public Image interactionBG;
	public Image Bbut;

	public GameObject homeportal;
	public GameObject progress;

	public Text goal1;
	public Text goal2;
	public GameObject final;
	public Animation GoalTextAnim;

	public GameObject GameManagerGO;
	public ScoreManager ScrManager;
	
	// Use this for initialization
	void Start () {
	
		goal1.enabled = true;
		goal2.enabled = false;
		interactionText.enabled = false;
		interactionBG.enabled = false;
		Bbut.enabled = false;
		final.SetActive (false);
		homeportal.SetActive (false);

        GameManagerGO = GameObject.Find("GameManager");
        ScrManager = GameManagerGO.GetComponent<ScoreManager>();

		gameObject.GetComponent<CircularProgressBar>().enabled = false;

	}


	// Update is called once per frame
	void OnTriggerStay (Collider portalcol) {
	
		if (portalcol.gameObject.tag == "Portal") {

			interactionText.enabled = true;
			interactionBG.enabled = true;
			Bbut.enabled = true;
			gameObject.GetComponent<CircularProgressBar> ().enabled = true;

			if (Input.GetButtonDown ("Fire3") || Input.GetKeyDown (KeyCode.F)) { //Key was pressed down, so take up time.
				DownTime = Time.time;
				isHandled = false;


			}
			if (Input.GetButton ("Fire3") || Input.GetKey(KeyCode.F)) { // is key still being hold down...  
				// And is it been hold over the time we want and is not handled yet?
				if ((Time.time > DownTime + WaitTime)) {
					// We've been here, don't do this again until button is pressed again.
					isHandled = true;
					// Do Something here
					Debug.Log ("Key \"B\" was pressed over " + WaitTime + " seconds.");
//					portal.SetActive (false);
					ScrManager.GetDynamite ();
					goal1.enabled = false;
					goal2.enabled = true;
					GoalTextAnim.Play ("GoalTextAnim");
					final.SetActive (true);
					interactionText.enabled = false;
					interactionBG.enabled = false;
					Bbut.enabled = false;
					//Destroy (gameObject);
					homeportal.SetActive (true);

					if(portal1)
					{
						Destroy(portal1);
					}

					if(portal2)
					{
						Destroy(portal2);
					}

					if(portal3)
					{
						Destroy(portal3);
					}

					if(portal4)
					{
						Destroy(portal4);
					}

					if(portal5)
					{
						Destroy(portal5);
					}

					if(portal6)
					{
						Destroy(portal6);
					}
				}

			}

//			Debug.Log("Portal");

		}

		else 
			if(gameObject.GetComponent<CircularProgressBar>() != null)
		{
			gameObject.GetComponent<CircularProgressBar>().enabled = false;
			progress.SetActive (false);
		}

	}


	void Update()
	{

	}

	void OnTriggerExit ()
	{
		interactionText.enabled = false;
		interactionBG.enabled = false;
		Bbut.enabled = false;

		if(gameObject.GetComponent<CircularProgressBar>() != null)
		gameObject.GetComponent<CircularProgressBar>().enabled = false;
	}


}
