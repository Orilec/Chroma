using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneLoader : MonoBehaviour
{
    [SerializeField] private int _mainScene;
    [SerializeField] private List<int> _scenesToLoadAdditive;
    private SceneLoader _sceneLoader;
    private PlayerController _player;

    private void Start()
    {
        _sceneLoader = ChroManager.GetManager<SceneLoader>();
        _player = ChroManager.GetManager<PlayerManager>().GetPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player.gameObject)
        {
            LoadLevel();
        }
    }

    public void LoadLevel()
    {
        _sceneLoader.LoadNewLevel(_mainScene, _scenesToLoadAdditive);
    }
}
