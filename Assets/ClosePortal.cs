using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClosePortal : MonoBehaviour {

	public GameObject Portal;
	public GameObject Interaction;
	public GameObject InteractionBG;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Portal")
		{

			Interaction.SetActive (true);
			InteractionBG.SetActive (true);

		}
			
	}

	void OnTriggerExit(Collider other)

	{
		if(other.gameObject.tag == "Portal")
		{
			Interaction.SetActive (false);
			InteractionBG.SetActive (false);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Portal")
		{

			if(Input.GetKey(KeyCode.F))
			{
				gameObject.GetComponent<CircularProgressBar>().enabled = true;
			}

			if(gameObject.GetComponent<CircularProgressBar>().progress.GetComponent<Image>().fillAmount == 1)
			{
				Destroy(Portal);
				gameObject.GetComponent<CircularProgressBar>().enabled = false;
				gameObject.GetComponent<CircularProgressBar>().progressBG.SetActive (false);
				InteractionBG.SetActive (false);
				Interaction.SetActive (false);
			}
		}
	}
}
