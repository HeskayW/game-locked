using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProyectile : MonoBehaviour
{
    public float speedY0;
    public float tmax;

    public float gravity = 9.8f;
    public float damage;
    public float explosionRadius;

    public Vector3 obj;
    public Transform objT;

    private Vector3 pos0;
    private float speedXZ0;
    private float speedX0;
    private float speedZ0;
    private float g;

    [SerializeField]
    private float t;


    private bool shoot;

    private void Start()
    {
        if (obj == Vector3.zero)
            obj = objT.position;

        CalculateParameters();
    }

    private void Update()
    {
        if (!shoot)
            return;

        MoveProyectile(t);
        t += Time.deltaTime;

        if (t >= tmax) 
        { 
            StopShooting();
            Explosion();
        }
    }

    private void CalculateParameters()
    {

        pos0 = transform.position;
        g = gravity;

        //tmax = (speedY0 + Mathf.Sqrt(Mathf.Pow(speedY0, 2) + 2 * pos0.y * g))/g;
        speedY0 = (0.5f * g * Mathf.Pow(tmax, 2) - pos0.y) / tmax;

        float distance = Vector3.Distance(new Vector3(obj.x, 0, obj.z), new Vector3(pos0.x, 0, pos0.z));
        speedXZ0 = distance / tmax;
        float ratioXZ = (obj.x - pos0.x) / (obj.z - pos0.z);
        
        speedX0 = Mathf.Sqrt(Mathf.Pow(ratioXZ * speedXZ0, 2) / (1 + Mathf.Pow(ratioXZ, 2)));
        speedZ0 = speedX0 / ratioXZ;

    }

    private void MoveProyectile(float t)
    {
        float disX;
        float disY;
        float disZ;

        disY = speedY0 * t - 0.5f * g * t*t;
        disX = speedX0 * t;
        disZ = speedZ0 * t;

        Vector3 nextDis = new Vector3(disX, disY, disZ);
        transform.position = pos0 + nextDis;
    }

    [ContextMenu("start")]
    public void StartShooting()
    {
        shoot = true;
        CalculateParameters();
    }
    [ContextMenu("stop")]
    
    private void StopShooting() => shoot = false;

    public void ChangePlayerDamage(float var) => damage = var;

    private void Explosion()
    {
        Collider[] hitColliders = Physics.OverlapSphere(obj, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.SendMessage("TakeDamage",damage,SendMessageOptions.DontRequireReceiver);
        }
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}
