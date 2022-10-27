/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

namespace JLO_VR.ToolGun
{
    /// <summary>
    /// Class used to handle the <see cref="ToolGun"/> Destroy mode functionality
    /// </summary>
    public class DestroyMode : ToolGunMode
    {
        #region Declarations

        #endregion

        #region Custom Methods

        /// <summary>
        /// Called when the primary input action is detected when this mode is active
        ///
        /// Destroys a hit object
        /// </summary>
        public override void OnModePrimaryTriggered()
        {
            var obj = ToolGun.instance.TryGetSelectableObject();
            if (obj)
                Destroy(obj.gameObject);
        }

        /// <summary>
        /// Called on the current mode on update
        /// </summary>
        public override void OnModeUpdate()
        {
            
        }

        #endregion
    }
}