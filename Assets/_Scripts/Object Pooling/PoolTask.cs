using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Object_Pooling
{
    class PoolTask
    {
        private readonly List<IPoolable> freeObjects;
        private readonly Transform container;

        public PoolTask (Transform container)
        {
            freeObjects = new List<IPoolable>();
            this.container = container;
        }

        public T GetFreeObject<T>(T prefab) where T : MonoBehaviour, IPoolable
        {
            T poolObject = null;
            if (freeObjects.Count > 0)
            {
                poolObject = freeObjects.Last() as T;
                poolObject.GameObject.SetActive(true);
                freeObjects.Remove(poolObject);
            }

            if (poolObject == null)
                poolObject = Object.Instantiate(prefab);
            poolObject.OnReturnToPool += ReturnToPool;
            return poolObject;
        }

        private void ReturnToPool (IPoolable poolObject)
        {
            freeObjects.Add(poolObject);
            poolObject.GameObject.SetActive(false);
            poolObject.GameObject.transform.position = new Vector3(0, 0, 0);
            poolObject.Transform.SetParent(container);
            poolObject.OnReturnToPool -= ReturnToPool;
        }
    }
}

