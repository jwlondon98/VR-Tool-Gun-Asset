/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JLO_VR.ToolGun
{
    /// <summary>
    /// Class for extended UI Toggle functionalities.
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class ToggleUIExt : MonoBehaviour
    {
        #region Declarations

        [SerializeField] private Toggle toggle;
        
        [SerializeField] private UnityEvent OnToggleEnabled;
        [SerializeField] private UnityEvent OnToggleDisabled;

        #endregion

        #region Unity Methods

        void OnValidate()
        {
            if (toggle == null)
                toggle = GetComponent<Toggle>();
        }

        #endregion
        
        #region Custom Methods

        /// <summary>
        /// Called by the <see cref="toggle"/>'s OnValueChanged method
        /// </summary>
        public void OnToggleValueChanged()
        {
            if (toggle.isOn)
                OnToggleEnabled.Invoke();
            else
                OnToggleDisabled.Invoke();
        }

        #endregion
    }
}