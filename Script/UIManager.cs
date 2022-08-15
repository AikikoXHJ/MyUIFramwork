using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    /// <summary>
    /// 用来存储UIBase的栈
    /// </summary>
    //public Stack<UIBase> uiStack;

    /// <summary>
    /// 存储UI栈，用来做不同类型UI的区分
    /// </summary>
    public Dictionary<string, Stack<UIBase>> uiStackDic;

    /// <summary>
    /// 用UI的名字来存UI的GameObject
    /// </summary>
    public Dictionary<string, GameObject> uiObjectDic;

    /// <summary>
    /// 当前场景下的Canvas
    /// </summary>
    public GameObject uiCanvasObj;

    /// <summary>
    /// UIManager作为单例，全局唯一存在
    /// </summary>
    private static UIManager instance;

    public static UIManager GetInstance()
    {
        if (instance == null)
        {
            Debug.Log("UIManager 实例不存在！");
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
    /// 压入一个UIbase
    /// </summary>
    /// <param name="uiBase"></param>
    public void PushUIBase(UIBase uiBase, string _groupName = "None")
    {


        Debug.Log($"{uiBase.uiInfo.UIName}被压入栈中");

        Stack<UIBase> uiStack;

        if (uiStackDic.ContainsKey(_groupName))
        {
            Debug.Log($"入栈时{_groupName}存在");
            uiStack = uiStackDic[_groupName];
        }
        else
        {
            Debug.LogWarning($"入栈时{_groupName}不存在");
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
    /// UIbase出栈
    /// </summary>
    /// <param name="isPopAll">true:Pop全部  false:Pop栈顶</param>
    public void PopUIBase(bool isPopAll, string _groupName = "None")
    {
        Stack<UIBase> uiStack;

        if (uiStackDic.ContainsKey(_groupName))
        {
            Debug.Log($"出栈时{_groupName}栈存在");
            uiStack = uiStackDic[_groupName];
        }
        else
        {
            Debug.LogWarning($"出栈时{_groupName}栈不存在");
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
    /// 移动栈中的某个UIBase到栈顶
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
