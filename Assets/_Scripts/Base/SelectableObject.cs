/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using UnityEngine;

namespace JLO_VR.ToolGun
{
    /// <summary>
    /// This script is attached to any GameObject that should be selectable by the <see cref="ToolGun"/>.
    /// The tool gun can only select object's with this component.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class SelectableObject : MonoBehaviour
    {
        #region Declarations

        /// <summary>
        /// The object's collider.
        /// TODO: Create a list for child colliders
        /// </summary>
        [SerializeField] private Collider m_collider;
        public Collider collider
        {
            get { return m_collider; }
            set { m_collider = value; }
        }
        
        /// <summary>
        /// The object's mesh renderer
        /// TODO: Create a list for child mesh renderers
        /// </summary>
        [SerializeField] private MeshRenderer m_meshRend;
        public MeshRenderer meshRend
        {
            get { return m_meshRend; }
            set { m_meshRend = value; }
        }
        
        /// <summary>
        /// The object's rigidbody
        /// TODO: Create a list for child rigidbodies
        /// </summary>
        public Rigidbody rb;

        /// <summary>
        /// The extents of the object's bounding box.
        /// Used to calculate the object's visualization offset for various modes.
        /// </summary>
        public Vector3 extents;

        /// <summary>
        /// The <see cref="configurableUI"/> panel for this specific object
        /// </summary>
        public ConfigurableUI configurableUI;
        
        #endregion

        #region Unity Methods

        void OnValidate()
        {
            if (collider == null)
            {
                collider = GetComponent<Collider>();
            }
            
            if (meshRend == null)
            {
                meshRend = GetComponent<MeshRenderer>();
            }
        }
        
        #endregion

        #region Custom Methods
        
        /// <summary>
        /// Context menu shortcut for quick updating newly created prefabs.
        /// </summary>
        [ContextMenu("Update Extents")]
        public void UpdateExtents()
        {
            extents = collider.bounds.extents;
        }

        /// <summary>
        /// Returns this object's offset relative to the normal direction param.
        /// </summary>
        /// <param name="normalDir">The normal direction used to determine which offset unit to return.</param>
        /// <returns>The object's offset relative to the normalDir param.</returns>
        public Vector3 GetOffset(Vector3 normalDir)
        {
            Direction closestDirection = ToolGunUtil.GetClosestDirection(normalDir);
            switch (closestDirection)
            {
                case Direction.UP:
                    return new Vector3(0,extents.y,0);
                case Direction.DOWN:
                    return new Vector3(0,-extents.y,0);
                case Direction.RIGHT:
                    return new Vector3(extents.x,0,0);
                case Direction.LEFT:
                    return new Vector3(-extents.x, 0,0);
                case Direction.FORWARD:
                    return new Vector3(0, 0,extents.z);
                case Direction.BACK:
                    return new Vector3(0, 0,-extents.z);
            }

            return Vector3.zero;
        }

        /// <summary>
        /// Updates this object's material.
        /// </summary>
        /// <param name="mat">The new material to set to.</param>
        public void UpdateMaterial(Material mat)
        {
            meshRend.sharedMaterial = mat;
        }
        
        #endregion
    }
}