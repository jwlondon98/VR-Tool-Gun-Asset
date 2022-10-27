/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using UnityEngine;

namespace JLO_VR.ToolGun
{
    /// <summary>
    /// Configurable Mesh Renderer
    /// </summary>
    public class ConfigurableMeshRenderer : ConfigurableComponent
    {
        #region Declarations

        [SerializeField] private MeshRenderer meshRenderer;
        
        #endregion
        
        #region Custom Methods

        public override void EnableComponent()
        {
            meshRenderer.enabled = true;
        }

        public override void DisableComponent()
        {
            meshRenderer.enabled = false;
        }
        
        #endregion
    }
}