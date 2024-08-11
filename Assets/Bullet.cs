using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private int _damageAmount = 1;
    private Rigidbody2D _rigidBody;
    private Vector2 _fireDirection;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }


    public void Init(/*Gun gun,*/ Vector2 bulletSpawnPos, Vector2 Direction)
    {
        //_gun = gun;
        transform.position = bulletSpawnPos;
        _fireDirection = (Direction - bulletSpawnPos).normalized;
        Destroy(gameObject, 1.5f);
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = _fireDirection * _moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Health health = other.gameObject.GetComponent<Health>();
        //health?.TakeDamage(_damageAmount);

        //knockBack knockBack = other.gameObject.GetComponent<knockBack>();
        //knockBack?.GetKnockback(PlayerController.Instance.transform.position, _knockBackThrust);

        //_gun.ReleaseBulletFromPool(this);
        IHealth health = other.GetComponent<IHealth>();
        health?.TakeDamage(1);
        Destroy(gameObject);

    }
}
