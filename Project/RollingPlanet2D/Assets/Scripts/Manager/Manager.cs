using MyException;
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
                // component = GetOrCreateManager<T>();
                component = gameObject.AddComponent<T>();
                if (!component)
                {
                    throw new InvalidClassException($"Can't find class inherits MonoBehaviour.");
                }
            }
            return component;
        }

        /// <summary>
        /// Get component in generics with string objectname.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectName"></param>
        /// <returns></returns>
        protected T FindComponent<T>(string objectName)
        {
            return GameObject.Find(objectName).GetComponent<T>();
        }
    }
}