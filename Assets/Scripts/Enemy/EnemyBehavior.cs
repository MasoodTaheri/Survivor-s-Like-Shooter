using System.Collections;
using UnityEngine;
public class EnemyBehavior : MonoBehaviour, IHealth
{
    public Animator animator;
    public EnemySO Enemydata;

    private Rigidbody2D _rigidbody;
    private GameObject _player;
    private Vector3 direction;
    private IHealth _playerhealth;
    private EnemiesController _enemiesController;
    private EnemyModel _enemyModel;

    public void Initialize(EnemiesController enemiesController, GameObject player)
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
        _enemiesController = enemiesController;
        _enemyModel = new EnemyModel(Enemydata);
        _player = player;
    }


    private void OnEnable()
    {
        StartCoroutine(PeriodicDamag());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void FixedUpdate()
    {
        if (_enemyModel.Health <= 0)
            return;
        Movement();
    }

    private void Movement()
    {
        direction = (_player.transform.position - transform.position).normalized
            * _enemyModel.Speed;
        _rigidbody.velocity = direction;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _playerhealth = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _playerhealth = collision.GetComponent<IHealth>();
        _playerhealth?.TakeDamage(_enemyModel.DamagePerSecond);
    }

    IEnumerator PeriodicDamag()
    {
        while (true)
        {
              yield return new WaitForSeconds(1.0f);
            _playerhealth?.TakeDamage(_enemyModel.DamagePerSecond);
        }
    }

    public void TakeDamage(float damage)
    {
        if (_enemyModel.Health < 0) return;
        _enemyModel.TakeDamage(damage);
        if (_enemyModel.Health <= 0)
        {
            _rigidbody.velocity = Vector2.zero;
            animator.SetBool("Dead", true);
            gameObject.SetActive(false);
            _enemiesController.EnemyDead(this);
        }
        else
            animator.SetTrigger("Hit");
    }
}
