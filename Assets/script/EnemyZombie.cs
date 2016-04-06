using UnityEngine;
using System.Collections;

public class EnemyZombie : MonoBehaviour {

	public float distance;

	Transform target;
	NavMeshAgent nav;
	Transform player;
	//Animator controller;
	public float health = 100;
	//ZombieSpawnPointManager game;
	CapsuleCollider capsuleCollider;
    SphereCollider sphereCollider;
	Animator anim;
	//public float TheDamage = 50;
	RaycastHit hit;
	//public bool isDead = false;
	//public bool isHit = false;
	//public bool isAttack = false;
	public GameObject[] pickups;
    
    //public float TimetoAttack;
    //float attackTimer;
    //public AudioClip zombieAttackClip;
    //public Text score;
    //public int scoreValue = 10;
    //private int scoring = 1;

	private GameObject GameManagerGO;
    private ScoreManager ScrManager;
    public bool IAmZombieA;
    public bool IAmZombieB;
    public bool IAmZombieC;
	public bool IAmZombieD;

	public float ZombieADamage = 5;
	public float ZombieBDamage = 10;
	public float ZombieCDamage = 20;
	public float ZombieDDamage = 15;

	private vp_PlayerEventHandler PlayerEvents = null;

	private GameObject PlayerReference;
	private PlayerHealthNewChar PlayerScriptReferece;


	//public GameObject indicatePain;

	// Use this for initialization
	void Awake () {
		nav = GetComponent <NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		//controller = GetComponentInParent <Animator> ();
		//game = FindObjectOfType<ZombieSpawnPointManager> ();
		capsuleCollider = GetComponent <CapsuleCollider> ();
        sphereCollider = GetComponent<SphereCollider>();
		anim = GetComponent <Animator> ();
        //score.GetComponent<Text>("Zombie");
       // currentState = ZombieState.isWalk;

		PlayerEvents = player.transform.GetComponent<vp_PlayerEventHandler> ();
	}

	void Start () {
        //scoring = 0;
        //SetScore ();
		PlayerReference = GameObject.Find ("PlayerOVR");
		PlayerScriptReferece = PlayerReference.GetComponent<PlayerHealthNewChar>();


		GameManagerGO = GameObject.Find("GameManager");
        ScrManager = GameManagerGO.GetComponent<ScoreManager>();

        if (IAmZombieA)
            MainDamage = ZombieADamage;
        if (IAmZombieB)
            MainDamage = ZombieBDamage;
        if (IAmZombieC)
            MainDamage = ZombieCDamage;
		if (IAmZombieD)
			MainDamage = ZombieDDamage;

	}

    public float currentValue;
	public bool canAttack = true;

	// Update is called once per frame
	void Update () {
		       
		distance = Vector3.Distance(transform.position,player.position);

		if(PlayerScriptReferece.PlayerIsDead == true)
			anim.SetBool("PlayerIsDead", true);
		
		if (anim.GetBool("EnemyStillAlive") == true)
		{
			if(canAttack ==  true)
			{
				nav.SetDestination(player.position);
				RotateTowards(player.transform);
				
				if (nav.remainingDistance <= 2)
					anim.SetBool("PlayerInRange", true);
				else if (nav.remainingDistance >= 2)
					anim.SetBool("PlayerInRange", false);
				
				anim.SetFloat("PlayerStillThere", currentValue);
				
				if (anim.GetBool("PlayerInRange") == true)
				{
					currentValue += 0.01f;
					if (currentValue >= 1.1f)
						currentValue = 0;
				}           
				
			}
		}

		if(distance <= 10)
		{
			canAttack = true;
			nav.stoppingDistance = 2;
			gameObject.GetComponent<Patrol>().enabled = false;
		}
    }



	public bool InRange;

    void OnTriggerStay (Collider other)
    {
        if (other.CompareTag ("Player"))
			InRange = true;

		if (other.CompareTag ("Dead"))
		{
			InRange = false;
			anim.SetBool("PlayerIsDead", true);
			canAttack = false;
			nav.enabled = false;
//			anim.SetFloat("PlayerStillThere", 0);
		}

    }

