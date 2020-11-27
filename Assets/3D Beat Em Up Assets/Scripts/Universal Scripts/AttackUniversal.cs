using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUniversal : MonoBehaviour
{

    public LayerMask collisionLayer;
    public float radius = 1f;
    public float damage = 2f;

    public bool isPlayer, isEnemy;

    public GameObject hitFXPrefab;

    // Update is called once per frame
    void Update()
    {
        DetectCollision();
    }

    //считывание столкновения
    void DetectCollision()
    {
        //массив ударов
        //Physics.OverlapSphere получение коллайдеров которые входят сферу (первый параметр) 
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, collisionLayer);
        //если есть попадание
        if (hit.Length > 0)
        {
            if (isPlayer)
            {
                Vector3 hitFXPos = hit[0].transform.position;
                //поднятие анимации вверх по оси Y
                hitFXPos.y += 1.3f;
                
                //если игрок развернулся, то анимация сдвинется правее или левее
                if (hit[0].transform.forward.x > 0)
                {
                    hitFXPos.x += 0.3f;
                }
                else if (hit[0].transform.forward.x < 0)
                {
                    hitFXPos.x -= 0.3f;
                }
                //создание эффекта вспышки
                Instantiate(hitFXPrefab, hitFXPos, Quaternion.identity);
                //gameObject.CompareTag-проверка помечел ли gameObject тегом
                //если gameObject LeftArm или LeftLeg
                if (gameObject.CompareTag("LeftArm") || gameObject.CompareTag("LeftLeg"))
                {
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage,true);
                }
                else
                {
                    hit[0].GetComponent<HealthScript>().ApplyDamage(damage,false);
                }
                
            }

            if (isEnemy)
            {
                hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false);
            }
            
            
            gameObject.SetActive(false);
        }
        
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}
