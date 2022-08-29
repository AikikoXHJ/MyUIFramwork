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
    /// ��ȡUI������Ϣ
    /// </summary>
    /// <param name="_uiPath">UI��·��</param>
    /// <param name="_uiName">UI������</param>
    public UIInfo(string _uiPath, string _uiName)
    {
        uiPath = _uiPath;
        uiName = _uiName;
    }

}
