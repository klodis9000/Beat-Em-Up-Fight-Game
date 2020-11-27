using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{

    public float power = 0.2f;
    public float duration = 0.2f; //продолжительность
    public float slowDownAmount = 1f; //замедление
    private bool shouldShake; //встряхивание
    private float initialDuration; //начальная продолжительность

    private Vector3 startPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
        initialDuration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        Shake();
    }

    void Shake() //встряхивание
    {
        if (shouldShake) //если встряхиваю камеру
        {
            if (duration > 0f)
            {
                transform.localPosition = startPosition + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                transform.localPosition = startPosition;
            }
        }
    }

    public bool ShouldShake
    {
        get { return shouldShake; }
        set { shouldShake = value; }
    }
    
}
