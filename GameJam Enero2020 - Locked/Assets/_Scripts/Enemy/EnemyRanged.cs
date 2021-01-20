using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRanged : Enemy
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _damage;

    private NavMeshAgent agent;

    public GameObject AOE;
    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = _speed;
    }

    private void Update()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            Debug.Log("Had to search player by myself", gameObject);
        }

        if (!_player.gameObject.activeInHierarchy)
            Destroy(gameObject);


        LookAtPlayer();


        NavMeshHit hit;
        if (!agent.Raycast(_player.position, out hit))
        {
            agent.SetDestination(transform.position);
            ShootAtPlayer();
        }
        else
        {
            agent.SetDestination(_player.position);
        }
        

        //Vector3 displacement = dir.normalized * _speed * Time.deltaTime;
        //transform.position += displacement;
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

    private float timeLastFire;
    public float fireRate = 6;
    public GameObject proyectile;
    public Transform gunIns;
    public Transform proyectileParent;

    private void ShootAtPlayer()
    {
        timeLastFire += Time.deltaTime;

        if (timeLastFire > fireRate)
        {
            GameObject _proy = Instantiate(proyectile, gunIns.position, gunIns.rotation, proyectileParent);

            _proy.GetComponent<EnemyProyectile>().objT = _player;
            _proy.GetComponent<EnemyProyectile>().StartShooting();
            timeLastFire = 0;
            
            
            Vector3 aoePos = _player.position;
            aoePos.y = 0.1f;
            Instantiate(AOE, aoePos,Quaternion.identity, proyectileParent);
            return;

        }
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
