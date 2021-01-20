using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TMP_Text _healthComponent;
    private string _healthString;

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _healthString = "Health :";
        _healthComponent.text = _healthString;
    }

    public void UpdateHealth(int var)
    {
        _healthString = "" + var;
        //_healthString = _healthString.Substring(0, _healthString.Length - 2);

        _healthComponent.text = "Health :" + _healthString;
    }
}
