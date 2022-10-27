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
    /// Class derived from Unity's BaseInputModule. Allows for the <see cref="VRPointer"/> to interact with UI properly.
    /// </summary>
    public class VRInputModule : BaseInputModule
    {
        #region Declarations

        public Camera cam;
        public GameObject currentObject;
        public Vector3 lastHitPoint;
        private PointerEventData eventData;

        public static VRInputModule instance;
        public bool useInput = true;

        #endregion

        #region Methods

        protected override void Awake()
        {
            instance = this;

            base.Awake();

            eventData = new PointerEventData(eventSystem);
        }

        public override void Process()
        {
            if (cam == null || !useInput)
            {
                print("NO CAM ON VR INPUT MODULE");
                return;
            }

            // Reset data, set cam
            eventData.Reset();
            eventData.position = new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2);

            // Raycast
            eventSystem.RaycastAll(eventData, m_RaycastResultCache);
            eventData.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
            currentObject = eventData.pointerCurrentRaycast.gameObject;
            var cachedHitPoint = lastHitPoint;
            lastHitPoint = eventData.pointerCurrentRaycast.worldPosition;

            // Clear
            m_RaycastResultCache.Clear();

            // Hover
            HandlePointerExitAndEnter(eventData, currentObject);
            
            // Press 
            if (VRInputManager.instance.GetButtonPressed(VRInputManager.VRInputType.RIGHT_INDEX_TRIGGER))
                ProcessClick(eventData);
            
            // Drag
            var hitPointDiff = (cachedHitPoint - lastHitPoint).magnitude;
            if (VRInputManager.instance.GetButton(VRInputManager.VRInputType.RIGHT_INDEX_TRIGGER) && hitPointDiff > 0)
                ProcessDrag(eventData);
            else
                ClearDragData(eventData);
            
            // Release
            if (VRInputManager.instance.GetButtonReleased(VRInputManager.VRInputType.RIGHT_INDEX_TRIGGER))
                ProcessRelease(eventData);
        }

        public void ProcessClick(PointerEventData data)
        {
            data.pointerPressRaycast = data.pointerCurrentRaycast;

            // get pointer click
            GameObject newPointerClick = ExecuteEvents.ExecuteHierarchy(currentObject, data, ExecuteEvents.pointerClickHandler);
            
            // get pointer drag
            GameObject newPointerDrag = ExecuteEvents.ExecuteHierarchy(currentObject, data, ExecuteEvents.beginDragHandler);

            data.pressPosition = data.position;
            data.pointerClick = newPointerClick;
            data.pointerDrag = newPointerDrag;
            data.rawPointerPress = currentObject;
        }

        public void ProcessDrag(PointerEventData data)
        {
            data.pointerPressRaycast = data.pointerCurrentRaycast;

            // get pointer drag
            GameObject newPointerDrag = ExecuteEvents.ExecuteHierarchy(currentObject, data, ExecuteEvents.dragHandler);

            data.pointerDrag = newPointerDrag;
            data.dragging = true;
            data.rawPointerPress = currentObject;
        }

        public void ProcessRelease(PointerEventData data)
        {
            // execute pointer up 
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

            // clear selected gameobject
            // eventSystem.SetSelectedGameObject(null);

            // reset data
            data.pressPosition = Vector2.zero;
            data.pointerPress = null;
            data.rawPointerPress = null;
            ClearDragData(data);
        }

        private void ClearDragData(PointerEventData data)
        {
            ExecuteEvents.Execute(data.pointerDrag, data, ExecuteEvents.endDragHandler);

            data.pointerDrag = null;
            data.dragging = false;
        }

        public PointerEventData GetData()
        {
            return eventData;
        }

        #endregion
    }
}