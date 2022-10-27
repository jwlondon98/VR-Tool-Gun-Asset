/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using UnityEngine;

namespace JLO_VR.ToolGun
{
    /// <summary>
    /// Base class for all <see cref="ToolGun"/> modes
    /// </summary>
    public abstract class ToolGunMode : MonoBehaviour
    {
        [SerializeField] public bool isActive { get; protected set; }
        [SerializeField] public float raycastDistance;

        /// <summary>
        /// Called on this mode when first changing to this mode
        /// </summary>
        public virtual void OnModeStart()
        {
            isActive = true;
        }

        /// <summary>
        /// Called when the primary input action is detected when this mode is active
        /// </summary>
        public abstract void OnModePrimaryTriggered();
        
        /// <summary>
        /// Called on the current mode on update
        /// </summary>
        public abstract void OnModeUpdate();
        
        /// <summary>
        /// Called on the current mode when changing to another mode
        /// </summary>
        public virtual void OnModeEnd()
        {
            isActive = false;
        }

        void Update()
        {
            if (isActive)
                OnModeUpdate();
        }
    }
}