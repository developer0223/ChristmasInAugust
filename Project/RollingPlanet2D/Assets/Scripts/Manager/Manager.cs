using System;
using UnityEngine;
using UnityEngineInternal;

namespace Manager
{
    public class Manager : MonoBehaviour
    {

        [TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
        public T GetOrCreateManager<T>() where T : Manager
        {
            T component = GetComponent<T>();
            if (!component)
            {
                Debug.Log("Component doesn't exist. Add manager component.");
                component = GetOrCreateManager<T>();
                if (!component)
                {
                    throw new Exception($"Can't find class inherits MonoBehaviour.");
                }
            }
            return component;
        }

        public T GetManager<T>() where T : Manager
        {
            return GetOrCreateManager<T>();
        }

        /// <summary>
        /// Get component in generics with string objectname.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectName"></param>
        /// <returns></returns>
        protected T FindComponent<T>(string objectName) where T : Component
        {
            return GameObject.Find(objectName).GetComponent<T>();
        }

        protected GameManager GetGameManager()
        {
            return GetComponent<GameManager>();
        }

        protected void DoNothing()
        {
            // do nothing
        }
    }
}