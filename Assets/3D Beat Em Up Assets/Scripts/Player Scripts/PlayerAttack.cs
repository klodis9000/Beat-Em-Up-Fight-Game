using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public enum ComboState
    {
        NONE, //0
        PUNCH_1, //1
        PUNCH_2, //2
        PUNCH_3, //3
        KICK_1, //4 
        KICK_2 //5
    }

    private CharacterAnimation playerAnim;

    //активация таймера для сброса
    private bool activateTimerToReset;
    //таймер по умолчанию
    private float defaultComboTimer = 0.4f;
    //текущий таймер
    private float currentComboTimer;
    //текущий режим атаки
    private ComboState currentComboState;
    // Start is called before the first frame update
    void Awake()
    {
        playerAnim = GetComponentInChildren<CharacterAnimation>();
    }

    void Start()
    {
        currentComboTimer = defaultComboTimer;
        currentComboState = ComboState.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        ComboAttacks();
        ResetComboState();
    }


    //атака
    void ComboAttacks()
    {
        //если нажата Z, то атака руками
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentComboState == ComboState.PUNCH_3 ||
                currentComboState == ComboState.KICK_1 ||
                currentComboState == ComboState.KICK_2) { return; }
            
            currentComboState++; //переходы между анимациями и ударами
            activateTimerToReset = true; //активирую таймер для комбо
            currentComboTimer = defaultComboTimer;

            if (currentComboState == ComboState.PUNCH_1)
            {
                //проигрываю анимацию
                playerAnim.Punch1();
            }

            if (currentComboState == ComboState.PUNCH_2)
            {
                //проигрываю анимацию
                playerAnim.Punch2();
            }

            if (currentComboState == ComboState.PUNCH_3)
            {
                //проигрываю анимацию
                playerAnim.Punch3();
            }
        }

        //если нажата X, то атака ногами
        if (Input.GetKeyDown(KeyCode.X))
        {
            //если текущее комбо kick2 или punch3, то вернуть пустое значение
            if (currentComboState == ComboState.KICK_2 ||
                currentComboState == ComboState.PUNCH_3) { return; }

            //если текущее состояние NONE, PUNCH1 или PUNCH2, то можно установить текущее комбо на KICK1
            if (currentComboState == ComboState.NONE ||
                currentComboState == ComboState.PUNCH_1 ||
                currentComboState == ComboState.PUNCH_2) { currentComboState = ComboState.KICK_1; }
            //иначе если текущее состояние kick1, то перейти на следующее состояние
            else if (currentComboState == ComboState.KICK_1) { currentComboState++; }

            activateTimerToReset = true;
            currentComboTimer = defaultComboTimer;

            if (currentComboState == ComboState.KICK_1)
            {
                playerAnim.Kick1();
            }
            if (currentComboState == ComboState.KICK_2)
            {
                playerAnim.Kick2();
            }
        }
    }
    
    //сброс комбо атаки
    void ResetComboState()
    {
        if (activateTimerToReset)
        {
            currentComboTimer -= Time.deltaTime;

            if (currentComboTimer <= 0)
            {
                currentComboState = ComboState.NONE;
                activateTimerToReset = false;
                currentComboTimer = defaultComboTimer;
            }
            
        }
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}
