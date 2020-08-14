using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GLDDownloadConfig
{
	public List<GLDDownloadData> downLoadList = new List<GLDDownloadData>();
}

[Serializable]
public class GLDDownloadData
{
	public int id;
	public string name;
	public string url;
	public string jsonPath;
	public string csPath;
}
