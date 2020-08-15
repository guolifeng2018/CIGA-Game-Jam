using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D m_rigidBody2D;
    private Animator m_animator;
    private SpriteRenderer m_render;
    

    public float m_moveSpeed;

    private Vector2 m_movement;
    private float m_recordY;
    private string m_curAnimation;
    private bool m_canInteraction;
    
    void Start()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        m_render = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    public void Update()
    {
        m_movement.x = Input.GetAxisRaw("Horizontal");
        //m_movement.y = Input.GetAxisRaw("Vertical");
        
        if (m_movement == Vector2.zero)
        {
            PlayAnimation("Idle", m_movement.x, m_recordY);
        }
        else
        {
            m_recordY = m_movement.y;
            PlayAnimation("Walk", m_movement.x, m_recordY);
        }
    }

    private void FixedUpdate()
    {
        m_rigidBody2D.MovePosition(m_rigidBody2D.position + m_movement * m_moveSpeed * Time.deltaTime);
    }

    private void PlayAnimation(string animation, float x, float y)
    {
        m_animator.SetFloat("Blend", y);

        if (x != 0)
        {
            m_render.flipX = x < 0;
        }
        
        if(animation == m_curAnimation)
        {
            return;
        }
        m_curAnimation = animation;
        m_animator.Play(animation);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        m_canInteraction = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        m_canInteraction = false;
    }
}
