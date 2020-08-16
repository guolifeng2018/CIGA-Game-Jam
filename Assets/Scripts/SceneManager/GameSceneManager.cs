using System.Collections;
using System.Collections.Generic;
using TC.Core.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoDefaultSingleton<GameSceneManager>, ISingletonCreateHandler
{
    public string m_sceneName;
    public void OnSingletonCreated()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        m_sceneName = sceneName;
        SceneManager.LoadScene (sceneName);
    }
}
