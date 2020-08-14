using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    void Start()
    {

        Test test = GLDCfgMgr.Instance.GetConfigData<Test>("Test");
        Debug.LogError(test._datas[0].id + "            " + test._datas[0].name + "            " + test._datas[0].value);

    }
}
