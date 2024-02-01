using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    [SerializeField] private int _mainScene;
    [SerializeField] private List<int> _scenesToLoadAdditive;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(_mainScene);
        LoadScenes(_scenesToLoadAdditive);
    }
    
    private void LoadScenes(List<int> scenes)
    {
        foreach (var scene in scenes)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene(6, LoadSceneMode.Single);
        SceneManager.LoadSceneAsync(_mainScene);
        LoadScenes(_scenesToLoadAdditive);
        _playerEvents.PauseGame.Invoke(false);
    }
}

