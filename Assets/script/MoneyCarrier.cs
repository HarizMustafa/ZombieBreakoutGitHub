using UnityEngine;
using System.Collections;

public class MoneyCarrier : MonoBehaviour {

	private GameObject PlayerReference; 
	
	private GameObject MoneyReference;
	public float Money;
	public float MoneyGathered;

	// Use this for initialization
	void Start () {

		DontDestroyOnLoad(this);
		PlayerReference = GameObject.Find("PlayerOVR");
		Money = PlayerReference.GetComponent<ShopSelection>().Money;
	}

	void OnLevelWasLoaded()//this function will run when a new level is loaded
	{

        MoneyReference = GameObject.Find("GameManager");
        if(Application.loadedLevelName == "Lobby")
            Money += MoneyGathered;

	}

	// Update is called once per frame
	void Update () {

		MoneyGathered = MoneyReference.GetComponent<ScoreManager>().CurrentScore;
	}
}
