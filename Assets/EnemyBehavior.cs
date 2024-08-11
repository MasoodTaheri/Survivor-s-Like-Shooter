using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour,IHealth
{
    private Rigidbody2D m_rigidbody;
    public float forcescale = 20.0f;
    public GameObject player;
    public Vector3 direction;
    public float forceFactor;
    public float MaxHealth;
    public float _currentHealth;
    public Animator animator;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        ResetHealth();
    }

    private void FixedUpdate()
    {
        if (_currentHealth <= 0)
            return;
        direction =(player.transform.position-transform.position).normalized;
        m_rigidbody.velocity = direction * forcescale* forceFactor;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        forceFactor = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(collision.gameObject);
        forceFactor = 0;
        //_currentHealth--;        
    }

    public void ResetHealth()
    {
        _currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            m_rigidbody.velocity = Vector2.zero;
            animator.SetBool("Dead", true);
        }
        else
            animator.SetTrigger("Hit");
    }
}
