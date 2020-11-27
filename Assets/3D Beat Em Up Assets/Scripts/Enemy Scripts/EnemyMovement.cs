using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyMovement : MonoBehaviour
{
    private CharacterAnimation enemyAnim;

    private Rigidbody myBody;
    public float speed = 2f;

    private Transform playerTarget;

    //расстояние атаки
    public float attackDistance = 1f;
    //преследование игрока после атаки
    public float chasePlayerAfterAttack = 1f;

    //текущее время атаки
    private float currentAttackTime;
    //базовое время атаки
    private float defaultAttackTime = 2f;
    //следование за игроком, атака игрока
    private bool followPlayer, attackPlayer;

    private Random rnd = new Random();
    
    // Start is called before the first frame update
    void Awake()
    {
        //получаю скрипт анимации врага
        enemyAnim = GetComponentInChildren<CharacterAnimation>();
        myBody = GetComponent<Rigidbody>();
        //присваю в качестве цели объект с тегом "Player"
        playerTarget = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        followPlayer = true;
        currentAttackTime = defaultAttackTime;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void FixedUpdate()
    {
        FollowTarget();
    }

    //преследование игрока
    void FollowTarget()
    {
        //если не нужено следовать за игроком
        //выйти из функции
        if (!followPlayer) { return; }
        
        //Vector3.Distance() возвращает расстоние между объектом и целью
        //если расстояние между врагом и игроком больше чем дисстанция для атаки, то направится к игроку
        if (Vector3.Distance(transform.position, playerTarget.position) > attackDistance)
        {
            //повернуться к игроку
            transform.LookAt(playerTarget);
            //двигаться к игроку
            myBody.velocity = transform.forward * speed;
            //если перемещение игрока не ноль, то идти
            if (myBody.velocity.sqrMagnitude != 0)
            {
                enemyAnim.Walk(true);
            }
            
        }
        //иначе если расстояние между объектом и целью меньше или равно дистанции атаки
        else if (Vector3.Distance(transform.position, playerTarget.position) <= attackDistance) 
        {
            //прекратить движение
            myBody.velocity=Vector3.zero;
            enemyAnim.Walk(false);

            followPlayer = false; //прекратить следование
            attackPlayer = true; //начать атаку

        }
        
    }

    void Attack()
    {
        //если не нужно атаковать игрока
        //выйти из функции
        if (!attackPlayer) { return; }

        currentAttackTime += Time.deltaTime;

        if (currentAttackTime > defaultAttackTime)
        {
            /*проигрывать случайную анимацию
            enemyAnim.EnemyAttack(Random.Range(0, 3)); //не работает, позже посмотреть*/
            
            //проигрывать случайную анимацию
            enemyAnim.EnemyAttack(rnd.Next(0, 3));

            currentAttackTime = 0f;
        }
        
        if (Vector3.Distance(transform.position, playerTarget.position) > attackDistance + chasePlayerAfterAttack)
        {
            attackPlayer = false;
            followPlayer = true;
        }
        




    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}
