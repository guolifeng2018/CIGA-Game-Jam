#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine.Networking;

public enum ECsvTableRowType
{
	VariableType = 0,
	VariableName = 1,
	BlankRow = 2,
	ConfigData = 3,
	Comment = 4,
	
	MaxType = 5,
}

public class GLDDownloadHandler
{
	private GLDDownloadConfig m_downloadConfig;

	private static CsvDataParser m_parser;

	public static void CreateTemplateDownloadConfig()
	{
		GLDDownloadConfig config = new GLDDownloadConfig();
		GLDDownloadData data = new GLDDownloadData();
		data.name = "template";
		data.jsonPath = Path.Combine(Application.dataPath, "template.json");
		data.csPath = Path.Combine(Application.dataPath, "template.cs");
		data.url = "tempalte url";
		config.downLoadList.Add(data);

		try
		{
			string path = DownloadConfigPath();
			string json = JsonUtility.ToJson(config, true);
			File.WriteAllText(path, json);
		}
		catch (Exception e)
		{
			Debug.LogError(e);
		}
	}

	#region Download

	public static void DownloadConfig<T>()
		where T : CsvDataParser, new()
	{
		m_parser = new T();
		
		GLDDownloadConfig downloadConfig = LoadDownLoadConfig();
		EditorCoroutineRunner.StartEditorCoroutine(DownloadGoogleSheet(downloadConfig));
	}

	private static IEnumerator DownloadGoogleSheet(GLDDownloadData data)
	{
		bool result = CheckGldDownloadData(data);
		if (result)
		{
			UnityWebRequest www = UnityWebRequest.Get(data.url);
			UnityWebRequestAsyncOperation async = www.SendWebRequest();

			while (!async.isDone)
			{
				yield return 0;
			}

			if (string.IsNullOrEmpty(www.error))
			{
				string text = www.downloadHandler.text;
				www.Dispose();

				if (!string.IsNullOrEmpty(text))
				{
					ParseCsvConfig(text, data.name, data.jsonPath, data.csPath);
				}
				else
				{
					Debug.LogError("Download text is null ! url : " + data.url);
				}
			}
			else
			{
				Debug.LogError(www.error);
			}
		}

		yield return new WaitForEndOfFrame();
	}

	private static bool CheckGldDownloadData(GLDDownloadData data)
	{
		if (data == null)
			return false;

		if (string.IsNullOrEmpty(data.url) ||
		    string.IsNullOrEmpty(data.jsonPath) ||
		    string.IsNullOrEmpty(data.name))
			return false;

		return true;
	}
	
	private static IEnumerator DownloadGoogleSheet(GLDDownloadConfig config)
	{
		if (config != null && config.downLoadList.Count > 0)
		{
			for (int i = 0; i < config.downLoadList.Count; i++)
			{
				GLDDownloadData data = config.downLoadList[i];
				if (data != null)
				{
					yield return DownloadGoogleSheet(data);
				}
			}
		}

		m_parser = null;
		
		AssetDatabase.Refresh();
	}

	private static GLDDownloadConfig LoadDownLoadConfig()
	{
		string path = DownloadConfigPath();
		string json = File.ReadAllText(path);
		GLDDownloadConfig config = JsonUtility.FromJson<GLDDownloadConfig>(json);
		return config;
	}

	private static string DownloadConfigPath()
	{
		string path = Path.Combine(Application.dataPath, "DownloadConfig.json");
		return path;
	}

	private static void ParseCsvConfig(string text, string name, string jsonPath, string csPath)
	{
		if(string.IsNullOrEmpty(text))
			return;

		string json = string.Empty;
		string cs = string.Empty;
		CsvToJsonAndCs(text, name, out json, out cs);

		WriteJsonFile(jsonPath, json);
		WriteCsFile(csPath, cs);
	}

