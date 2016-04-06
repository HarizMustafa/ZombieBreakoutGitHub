using UnityEngine;
using System.Collections;

public class TestStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<RandomSpawn>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
