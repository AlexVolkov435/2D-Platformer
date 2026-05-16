using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolMono<T> where T : MonoBehaviour
{
    private T _prefab;
    private Transform _container;

    private List<T> _pool;
    public bool AutoExpand { get;  private set; }
    
    public PoolMono(T prefab, int count, Transform container)
    {
        _prefab = prefab;
        _container = container;
        _pool = new List<T>(count);
        
        CreatePool(count);
    }
    
    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
        {
            return element;
        }

        if (AutoExpand)
        {
            return CreateObject(true);
        }

        throw new Exception($"Pool is over! {typeof(T)}");
    }
    
    public void SetAutoExpand(bool autoExpand)
    {
        AutoExpand = autoExpand;
    } 

    private void CreatePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        T createdObject = Object.Instantiate(_prefab, _container);
       
        createdObject.gameObject.SetActive(isActiveByDefault);
        _pool.Add(createdObject);
        
        return createdObject;
    }

    private bool HasFreeElement(out T element)
    {
        foreach (T enemy in _pool)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                element = enemy;
                enemy.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }
}