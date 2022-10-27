/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using UnityEngine;

namespace JLO_VR.ToolGun
{
    /// <summary>
    /// Class used for configuring the settings for various <see cref="SelectableObject"/>
    /// </summary>
    public class ConfigureMode : ToolGunMode
    {
        #region Declarations
        
        private ConfigurableUI lastConfigurableUI;
        
        #endregion
        
        #region Custom Methods
        
        #region Mode Base Class Methods
        
        public override void OnModeStart()
        {
            base.OnModeStart();
        }
        
        public override void OnModePrimaryTriggered()
        {
            OnObjectSelectAttempt();
        }

        public override void OnModeUpdate()
        {
            
        }

        public override void OnModeEnd()
        {
            base.OnModeEnd();

            if (lastConfigurableUI)
            {
                lastConfigurableUI.EnableDisableUI(false);
                lastConfigurableUI = null;
            }
        }
        
        #endregion
        
        /// <summary>
        /// Logic to call during the object select state
        /// </summary>
        private void OnObjectSelectAttempt()
        {
            var selectableObject = ToolGun.instance.TryGetSelectableObject();
            if (selectableObject)
            {
                // enable the object's configurable UI
                lastConfigurableUI = selectableObject.configurableUI;
                lastConfigurableUI.EnableDisableUI(null);
            }   
        }
        
        #endregion
    }
}