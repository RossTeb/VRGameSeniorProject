using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class DetectDamagingHitEnemy : MonoBehaviour
{

    //public Slider HealthBar;
    //private Animator anim;
    public int health;

    private void OnTriggerEnter(Collider other)
    {
        //if (offenderMoveState.m_IsAttacking)
        //    HealthBar.value -= offendingCombatStats.DamageValue;

    }

    // Use this for initialization
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            return;
    }

    public void ApplyDamage(int DamageTaken)
    {
        health -= DamageTaken;
    }
}
