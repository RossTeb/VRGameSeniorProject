using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonPatrol : MonoBehaviour 
{
	public Transform targetA;
    public static float trailTime = 5.0f;
	public Transform targetB;
	//public Transform player;
	static Animator anim;
    GameObject temp;
	NavMeshAgent patrolAgent;
	Vector3 targetDirection;
    public GridBehavior Grid;
    bool touchedA = false;
    bool touchedB = false;
    bool Solveable = false;
    float initalTime = 0;
    Vector2 priorLocation;
    float timer;
    bool timeStart=false;
    bool stuck = false;
    bool doOnce = true;
    float checkTime;
    // Use this for initialization
    void Start () {
        this.GetComponent<TrailRenderer>().time = trailTime;
        this.transform.position = targetA.transform.position;
		patrolAgent = GetComponent<NavMeshAgent> ();
		targetDirection = targetB.position;
        checkTime = Time.time + 0.1f;

    }
    /*
	void chasePlayer()
	{
		//copied from previous skeleton script
		if (Vector3.Distance(player.position, this.transform.position) < 100.0f) 
		{
			Vector3 direction = player.position - this.transform.position;
			direction.y = 0;

			this.transform.rotation = Quaternion.Slerp((Quaternion)this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);


			if (direction.magnitude < 20.0f && direction.magnitude > 2.0f) {
				this.transform.Translate (0, 0, 0.04f);
				anim.SetBool ("isWalking", true);
				anim.SetBool ("isIdle", false);
				anim.SetBool ("isAttacking", false);
			} 
			else
			{
				anim.SetBool ("isWalking", false);
				anim.SetBool ("isIdle", true);
				anim.SetBool ("isAttacking", false);
			}

			if (direction.magnitude < 2.0f) 
			{
				anim.SetBool ("isWalking", false);
				anim.SetBool ("isIdle", false);
				anim.SetBool ("isAttacking", true);
			}

		}
	}*/

	// Update is called once per frame
	void Update () 
	{


        if (GameStateManager.GetCurrentState() == GameStateManager.Types.Build)
        {
            if (Grid.GetRecentChange())
            {
                Grid.SetRecentChange(false);
                temp = Instantiate(gameObject, targetA.transform.position, Quaternion.identity);
                temp.GetComponent<NavMeshAgent>().SetDestination(targetDirection);
                Destroy(this.gameObject);
                Debug.Log("Called for some reason");

            }

            if (doOnce && Time.time >= checkTime)
            {
                doOnce = false;
                if (CalculateNewPath())
                {

                    Grid.SetSolvable(true);
                    GetComponent<TrailRenderer>().material.color = new Color(0, 0, 100);
                   
                }
                else
                {

                    Grid.SetSolvable(false);
                    GetComponent<TrailRenderer>().material.color = new Color(100, 0, 0);
                    
                } 
            }

            Vector3 targetBDistance = targetB.position - this.transform.position;
            //bool initialDecision = true;

            /*if (Vector3.Distance(targetA.position, this.transform.position) < Vector3.Distance(targetB.position, this.transform.position)) {
                targetDirection = targetA.position;
            } else {
                targetDirection = targetB.position;
            }*/

            if (Vector3.Distance(targetDirection, this.transform.position) < 2.0f)
            {
                if (targetDirection == targetB.position)
                {
                    touchedB = true;
                    if (touchedB && Grid.GetRecentChange() == false)
                    {

                        trailTime = Time.time - initalTime;
                        this.GetComponent<TrailRenderer>().time = trailTime;
                        if (GetComponent<NavMeshAgent>().isActiveAndEnabled)
                        {
                            GetComponent<NavMeshAgent>().Stop();
                        }

                    }

                }
            }
        }


		//chasePlayer ();
	}

    bool CalculateNewPath()
    {
        NavMeshPath navpath = new NavMeshPath();
        GetComponent<NavMeshAgent>().CalculatePath(targetDirection, navpath);
        print("New path calculated");
        if (navpath.status != NavMeshPathStatus.PathComplete)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
