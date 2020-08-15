using System.Collections;
using System.Collections.Generic;
using TC.Core.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoDefaultSingleton<GameSceneManager>, ISingletonCreateHandler
{
    public void OnSingletonCreated()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene (sceneName);
    }
}
