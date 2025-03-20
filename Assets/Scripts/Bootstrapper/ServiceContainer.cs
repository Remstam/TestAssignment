using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    public static class ServiceContainer
    {
        private static readonly IDictionary<Type, object> Container = new Dictionary<Type, object>(1024);

        public static void ClearContainer()
        {
            Container?.Clear();
        }

        public static void Register<T>(T instance) => Register(typeof(T), instance);
        
        public static void Register(Type type, object instance)
        {
            if (Container.ContainsKey(type))
            {
                Debug.LogWarning(
                    $"[{nameof(ServiceContainer)}] Register: object {type} is already presented in container.");

                return;
            }
               
            if (instance == null)
            {
                Debug.LogError($"[{nameof(ServiceContainer)}] Register: object of type {type} is null.");

                return;
            }

            Container[type] = instance;
        }

        public static T Get<T>()
        {
            var result = Get(typeof(T));
            if (result != null)
            {
                return (T)result;
            }

            Debug.LogError($"[{nameof(ServiceContainer)}] Object {typeof(T)} does not exist!");

            return default;
        }

        public static bool Exist<T>()
        {
            return Container.ContainsKey(typeof(T));
        }

        public static void Unregister<T>()
        {
            Container.Remove(typeof(T));
            Debug.Log($"[{nameof(ServiceContainer)}] Unregistered: type {typeof(T)}");
        }

        private static object Get(Type type)
        {
            return Container.TryGetValue(type, out var value) ? value : null;
        }
    }
}