	private static void CsvToJsonAndCs(string text, string name, out string json, out string cs)
	{
		json = cs = string.Empty;
		string[][] tCsvData = CsvParser2.Parse(text);
		List<CsvGridData> variableTypeList = new List<CsvGridData>();
		List<CsvGridData> variableNameList = new List<CsvGridData>();
		bool result = false;
		JObject obj = new JObject();
		JArray array = new JArray();
		JProperty property = new JProperty("_datas", array);
		obj.Add(property);
		bool findVariableType = false;
		bool findVariableName = false;
		
		for (int row = 0; row < tCsvData.Length; row++)
		{
			if(tCsvData[row].Length <= 0)
				continue;

			if (!result)
			{
				result = GenerateVariableTypeAndNameList(tCsvData[row], ref variableTypeList, ref variableNameList,
					ref findVariableType, ref findVariableName);
			}
			else
			{
				//变量名称和变量类型匹配
				if (variableNameList.Count == variableTypeList.Count)
				{
					GenerateJsonStr(variableNameList, variableTypeList, tCsvData[row], ref array);
				}
				else
				{
					Debug.LogErrorFormat("Current profile variable name and variable type do not match！！  file name：{0}", name);
				}
			}
		}

		json = obj.ToString();
		cs = GenerateCsStr(name, variableTypeList, variableNameList);
	}

	private static void GenerateJsonStr(List<CsvGridData> variableNameList, List<CsvGridData> variableTypeList, string[] csvRowData, ref JArray jArray)
	{
		if (csvRowData.Length <= 0 || string.IsNullOrEmpty(csvRowData[0]))
		{
			return;
		}
		
		Dictionary<string, CsvVariableData> map = GenerateVariableNameToGridDataMap(variableNameList, variableTypeList);
		JObject obj = new JObject();
    		jArray.Add(obj);
		foreach (string variableName in map.Keys)
		{
			CsvVariableData variableData = map[variableName];
			object property = m_parser.Parse(variableData, csvRowData);
			if (property != null)
			{
				obj.Add(new JProperty(variableData.variableName, property));
			}
		}
		
//		for (int i = 0; i < variableNameList.Count; i++)
//		{
//			CsvGridData gridData = variableNameList[i];
//			if (gridData.col >= 0 && gridData.col < csvRowData.Length)
//			{
//				object property = m_parser.Parse(variableTypeList[i].content, csvRowData[gridData.col]);
//				obj.Add(new JProperty(variableNameList[i].content, property));
//			}
//		}
	}

	private static Dictionary<string, CsvVariableData> GenerateVariableNameToGridDataMap(List<CsvGridData> variableNameList,
		List<CsvGridData> variableTypeList)
	{
		Dictionary<string, CsvVariableData> map = new Dictionary<string, CsvVariableData>();
		for (int i = 0; i < variableNameList.Count; i++)
		{
			CsvGridData variableNameData = variableNameList[i];
			CsvGridData variableTypeData = variableTypeList[i];
			if (!map.ContainsKey(variableNameData.content))
			{
				CsvVariableData data = new CsvVariableData(variableNameData.content, variableTypeData.content);
				data.PushCol(variableNameData.col);
				map.Add(variableNameData.content, data);
			}
			else
			{
				CsvVariableData data = map[variableNameData.content];
				data.PushCol(variableNameData.col);
			}
		}

		return map;
	}

