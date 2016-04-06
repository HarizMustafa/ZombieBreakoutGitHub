using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CircularProgressBar : MonoBehaviour {

	public int WaitTime = 1;
	public GameObject progress;
	public GameObject progressBG;
	private float counter = 1f;
//	public GameObject portal; 

	// Use this for initialization
	void Start () {
	
		counter = 0;
		progress.GetComponent<Image> ().fillAmount = 0;
		progressBG.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetButton("Fire3") || Input.GetKey(KeyCode.F))
		{
			if(Mathf.FloorToInt(counter) < WaitTime)
			{
				counter += 1 * Time.deltaTime;
//				print(Mathf.FloorToInt(counter));
				progress.GetComponent<Image>().fillAmount = counter / WaitTime;
				progressBG.SetActive(true);
			}
		}
		
		else
			if(Input.GetButtonUp("Fire3") || Input.GetKeyUp(KeyCode.F))
		{
			counter = 0;
			progressBG.SetActive(false);
		}
	}
}
