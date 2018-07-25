using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ChaseAI : MonoBehaviour {

	public Transform player;
	public Animator anim;
    //public int Health;
    NavMeshAgent patrolAgent;
    DetectDamagingHitEnemy DamageHandler;

    // Use this for initialization
    void Start () 
	{
		anim = GetComponent<Animator> ();
        patrolAgent = GetComponent<NavMeshAgent>();
        DamageHandler = GetComponent<DetectDamagingHitEnemy>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () 
	{
        //read healthbar to determine if dead
        //healthbar is updated in the "detectDamagingHit" script
        if (DamageHandler.health <= 0)
        {
            patrolAgent.Stop();
            anim.SetBool("isDead", true);
            return;
        }            
        chasePlayer();

	}

    void chasePlayer()
    {
        //copied from previous skeleton script
        if (Vector3.Distance(player.position, this.transform.position) < 100.0f)
        {
            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp((Quaternion)this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);


            if (direction.magnitude < 20.0f && direction.magnitude > 2.0f)
            {
                //this.transform.Translate(0, 0, 0.04f);
                patrolAgent.Resume();
                patrolAgent.SetDestination(player.position);
                anim.SetBool("isWalking", true);
                anim.SetBool("isIdle", false);
                anim.SetBool("isAttacking", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
                anim.SetBool("isAttacking", false);
            }

            if (direction.magnitude < 5.0f)
            {
                patrolAgent.Stop();
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", false);
                anim.SetBool("isAttacking", true);
            }

        }
    }

    public void DealDamage()
    {
        player.SendMessage("TakeDamage", 20);
    }
}
