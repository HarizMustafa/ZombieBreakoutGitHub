using UnityEngine;
using System.Collections;

public class ShopSelection : MonoBehaviour {

	public GameObject InventoryCarrierReference;
	public Material SelectionMaterial;
	[Space(15)]
	public float Money;
	public GameObject MoneyTextReference;
	[Space(15)]
	public KeyCode ShopButton;
	public KeyCode AlternateShopButton;
//	public KeyCode CloseShopButton;
	public int OptionSelection = 1;
	private int CurrentSelection;
	[Space(15)]
	public int SelectionWidth;
	public int SelectionHeight;
	[Space(15)]
	public GameObject MainMenuReference;
	private Vector3 MenuCloseLocation;
	public int ShopAnimationSpeed;
	public bool ShopOpen;
	[HideInInspector]
	public bool ShopCanOpen;

	public GameObject canvas;


	public GameObject[] Contents;

	private Material DefaultMaterial;
	
	void Start () 
	{
		MenuCloseLocation = MainMenuReference.transform.localPosition;

		//To set the material on each of the GameObject so that when they are chosen, their material can be changed properly
		for(int i=0;i < Contents.Length;i++)
		{
		Contents[i].GetComponent<ItemDescription>().SelectMaterial = SelectionMaterial;
		Contents[i].GetComponent<ItemDescription>().CloseLocation = MenuCloseLocation;
		StartCoroutine(Contents[i].GetComponent<ItemDescription>().CloseMenu(50));
		ShopOpen = false;
		}
	}

	void Update () 
	{
        Money = GameObject.Find("MoneyCarrier").GetComponent<MoneyCarrier>().Money;
		MoneyTextReference.GetComponent<TextMesh>().text = "RM" + Money;

		if(ShopOpen == true)
		{
			//Everything will always be false, except for the ones that is chosen
				for(int i=0;i < Contents.Length;i++)
				{
					if(OptionSelection-1 == i)
					{
						CurrentSelection = i;
						Contents[i].GetComponent<ItemDescription>().isChosen = true;
					}
					else
					{
						Contents[i].GetComponent<ItemDescription>().isChosen = false;
					}
				}

			if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("DPadLeft")>0.001)
			   {
					if(OptionSelection % SelectionWidth-1 == 0)
					{
					OptionSelection = OptionSelection;
					}
					else
					{
					OptionSelection -= 1;
					}
				}

			if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("DPadRight")>0.98f)
			   {
					if(OptionSelection % SelectionWidth == 0)
					{
					OptionSelection = OptionSelection;
					}
					else
					{
					OptionSelection += 1;
					}
				}

			if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("DPadUp")>0.98f)
			{
				if(OptionSelection <= SelectionWidth)
				{
					OptionSelection = OptionSelection;
				}
				else
				{
					OptionSelection -= SelectionWidth;
				}
			}

			if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("DPadDown")>0.001)
			{
				if(OptionSelection >= SelectionWidth*SelectionHeight || OptionSelection > SelectionWidth*SelectionHeight - SelectionWidth)
				{
					OptionSelection = OptionSelection;
				}
				else
				{
					OptionSelection += SelectionWidth;
				}
			}

			if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.JoystickButton0))
			{
				if(Money - Contents[CurrentSelection].GetComponent<ItemDescription>().Price < 0)
				{
					print ("INSUFFICIENT FUNDS");
				}
				else
				{
					
					if(transform.GetComponent<vp_Inventory>().HaveItem(Contents[CurrentSelection].GetComponent<ItemDescription>().Item,1) == false)//to check if the player already have the item or not
					{
						Money -= Contents[CurrentSelection].GetComponent<ItemDescription>().Price;
						transform.GetComponent<vp_Inventory>().TryGiveItem(Contents[CurrentSelection].GetComponent<ItemDescription>().Item,1);//to give the player weapon based on ItemDescription
						InventoryCarrierReference.GetComponent<InventoryCarrier>().PlayerInventory[CurrentSelection] = Contents[CurrentSelection].GetComponent<ItemDescription>().Item;
					}

				}
			}

		//Open/Close shop menu

		}

		if((Input.GetKeyDown(ShopButton) && ShopCanOpen == true) || (Input.GetKeyDown(AlternateShopButton) && ShopCanOpen == true))
		{
			if(ShopOpen == true)
			{
				gameObject.GetComponent<CharacterController>().enabled = true;
//				gameObject.GetComponent<OVRPlayerController>().enabled = true;
				gameObject.GetComponent<vp_FPInput>().enabled = true;
				canvas.SetActive(true);

				for(int i=0;i < Contents.Length;i++)
				{
					StartCoroutine(Contents[i].GetComponent<ItemDescription>().CloseMenu(ShopAnimationSpeed));
					ShopOpen = false;
				}
			}
			else
				if(ShopOpen == false)
				{
					gameObject.GetComponent<CharacterController>().enabled = false;
//					gameObject.GetComponent<OVRPlayerController>().enabled = false;
					gameObject.GetComponent<vp_FPInput>().enabled = false;
					canvas.SetActive(false);

					for(int i=0;i < Contents.Length;i++)
					{
						StartCoroutine(Contents[i].GetComponent<ItemDescription>().OpenMenu(ShopAnimationSpeed));
						ShopOpen = true;
					}
				}
		}
		
//		if(Input.GetKeyDown(CloseShopButton))
//		{
//			if(ShopOpen == false)
//			{
//				for(int i=0;i < Contents.Length;i++)
//				{
//					StartCoroutine(Contents[i].GetComponent<ItemDescription>().OpenMenu(ShopAnimationSpeed));
//					ShopOpen = true;
//				}
//			}
//		}




	}
	
}