using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : Enemy
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _damage;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = _speed;
    }

    private void Update()
    {
        if (!_player.gameObject.activeInHierarchy)
            Destroy(gameObject);

        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            Debug.Log("Had to search player by myself", gameObject);
        }

        LookAtPlayer();

        agent.SetDestination(_player.position);

    }

    private void LookAtPlayer()
    {
        Vector3 dir = _player.position - transform.position;

        if (dir.magnitude <= 0.3f)
            return;

        dir.y = 0;

        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
    }

    public void ChangeEnemySpeed(float var) 
    {
        _speed = var;
        //agent.speed = _speed;
    }
    
    public void ChangeEnemyDamage(float var) => _damage = var;


    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        collision.gameObject.GetComponent<Health>().TakeDamage(_damage);     
    }
}
