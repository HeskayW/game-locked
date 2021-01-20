using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public Transform _player;

    public void SetPlayer(Transform player)
    {
        _player = player;
    }
}
