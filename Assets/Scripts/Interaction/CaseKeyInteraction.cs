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

    private void HandleDropDownLogic(params object[] args)
    {
        LeanTween.moveLocal(gameObject, new Vector3(-2.692f, -0.58f, 0f), 0.5f).setEaseInOutSine();
    }
}
