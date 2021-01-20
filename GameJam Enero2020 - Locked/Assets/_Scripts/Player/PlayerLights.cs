using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLights : MonoBehaviour
{
    public GameObject gunLight;
    public GameObject moveLight;

    public static PlayerLights ins;

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        ToggleLight(1);
    }

    public void ToggleLight(float mode)
    {
        if (mode == 1)
        {
            gunLight.SetActive(false);
            moveLight.SetActive(true);
        }
        else if (mode == 2)
        {
            gunLight.SetActive(true);
            moveLight.SetActive(false);
        }
        else
            Debug.LogError("Se esta llamando con un modo incorrecto", this);

    }
}
