using System.Collections.Generic;
using UnityEngine;


namespace Object_Pooling
{
    class ObjectPool
    {
        private readonly Dictionary<IPoolable, PoolTask> activePoolTask;
        public readonly Transform objectPoolTransform;

        private static ObjectPool instance;
        public static ObjectPool Instance => instance ?? new ObjectPool();
        private ObjectPool()
        {
            activePoolTask = new Dictionary<IPoolable, PoolTask>();
            objectPoolTransform = new GameObject().transform;
            objectPoolTransform.name = "ObjectPool";
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

