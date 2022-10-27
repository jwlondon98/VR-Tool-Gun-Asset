/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using UnityEngine;

namespace JLO_VR.ToolGun
{
    /// <summary>
    /// Base class for various components that can be configured via the <see cref="ConfigurableUI"/>.
    /// </summary>
    public abstract class ConfigurableComponent : MonoBehaviour
    {
        #region Declarations

        #endregion

        #region Custom Methods

        public abstract void EnableComponent();
        public abstract void DisableComponent();

        #endregion
    }
}