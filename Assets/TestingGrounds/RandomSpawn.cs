using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RandomSpawn : MonoBehaviour {

	public bool StartMode = false;
	public int PortalAmount;

	public GameObject[] Portals;

	private int[] RandomNumbers;

	private bool Same = false;



	void Start () 
	{
//		StartMode = true;
		SpawnStart();
	}

	void Awake()
	{

	}

	void SpawnStart () 
	{
		if(StartMode == false)
		{
			RandomNumbers = new int[PortalAmount];

			if(PortalAmount <= Portals.Length)
			SetRandomNum();

			SetRandomLocation();
		}
	}

	public void SetRandomNum()
	{
		for(int i = 0; i < RandomNumbers.Length ; i++)
		{
			RandomNumbers[i] = Random.Range(0,Portals.Length);//setting the initial random number

			do //keep repeating the process of changing the RandomNumbers variable as long as there is still similarities
			{
				Same =  false;//this is to prevent an infinite loop, and also to default the variable to false
				//also, the variable of Same must be false till the end of the code in order to finish the particular Do function

				for(int j = 0; j < RandomNumbers.Length; j++)
				{
					if(RandomNumbers[i] == RandomNumbers[j] && i != j)
					{
						Same = true;//this means that the "Same = false" flow are interupted and therefore the whole Do function will be repeated again until it is not interupted
					}
				}

				if(Same == true)
				{
					RandomNumbers[i] = Random.Range(0,Portals.Length);
					// if there are similarities within the numbers inside the array, this line here will change the CURRENT random number, hence the "i" in RandomNumbers[i]
				}

			}while(Same == true);
			//it's like
			//--||--
			//--||--
			//-- X --      <-- have similarities
			//--    --
			//--    --

			//the correct way
			//--||--
			//--||--
			//--||--
			//--||--
			//--||--


		}
	}

	public void SetRandomLocation()
	{
		int TempVar;

		for(int h = 0; h < Portals.Length ; h++)
		{
			Portals[h].SetActive(false); //to set everything to false if it's not chosen
		}

		for(int L = 0; L < RandomNumbers.Length ; L++)
		{
			TempVar = RandomNumbers[L] ;
			Portals[TempVar].SetActive(true);
		}
		return;
	}

//	[System.Serializable]
//	public class PortalLoc
//	{
//		public Transform PortalLocation;
//	}








}
