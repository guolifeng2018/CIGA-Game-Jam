#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvGridData
{
    public int col;
    public string content;
    public ECsvTableRowType type;

    public CsvGridData()
    {
        col = 0;
        content = string.Empty;
        type = ECsvTableRowType.MaxType;
    }

    public CsvGridData(int col, string content, ECsvTableRowType rowType)
    {
        this.col = col;
        this.content = content;
        this.type = rowType;
    }
}

public class CsvVariableData
{
    public string variableName;
    public string variableType;
    public List<int> cols;

    public CsvVariableData(string varName, string varType)
    {
        variableName = varName;
        variableType = varType;
        cols = new List<int>();
    }

    public void PushCol(int col)
    {
        if (!cols.Contains(col))
        {
            cols.Add(col);
        }
    }
}
#endif
