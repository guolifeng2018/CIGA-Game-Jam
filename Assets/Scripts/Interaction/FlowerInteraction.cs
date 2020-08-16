using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerInteraction : InteractionScript
{
    private int m_dropDownTime = 4;        //放四次才会掉落
    private float m_moveDistance;

    public Sprite m_brokenFlower;
    public InteractionScript m_key;
    
    protected override void OnStart()
    {
        base.OnStart();

        m_moveDistance = (4.47f - 4.14f) / m_dropDownTime;
        
        GlobalEvent.AddEvent("WeightDropDown", HandleWeightDropDown);
    }

    private void HandleWeightDropDown(params object[] args)
    {
        m_dropDownTime -= 1;
        if (m_dropDownTime > 0)
        {
            float originPosY = transform.localPosition.y;
            //振动,花瓶移动
            LeanTween.moveLocalX(gameObject, transform.localPosition.x + m_moveDistance, 0.2f)
                .setEaseInOutSine();
            LeanTween.moveLocalY(gameObject, transform.localPosition.y + 0.15f, 0.1f).setEaseOutSine();
            LeanTween.moveLocalY(gameObject, originPosY, 0.1f).setEaseInSine().setDelay(0.1f);
        }
        else
        {
            //掉落，摔碎
            LeanTween.moveLocalX(gameObject, 4.75f, 0.1f)
                .setEaseInOutSine();
            LeanTween.moveLocalY(gameObject, transform.localPosition.y + 0.15f, 0.1f).setEaseOutSine();
            LeanTween.moveLocalY(gameObject, -0.3f, 0.1f).setEaseInSine().setDelay(0.1f).setOnComplete((o =>
                {
                    m_render.sprite = m_brokenFlower;
                    if (m_key != null)
                    {
                        m_key.transform.localPosition = transform.localPosition;
                        m_key.gameObject.SetActive(true);
                    }

                    Character character = FindObjectOfType<Character>();
                    character.PlayAudio("vase_broken");
                }));
            
            
            GlobalEvent.RemoveEvent("WeightDropDown", HandleWeightDropDown);
        }
    }
}
