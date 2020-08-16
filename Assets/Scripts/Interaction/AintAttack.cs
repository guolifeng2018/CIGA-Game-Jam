using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AintAttack : MonoBehaviour
{
    public bool m_attack = false;
    private Character m_character;

    private float m_moveSpeed = 2;
    
    void Start()
    {
        m_character = FindObjectOfType<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_attack)
        {
            Vector3 position = m_character.transform.position;
            Vector3 direction = Vector3.zero;
            if (transform.position.x < position.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                direction = new Vector3(1f, 0f, 0f);
            }
            else
            {
                transform.localScale = Vector3.one;
                direction = new Vector3(-1f, 0f, 0f);
            }

            transform.position += direction * m_moveSpeed * Time.deltaTime;
        }
    }
}
