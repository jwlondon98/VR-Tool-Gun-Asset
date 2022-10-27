/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using UnityEngine;
using UnityEngine.Animations;

namespace JLO_VR.ToolGun
{
    public class ConfigurableUI : MonoBehaviour
    {
        #region Declarations

        /// <summary>
        /// The canvas containing all configurable UI
        /// </summary>
        [SerializeField] private Canvas canvas;

        /// <summary>
        /// The panel that was enabled last.
        /// </summary>
        private GameObject lastActivePanel;

        [SerializeField] private RotationConstraint rotationConstraint;
        
        #endregion

        #region Unity Methods

        void Start()
        {
            canvas.worldCamera = VRInputModule.instance.cam;

            ConstraintSource source = new ConstraintSource();
            source.sourceTransform = ToolGunUIManager.instance.transform;
            rotationConstraint.AddSource(source);
        }

        #endregion
        
        #region Custom Methods

        /// <summary>
        /// Enables/Disables the <see cref="canvas"/>
        /// </summary>
        /// <param name="enable">Enables = true, Disable = false, Inverse of canvas's activeSelf if null</param>
        public void EnableDisableUI(bool? enable)
        {
            if (enable.HasValue)
                canvas.gameObject.SetActive(enable.Value);
            else
                canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
        }

        /// <summary>
        /// Enables the passed panel GameObject. Called by any of this <see cref="ConfigurableUI"/>'s component-buttons.
        /// </summary>
        /// <param name="panel">The panel to enable.</param>
        public void EnablePanel(GameObject panel)
        {
            if (lastActivePanel)
                lastActivePanel.SetActive(false);

            lastActivePanel = panel;
            panel.SetActive(true);
        }

        #endregion
    }
}