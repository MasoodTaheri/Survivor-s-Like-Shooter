using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private int _damageAmount = 1;

    private Rigidbody2D _rigidBody;
    private Vector2 _fireDirection;
    private PlayerGunController _gunController;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }


    public void Init(PlayerGunController gunController, Vector2 bulletSpawnPos, Vector2 Direction)
    {
        _gunController = gunController;
        transform.position = bulletSpawnPos;
        _fireDirection = (Direction - bulletSpawnPos).normalized;
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = _fireDirection * _moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IHealth health = other.GetComponent<IHealth>();
        health?.TakeDamage(_damageAmount);
        _gunController.ReleaseBulletFromPool(this);

    }
}
