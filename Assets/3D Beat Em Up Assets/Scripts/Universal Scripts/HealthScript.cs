using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Random = System.Random;

public class HealthScript : MonoBehaviour
{
    public float health = 100f;

    private CharacterAnimation animationScript;
    private EnemyMovement enemyMovement;

    private bool characterDied;

    public bool isPlayer;

    private HealthUI healthUI;

    //private int index = UnityEngine.Random.Range(0, 2);
    
    void Awake()
    {
        animationScript = GetComponentInChildren<CharacterAnimation>();
        if (isPlayer)
        {
            healthUI = GetComponent<HealthUI>();
        }
    }
    //нанесение урона
    public void ApplyDamage(float damage, bool knockDown)
    {
        //если персонаж мёртв, то выйти
        if (characterDied) { return; }

        health -= damage;

        if (isPlayer)
        {
            //display health UI
            healthUI.DisplayHealth(health);
        }

        if (health <= 0)
        {
            animationScript.Death();
            characterDied = true;

            if (isPlayer)
            {
                GameObject.FindWithTag("Enemy").GetComponent<EnemyMovement>().enabled = false;
            }
            
            
            return;
        }

        if (!isPlayer)
        {
            if (knockDown)
            {
                if (UnityEngine.Random.Range(0, 2) > 0) 
                {
                    animationScript.KnockDown();
                }
            }
            else
            {
                //animationScript.Hit();
                if (UnityEngine.Random.Range(0, 3) > 1) 
                {
                    animationScript.Hit();
                }
            }
        }



    }
    
}
