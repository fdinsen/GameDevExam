using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 offset;

    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if(_player == null)
        {
            Debug.LogError("No Game Object tagged Player");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _player.transform.position + offset;
    }
}
