using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAI : MonoBehaviour
{
    public Transform targetA;
    public Transform targetB;
    public Transform player;
    static Animator anim;
    NavMeshAgent patrolAgent;
    Vector3 targetDirection;
    DetectDamagingHitEnemy DamageHandler;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        patrolAgent = GetComponent<NavMeshAgent>();
        targetDirection = targetA.position;
        DamageHandler = GetComponent<DetectDamagingHitEnemy>();
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", true);
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
                //anim.SetBool("isAttacking", true);
                anim.Play("Attack");
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (DamageHandler.health <= 0)
        {
            anim.SetBool("isDead", true);
            patrolAgent.Stop();
            return;
        }

        Vector3 targetADistance = targetA.position - this.transform.position;
        Vector3 targetBDistance = targetB.position - this.transform.position;

        //bool initialDecision = true;

        /*if (Vector3.Distance(targetA.position, this.transform.position) < Vector3.Distance(targetB.position, this.transform.position)) {
			targetDirection = targetA.position;
		} else {
			targetDirection = targetB.position;
		}*/

        if (Vector3.Distance(targetDirection, this.transform.position) < 5.0f)
        {
            if (targetDirection == targetA.position)
            {
                targetDirection = targetB.position;
            }
            else if (targetDirection == targetB.position)
            {
                targetDirection = targetA.position;
            }
        }

        patrolAgent.SetDestination(targetDirection);

        anim.SetBool("isWalking", true);
        anim.SetBool("isIdle", false);
        anim.SetBool("isAttacking", false);

        chasePlayer();
    }

    public void DealDamage()
    {
        player.SendMessage("TakeDamage", 20, SendMessageOptions.DontRequireReceiver);
        //Debug.Log("Skele dealing damage");
    }
}