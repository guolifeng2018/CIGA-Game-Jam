using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.value(0, 1f, 0.5f).setOnComplete((o => { GameSceneManager.Instance.LoadScene("Level5"); }));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
