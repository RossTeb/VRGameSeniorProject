using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardScript : MonoBehaviour {
    public int range;
    public float check_period;
    private float checkTime=0;
    public Transform TipofWand;
    public GameObject Attack;
    private float restPeriod;
    bool JustAttacked=false;
    Transform player;
    public DetectDamagingHitEnemy hit;
    float despawnWait=5.0f;
    float despawnTime = -1;
    bool dead = false;
    // Use this for initialization
    void Start () {
        hit = GetComponent<DetectDamagingHitEnemy>();

        if (check_period == 0)
        {
            check_period = 5;
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (hit.health>0)
        {
            if (Time.time >= checkTime)
            {
                checkTime = Time.time + check_period;
                try
                {
                    var player = LookForPlayer(range);
                    if (!player.Equals(null))
                    {
                        gameObject.transform.LookAt(player.transform);
                        GetComponent<Animator>().SetBool("isIdle", false);
                        GetComponent<Animator>().SetBool("isAttacking", true);
                        restPeriod = Time.time + (check_period / 3);
                        JustAttacked = true;
                        this.player = player.transform;
                    }
                }
                catch (System.Exception)
                {


                }
            }
            else if (Time.time >= restPeriod && JustAttacked)
            {
                Shoot(player.GetComponent<Collider>());
                GetComponent<Animator>().SetBool("isAttacking", false);
                GetComponent<Animator>().SetBool("isIdle", true);
                JustAttacked = false;
            } 
        }
        else
        {
            GetComponent<Animator>().SetBool("isAttacking", false);
            GetComponent<Animator>().SetBool("isIdle", false);
            GetComponent<Animator>().SetBool("isDead", true);

            if (!dead)
            {
                despawnTime = Time.time + despawnWait;
                    dead = true;
            }
            else if (despawnTime<Time.time&&despawnTime>0)
            {
                Destroy(gameObject);
            }
        }

	}

    Collider LookForPlayer( float radius)
    {
        var layer =1<< 8;
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, radius,layer);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if(hitColliders[i].tag == "Player")
            {
                return hitColliders[i];
            }
        }
        return null;
    }
    void Shoot(Collider player)
    {
        if (!TipofWand.Equals(null))
        {
            Instantiate(Attack, TipofWand.position, Quaternion.identity);
        }
    }
    public void DealDamage()
    {

    }
    //public void ApplyDamage(int damage)
    //{
    //    hit.health -= damage;
    //}
}
