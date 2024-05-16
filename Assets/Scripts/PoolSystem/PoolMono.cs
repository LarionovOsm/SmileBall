using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
    public T Prefab { get; }
    public bool AutoExpand { get; set; }
    public Transform Container { get; }
    private List<T> _pool;

    public PoolMono(T prefab, int count)
    {
        Prefab = prefab;
        CreatePool(count);
    }

    public PoolMono(T prefab, int count, Transform container)
    {
        Prefab = prefab;
        Container = container;
        CreatePool(count);
    }

    public void CreatePool(int count)
    {
        _pool = new List<T>();
        for (int i = 0; i < count; i++) CreateObject();
    }

    private T CreateObject(bool IsActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(Prefab, Container);
        createdObject.gameObject.SetActive(IsActiveByDefault);
        _pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var mono in _pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element)) return element;
        if (AutoExpand) return CreateObject(true);
        return null;
    }
}
