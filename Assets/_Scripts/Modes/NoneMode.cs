/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

namespace JLO_VR.ToolGun
{
    /// <summary>
    /// Class used for temporarily testing new <see cref="ToolGun"/> mode functionality.
    /// Also operates as <see cref="ModeType"/> NONE (no functionality required).
    /// </summary>
    public class NoneMode : ToolGunMode
    {
        #region Custom Methods
        
        public override void OnModePrimaryTriggered()
        {
        }

        public override void OnModeUpdate()
        {
            
        }

        #endregion
    }
}