﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseKeyInteraction : InteractionScript
{
    protected override void OnStart()
    {
        base.OnStart();
        
        GlobalEvent.AddEvent("BookCaseDown", HandleBookCaseDown);
    }

    private void HandleBookCaseDown(params object[] args)
    {
        GlobalEvent.AddEvent("WeightDropDown", HandleDropDownLogic);
    }

    private void OnDestroy()
    {
        GlobalEvent.RemoveEvent("BookCaseDown", HandleBookCaseDown);
    }

    private void HandleDropDownLogic(params object[] args)
    {
        LeanTween.moveLocal(gameObject, new Vector3(-2.692f, -0.58f, 0f), 0.5f).setEaseInOutSine();
        GlobalEvent.RemoveEvent("WeightDropDown", HandleDropDownLogic);
    }

    public override void PickUpItem(Transform parent)
    {
        base.PickUpItem(parent);
        
        GlobalEvent.DispatchEvent("Cellar_Open");
    }

    public override void DropDownItem(Transform character)
    {
        base.DropDownItem(character);

        Character player = FindObjectOfType<Character>();
        player.PlayAudio("key_knife_drop");
    }
}
