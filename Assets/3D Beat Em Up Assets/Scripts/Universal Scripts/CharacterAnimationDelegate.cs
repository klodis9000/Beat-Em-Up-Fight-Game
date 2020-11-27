using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationDelegate : MonoBehaviour
{
    public GameObject leftArmAttackPoint,
        rightArmAttackPoint,
        leftLegAttackPoint,
        rightLegAttackPoint;

    public float standUpTimer = 2f;

    private CharacterAnimation animationScript;

    private AudioSource audioSource;

    [SerializeField]
    //звук летящей руки, звук падения, звук падения о землю, звук смерти
    private AudioClip whooshSound, fallSound, groundHitSound, deadSound;

    private EnemyMovement enemyMovement;

    private ShakeCamera shakeCamera;
    
    void Awake()
    {
        animationScript = GetComponent<CharacterAnimation>();
        audioSource = GetComponent<AudioSource>();

        if (gameObject.CompareTag("Enemy"))
        {
            enemyMovement = GetComponentInParent<EnemyMovement>();
        }

        shakeCamera = GameObject.FindWithTag("MainCamera").GetComponent<ShakeCamera>();

    }


    void leftArmAttackOn()
    {
        leftArmAttackPoint.SetActive(true);
    }
    void leftArmAttackOff()
    {
        if (leftArmAttackPoint.activeInHierarchy)
        {
            leftArmAttackPoint.SetActive(false);
        }
    }
    void rightArmAttackOn()
    {
        rightArmAttackPoint.SetActive(true);
    }
    void rightArmAttackOff()
    {
        if (rightArmAttackPoint.activeInHierarchy)
        {
            rightArmAttackPoint.SetActive(false);
        }
    }

    void leftLegAttackOn()
    {
        leftLegAttackPoint.SetActive(true);
    }
    void leftLegAttackOff()
    {
        if (leftLegAttackPoint.activeInHierarchy)
        {
            leftLegAttackPoint.SetActive(false);
        }
    }
    void rightLegAttackOn()
    {
        rightLegAttackPoint.SetActive(true);
    }
    void rightLegAttackOff()
    {
        if (leftLegAttackPoint.activeInHierarchy)
        {
            rightLegAttackPoint.SetActive(false);
        }
    }
    void TagLeftArm()
    {
        leftArmAttackPoint.tag = "LeftArm";
    }
    void UnTagLeftArm()
    {
        leftArmAttackPoint.tag = "Untagged";
    }
    void TagLeftLeg()
    {
        leftArmAttackPoint.tag = "LeftLeg";
    }
    void UnTagLeftLeg()
    {
        leftArmAttackPoint.tag = "Untagged";
    }

    void EnemyStandUp()
    {
        StartCoroutine(StandUpTime());
    }

    IEnumerator StandUpTime()
    {
        yield return new WaitForSeconds(standUpTimer);
        animationScript.StandUp();
    }

    public void AttackFXSound() //звук атаки
    {
        audioSource.volume = 0.2f; // громкость звучания
        audioSource.clip = whooshSound;
        audioSource.Play();
    }
    
    void CharacterDiedSound() //звук смерти
    {
        audioSource.volume = 1f; // громкость звучания
        audioSource.clip = deadSound;
        audioSource.Play();
    }
    
    void EnemyKnockedDown() //звук падения врага
    {
        audioSource.clip = fallSound;
        audioSource.Play();
    }

    void EnemyHitGround() //звук столкновения с землёй
    {
        audioSource.clip = groundHitSound;
        audioSource.Play();
    }

    void DisableMovement() //остановка движения
    {
        enemyMovement.enabled = false;

        transform.parent.gameObject.layer = 0; //установка вражеского родителя на слой по умолчанию
    }

    void EnableMovement() //включение движения
    {
        enemyMovement.enabled = true;
        transform.parent.gameObject.layer = 10; //установка вражеского родителя на вражеский слой 
    }

    void ShakeCameraOnFall()
    {
        shakeCamera.ShouldShake = true;
    }

    void CharacterDied()
    {
        Invoke("DeactivateGameObject", 2f);
    }

    void DeactivateGameObject()
    {
        EnemyManager.instance.SpawnEnemy();
        gameObject.SetActive(false);
    }
    
    
}
