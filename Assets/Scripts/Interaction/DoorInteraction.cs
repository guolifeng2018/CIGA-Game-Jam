using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : InteractionScript
{
    private Animator m_aniamtor;
    private bool m_doorOpened;

    public string m_sceneName;

    protected override void OnStart()
    {
        base.OnStart();
        m_aniamtor = GetComponent<Animator>();
    }

    public override void TriggerEnterAction()
    {
        base.TriggerEnterAction();
        Character character = FindObjectOfType<Character>();
        if (character.CarryItem != null)
        {
            switch (character.CarryItem.m_type)
            {
                case InteractionType.Key:
                    TriggerWithKey(character.CarryItem);
                    break;
            }
        }
        else
        {
            if (m_doorOpened)
            {
                GameSceneManager.Instance.LoadScene(m_sceneName);
            }
        }
    }

    private void TriggerWithKey(InteractionScript script)
    {
        m_aniamtor.Play("Door_Open");
        script.gameObject.SetActive(false);
        Character character = FindObjectOfType<Character>();
        character.DropCarryItem();

        m_doorOpened = true;
    }
}
