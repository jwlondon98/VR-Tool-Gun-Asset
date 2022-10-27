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
    /// Main class responsible for handling high-level tool gun functionality.
    /// </summary>
    public class ToolGun : MonoSingleton<ToolGun>
    {
        #region Declarations

        /// <summary>
        /// The <see cref="ModeType"/> that is currently active.
        /// </summary>
        [SerializeField] private ModeType currentModeType;

        /// <summary>
        /// The currently active <see cref="ToolGunMode"/>.
        /// </summary>
        public ToolGunMode currentMode
        {
            get { return modeDictionary[currentModeType]; }
        }

        /// <summary>
        /// Serializable Dictionary for <see cref="ToolGunMode"/>
        /// </summary>
        [SerializeField] private ModeDictionary modeDictionary;
        [Serializable] public class ModeDictionary : SerializableDictionary<ModeType, ToolGunMode> { }

        /// <summary>
        /// The number of modes
        /// </summary>
        private int modeCount
        {
            get { return modeDictionary.Count; }
        }

        /// <summary>
        /// The raycast origin's transform
        /// </summary>
        [SerializeField] private Transform m_raycastOrigin;
        public Transform raycastOrigin
        {
            get { return m_raycastOrigin; }
        }

        /// <summary>
        /// Raycast distance of the current mode, or the modifiable raycast distance.
        /// </summary>
        public float raycastDistance
        {
            get
            {
                if (!VRPointer.instance.raycastDistanceModEnabled)
                    return currentMode.raycastDistance;
                else
                    return VRPointer.instance.raycastDistanceModSum;
            }
        }

        /// <summary>
        /// The last active <see cref="SelectableObject"/>
        /// </summary>
        [SerializeField] private SelectableObject m_lastSelectedObject;
        public SelectableObject lastSelectedObject
        {
            get { return m_lastSelectedObject; }
            private set { m_lastSelectedObject = value; }
        }

        /// <summary>
        /// The layer mask for selectable objects
        /// </summary>
        [Header("Layer Masks")]
        [SerializeField] private LayerMask selectableObjectLayerMask;
        
        /// <summary>
        /// The layer mask for surfaces that can be hit when raycasting
        /// </summary>
        [SerializeField] private LayerMask raycastableSurfaceLayerMask;
        
        #endregion

        #region Unity Methods

        public override void Awake()
        {
            base.Awake();
            
            // init mode to NONE
            SetMode(ModeType.NONE);
        }

        void OnDrawGizmos()
        {
            // show the raycast in editor
            Gizmos.color = Color.red;
            Gizmos.DrawLine(raycastOrigin.position, raycastOrigin.position + raycastOrigin.forward * currentMode.raycastDistance);
        }

        void Update()
        {
            DetectInputs();
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Detects various inputs on Update
        /// </summary>
        private void DetectInputs()
        {
            if (VRInputManager.instance.GetButtonPressed(VRInputManager.VRInputType.RIGHT_INDEX_TRIGGER) &&
                !VRPointer.instance.pointerOnUI)
            {
                currentMode.OnModePrimaryTriggered();
            }

            if (VRInputManager.instance.GetButtonPressed(VRInputManager.VRInputType.RIGHT_SECONDARY_BTN))
                CycleMode(Direction.UP);
            
            if (VRInputManager.instance.GetButtonPressed(VRInputManager.VRInputType.RIGHT_PRIMARY_BTN))
                CycleMode(Direction.DOWN);

            if (VRInputManager.instance.GetButtonPressed(VRInputManager.VRInputType.RIGHT_JOYSTICK_CLICK))
                VRPointer.instance.ToggleRaycastDistanceModifier();
        }

        #region Mode Control

        /// <summary>
        /// Sets the <see cref="ToolGun"/>'s active mode
        /// </summary>
        /// <param name="mode"></param>
        public void SetMode(ModeType mode)
        {
            currentMode.OnModeEnd();
            
            currentModeType = mode;
            
            currentMode.OnModeStart();

            ToolGunUIManager.instance.SetToolGunMode(mode);
        }
        
        /// <summary>
        /// Cycles up/down through the <see cref="ToolGun"/>'s mode
        /// </summary>
        /// <param name="dir"></param>
        public void CycleMode(Direction dir)
        {
            ModeType nextMode = ModeType.NONE;
            
            switch (dir)
            {
                case Direction.UP:
                    nextMode = currentModeType + 1;
                    if ((int)nextMode > modeCount - 1)
                        nextMode = 0;
                    SetMode(nextMode);
                    break;
                case Direction.DOWN:
                    nextMode = currentModeType - 1;
                    if ((int)nextMode < 0)
                        nextMode = (ModeType)(modeCount - 1);
                    SetMode(nextMode);
                    break;
            }
        }

        #endregion
        
        #region Raycasting
        
        /// <summary>
        /// Raycasts from the front of the tool gun. Tries to return a hit GameObject.
        /// </summary>
        /// <returns>A GameObject hit by a raycast, or null.</returns>
        public SelectableObject TryGetSelectableObject()
        {
            RaycastHit hit = Raycast(selectableObjectLayerMask);
            if (hit.collider)
            {
                lastSelectedObject = hit.collider.GetComponent<SelectableObject>();
                return lastSelectedObject;
            }

            lastSelectedObject = null;
            return null;
        }
        
        /// <summary>
        /// Returns a <see cref="RaycastHit"/> found on surfaces with respect to the <see cref="raycastableSurfaceLayerMask"/> 
        /// </summary>
        public RaycastHit GetRaycastHitOnSurface()
        {
            return Raycast(raycastableSurfaceLayerMask);
        }

        /// <summary>
        /// Returns a raycast hit with respect to the layerMask param
        /// </summary>
        private RaycastHit Raycast(LayerMask layerMask)
        {
            RaycastHit hit;
            Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, raycastDistance, layerMask);
            return hit;
        }

        #endregion
        
        #endregion
    }
}