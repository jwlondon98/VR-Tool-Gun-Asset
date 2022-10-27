/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using UnityEngine;

namespace JLO_VR.ToolGun
{
    /// <summary>
    /// Class used to handle the <see cref="ToolGun"/> Spawn mode functionality
    /// </summary>
    public class SpawnMode : ToolGunMode
    {
        #region Declarations

        /// <summary>
        /// ScriptableObject containing all possible <see cref="Spawnable"/>s
        /// </summary>
        [SerializeField] private Spawnables spawnables;
        
        /// <summary>
        /// The <see cref="Spawnable"/> that is able to currently be spawned.
        /// </summary>
        [SerializeField] private Spawnable spawnable;
        
        /// <summary>
        /// The last spawned <see cref="Spawnable"/>
        /// </summary>
        private Spawnable lastSpawnable;
        
        /// <summary>
        /// The visual representation of <see cref="spawnable"/>
        /// </summary>       
        private GameObject visualizer;
        
        /// <summary>
        /// The <see cref="visualizer"/>'s material
        /// </summary>
        [SerializeField] private Material visualizerMaterial;

        /// <summary>
        /// The <see cref="visualizer"/>'s current position 
        /// </summary>
        private Vector3 visualizationPos;
        
        #endregion

        #region Unity Methods

        void Awake()
        {
            SetSpawnableObject(0);
        }

        #endregion
        
        #region Custom Methods

        #region Mode Base Class Methods
        
        /// <summary>
        /// Called on this mode when first changing to this mode
        /// </summary>
        public override void OnModeStart()
        {
            base.OnModeStart();
            
            if (spawnable == null)
            {
                spawnable = lastSpawnable;
            }
        }
        
        /// <summary>
        /// Called when the primary input action is detected when this mode is active
        ///
        /// Spawns the current <see cref="spawnable"/>
        /// </summary>
        public override void OnModePrimaryTriggered()
        {
            SpawnObject();
        }

        /// <summary>
        /// Called on the current mode on update
        ///
        /// Handles visualization of the <see cref="visualizer"/> spawnable
        /// </summary>
        public override void OnModeUpdate()
        {
            if (spawnable != null)
            {
                VisualizeObject();    
            }
        }
        
        /// <summary>
        /// Called on the current mode when changing to another mode
        ///
        /// Destroys the <see cref="visualizer"/>
        /// </summary>
        public override void OnModeEnd()
        {
            base.OnModeEnd();
            
            if (visualizer)
            {
                Destroy(visualizer);
                spawnable = null;
            }
        }
        
        #endregion

        /// <summary>
        /// Visualizes the <see cref="spawnable"/> at the <see cref="ToolGun"/>'s current raycast pos
        /// </summary>
        private void VisualizeObject()
        {
            if (visualizer == null)
            {
                visualizer = Instantiate(spawnable.prefab);
                visualizer.name = "VISUALIZER";
                var visualizerSelectable = visualizer.GetComponent<SelectableObject>();
                visualizerSelectable.UpdateMaterial(visualizerMaterial);
                var collider = visualizerSelectable.collider;
                Destroy(visualizerSelectable);
                Destroy(collider);
            }
            
            RaycastHit hit = ToolGun.instance.GetRaycastHitOnSurface();
            visualizationPos = Vector3.zero;
            if (hit.collider)
            {
                var offset = spawnable.selectableObject.GetOffset(hit.normal);
                visualizationPos = hit.point + offset;
            }
            else
            {
                visualizationPos = VRPointer.instance.raycastEndPos;
            }

            visualizer.transform.position = visualizationPos;
        }

        /// <summary>
        /// Spawns the current <see cref="spawnable"/> at the <see cref="visualizationPos"/>
        /// </summary>
        private void SpawnObject()
        {
            if (spawnable)
            {
                Instantiate(spawnable.prefab, visualizationPos, Quaternion.identity);
            }
        }

        /// <summary>
        /// Sets the current<see cref="spawnable"/>
        /// </summary>
        /// <param name="spawnableIndex">The index of the spawnable to set in the <see cref="spawnables"/> scriptable.</param>
        public void SetSpawnableObject(int spawnableIndex)
        {
            Destroy(visualizer);
            spawnable = spawnables.spawnablesList[spawnableIndex];
            lastSpawnable = spawnable;
        }

        #endregion
    }
}