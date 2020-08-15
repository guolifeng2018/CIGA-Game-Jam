using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    public TweenButton m_start;

    public TweenButton m_exit;
    
    void Start()
    {
        
    }

    public void OnStart()
    {
        GameSceneManager.Instance.LoadScene("Level1");
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
