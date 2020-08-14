/**************************************************************************************************
                                   自动生成代码  请勿手动修改
**************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Test : ConfigBase
{
	public List<Test_Data> _datas = new List<Test_Data>();
}

[System.Serializable]
public class Test_Data : ConfigData
{
	public int id;
	public string name;
	public float value;
}
