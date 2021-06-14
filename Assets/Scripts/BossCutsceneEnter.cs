using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossCutsceneEnter : MonoBehaviour
{
    [SerializeField] private GameObject _cutsceneCam;
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayableDirector _director;

    void OnTriggerEnter(Collider other)
    {
        _cutsceneCam.SetActive(true);
        _player.SetActive(false);
        _director.Play();
    }

}
