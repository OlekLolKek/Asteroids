using System;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class SingletonMonoBehaviour<T>
        : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance = null;
        private static bool _isAlive = true;
        private static readonly object _lock = new object();

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance != null)
                    {
                        return _instance;
                    }

                    var instances = FindObjectsOfType<T>();
                    if (instances != null)
                    {
                        if (instances.Length == 1)
                        {
                            _instance = instances[0];
                            DontDestroyOnLoad(_instance);
                            return _instance;
                        }

                        if (instances.Length > 1)
                        {
                            for (int i = 0; i < instances.Length; ++i)
                            {
                                T manager = instances[i];
                                Destroy(manager.gameObject);
                            }
                        }
                    }

                    var gameobject = new GameObject(typeof(T).Name, typeof(T));
                    _instance = gameobject.GetComponent<T>();
                    DontDestroyOnLoad(_instance.gameObject);
                    return _instance;
                }
            }
        }

        public static bool IsAlive
        {
            get
            {
                if (_instance == null)
                {
                    return false;
                }

                return _isAlive;
            }
        }

        protected void Awake()
        {
            if (_instance == null)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected void OnDestroy()
        {
            _isAlive = false;
        }

        private void OnApplicationQuit()
        {
            _isAlive = false;
        }
    }
}