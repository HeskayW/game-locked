using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform _player;
    private Vector3 offset;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        offset = transform.position;
        offset.y -= _player.position.y;
    }

    private void LateUpdate()
    {
        transform.position = _player.position + offset;
    }
}
