/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using UnityEngine;
using UnityEngine.XR;

namespace JLO_VR.ToolGun
{
    /// <summary>
    /// Input Manager for VR input.
    /// </summary>
    public class VRInputManager : MonoSingleton<VRInputManager>
    {
        OculusTouchController inputActions;

        public enum VRInputType
        {
            LEFT_PRIMARY_BTN, LEFT_SECONDARY_BTN,
            LEFT_JOYSTICK, LEFT_JOYSTICK_CLICK,
            LEFT_INDEX_TRIGGER, LEFT_GRIP_TRIGGER,
            RIGHT_PRIMARY_BTN, RIGHT_SECONDARY_BTN,
            RIGHT_JOYSTICK, RIGHT_JOYSTICK_CLICK,
            RIGHT_INDEX_TRIGGER, RIGHT_GRIP_TRIGGER,
        }

        public override void Awake()
        {
            base.Awake();

            inputActions = new OculusTouchController();
            inputActions.Enable();
        }

        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        private void OnDestroy()
        {
            inputActions.Dispose();
        }

        public bool GetButtonPressed(VRInputType inputType)
        {
            switch (inputType)
            {
                case VRInputType.LEFT_PRIMARY_BTN:
                    return inputActions.LeftController.Y_Button.WasPressedThisFrame();
                case VRInputType.RIGHT_PRIMARY_BTN:
                    return inputActions.RightController.A_Button.WasPressedThisFrame();
                case VRInputType.LEFT_SECONDARY_BTN:
                    return inputActions.LeftController.X_Button.WasPressedThisFrame();
                case VRInputType.RIGHT_SECONDARY_BTN:
                    return inputActions.RightController.B_Button.WasPressedThisFrame();
                case VRInputType.LEFT_INDEX_TRIGGER:
                    return inputActions.LeftController.Trigger.WasPressedThisFrame();
                case VRInputType.RIGHT_INDEX_TRIGGER:
                    return inputActions.RightController.Trigger.WasPressedThisFrame();
                case VRInputType.LEFT_GRIP_TRIGGER:
                    return inputActions.LeftController.Grip.WasPressedThisFrame();
                case VRInputType.RIGHT_GRIP_TRIGGER:
                    return inputActions.RightController.Grip.WasPressedThisFrame();
                case VRInputType.LEFT_JOYSTICK_CLICK:
                    return inputActions.LeftController.Joystick_Click.WasPressedThisFrame();
                case VRInputType.RIGHT_JOYSTICK_CLICK:
                    return inputActions.RightController.Joystick_Click.WasPressedThisFrame();
                default:
                    Debug.LogError("[VRInputManager] GetButtonPressed error. Invalid VRInputType provided. Unable to return valid input.");
                    return false;
            }
        }

        public bool GetButton(VRInputType inputType)
        {
            switch (inputType)
            {
                case VRInputType.LEFT_PRIMARY_BTN:
                    return inputActions.LeftController.Y_Button.IsPressed();
                case VRInputType.RIGHT_PRIMARY_BTN:
                    return inputActions.RightController.A_Button.IsPressed();
                case VRInputType.LEFT_SECONDARY_BTN:
                    return inputActions.LeftController.X_Button.IsPressed();
                case VRInputType.RIGHT_SECONDARY_BTN:
                    return inputActions.RightController.B_Button.IsPressed();
                case VRInputType.LEFT_INDEX_TRIGGER:
                    return inputActions.LeftController.Trigger.IsPressed();
                case VRInputType.RIGHT_INDEX_TRIGGER:
                    return inputActions.RightController.Trigger.IsPressed();
                case VRInputType.LEFT_GRIP_TRIGGER:
                    return inputActions.LeftController.Grip.IsPressed();
                case VRInputType.RIGHT_GRIP_TRIGGER:
                    return inputActions.RightController.Grip.IsPressed();
                case VRInputType.LEFT_JOYSTICK_CLICK:
                    return inputActions.LeftController.Joystick_Click.IsPressed();
                case VRInputType.RIGHT_JOYSTICK_CLICK:
                    return inputActions.RightController.Joystick_Click.IsPressed();
                default:
                    Debug.LogError("[VRInputManager] GetButton error. Invalid VRInputType provided. Unable to return valid input.");
                    return false;
            }
        }

