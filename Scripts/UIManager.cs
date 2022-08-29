using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    /// <summary>
    /// �����洢UIBase��ջ
    /// </summary>
    //public Stack<UIBase> uiStack;

    /// <summary>
    /// �洢UIջ����������ͬ����UI������
    /// </summary>
    public Dictionary<string, Stack<UIBase>> uiStackDic;

    /// <summary>
    /// ��UI����������UI��GameObject
    /// </summary>
    public Dictionary<string, GameObject> uiObjectDic;

    /// <summary>
    /// ��ǰ�����µ�Canvas
    /// </summary>
    public GameObject uiCanvasObj;

    /// <summary>
    /// UIManager��Ϊ������ȫ��Ψһ����
    /// </summary>
    private static UIManager instance;

    public static UIManager GetInstance()
    {
        if (instance == null)
        {
            Debug.Log("UIManager ʵ�������ڣ�");
            return instance;
        }
        else
        {
            return instance;
        }
    }

    public UIManager()
    {
        instance = this;
        //uiStack = new Stack<UIBase>();
        uiStackDic = new Dictionary<string, Stack<UIBase>>();
        uiObjectDic = new Dictionary<string, GameObject>();

    }

    public GameObject GetSingleObject(UIInfo uiInfo)
    {
        if (uiObjectDic.ContainsKey(uiInfo.UIName))
        {
            return uiObjectDic[uiInfo.UIName];
        }

        if (uiCanvasObj == null)
        {
            uiCanvasObj =  UIFunction.GetInstance().FindCanvas();
        }

        GameObject gameObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(uiInfo.UIPath), uiCanvasObj.transform);
        return gameObject;

    }

    /// <summary>
    /// ѹ��һ��UIbase
    /// </summary>
    /// <param name="uiBase"></param>
    public void PushUIBase(UIBase uiBase, string _groupName = "None")
    {


        Debug.Log($"{uiBase.uiInfo.UIName}��ѹ��ջ��");

        Stack<UIBase> uiStack;

        if (uiStackDic.ContainsKey(_groupName))
        {
            Debug.Log($"��ջʱ{_groupName}����");
            uiStack = uiStackDic[_groupName];
        }
        else
        {
            Debug.LogWarning($"��ջʱ{_groupName}������");
            uiStack = new Stack<UIBase>();
            uiStackDic.Add(_groupName, uiStack);
        }

        if (uiStack.Count > 0)
        {
            uiStack.Peek().OnHide();
        }

        GameObject uiObj = GetSingleObject(uiBase.uiInfo);
        uiObjectDic.Add(uiBase.uiInfo.UIName, uiObj);
        uiBase.activeObj = uiObj;

        if (uiStack.Count == 0)
        {
            uiStack.Push(uiBase);
        }
        else 
        {
            if (uiStack.Peek().uiInfo.UIName != uiBase.uiInfo.UIName)
            {
                uiStack.Push(uiBase);
            }
        }

        uiBase.OnStart();
    }

    /// <summary>
    /// UIbase��ջ
    /// </summary>
    /// <param name="isPopAll">true:Popȫ��  false:Popջ��</param>
    public void PopUIBase(bool isPopAll, string _groupName = "None")
    {
        Stack<UIBase> uiStack;

        if (uiStackDic.ContainsKey(_groupName))
        {
            Debug.Log($"��ջʱ{_groupName}ջ����");
            uiStack = uiStackDic[_groupName];
        }
        else
        {
            Debug.LogWarning($"��ջʱ{_groupName}ջ������");
            uiStack = new Stack<UIBase>();
            uiStackDic.Add(_groupName, uiStack);
        }


        if (isPopAll == true)
        {
            if (uiStack.Count > 0)
            {
                uiStack.Peek().OnHide();
                uiStack.Peek().OnDestory();
                GameObject.Destroy(uiObjectDic[uiStack.Peek().uiInfo.UIName]);
                uiObjectDic.Remove(uiStack.Peek().uiInfo.UIName);
                uiStack.Pop();
                PopUIBase(true);
            }
        }
        else
        {
            if (uiStack.Count > 0)
            {
                uiStack.Peek().OnHide();
                uiStack.Peek().OnDestory();
                GameObject.Destroy(uiObjectDic[uiStack.Peek().uiInfo.UIName]);
                uiObjectDic.Remove(uiStack.Peek().uiInfo.UIName);
                uiStack.Pop();

                if (uiStack.Count > 0)
                {
                    uiStack.Peek().OnOpen();
                }

            }
        }
        if (uiStack.Count <= 0)
        {
            uiStackDic.Remove(_groupName);
        }
    }

    /// <summary>
    /// �ƶ�ջ�е�ĳ��UIBase��ջ��
    /// </summary>
    public void MoveUIBaseToStackTop(Stack<UIBase> _rootStack, UIBase uiBase)
    {
        if (_rootStack.Count <= 0) return;
        Stack<UIBase> _localStack = new Stack<UIBase>();
        for (int index = 0; index < _rootStack.Count; ++index)
        {
            if(_rootStack.Peek().uiInfo.UIName != uiBase.uiInfo.UIName)
            {
                _localStack.Push(_rootStack.Peek());
                _rootStack.Pop();
            }
        }



    }


}
