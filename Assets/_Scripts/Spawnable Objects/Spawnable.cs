/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using UnityEngine;

namespace JLO_VR.ToolGun
{
    [CreateAssetMenu(menuName = "JLO VR/VR Tool Gun/Spawnable Object")]
    public class Spawnable : ScriptableObject
    {
        public GameObject prefab;
        public SelectableObject selectableObject;
        public Vector3 extents;

        [ContextMenu("Update Fields By Prefab")]
        public void UpdateFieldsByPrefab()
        {
            selectableObject = prefab.GetComponent<SelectableObject>();
            extents = prefab.GetComponent<SelectableObject>().extents;
        }
    }
}