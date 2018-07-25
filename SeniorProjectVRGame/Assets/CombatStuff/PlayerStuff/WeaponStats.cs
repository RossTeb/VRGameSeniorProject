using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;


public class WeaponStats : MonoBehaviour {

    FirstPersonControllerEditMe PlayerLOS;
    public Animator WeaponAnimations; //the fps controller needs access to this to control weapon animations
    public int DamageValue; //change this value in unity
    public int WeaponRange;
    //for ranged
    public Rigidbody Projectile;
    public float ProjectileSpeed;

    // Use this for initialization
    void Start ()
    {
        WeaponAnimations = GetComponent<Animator>();
        PlayerLOS = GetComponentInParent<FirstPersonControllerEditMe>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1"))
        {
            WeaponAnimations.SetBool("isIdle", false);
            WeaponAnimations.SetBool("isAttacking", true);
            //WeaponAnimations.Play("SwordAttack");
            //melee hit logic does not work when called from this class
        }
        else
        {
            WeaponAnimations.SetBool("isAttacking", false);
            WeaponAnimations.SetBool("isIdle", true);
        }
    }

    public void MeleeAttack()
    {
        PlayerLOS.thisIsDumb();
        //melee hit logic does not work when called from this class
    }

    public void RangedAttack()
    {
        PlayerLOS.FireRangedAttack();
    }
}
