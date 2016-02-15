﻿using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {

	public float AnimationDelay;

	public Transform[] waypoints;

	[ReadOnlyAttribute]
	public int NextDest = 0;

	private bool counter;
	private string ObjectName;

	private NavMeshAgent agent;
	public Animator ZombiePref;
	public GameObject Sun;


	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.stoppingDistance = 0;
		agent.angularSpeed = 100;
		ZombiePref = GetComponent<Animator>();
		Sun = GameObject.Find ("Sun");
		ZombiePref.GetComponent<EnemyZombie>().canAttack = false;

		NextDest = Random.Range(0,waypoints.Length);

		for (int i = 0; i < waypoints.Length; i++) 
		{
			ObjectName = "WPoints (" + (i + 1)  + ")";
			waypoints[i] = GameObject.Find(ObjectName).transform;
		}
	}

	void Update()
	{	
		if (agent.remainingDistance <= AnimationDelay && counter == false) {

			counter = true;
			ZombiePref.SetBool ("PlayerIsDead", true);
			ZombiePref.SetBool ("PlayerInRange", true);
			StartCoroutine (SetTimerZero (3f));
		}	

		if(agent.remainingDistance > AnimationDelay)
		{
			ZombiePref.SetBool ("PlayerIsDead", false);
			ZombiePref.SetBool ("PlayerInRange", false);
		}

		if(Sun.GetComponent<DayNightCycle>().TimeFloat <= 6)
		{
			print ("OI");
			gameObject.GetComponent<EnemyZombie>().enabled = true;
//			gameObject.GetComponent<ZombieAIWandering>().enabled = false;
			ZombiePref.GetComponent<EnemyZombie>().canAttack = true;
			agent.stoppingDistance = 1;

		}
	}

	IEnumerator SetTimerZero(float IdleDelay)
	{
		yield return new WaitForSeconds (IdleDelay);
		agent.SetDestination(waypoints[NextDest].position);
		NextDest = Random.Range(0,waypoints.Length);
		counter = false;
	}
}