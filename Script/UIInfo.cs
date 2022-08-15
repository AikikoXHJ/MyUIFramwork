using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInfo
{
    private string uiName;
    private string uiPath;

    public string UIPath { get => uiPath; }
    public string UIName { get => uiName; }

    /// <summary>
    /// 获取UI基础信息
    /// </summary>
    /// <param name="_uiPath">UI的路径</param>
    /// <param name="_uiName">UI的名字</param>
    public UIInfo(string _uiPath, string _uiName)
    {
        uiPath = _uiPath;
        uiName = _uiName;
    }

}
