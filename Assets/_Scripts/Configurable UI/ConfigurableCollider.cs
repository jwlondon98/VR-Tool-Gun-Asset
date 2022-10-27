/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using UnityEngine;

namespace JLO_VR.ToolGun
{
    /// <summary>
    /// Configurable Collider
    /// </summary>
    public class ConfigurableCollider : ConfigurableComponent
    {
        #region Declarations

        [SerializeField] private Collider collider;
        
        #endregion
        
        #region Custom Methods

        public override void EnableComponent()
        {
            collider.enabled = true;
        }

        public override void DisableComponent()
        {
            collider.enabled = false;
        }
        
        #endregion
    }
}