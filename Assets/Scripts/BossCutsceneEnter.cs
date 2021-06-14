using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class BossCutsceneEnter : MonoBehaviour
{
    [SerializeField] private GameObject _cutsceneCam;
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayableDirector _director;

    void OnTriggerEnter(Collider other)
    {

    }

    private IEnumerator PlayCutscene()
    {
        GameObject.FindGameObjectWithTag("Canvas").SetActive(false);
        _cutsceneCam.SetActive(true);
        _player.SetActive(false);
        _director.Play();
        yield return new WaitForSeconds(10);
        GameObject.FindGameObjectWithTag("DeathText")
            .GetComponent<TextMeshProUGUI>()
            .SetText("Thanks for playing!");
        yield return new WaitForSeconds(30f);
        SceneManager.LoadScene(0);
    }

}
