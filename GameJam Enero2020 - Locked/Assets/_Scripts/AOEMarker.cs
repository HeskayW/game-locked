using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEMarker : MonoBehaviour
{
    public float timeAlive;
    public float maxTime;

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive >= maxTime)
            Destroy(gameObject);
    }
}
