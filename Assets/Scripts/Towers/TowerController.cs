using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class TowerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem shootParticleSystem;
    private EnemySpawnerController _enemySpawnerController;
    private bool _isCanShoot = true;
    private EnemyController _currentEnemy;

    private float range;
    private float damage;
    private float attackCooldown;

    [Inject]
    public void Construct(EnemySpawnerController enemySpawnerController)
    {
        _enemySpawnerController = enemySpawnerController;
    }

    private void Update()
    {
        if (!_isCanShoot)
            return;

        if (_currentEnemy)
            if (!IsTargetInRange(_currentEnemy.gameObject))
                _currentEnemy = null;

        foreach (var enemy in _enemySpawnerController.enemyPool)
        {
            if (IsTargetInRange(enemy.gameObject) && _currentEnemy == null)
            {
                _currentEnemy = enemy;
                continue;
            }

            if (!_currentEnemy)
                continue;

            if (enemy.distanceTravelled > _currentEnemy.distanceTravelled && IsTargetInRange(enemy.gameObject))
                _currentEnemy = enemy;
        }

        if (_currentEnemy != null)
            StartCoroutine(Shoot());
    }

    private bool IsTargetInRange(GameObject target)
    {
        return Vector3.Distance(gameObject.transform.position, target.gameObject.transform.position) <= range;
    }

    private IEnumerator Shoot()
    {
        if (shootParticleSystem)
            shootParticleSystem.Play();

        _isCanShoot = false;
        _currentEnemy.DealDamage(damage);
        yield return new WaitForSeconds(attackCooldown);
        _isCanShoot = true;
    }

    public void SetParameters(float towerDamage, float towerRange, float towerAttackCooldown)
    {
        damage = towerDamage;
        range = towerRange;
        attackCooldown = towerAttackCooldown;
    }
}