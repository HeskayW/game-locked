using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public Button restartButton;

    [SerializeField]
    private float invulnerableTime;

    private float timerLastHit;

    private PlayerUI _playerUI;

    new private void Start()
    {
        currentHealth = maxHealth;

        _playerUI = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<PlayerUI>();
        restartButton.onClick.AddListener(Restart);

        _playerUI.UpdateHealth((int)currentHealth);
    }

    private void Update()
    {
        timerLastHit += Time.deltaTime;   
    }

    public void ChangeInvulnerableTime(float var) => invulnerableTime = var;

    public override void TakeDamage(float dmg)
    {
        if (timerLastHit < invulnerableTime)
            return;

        base.TakeDamage(dmg);
        _playerUI.UpdateHealth((int)currentHealth);
        timerLastHit = 0;
    }

    public override void Die()
    {
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        gameObject.SetActive(true);
        currentHealth = maxHealth;
        transform.position = new Vector3(0, 1, 0);

        _playerUI.UpdateHealth((int)currentHealth);
    }

}
