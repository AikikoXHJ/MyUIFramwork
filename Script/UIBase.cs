using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase
{
    public UIInfo uiInfo;

    public GameObject activeObj;

    public UIBase(UIInfo _uiInfo)
    {
        uiInfo = _uiInfo;
    }

    /// <summary>
    /// UI被启动时候要执行的
    /// </summary>
    public virtual void OnStart() 
    {
        Debug.Log($"{uiInfo.UIName}被启用了");
        activeObj.SetActive(true);
    }

    /// <summary>
    /// UI开启的时候执行
    /// </summary>
    public virtual void OnOpen() 
    {
        Debug.Log($"{uiInfo.UIName}开启了");
        activeObj.SetActive(true);
    }

    /// <summary>
    /// UI的active为false时候执行
    /// </summary>
    public virtual void OnHide() 
    {
        Debug.Log($"{uiInfo.UIName}被关闭了");
        activeObj.SetActive(false);
    }

    /// <summary>
    /// UI的GameObject被卸载的时候执行
    /// </summary>
    public virtual void OnDestory() 
    {
        Debug.Log($"{uiInfo.UIName}被卸载了");
    }

}
