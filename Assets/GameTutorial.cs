using UnityEngine;
using System.Collections;

public class GameTutorial : MonoBehaviour {

    public GameObject interactionUI;

    public GameObject moveInst;
    public GameObject fireInst;
    public GameObject swordInst;
    public GameObject grenadeInst;
    public GameObject done;

    public bool show;
    public bool move;
    public bool fire;
    public bool sword;
    public bool grenade;

    void Start()
    {
        show = false;
        move = false;
        fire = false;
        sword = false;
        grenade = false;

        done.SetActive(false);
        moveInst.SetActive(false);
        fireInst.SetActive(false);
        swordInst.SetActive(false);
        grenadeInst.SetActive(false);
    }


    void Update()
    {
        if (Time.timeSinceLevelLoad % 60 >= 2.0f && !move && !show)
        {
            moveInst.SetActive(true);
            move = true;
            show = true;

        }

        if (Input.GetKey(KeyCode.W) && move && !fire && !sword && !grenade && interactionUI.activeSelf == false) //player done control the character
        {
            StartCoroutine(showFireInst());
            fire = true;
            show = true;
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q))
        {
            fireInst.SetActive(false);
            show = false;
        }
        
        if (fire && !sword && !show && Input.GetMouseButtonDown(0) && interactionUI.activeSelf == false)
        {
            StartCoroutine(showSwordInst());
            show = true;
        }

        if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q)) && sword)
        {
            swordInst.SetActive(false);
            show = false;
        }
        
        if (sword && !fire && !show && !grenade && Input.GetMouseButtonDown(0) && interactionUI.activeSelf == false)
        {
            StartCoroutine(showGrenadeInst());
            show = true;
        }
                    
        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q) && grenade)
        {
            grenadeInst.SetActive(false);
            show = false;
        }

        if (grenade && !sword && !fire && !show && Input.GetMouseButtonDown(0) && interactionUI.activeSelf == false)
        {
            StartCoroutine(tutorialEnd());
        }

    }

    void OnGUI()
    {
        if (interactionUI.activeSelf == true)
        {
            moveInst.SetActive(false);
            fireInst.SetActive(false);
            grenadeInst.SetActive(false);
            done.SetActive(false);
        }/*
        else if (fire)
        {
            fireInst.SetActive(true);
            done.SetActive(false);
        }
            
        else if (sword)
        {
            swordInst.SetActive(true);
            done.SetActive(false);
        }
        else if (grenade)
        {
            grenadeInst.SetActive(true);
            done.SetActive(false);
        }*/
    }

    private IEnumerator showFireInst()
    {
        yield return new WaitForSeconds(3f);
        moveInst.SetActive(false);

        done.SetActive(true);
        yield return new WaitForSeconds(2f);
        done.SetActive(false);

        fireInst.SetActive(true);
    }

    private IEnumerator showSwordInst()
    {
        fire = false;
        sword = true;

        yield return new WaitForSeconds(3f);
        fireInst.SetActive(false);

        done.GetComponent<TextMesh>().text = "\n\nuse lasergun";
        done.SetActive(true);
        yield return new WaitForSeconds(2f);
        done.SetActive(false);

        swordInst.SetActive(true);
        sword = true;
        
    }

    private IEnumerator showGrenadeInst()
    {
        sword = false;
        grenade = true;

        yield return new WaitForSeconds(3f);
        swordInst.SetActive(false);

        done.GetComponent<TextMesh>().text = "\n\nuse sword";
        done.SetActive(true);
        yield return new WaitForSeconds(2f);
        done.SetActive(false);

        grenadeInst.SetActive(true);
        show = true;
    }

    private IEnumerator tutorialEnd()
    {
        yield return new WaitForSeconds(3f);
        grenadeInst.SetActive(false);

        done.GetComponent<TextMesh>().text = "\n\nuse grenade";
        done.SetActive(true);
        yield return new WaitForSeconds(2f);
        done.SetActive(false);

    }
}
