/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using UnityEngine;

namespace JLO_VR.ToolGun
{
    /// <summary>
    /// Class used to handle the <see cref="ToolGun"/> Move mode functionality
    /// </summary>
    public class MoveMode : ToolGunMode
    {
        #region Declarations

        /// <summary>
        /// Enum for handling the move mode state machine
        /// </summary>
        private enum MoveModeState
        {
            OBJECT_SELECT, OBJECT_MOVE
        }

        /// <summary>
        /// The currently active mode mode state
        /// </summary>
        [SerializeField] private MoveModeState moveModeState;

        /// <summary>
        /// The last selected object referenced within the <see cref="ToolGun"/>
        /// </summary>
        private SelectableObject selectedObject
        {
            get { return ToolGun.instance.lastSelectedObject; }
        }

        #endregion

        #region Custom Methods

        #region Mode Base Class Methods
        
        /// <summary>
        /// Called when the primary input action is detected when this mode is active
        /// </summary>
        public override void OnModePrimaryTriggered()
        {
            switch (moveModeState)
            {
                case MoveModeState.OBJECT_SELECT:
                    ObjectSelectState();
                    break;
                case MoveModeState.OBJECT_MOVE:
                    EndObjectMovement();
                    break;
            }
        }

        /// <summary>
        /// Called on the current mode on update
        /// </summary>
        public override void OnModeUpdate()
        {
            switch (moveModeState)
            {
                case MoveModeState.OBJECT_MOVE:
                    ObjectMoveState();
                    break;
            }
        }
        
        /// <summary>
        /// Called on the current mode when changing to another mode
        /// </summary>
        public override void OnModeEnd()
        {
            base.OnModeEnd();
            
            switch (moveModeState)
            {
                case MoveModeState.OBJECT_MOVE:
                    EndObjectMovement();
                    break;
            }
        }

        #endregion
        
        /// <summary>
        /// Logic to call during the object select state
        /// </summary>
        private void ObjectSelectState()
        {
            if (ToolGun.instance.TryGetSelectableObject() != null)
            {
                StartObjectMovement();
                SetObjectMoveModeState(MoveModeState.OBJECT_MOVE);
            }   
        }

        /// <summary>
        /// Logic to call during the object move state
        /// </summary>
        private void ObjectMoveState()
        {
            RaycastHit hit = ToolGun.instance.GetRaycastHitOnSurface();
            if (hit.collider != null)
            {
                var offset = selectedObject.GetOffset(hit.normal);
                selectedObject.transform.position = hit.point + offset;
            }
            else
            {
                selectedObject.transform.position = VRPointer.instance.raycastEndPos;
            }
        }

        /// <summary>
        /// Sets the object move mode state
        /// </summary>
        /// <param name="state">The new state to make active.</param>
        private void SetObjectMoveModeState(MoveModeState state)
        {
            moveModeState = state;
        }

        /// <summary>
        /// Called at the start of the object movement state
        /// </summary>
        private void StartObjectMovement()
        {
            // make object's rb kinematic
            // selectedObject.rb.isKinematic = true;
            // selectedObject.rb.useGravity = false;
        }

        /// <summary>
        /// Called when finished moving an object
        /// </summary>
        private void EndObjectMovement()
        {
            // make object's rb non-kinematic
            // selectedObject.rb.isKinematic = false;
            // selectedObject.rb.useGravity = true;

            SetObjectMoveModeState(MoveModeState.OBJECT_SELECT);
        }

        #endregion
    }
}