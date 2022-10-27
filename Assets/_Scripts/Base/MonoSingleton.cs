using UnityEngine;

namespace JLO_VR.ToolGun
{
    /// <summary>
    /// Singleton base class for static instance referencing where needed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T instance;

        public virtual void Awake()
        {
            instance = this as T;
        }
    }
}