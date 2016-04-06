using UnityEngine;
using System.Collections;

public class ZombieTrigger : MonoBehaviour {

	public GameObject Zombie1;
	public GameObject Zombie2;

	void Start ()
	{
		Zombie1.SetActive (false);
		Zombie2.SetActive (false);
	}

	void OnTriggerEnter(Collider trigger)
	{
		if(trigger.gameObject.tag == "Player")
		{
			Zombie1.SetActive (true);
			Zombie2.SetActive (true);
			gameObject.GetComponent<BoxCollider>().enabled = false;
		}
	}
}
