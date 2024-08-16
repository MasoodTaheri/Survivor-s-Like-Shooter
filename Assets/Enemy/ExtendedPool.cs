using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[Serializable]
public class ExtendedPool<T> where T : class
{
    public List<T> AvailableItems = new List<T>();
    [SerializeField] private ObjectPool<T> _pool;

    public ExtendedPool(Func<T> createFunc,
        Action<T> actionOnGet = null,
        Action<T> actionOnRelease = null,
             Action<T> actionOndestroy = null)
    {
        _pool = new ObjectPool<T>(
         createFunc,
         actionOnGet,
         actionOnRelease,
         actionOndestroy,
         false, 15, 20);
    }

    public T Get()
    {
        T temp = _pool.Get();
        AvailableItems.Add(temp);
        return temp;
    }

    public void Release(T item)
    {
        AvailableItems.Remove(item);
        _pool.Release(item);
    }

    public bool IsInList(T item)
    {
        return AvailableItems.Contains(item);
    }

    public void Clear()
    {
        foreach (T item in AvailableItems)
            _pool.Release(item);

        _pool.Clear();
        AvailableItems.Clear();
    }
}
