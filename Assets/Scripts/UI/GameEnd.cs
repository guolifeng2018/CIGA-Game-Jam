using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public GameObject m_gameEnd;
    // Start is called before the first frame update
    void Start()
    {
        m_gameEnd.SetActive(false);

        LeanTween.value(0, 1f, 2f).setOnComplete((o =>
        {
            m_gameEnd.SetActive(true);
            LeanTween.value(0, 1f, 2f).setOnComplete(o1 =>
            {
                GameSceneManager.Instance.LoadScene("Start");
            });
        }));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
