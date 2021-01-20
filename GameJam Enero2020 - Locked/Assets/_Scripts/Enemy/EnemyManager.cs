using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform _player;
    public List<Transform> spawnPoints;
    public Transform _enemyParent;

    [Header("Enemies")]
    public GameObject pf_enemyMelee;
    public float spawnRateMelee;
    public float enemyMeleeHealth;
    public float enemyMeleeSpeed;
    public float enemyMeleeDamage;

    public GameObject pf_enemyRanged;
    public float spawnRateRanged;

    [SerializeField]
    private float timerMelee;
    [SerializeField]
    private float timerRanged;

    private void Start()
    {
        spawnRateMelee = 1 / spawnRateMelee;
        spawnRateRanged = 1 / spawnRateRanged;
    }

    private void Update()
    {
        if (!_player.gameObject.activeInHierarchy)
            return;

        AutoSpawnMelee();
        AutoSpawnRanged();
    }

    public void ChangeSpawnRateMelee(float value)
    {
        spawnRateMelee = value;
    }
    public void ChangeEnemyMeleeHealth(float value) => enemyMeleeHealth = value;
    public void ChangeEnemySpeed(float var) => enemyMeleeSpeed = var;
    public void ChangeEnemyDamage(float var) => enemyMeleeDamage = var;



    private void AutoSpawnMelee()
    {
        timerMelee += Time.deltaTime;
        
        if (timerMelee < spawnRateMelee)
            return;
        timerMelee = 0;

        int randSpawnPoint = Mathf.CeilToInt(Random.Range(0, spawnPoints.Count));

        Vector3 spawnPos = spawnPoints[randSpawnPoint].position + new Vector3(1,0,1) * Random.Range(0f, 2f);
        spawnPos += Vector3.up * 2;
        GameObject enemy = Instantiate(pf_enemyMelee, spawnPos, Quaternion.identity,_enemyParent);
        
        enemy.GetComponent<Enemy>().SetPlayer(_player);
        enemy.GetComponent<Health>().ChangeMaxHealth(enemyMeleeHealth);
        enemy.GetComponent<EnemyMelee>().ChangeEnemyDamage(enemyMeleeDamage);
        enemy.GetComponent<EnemyMelee>().ChangeEnemySpeed(enemyMeleeSpeed);
    
    }

    private void AutoSpawnRanged()
    {
        timerRanged += Time.deltaTime;

        if (timerRanged < spawnRateRanged)
            return;
        timerRanged = 0;

        int randSpawnPoint = Mathf.CeilToInt(Random.Range(0, spawnPoints.Count));

        Vector3 spawnPos = spawnPoints[randSpawnPoint].position + new Vector3(1, 0, 1) * Random.Range(0f, 2f);
        spawnPos += Vector3.up * 2;
        GameObject enemy = Instantiate(pf_enemyRanged, spawnPos, Quaternion.identity, _enemyParent);

        enemy.GetComponent<Enemy>().SetPlayer(_player);


    }
}
