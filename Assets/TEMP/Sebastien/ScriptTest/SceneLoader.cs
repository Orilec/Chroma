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
    [SerializeField] private bool _build;

    private bool _endOfLevel;
    
    private void Awake()
    {
        _narrativeEvents.EndOfVSLevel.AddListener(SetEndOfLevel);
        _UiEvents.FirstFadeFinished.AddListener(EndOfVSLevel);
        if (_build)
        {
            SceneManager.LoadScene(_mainScene);
            LoadScenes(_scenesToLoadAdditive);
        }
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
        SceneManager.LoadScene(_mainScene);
        LoadScenes(_scenesToLoadAdditive);
        _playerEvents.PauseGame.Invoke(false);
    }

    public void LoadNewLevel(int mainScene, List<int> scenesToLoadAdditive)
    {
        _mainScene = mainScene;
        _scenesToLoadAdditive = scenesToLoadAdditive;
        SceneManager.LoadScene(_mainScene);
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

