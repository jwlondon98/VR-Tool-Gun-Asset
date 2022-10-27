/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using UnityEngine;
using UnityEngine.EventSystems;

namespace JLO_VR.ToolGun
{ 
    /// <summary>
    /// A VR pointer used to interact with UI.
    /// </summary>
    public class VRPointer : MonoSingleton<VRPointer>
    {
        public bool pointerEnabled = true;

        [SerializeField]
        private GameObject lineEndpointIcon;

        public float raycastDistance
        {
            get
            {
                return ToolGun.instance.raycastDistance;
            }
        }
        
        public float raycastDistanceModSum;
        [SerializeField] private float raycastDistanceModFactor;
        [SerializeField] public bool raycastDistanceModEnabled;
        [SerializeField] private float raycastDistanceModModeInitVal = 15f;
        public Vector3 raycastEndPos { get; private set; }

        [SerializeField] private LayerMask raycastableSurfaceLayerMask;
        [SerializeField]
        private GameObject raycastOrigin;
        [SerializeField]
        private LineRenderer lineRend;
        [SerializeField]
        private Camera eventCamera;

        public bool pointerOnUI
        {
            get { return vrInputModule.currentObject != null; }
        }

        private VRInputModule vrInputModule;

        void Start()
        {
            vrInputModule = VRInputModule.instance;
            vrInputModule.cam = eventCamera;
        }

        void Update()
        {
            if (pointerEnabled)
            {
                UpdateLine();

                if (raycastDistanceModEnabled)
                {
                    var jsInput = VRInputManager.instance.GetJoystickVector(VRInputManager.VRInputType.RIGHT_JOYSTICK);
                    raycastDistanceModSum += jsInput.y * raycastDistanceModFactor;
                }
            }

        }

        private void UpdateLine()
        {
            // use default or distance
            PointerEventData data = vrInputModule.GetData();

            float targetLength;
            if (!raycastDistanceModEnabled)
                targetLength = data.pointerCurrentRaycast.distance == 0 ? raycastDistance : data.pointerCurrentRaycast.distance;
            else    
                targetLength = raycastDistance;

            Vector3 endPosOffest = Vector3.zero;
            RaycastHit hit = CreateRaycast(targetLength);

            raycastEndPos = raycastOrigin.transform.position + (raycastOrigin.transform.forward * targetLength);
            if (hit.collider)
                raycastEndPos = hit.point;
            else
            {
                RaycastHit extendedHit = CreateRaycast(targetLength * 1.25f);
                if (extendedHit.collider && ToolGun.instance.lastSelectedObject)
                {
                    endPosOffest = ToolGun.instance.lastSelectedObject.GetOffset(extendedHit.normal);
                }
            }
            lineEndpointIcon.transform.position = raycastEndPos + endPosOffest;

            lineRend.SetPosition(0, raycastOrigin.transform.position);
            lineRend.SetPosition(1, raycastEndPos);
        }

        private RaycastHit CreateRaycast(float length)
        {
            RaycastHit hit;
            Ray ray = new Ray(raycastOrigin.transform.position, raycastOrigin.transform.forward);
            Physics.Raycast(ray, out hit, length, raycastableSurfaceLayerMask);

            return hit;
        }

        public void EnableDisablePointer(bool enable)
        {
            vrInputModule.useInput = enable;
            lineRend.enabled = enable;
            pointerEnabled = enable;
        }

        public void ToggleRaycastDistanceModifier()
        {
            if (!raycastDistanceModEnabled)
                raycastDistanceModSum = raycastDistanceModModeInitVal;
            
            raycastDistanceModEnabled = !raycastDistanceModEnabled;
        }
    }
}