    void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player"))
            InRange = false;
    }

    private GameObject Player;
    private PlayerHealthNewChar Phealth;
    private float MainDamage = 5;
	private vp_FPPlayerDamageHandler hp;

   public void Attack()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Phealth = Player.GetComponent<PlayerHealthNewChar>();
        
        if (InRange)
        {
            if (Phealth != null)
            {

                Phealth.remove(MainDamage);
				PlayerEvents.TakeDamage.Send();
            }

			AudioSource noise = GetComponent<AudioSource>();
			noise.Play();

        }        
    }
    


	void ApplyDamage(float damage)
	{
		health -= damage;

        if (IAmZombieA || (IAmZombieC) || (IAmZombieB) || (IAmZombieD)) {
			anim.SetBool ("EnemyGotHit", true);
			StartCoroutine (DamageCoolDown ());

		} 


			


		if (health <= 0)
		Death ();
	}

    IEnumerator DamageCoolDown ()
    {
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("EnemyGotHit", false);
    }


    /*
    void OnTriggerEnter(Collider other)
	{
        currentState = ZombieState.isAttack;

        if (IAmZombieA)
            anim.SetTrigger ("isAttack");
        if ((IAmZombieC) || (IAmZombieB))
            anim.SetTrigger("mutantAttack");
            //other.GetComponent<Collider>().SendMessageUpwards("PlayerDamage", TheDamage, SendMessageOptions.RequireReceiver);
            //zombiedamage = zombiedamage + 1;
            //ScorePoint();

    }

    void OnTriggerStay (Collider other)
    {
        currentState = ZombieState.isAttack;

        if (IAmZombieA)
            anim.SetTrigger("isAttack");
        if ((IAmZombieC) || (IAmZombieB))
            anim.SetTrigger("mutantAttack");
    }

    */

	//void SetScore()
	//{
	//	score.text = "Zombie Kill: " + scoring.ToString();
	//}

	void Death ()
		{
        
		nav.Stop ();
        //		capsuleCollider.isTrigger = true;
        capsuleCollider.enabled = false;
        sphereCollider.enabled = false;

        if (IAmZombieA || (IAmZombieC) || (IAmZombieB) || (IAmZombieD)) {
            anim.SetBool("EnemyStillAlive", false);

        }


        //Scoring.kill += scoring;
        //score.text = "Zombie Kill: " + scoring++;
        //SendMessageUpwards ("SetScore", TheDamage, SendMessageOptions.RequireReceiver);
        //scoring = scoring + 1;
        //Scoring.kill += scoreValue;
        //score.text = "Zombie Kill: " + scoring.ToString();

        StartCoroutine(SpawnMedicine());

        CheckWhichZombieIAm();
		Destroy (gameObject, 4f);
		}

    IEnumerator SpawnMedicine ()
    {
        yield return new WaitForSeconds(2);
        int randomDrop = Random.Range(0, 10);
        int randomPickup = Random.Range(0, pickups.Length - 1);
        if (randomDrop < 2)
        {
            Instantiate(pickups[randomPickup], transform.position + new Vector3(0, 0.25f, 0), Quaternion.Euler(0, 0, 0));
        }
    }

    void CheckWhichZombieIAm ()
    {
        if (IAmZombieA)
            ScrManager.KilledZombieA();
        if (IAmZombieB)
            ScrManager.KilledZombieB();
        if (IAmZombieC)
            ScrManager.KilledZombieC();
		if (IAmZombieD)
			ScrManager.KilledZombieD();
    }

	protected virtual void OnEnable()
	{
		if (PlayerEvents != null)
			PlayerEvents.Unregister (this);
	}

	protected virtual void OnDisable()
	{
		if (PlayerEvents != null)
			PlayerEvents.Unregister (this);
	}

	protected virtual void SendMessage()
	{
		PlayerEvents.TakeDamage.Send ();
	}
	private void RotateTowards (Transform target) 
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
	}

}
