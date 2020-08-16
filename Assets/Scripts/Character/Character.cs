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

    public CanvasGroup m_group;
    public GameObject m_image;

    private List<InteractionScript> m_interactionItems = new List<InteractionScript>();

    private InteractionScript m_carryItem;

    private AudioListener m_listener;
    private AudioSource m_music;
    private AudioSource m_sound;
    private AudioSource m_footAudio;
    
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
        m_listener = gameObject.AddComponent<AudioListener>();
        m_music = gameObject.AddComponent<AudioSource>();
        m_sound = gameObject.AddComponent<AudioSource>();
        m_footAudio = gameObject.AddComponent<AudioSource>();
        m_music.loop = true;
        m_music.clip = Resources.Load<AudioClip>("Audio/music/ghost_sigh");
        //m_music.Play();

        m_footAudio.clip = Resources.Load<AudioClip>("Audio/sound/walking_on_a_floor");
        m_footAudio.loop = true;
    }

    public void PlayAudio(string name)
    {
        m_sound.clip = Resources.Load<AudioClip>(string.Format("Audio/sound/{0}", name));
        m_sound.Play();
    }

    public void Update()
    {
        if(!m_canInput){return;}
        
        m_movement.x = Input.GetAxisRaw("Horizontal");
        //m_movement.y = Input.GetAxisRaw("Vertical");
        
        if (m_movement == Vector2.zero)
        {
            string animName = m_carryItem != null ? "Pick_Idle" : "Idle";
            m_footAudio.Stop();
            PlayAnimation(animName, m_movement.x, m_recordY);
        }
        else
        {
            m_recordY = m_movement.y;
            m_footAudio.Play();
            string animName = m_carryItem != null ? "Pick_Walk" : "Walk";
            PlayAnimation(animName, m_movement.x, m_recordY);
        }

        if (m_interactionItems.Count > 0)
        {
            //交互
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 0; i < m_interactionItems.Count; i++)
                {
                    bool result = m_interactionItems[i].TriggerEnterAction();
                    if (result)
                    {
                        return;
                    }
                }
                return;
            }

            if (m_carryItem == null)
            {
                float distance = float.MaxValue;
                InteractionScript item = null;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    for (int i = 0; i < m_interactionItems.Count; i++)
                    {
                        //捡起、放下
                        if(m_interactionItems[i].m_canPick)
                        {
                            float dis = Vector2.Distance(transform.position, m_interactionItems[i].transform.position);
                            if (dis < distance)
                            {
                                distance = dis;
                                item = m_interactionItems[i];
                            }
                        }
                    }

                    if (item != null)
                    {
                        m_carryItem = item;
                        m_carryItem.PickUpItem(m_pickNode.transform);
                        m_interactionItems.Remove(m_carryItem);
                    }
                    
                    return;
                }
            }
        }

        if (m_carryItem != null && Input.GetKeyDown(KeyCode.E))
        {
            m_carryItem.DropDownItem(transform);
            m_carryItem = null;
        }
    }

    public void PickUpItem(InteractionScript script)
    {
        if (script.m_canPick)
        {
            m_carryItem = script;
            m_carryItem.PickUpItem(m_pickNode.transform);
            m_interactionItems.Remove(m_carryItem);
        }
    }

    private void FixedUpdate()
    {
        m_rigidBody2D.MovePosition(m_rigidBody2D.position + m_movement * m_moveSpeed * Time.deltaTime);
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -7.25f, 11.13f);
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

        if (other != null)
        {
            AintAttack aintAttack = other.GetComponent<AintAttack>();
            if (aintAttack != null)
            {
                GameSceneManager.Instance.LoadScene("Empty");
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
