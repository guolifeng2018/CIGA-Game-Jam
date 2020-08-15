using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    private Transform m_parent;
    private SpriteRenderer m_render;
    private BoxCollider2D m_collider2D;
    private Material m_defaultMaterial;
    private static Material m_outLineMaterial;
    private float m_recordY;
    private int m_recordSortLayer;

    public bool m_canPick;
    public bool m_canShowOutline = true;
    
    void Start()
    {
        m_parent = transform.parent;
        m_render = GetComponent<SpriteRenderer>();
        m_collider2D = GetComponent<BoxCollider2D>();
        m_defaultMaterial = m_render.material;
        m_recordY = transform.position.y;
        
        Vector3 position = transform.position;
        position.z = -position.y;
        transform.position = position;

        LoadOutlineMaterial();
    }

    public void SetOutLine(bool outline)
    {
        if(!m_canShowOutline && outline){return;}
        Material material = outline ? m_outLineMaterial : m_defaultMaterial;
        if (m_render != null)
        {
            m_render.material = material;
        }
    }

    private static void LoadOutlineMaterial()
    {
        if (m_outLineMaterial == null)
        {
            m_outLineMaterial = Resources.Load<Material>("Material/Outline");
        }
    }

    public void TriggerEnterAction()
    {
        Debug.LogError("Trigger Interaction");
    }

    public void PickUpItem(Transform parent)
    {
        m_recordY = transform.position.y;
        transform.parent = parent;
        m_recordSortLayer = m_render.sortingOrder;
        m_render.sortingOrder = 5;
        //transform.localScale = Vector3.one * 0.7f;
        gameObject.transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        m_collider2D.enabled = false;
        //LeanTween.moveLocal(gameObject, Vector3.zero, 0.3f).setEaseInOutSine();
    }

    public void DropDownItem(Transform character)
    {
        LeanTween.moveY(gameObject, m_recordY, 0.3f).setEaseOutBounce().setOnComplete((o =>
        {
            transform.parent = m_parent;
            m_render.sortingOrder = m_recordSortLayer;
            Vector3 position = new Vector3(character.position.x, m_recordY, 0f);
            gameObject.transform.position = position;
            m_collider2D.enabled = true;
        }));
    }
}
