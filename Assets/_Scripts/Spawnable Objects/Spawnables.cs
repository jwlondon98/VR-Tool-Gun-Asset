/* Copyright (C) 2022 - Present; Jordan London
 * All Rights Reserved.
 * You may not use, distribute or modify any code within this project
 * without obtaining a license from Jordan London.
 */

using System;
using UnityEngine;

namespace JLO_VR.ToolGun
{
    [CreateAssetMenu(menuName = "JLO VR/VR Tool Gun/Spawnables")]
    public class Spawnables : ScriptableObject
    {
        public SpawnablesList spawnablesList;
    }
        
    [Serializable]
    public class SpawnablesList : SerializableDictionary<int, Spawnable> {}
}