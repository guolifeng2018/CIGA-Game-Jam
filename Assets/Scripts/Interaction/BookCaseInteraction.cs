using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCaseInteraction : InteractionScript
{
    public Vector3 m_downPosition;
    public Vector3 m_originPosition;

    public GameObject m_bookSlot;
    public InteractionScript m_book;

    public bool m_canTakeOff = false;
    
    protected override void OnStart()
    {
        base.OnStart();
        
        m_downPosition = new Vector3(1.29f, 2.07f, 0f);
        m_originPosition = transform.localPosition;

        if (m_book != null)
        {
            m_book.gameObject.SetActive(false);
        }
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
        else
        {
            if (m_canTakeOff)
            {
                TriggerTakeOffBook(m_book);
            }
        }

        return false;
    }

    private void TriggerTakeOffBook(InteractionScript script)
    {
        m_bookSlot.SetActive(false);
        script.gameObject.SetActive(true);
        Character character = FindObjectOfType<Character>();
        character.PickUpItem(script);
        
        GlobalEvent.DispatchEvent("BookCaseUp");
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
