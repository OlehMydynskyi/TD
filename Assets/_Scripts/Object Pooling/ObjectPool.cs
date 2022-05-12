using System.Collections.Generic;
using UnityEngine;


namespace Object_Pooling
{
    class ObjectPool : MonoBehaviour
    {
        private Dictionary<IPoolable, PoolTask> activePoolTask;
        [HideInInspector] public Transform objectPoolTransform;
        public static ObjectPool Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            objectPoolTransform = gameObject.transform;
            activePoolTask = new Dictionary<IPoolable, PoolTask>();
        }

        public T GetObject<T>(T prefab) where T : MonoBehaviour, IPoolable
        {
            if (!activePoolTask.TryGetValue(prefab, out var poolTask))
                AddTaskToPool(prefab, out poolTask);

            return poolTask.GetFreeObject<T>(prefab);
        }

        private void AddTaskToPool<T>(T prefab, out PoolTask poolTask) where T : MonoBehaviour, IPoolable
        {
            GameObject container = new GameObject
            {
                name = $"{prefab.name}s_pool"
            };
            container.transform.SetParent(objectPoolTransform);
            poolTask = new PoolTask(container.transform);
            activePoolTask.Add(prefab, poolTask);
        }
    }
}