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

        protected T FindComponentWithTag<T>(string objectName) where T : Component
        {
            return GameObject.FindGameObjectWithTag(objectName).GetComponent<T>();
        }

        protected T[] FindComponentsWithTag<T>(string objectName) where T : Component
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(objectName);
            T[] array = new T[objects.Length];
            for (int i = 0; i < objects.Length; i++)
            {
                array[i] = objects[i].GetComponent<T>();
            }
            return array;
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