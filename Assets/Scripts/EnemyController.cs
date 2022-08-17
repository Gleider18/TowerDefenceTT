using System.Collections;
using PathCreation;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _takeDamageSystem;
    private PathCreator _pathCreator;
    private EnemySpawnerController _enemySpawnerController;
    private float _speed;
    private float _health;
    private int _moneyForDefeat;
    public float distanceTravelled;
    public EndOfPathInstruction endOfPathInstruction;

    [Inject]
    public void Construct(PathCreator pathCreator, EnemySpawnerController enemySpawnerController)
    {
        _pathCreator = pathCreator;
        _enemySpawnerController = enemySpawnerController;
    }

    void Update()
    {
        if (_pathCreator != null)
        {
            distanceTravelled += _speed * Time.deltaTime;
            transform.position = _pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            if (distanceTravelled >= _pathCreator.path.length)
            {
                GameController.LoseHealth(1);
                _enemySpawnerController.DefeatEnemy(this);
                Destroy(gameObject);
            }
            // transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }
    
    public void DealDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _enemySpawnerController.DefeatEnemy(this);
            Destroy(gameObject);
            GameController.GainMoney(_moneyForDefeat);
        }

        if (_takeDamageSystem != null)
            _takeDamageSystem.Play();
    }

    public void SetParameters(float enemySpeed, float enemyHealth, int moneyForDefeat)
    {
        _speed = enemySpeed;
        _health = enemyHealth;
        _moneyForDefeat = moneyForDefeat;
    }
}