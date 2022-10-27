/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using System;
using UnityEngine;

namespace JLO_VR.ToolGun
{
    /// <summary>
    /// Configurable Rigidbody
    /// </summary>
    public class ConfigurableRigidbody : ConfigurableComponent
    {
        #region Declarations

        [SerializeField] private Rigidbody rigidbody;

        [SerializeField] private IncrementDecrementGroup massIncDecGroup;
        
        public event Action<float> OnMassValueChanged;

        #endregion
        
        #region Unity Methods

        void Start()
        {
            massIncDecGroup.OnIncrement += SetMass;
            massIncDecGroup.OnDecrement += SetMass;
            OnMassValueChanged += massIncDecGroup.UpdateValueText;
        }

        #endregion
        
        #region Custom Methods

        public override void EnableComponent()
        {
        }

        public override void DisableComponent()
        {
        }

        public void EnableIsKinematic()
        {
            rigidbody.isKinematic = true;
        }
        
        public void DisableIsKinematic()
        {
            rigidbody.isKinematic = false;
        }
        
        public void EnableGravity()
        {
            rigidbody.useGravity = true;
        }

        public void DisableGravity()
        {
            rigidbody.useGravity = false;
        }
        
        public void SetMass(float valToAdd)
        {
            Debug.Log("set mass valToAdd: " + valToAdd);
            
            rigidbody.mass += valToAdd;

            OnMassValueChanged.Invoke(rigidbody.mass);
        }

        #endregion
    }
}