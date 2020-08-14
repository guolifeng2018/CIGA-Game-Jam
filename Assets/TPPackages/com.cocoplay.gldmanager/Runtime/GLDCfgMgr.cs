using System.Collections.Generic;
using TC.Core.Singleton;
using UnityEngine;

public class GLDCfgMgr : GeneralDefaultSingleton<GLDCfgMgr>, ISingletonCreateHandler
{
    private Dictionary<string, ConfigBase> m_gameConfigMap;
    
    public void OnSingletonCreated()
    {
        m_gameConfigMap = new Dictionary<string, ConfigBase>();
    }

    public bool Inited()
    {
        return m_gameConfigMap != null;
    }

    public T GetConfigData<T>(string resId)
        where T : ConfigBase
    {
        string key = resId.ToString();
        if (m_gameConfigMap.ContainsKey(key))
        {
            return m_gameConfigMap[key] as T;
        }

        T data = LoadGameConfig<T>(resId);
        return data;
    }

    private T LoadGameConfig<T>(string resID)
        where T : ConfigBase
    {
        string path = string.Format("Config/{0}", resID);
        TextAsset asset = Resources.Load<TextAsset>(path);
        string json = asset.text;
        T data = JsonUtility.FromJson<T>(json);
        if (data != null)
        {
            m_gameConfigMap.Add(resID.ToString(), data);
        }
        return data;
    }
}
