using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GameObject proyectile;
    public Transform proyectileParent;
    public Transform gunIns;

    public float fireRate;

    public float timeLastFire;
    private void Start()
    {
        fireRate = 1 / fireRate;
    }
    private void Update()
    {
        timeLastFire += Time.deltaTime;
        
        if (timeLastFire > fireRate)
        {
            GameObject _proy = Instantiate(proyectile, gunIns.position, gunIns.rotation, proyectileParent);

            Vector3 moveDir = gunIns.position - transform.position;
            moveDir.y = 0;
            _proy.GetComponent<PlayerProyectile>().moveDir = moveDir.normalized;

            timeLastFire = 0;
        }
    }

    public void ChangeFireRate(float var) => fireRate = var;

}
