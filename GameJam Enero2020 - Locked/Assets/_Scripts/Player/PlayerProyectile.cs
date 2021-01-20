using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProyectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float lifeTime;

    [HideInInspector]
    public Vector3 moveDir;

    private float timeAlive;

    private void Update()
    {
        transform.localPosition += moveDir * Time.deltaTime * speed;

        timeAlive += Time.deltaTime;
        if (timeAlive > lifeTime)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("destructable"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }else if (collision.gameObject.CompareTag("Terrain"))
        {
            Destroy(gameObject);
        }
        
    
        

    }

    public void ChangePlayerDamage(float var) => damage = var;
    public void ChangeProyectileSpeed(float var) => speed = var;

}