        public bool GetButtonReleased(VRInputType inputType)
        {
            switch (inputType)
            {
                case VRInputType.LEFT_PRIMARY_BTN:
                    return inputActions.LeftController.Y_Button.WasReleasedThisFrame();
                case VRInputType.RIGHT_PRIMARY_BTN:
                    return inputActions.RightController.A_Button.WasReleasedThisFrame();
                case VRInputType.LEFT_SECONDARY_BTN:
                    return inputActions.LeftController.X_Button.WasReleasedThisFrame();
                case VRInputType.RIGHT_SECONDARY_BTN:
                    return inputActions.RightController.B_Button.WasReleasedThisFrame();
                case VRInputType.LEFT_INDEX_TRIGGER:
                    return inputActions.LeftController.Trigger.WasReleasedThisFrame();
                case VRInputType.RIGHT_INDEX_TRIGGER:
                    return inputActions.RightController.Trigger.WasReleasedThisFrame();
                case VRInputType.LEFT_GRIP_TRIGGER:
                    return inputActions.LeftController.Grip.WasReleasedThisFrame();
                case VRInputType.RIGHT_GRIP_TRIGGER:
                    return inputActions.RightController.Grip.WasReleasedThisFrame();
                case VRInputType.LEFT_JOYSTICK_CLICK:
                    return inputActions.LeftController.Joystick_Click.WasReleasedThisFrame();
                case VRInputType.RIGHT_JOYSTICK_CLICK:
                    return inputActions.RightController.Joystick_Click.WasReleasedThisFrame();
                default:
                    Debug.LogError("[VRInputManager] GetButtonReleased error. Invalid VRInputType provided. Unable to return valid input.");
                    return false;
            }
        }

        public Vector2 GetJoystickVector(VRInputType inputType)
        {
            switch (inputType)
            {
                case VRInputType.LEFT_JOYSTICK:
                    return new Vector2(
                        inputActions.LeftController.Joystick_X.ReadValue<float>(),
                        inputActions.LeftController.Joystick_Y.ReadValue<float>());
                case VRInputType.RIGHT_JOYSTICK:
                    return new Vector2(
                        inputActions.RightController.Joystick_X.ReadValue<float>(),
                        inputActions.RightController.Joystick_Y.ReadValue<float>());
                default:
                    Debug.LogError("[VRInputManager] GetJoystick error. Invalid VRInputType provided. Unable to return valid input.");
                    return new Vector2(0, 0);
            }
        }

        public float GetTriggerValue(VRInputType inputType)
        {
            switch (inputType)
            {
                case VRInputType.LEFT_INDEX_TRIGGER:
                    return inputActions.LeftController.Trigger_Value.ReadValue<float>();
                case VRInputType.LEFT_GRIP_TRIGGER:
                    return inputActions.LeftController.Grip_Value.ReadValue<float>();
                case VRInputType.RIGHT_INDEX_TRIGGER:
                    return inputActions.RightController.Trigger_Value.ReadValue<float>();
                case VRInputType.RIGHT_GRIP_TRIGGER:
                    return inputActions.RightController.Grip_Value.ReadValue<float>();
                default:
                    Debug.LogError("[VRInputManager] GetTriggerValue error. Invalid VRInputType provided. Unable to return valid input.");
                    return 0;
            }
        }

        public VRControllerData GetControllerData()
        {
            var rightPos = inputActions.RightController.Position.ReadValue<Vector3>();
            var rightRot = inputActions.RightController.Rotation.ReadValue<Quaternion>();
            var leftPos = inputActions.LeftController.Position.ReadValue<Vector3>();
            var leftRot = inputActions.LeftController.Rotation.ReadValue<Quaternion>();
            var rightTracking = (InputTrackingState)inputActions.RightController.Tracking_State.ReadValue<int>();
            var leftTracking = (InputTrackingState)inputActions.LeftController.Tracking_State.ReadValue<int>();

            return new VRControllerData(rightPos, rightRot, leftPos, leftRot, rightTracking, leftTracking);
        }
    }
}