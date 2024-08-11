using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //public Color materialColor;
    private Rigidbody2D m_rigidbody;

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public string jumpButton = "Jump";

    public float inputHorizontal;
    public float inputVertical;
    public float forcescale = 20.0f;

    void Awake()
    {
        //GetComponent<Renderer>().material.color = materialColor;
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputHorizontal = SimpleInput.GetAxis(horizontalAxis);
        inputVertical = SimpleInput.GetAxis(verticalAxis);

        //transform.Rotate(0f, inputHorizontal * 5f, 0f, Space.World);

        //if (SimpleInput.GetButtonDown(jumpButton) && IsGrounded())
        //    m_rigidbody.AddForce(0f, 10f, 0f, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        if (inputHorizontal != 0 || inputVertical != 0)
            m_rigidbody.velocity = new Vector3(inputHorizontal, inputVertical, 0f) * forcescale;
        else
            m_rigidbody.velocity = Vector3.zero;
        //m_rigidbody.AddForce(new Vector3(inputHorizontal,inputVertical, 0f) * forcescale);
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //        m_rigidbody.AddForce(collision.contacts[0].normal * 10f, ForceMode2D.Impulse);
    //}
}
