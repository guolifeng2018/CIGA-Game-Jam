using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCaseInteraction : InteractionScript
{
    public Vector3 m_downPosition;
    public Vector3 m_originPosition;
    
    protected override void OnStart()
    {
        base.OnStart();
        
        m_downPosition = new Vector3(1.29f, 2.07f, 0f);
        m_originPosition = transform.localPosition;
    }
    
    public override void TriggerEnterAction()
    {
        base.TriggerEnterAction();
        Character character = FindObjectOfType<Character>();
        if (character.CarryItem != null)
        {
            switch (character.CarryItem.m_type)
            {
                case InteractionType.Book:
                    TriggerWithBook(character.CarryItem);
                    break;
            }
        }
    }
    
    private void TriggerWithBook(InteractionScript script)
    {
        script.gameObject.SetActive(true);
        script.transform.parent = transform;
        script.transform.localPosition = Vector3.zero;
        Character character = FindObjectOfType<Character>();
        character.DropCarryItem();

        LeanTween.moveLocal(gameObject, m_downPosition, 0.3f).setEaseInOutSine().setOnComplete((o =>
        {
            GlobalEvent.DispatchEvent("BookCaseDown");
        }));
    }
}
