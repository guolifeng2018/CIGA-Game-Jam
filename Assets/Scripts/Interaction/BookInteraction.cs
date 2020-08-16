using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteraction : InteractionScript
{
    private float m_y;
    protected override void OnStart()
    {
        base.OnStart();

        m_y = m_recordY;
    }

    public override void DropDownItem(Transform character)
    {
        LeanTween.moveY(gameObject, m_y, 0.3f).setEaseOutBounce().setOnComplete((o =>
        {
            transform.parent = m_parent;
            m_render.sortingOrder = m_recordSortLayer;
            Vector3 position = new Vector3(character.position.x, m_y, 0f);
            gameObject.transform.position = position;
            m_collider2D.enabled = true;
        }));
    }
}
