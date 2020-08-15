using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D m_rigidBody2D;
    private Animator m_animator;
    private SpriteRenderer m_render;
    public GameObject m_pickNode;

    private List<InteractionScript> m_interactionItems = new List<InteractionScript>();

    private InteractionScript m_carryItem;
    
    public InteractionScript CarryItem
    {
        get { return m_carryItem; }
    }

    public bool m_canInput = false;
    

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
        if(!m_canInput){return;}
        
        m_movement.x = Input.GetAxisRaw("Horizontal");
        //m_movement.y = Input.GetAxisRaw("Vertical");
        
        if (m_movement == Vector2.zero)
        {
            string animName = m_carryItem != null ? "Pick_Idle" : "Idle";
            PlayAnimation(animName, m_movement.x, m_recordY);
        }
        else
        {
            m_recordY = m_movement.y;
            string animName = m_carryItem != null ? "Pick_Walk" : "Walk";
            PlayAnimation(animName, m_movement.x, m_recordY);
        }

        if (m_interactionItems.Count > 0)
        {
            //交互
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_interactionItems[0].TriggerEnterAction();
                return;
            }

            if (m_carryItem == null)
            {
                for (int i = 0; i < m_interactionItems.Count; i++)
                {
                    //捡起、放下
                    if(m_interactionItems[i].m_canPick && Input.GetKeyDown(KeyCode.E))
                    {
                        m_carryItem = m_interactionItems[i];
                        m_carryItem.PickUpItem(m_pickNode.transform);
                        m_interactionItems.Remove(m_carryItem);
                        return;
                    }
                }
            }
        }

        if (m_carryItem != null && Input.GetKeyDown(KeyCode.E))
        {
            m_carryItem.DropDownItem(transform);
            m_carryItem = null;
        }
    }

    private void FixedUpdate()
    {
        m_rigidBody2D.MovePosition(m_rigidBody2D.position + m_movement * m_moveSpeed * Time.deltaTime);
        Vector3 position = transform.position;
        position.z = -position.y;
        transform.position = position;
    }

    private void PlayAnimation(string animation, float x, float y)
    {
        m_animator.SetFloat("Blend", y);

        if (x != 0)
        {
            float scaleX = x < 0 ? -1 : 1;
            Vector3 scale = transform.localScale;
            scale.x = scaleX;
            transform.localScale = scale;
        }
        
        if(animation == m_curAnimation)
        {
            return;
        }
        m_curAnimation = animation;
        m_animator.Play(animation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.gameObject.layer == LayerMask.NameToLayer("InteractiveItem"))
        {
            InteractionScript script = other.gameObject.GetComponent<InteractionScript>();
            if (script != null)
            {
                m_interactionItems.Add(script);
                script.SetOutLine(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.gameObject.layer == LayerMask.NameToLayer("InteractiveItem"))
        {
            InteractionScript script = other.gameObject.GetComponent<InteractionScript>();
            if (script != null)
            {
                m_interactionItems.Remove(script);
                script.SetOutLine(false);
            }
        }
    }

    public void DropCarryItem()
    {
        m_carryItem = null;
    }
}