	private static string GenerateCsStr(string name, List<CsvGridData> variableTypeList, List<CsvGridData> variableNameList)
	{
		StringBuilder code = new StringBuilder();
		code.AppendLine("/**************************************************************************************************");
		code.AppendLine("                                   自动生成代码  请勿手动修改");
		code.AppendLine("**************************************************************************************************/");
		code.AppendLine();
		
		//Append using
		code.AppendLine("using System.Collections;");
		code.AppendLine("using System.Collections.Generic;");
		code.AppendLine("using UnityEngine;");
		code.AppendLine();
		
		//Append config class
		code.AppendLine("[System.Serializable]");
		code.AppendLine(string.Format("public class {0} : ConfigBase", name));
		code.AppendLine("{");
		string className = string.Format("{0}_Data", name);
		code.AppendLine(string.Format("	public List<{0}> _datas = new List<{1}>();", className, className));
		code.AppendLine("}");
		code.AppendLine();
		
		//Append config data class
		code.AppendLine("[System.Serializable]");
		code.AppendLine(string.Format("public class {0}_Data : ConfigData", name));
		code.AppendLine("{");
		Dictionary<string, CsvVariableData> map = GenerateVariableNameToGridDataMap(variableNameList, variableTypeList);
		foreach (string variableName in map.Keys)
		{
			CsvVariableData data = map[variableName];
			string type = m_parser.ParseVariableType(data.variableType);
			string varName = data.variableName;
			string variable = string.Format("	public {0} {1};", type, varName);
			code.AppendLine(variable);
		}
		code.AppendLine("}");

		return code.ToString();
	}
	
	private static bool GenerateVariableTypeAndNameList(string[] csvRow, ref List<CsvGridData> variableTypeList,
		ref List<CsvGridData> variableNameList,ref bool findVariableType, ref bool findVariableName)
	{
		ECsvTableRowType rowType = GetTableRowType(csvRow[0], ref findVariableType, ref findVariableName);
		if (rowType == ECsvTableRowType.BlankRow ||
		    rowType == ECsvTableRowType.Comment ||
		    rowType == ECsvTableRowType.MaxType)
		{
			return false;
		}

		for (int col = 0; col < csvRow.Length; col++)
		{
			if (rowType == ECsvTableRowType.VariableType)
			{
				string type = CsvDataParser.RemoveStringBlankChar(csvRow[col]);
				if (string.IsNullOrEmpty(type))
					continue;

				CsvGridData data = new CsvGridData(col, type, rowType);
				variableTypeList.Add(data);
			}

			if (rowType == ECsvTableRowType.VariableName)
			{
				string type = CsvDataParser.RemoveStringBlankChar(csvRow[col]);
				if (string.IsNullOrEmpty(type))
					continue;

				CsvGridData data = new CsvGridData(col, type, rowType);
				variableNameList.Add(data);
			}
		}

		return (findVariableType && findVariableName);
	}

	private static ECsvTableRowType GetTableRowType(string text, ref bool findVariableType, ref bool findVariableName)
	{
		if (string.IsNullOrEmpty(text))
		{
			return ECsvTableRowType.BlankRow;
		}
		
		string firstStr = text.Substring(0, 1);

		if (string.IsNullOrEmpty(firstStr))
		{
			return ECsvTableRowType.BlankRow;
		}
		
		if (firstStr == "#")
		{
			return ECsvTableRowType.Comment;
		}

		if (!findVariableType)
		{
			if (m_parser.CheckIsVaraibleType(text))
			{
				findVariableType = true;
				return ECsvTableRowType.VariableType;
			}
			
			return ECsvTableRowType.BlankRow;
		}

		if (!findVariableName)
		{
			if (!string.IsNullOrEmpty(text))
			{
				findVariableName = true;
				return ECsvTableRowType.VariableName;
			}
			
			return ECsvTableRowType.BlankRow;
		}
		
		return ECsvTableRowType.ConfigData;
	}

	private static void WriteJsonFile(string jsonPath, string json)
	{
		jsonPath = Path.Combine(Application.dataPath, jsonPath);
		string path = Path.GetDirectoryName(jsonPath);
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		File.WriteAllText(jsonPath, json, Encoding.UTF8);
		Debug.LogErrorFormat("Write json file : {0}", jsonPath);
	}

	private static void WriteCsFile(string csPath, string cs)
	{
		csPath = Path.Combine(Application.dataPath, csPath);
		string path = Path.GetDirectoryName(csPath);
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		File.WriteAllText(csPath, cs, Encoding.UTF8);
	}
	#endregion
}

#endif

