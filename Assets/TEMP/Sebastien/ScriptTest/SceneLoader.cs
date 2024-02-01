using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    [SerializeField] private string _mainScene;
    [SerializeField] private List<string> _scenesToLoadAdditive;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadSceneAsync(_mainScene);
        LoadScenes(_scenesToLoadAdditive);
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
        SceneManager.LoadSceneAsync(_mainScene);
        LoadScenes(_scenesToLoadAdditive);
        _playerEvents.PauseGame.Invoke(false);
    }
}

