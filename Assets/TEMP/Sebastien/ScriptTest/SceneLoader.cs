using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    [SerializeField] private List<string> _scenesToLoad;
    

    private void Awake()
    {
        LoadScenes(_scenesToLoad);
        DontDestroyOnLoad(gameObject);
    }
    
    private void LoadScenes(List<string> scenes)
    {
        foreach (var scene in scenes)
        {
            if(SceneManager.GetActiveScene().name != scene) SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene("EmptyScene", LoadSceneMode.Single);
        SceneManager.LoadSceneAsync(_scenesToLoad[0]);
        LoadScenes(_scenesToLoad);
    }
}

