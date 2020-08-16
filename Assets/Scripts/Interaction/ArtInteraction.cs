using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtInteraction : InteractionScript
{
    public BlockHoleInteraction m_holeInteraction;
    protected override void OnStart()
    {
        base.OnStart();
        
        GlobalEvent.AddEvent("BookCaseDown", HandleBookCaseDown);
        GlobalEvent.AddEvent("BookCaseUp", HandleBookCaseUp);
    }

    private void OnDestroy()
    {
        GlobalEvent.RemoveEvent("BookCaseDown", HandleBookCaseDown);
        GlobalEvent.RemoveEvent("BookCaseUp", HandleBookCaseUp);
    }

    private void HandleBookCaseDown(params object[] args)
    {
        LeanTween.moveLocalX(gameObject, -1.56f, 0.3f).setEaseInOutSine();
    }

    private void HandleBookCaseUp(params object[] args)
    {
        LeanTween.moveLocalX(gameObject, -2.71f, 0.3f).setEaseInOutSine().setOnComplete((o =>
            {
                m_holeInteraction.m_isClose = true;
            }));
    }
}
