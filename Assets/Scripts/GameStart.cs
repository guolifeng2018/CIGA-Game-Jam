using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        Character character = FindObjectOfType<Character>();
        OpenWorld openWorld = FindObjectOfType<OpenWorld>();

        LeanTween.delayedCall(2f, o => { openWorld.OpenTheWorld((() => { character.m_canInput = true; })); });
    }
}
