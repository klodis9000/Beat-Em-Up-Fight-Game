﻿using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    private Image healthUI;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        healthUI = GameObject.FindWithTag("HealthUI").GetComponent<Image>();
    }

    public void DisplayHealth(float value)
    {
        value /= 100;

        if (value <= 0f)
        {
            value = 0f;
        }

        healthUI.fillAmount = value;
    }
    
    
    
    
}
