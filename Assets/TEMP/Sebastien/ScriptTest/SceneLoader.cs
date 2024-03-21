using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Manager
{
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    [SerializeField] private NarrativeEventsPublisher _narrativeEvents;
    [SerializeField] private UIEventsPublisher _UiEvents;
    [SerializeField] private int _mainScene;
    [SerializeField] private List<int> _scenesToLoadAdditive;

    private bool _endOfLevel;
    
    private void Awake()
    {
        // SceneManager.LoadScene(_mainScene);
        // LoadScenes(_scenesToLoadAdditive);
        _narrativeEvents.EndOfVSLevel.AddListener(SetEndOfLevel);
        _UiEvents.FirstFadeFinished.AddListener(EndOfVSLevel);
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
        _endOfLevel = false;
        SceneManager.LoadScene(6, LoadSceneMode.Single);
        SceneManager.LoadSceneAsync(_mainScene);
        LoadScenes(_scenesToLoadAdditive);
        _playerEvents.PauseGame.Invoke(false);
    }

    private void SetEndOfLevel()
    {
        _endOfLevel = true;
    }

    private void EndOfVSLevel()
    {
        if(_endOfLevel) SceneManager.LoadScene(7, LoadSceneMode.Single);
    }
}

