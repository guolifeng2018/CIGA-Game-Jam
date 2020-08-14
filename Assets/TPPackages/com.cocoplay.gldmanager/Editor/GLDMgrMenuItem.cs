#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GLDMgrMenuItem 
{
    [MenuItem("TC/GLD/Create Download Config Template")]
    public static void CreateDownloadConfigTemp()
    {
        GLDDownloadHandler.CreateTemplateDownloadConfig();
        AssetDatabase.Refresh();
    }
    
    [MenuItem("TC/GLD/DownLoad/Test Download TestConfig")]
    public static void TestDownloadConfig()
    {
        GLDDownloadHandler.DownloadConfig<CsvDataParser>();
        AssetDatabase.Refresh();
    }
}

#endif

