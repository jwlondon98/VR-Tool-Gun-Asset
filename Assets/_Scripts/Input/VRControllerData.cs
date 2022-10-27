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
    /// Class containing data relating to VR controllers.
    /// </summary>
    public class VRControllerData 
    {
        public Vector3 rightControllerPos { get; private set; }
        public Quaternion rightControllerRot { get; private set; }
        public Vector3 leftControllerPos { get; private set; }
        public Quaternion leftControllerRot { get; private set; }
        public InputTrackingState rightTrackingState { get; private set; }
        public InputTrackingState leftTrackingState { get; private set; }
        public bool rightTracked { get; private set; }
        public bool leftTracked { get; private set; }

        public VRControllerData(Vector3 rightControllerPos, Quaternion rightControllerRot,
            Vector3 leftControllerPos, Quaternion leftControllerRot,
             InputTrackingState rightTrackingState, InputTrackingState leftTrackingState)
        {
            this.rightControllerPos = rightControllerPos;
            this.rightControllerRot = rightControllerRot;
            this.leftControllerPos = leftControllerPos;
            this.leftControllerRot = leftControllerRot;
            this.rightTrackingState = rightTrackingState;
            this.leftTrackingState = leftTrackingState;
            rightTracked = rightTrackingState != InputTrackingState.None;
            leftTracked = leftTrackingState != InputTrackingState.None;
        }
    }
}