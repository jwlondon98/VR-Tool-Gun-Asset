/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace JLO_VR.ToolGun
{
    /// <summary>
    /// Manages the UI elements for the Tool Gun.
    /// </summary>
    public class ToolGunUIManager : MonoSingleton<ToolGunUIManager>
    {
        #region Declarations

        /// <summary>
        /// Serializable Dictionary for storing tool gun mode-specific UI panels that get enabled/disabled.
        /// </summary>
        [Serializable]
        public class ModeUIPanelDictionary : SerializableDictionary<ModeType, GameObject> {}
        public ModeUIPanelDictionary modeUIPanelDictionary;
        
        /// <summary>
        /// Serializable Dictionary for storing tool gun mode-specific UI buttons.
        /// </summary>
        [Serializable]
        public class ModeButtonDictionary : SerializableDictionary<ModeType, Button> {}
        public ModeButtonDictionary modeButtonDictionary;
        
        /// <summary>
        /// The active mode's UI panel.
        /// </summary>
        private GameObject activeModeUIPanel;
        
        /// <summary>
        /// The active mode's UI button.
        /// </summary>
        private Button activeModeButton;
        
        #endregion
        
        #region Custom Methods

        /// <summary>
        /// Called by Tool Gun mode UI buttons to enable their corresponding modes.
        /// </summary>
        /// <param name="modeNum"></param>
        public void SetToolGunMode(int modeNum)
        {
            ModeType newMode = (ModeType)modeNum;
            ToolGun.instance.SetMode(newMode);

            SetToolGunMode(newMode);
        }

        /// <summary>
        /// Handles UI changes based on a newly enabled tool gun mode.
        /// </summary>
        /// <param name="newMode"></param>
        public void SetToolGunMode(ModeType newMode)
        {
            if (activeModeUIPanel)
                activeModeUIPanel.SetActive(false);
            
            if (modeUIPanelDictionary.ContainsKey(newMode))
            {
                activeModeUIPanel = modeUIPanelDictionary[newMode];
                activeModeUIPanel.SetActive(true);
            }

            if (modeButtonDictionary.ContainsKey(newMode))
            {
                var btn = modeButtonDictionary[newMode];
                btn.Select();
            }
        }

        #endregion
    }
}