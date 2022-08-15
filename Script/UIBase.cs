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
    /// UI������ʱ��Ҫִ�е�
    /// </summary>
    public virtual void OnStart() 
    {
        Debug.Log($"{uiInfo.UIName}��������");
        activeObj.SetActive(true);
    }

    /// <summary>
    /// UI������ʱ��ִ��
    /// </summary>
    public virtual void OnOpen() 
    {
        Debug.Log($"{uiInfo.UIName}������");
        activeObj.SetActive(true);
    }

    /// <summary>
    /// UI��activeΪfalseʱ��ִ��
    /// </summary>
    public virtual void OnHide() 
    {
        Debug.Log($"{uiInfo.UIName}���ر���");
        activeObj.SetActive(false);
    }

    /// <summary>
    /// UI��GameObject��ж�ص�ʱ��ִ��
    /// </summary>
    public virtual void OnDestory() 
    {
        Debug.Log($"{uiInfo.UIName}��ж����");
    }

}
