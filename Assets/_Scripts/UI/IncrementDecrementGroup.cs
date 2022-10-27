/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace JLO_VR.ToolGun
{
    public class IncrementDecrementGroup : MonoBehaviour
    {
        public event Action<float> OnIncrement;
        public event Action<float> OnDecrement;

        [SerializeField] private TMP_Text valueText;
        
        public void Increment(float val)
        {
            Debug.Log("Increment val: " + val);
            
            OnIncrement.Invoke(val);
        }

        public void Decrement(float val)
        {
            Debug.Log("Decrement val: " + val);

            OnDecrement.Invoke(-val);
        }

        public void UpdateValueText(float value)
        {
            Debug.Log("UpdateValueText value: " + value);
            
            valueText.text = value.ToString();
        }
    }
}