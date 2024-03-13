using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChroManager : MonoBehaviour
{
    
    [SerializeField] private List<Manager> _managers;
    private static ChroManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this);
    }
    
    public static T GetManager<T>() where T : Manager
    {
        foreach (Manager manager in _instance._managers)
        {
            if (manager.GetType() == typeof(T))
            {
                return manager as T;
            }
        }

        return null;
    }
}
