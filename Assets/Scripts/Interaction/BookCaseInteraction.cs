using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCaseInteraction : InteractionScript
{
    public Vector3 m_downPosition;
    public Vector3 m_originPosition;

    public GameObject m_bookSlot;
    
    protected override void OnStart()
    {
        base.OnStart();
        
        m_downPosition = new Vector3(1.29f, 2.07f, 0f);
        m_originPosition = transform.localPosition;
    }
    
    public override bool TriggerEnterAction()
    {
        base.TriggerEnterAction();
        Character character = FindObjectOfType<Character>();
        if (character.CarryItem != null)
        {
            switch (character.CarryItem.m_type)
            {
                case InteractionType.Book:
                    TriggerWithBook(character.CarryItem);
                    return true;
            }
        }

        return false;
    }
    
    private void TriggerWithBook(InteractionScript script)
    {
        // 
        // script.transform.parent = transform;
        // script.transform.localPosition = Vector3.zero;
        
        m_bookSlot.SetActive(true);
        
        Character character = FindObjectOfType<Character>();
        character.DropCarryItem();
        script.gameObject.SetActive(false);

        GlobalEvent.DispatchEvent("BookCaseDown");
        // LeanTween.moveLocal(gameObject, m_downPosition, 0.3f).setEaseInOutSine().setOnComplete((o =>
        // {
        //     GlobalEvent.DispatchEvent("BookCaseDown");
        // }));
    }
}
