#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class CsvDataParser
{
	public const string INT = "int";
	public const string STRING = "string";
	public const string BOOL = "bool";
	public const string FLOAT = "float";
	public const string LONG = "long";
	public const string LIST_STRING = "list<string>";
	public const string LIST_INT = "list<int>";
	public const string LIST_FLOAT = "list<float>";

	public const char LIST_SPLIT_CHAR = '*';
	protected List<string> m_variableTypeList;

	protected List<string> VariableTypeList
	{
		get
		{
			if (m_variableTypeList == null)
			{
				InitVariableTypeList();
			}

			return m_variableTypeList;
		}
	}

	protected void InitVariableTypeList()
	{
		m_variableTypeList = new List<string>
		{
			INT,STRING,BOOL,FLOAT,LONG,LIST_STRING,LIST_INT,LIST_FLOAT
		};
	}

	public virtual object Parse(CsvVariableData data, string[] rowData)
	{
		switch (data.variableType)
		{
			case INT:
				return IntParse(data, rowData);
			case STRING:
				return StringParse(data, rowData);
			case BOOL:
				return BoolParse(data, rowData);
			case FLOAT:
				return FloatParse(data, rowData);
			case LONG:
				return LongParse(data, rowData);
			case LIST_STRING:
				return ListStringParse(data, rowData);
			case LIST_INT:
				return ListIntParse(data, rowData);
			case LIST_FLOAT:
				return ListFloatParse(data, rowData);
		}
		return null;
	}
	
	public virtual object Parse(string typeStr, string content)
	{
		switch (typeStr)
		{
				case INT:
					return IntParse(content);
				case STRING:
					return StringParse(content);
				case BOOL:
					return BoolParse(content);
				case FLOAT:
					return FloatParse(content);
				case LONG:
					return LongParse(content);
				case LIST_STRING:
					return ListStringParse(content);
				case LIST_INT:
					return ListIntParse(content);
				case LIST_FLOAT:
					return ListFloatParse(content);
		}

		DebugError(typeStr, content);
		return content;
	}

	public virtual string ParseVariableType(string typeStr)
	{
		switch (typeStr)
		{
			case INT:
				return "int";
			case STRING:
				return "string";
			case BOOL:
				return "bool";
			case FLOAT:
				return "float";
			case LONG:
				return "long";
			case LIST_STRING:
				return "List<string>";
			case LIST_INT:
				return "List<int>";
			case LIST_FLOAT:
				return "List<float>";
		}

		return string.Empty;
	}

	public static string RemoveStringBlankChar(string text)
	{
		text = Regex.Replace(text, @"\s", "");
		return text;
	}

	public bool CheckIsVaraibleType(string text)
	{
		text = RemoveStringBlankChar(text).ToLower();
		if (VariableTypeList.Contains(text))
		{
			return true;
		}

		return false;
	}

	private void DebugError(string typeStr, string content)
	{
		Debug.LogWarningFormat("Error type : {0}			content : {1}", typeStr, content);
	}

	#region Parser

	private bool Check(CsvVariableData data, string[] rowData)
	{
		bool succss = true;
		for (int i = 0; i < data.cols.Count; i++)
		{
			if (!(data.cols.Count > 0 && data.cols[i] >= 0 && data.cols[i] < rowData.Length &&
			    !string.IsNullOrEmpty(rowData[data.cols[i]])))
			{
				succss = false;
				break;
			}
		}

		return succss;
	}

	private int IntParse(string val)
	{
		int v = 0;
		if (!int.TryParse(val, out v))
		{
			DebugError("IntParse", val);
		}
		return v;
	}
	
	private int IntParse(CsvVariableData data, string[] rowData)
	{
		int v = 0;
		if (Check(data, rowData))
		{
			if (!int.TryParse(rowData[data.cols[0]], out v))
			{
				DebugError("IntParse", rowData[data.cols[0]]);
			}
		}
		return v;
	}

	private string StringParse(string val)
	{
		return val;
	}
	
	private string StringParse(CsvVariableData data, string[] rowData)
	{
		string v = string.Empty;
		if (Check(data, rowData))
		{
			return rowData[data.cols[0]];
		}
		return v;
	}

	private bool BoolParse(string val)
	{
		bool v = true;
		if (!bool.TryParse(val, out v))
		{
			DebugError("BoolParse", val);
		}
		return v;
	}
	
	private bool BoolParse(CsvVariableData data, string[] rowData)
	{
		bool v = true;
		if (Check(data, rowData))
		{
			if (!bool.TryParse(rowData[data.cols[0]], out v))
			{
				DebugError("BoolParse", rowData[data.cols[0]]);
			}
		}
		
		return v;
	}

	private float FloatParse(string val)
	{
		float v = 0f;
		if (!float.TryParse(val, out v))
		{
			DebugError("FloatParse", val);
		}
		return v;
	}
	
	private float FloatParse(CsvVariableData data, string[] rowData)
	{
		float v = 0f;

		if (Check(data, rowData))
		{
			if (!float.TryParse(rowData[data.cols[0]], out v))
			{
				DebugError("FloatParse", rowData[data.cols[0]]);
			}
		}
		
		return v;
	}

	private float LongParse(string val)
	{
		long v = 0;
		if (!long.TryParse(val, out v))
		{
			DebugError("LongParse", val);
		}
		return v;
	}
	
	private long LongParse(CsvVariableData data, string[] rowData)
	{
		long v = 0;
		if (Check(data, rowData))
		{
			if (!long.TryParse(rowData[data.cols[0]], out v))
			{
				DebugError("LongParse", rowData[data.cols[0]]);
			}
		}
		
		return v;
	}

	private List<string> ListStringParse(string val)
	{
		string[] arr = val.Split(LIST_SPLIT_CHAR);
		List<string> list = new List<string>();
		list.AddRange(arr);
		return list;
	}
	
	private List<string> ListStringParse(CsvVariableData data, string[] rowData)
	{
		List<string> list = new List<string>();
		for (int i = 0; i < data.cols.Count; i++)
		{
			int col = data.cols[i];
			if (!string.IsNullOrEmpty(rowData[col]))
			{
				list.Add(rowData[col]);
			}
		}
		return list;
	}

	private List<int> ListIntParse(string val)
	{
		string[] arr = val.Split(LIST_SPLIT_CHAR);
		List<int> list = new List<int>();
		for (int i = 0; i < arr.Length; i++)
		{
			int v = IntParse(arr[i]);
			list.Add(v);
		}

		return list;
	}
	
	private List<int> ListIntParse(CsvVariableData data, string[] rowData)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < data.cols.Count; i++)
		{
			int col = data.cols[i];
			if (!string.IsNullOrEmpty(rowData[col]))
			{
				int v = IntParse(rowData[col]);
				list.Add(v);
			}
		}

		return list;
	}
	
	private List<float> ListFloatParse(string val)
	{
		string[] arr = val.Split(LIST_SPLIT_CHAR);
		List<float> list = new List<float>();
		for (int i = 0; i < arr.Length; i++)
		{
			float v = FloatParse(arr[i]);
			list.Add(v);
		}

		return list;
	}
	
	private List<float> ListFloatParse(CsvVariableData data, string[] rowData)
	{
		List<float> list = new List<float>();
		for (int i = 0; i < data.cols.Count; i++)
		{
			int col = data.cols[i];
			if (!string.IsNullOrEmpty(rowData[col]))
			{
				float v = FloatParse(rowData[col]);
				list.Add(v);
			}
		}

		return list;
	}
	
	protected JObject GenerateJObjectFromObj<T>(T obj)
		where T : class, new()
	{
		JObject jObj = new JObject();
		FieldInfo[] properties = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public);
		if(properties.Length <= 0){ return null; }

		for (int i = 0; i < properties.Length; i++)
		{
			FieldInfo info = properties[i];
			string name = info.Name;
			object value = info.GetValue(obj);
			jObj.Add(new JProperty(name, value));
		}

		return jObj;
	}

	#endregion
	
}
#endif

