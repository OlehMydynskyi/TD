using System;
using UnityEngine;

namespace Object_Pooling
{
    public interface IPoolable
    {
        Transform Transform { get; }
        GameObject GameObject { get; }
        event Action<IPoolable> OnReturnToPool;
        void ReturnToPool();
    }
}
