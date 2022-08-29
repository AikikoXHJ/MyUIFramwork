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

    /*---------------------------------------- 对外暴露的接口 ----------------------------------------*/

    /// <summary>
    /// 将UIbase压入栈中
    /// </summary>
    public void Push(UIBase _uiBase, string _groupName = "None")
    {
        PushUIBase(_uiBase, _groupName);
    }

    public void Pop(bool _isPopAll, string _groupName = "None")
    {
        PopUIBase(_isPopAll, _groupName);
    }


    /// <summary>
    /// 移动UIBase至栈顶
    /// </summary>
    public void MoveUIBaseToStackTop(Stack<UIBase> _rootStack, UIBase uiBase)
    {
        MoveUIBaseByStackIndex(_rootStack, uiBase, 1);
    }


    /*---------------------------------------- 内部方法 ----------------------------------------*/

    /// <summary>
    /// 加载并获取UI预制件
    /// </summary>
    /// <param name="uiInfo">UI预制件信息</param>
    /// <returns>加载的预制件</returns>
    private GameObject GetSingleObject(UIInfo uiInfo)
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
    private void PushUIBase(UIBase uiBase, string _groupName = "None")
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
    private void PopUIBase(bool isPopAll, string _groupName = "None")
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
    /// 移动栈中的某个UIBase到特定位置,栈顶为1
    /// </summary>
    private void MoveUIBaseByStackIndex(Stack<UIBase> _rootStack, UIBase uiBase, int _index)
    {
        if (_rootStack.Count <= 0)
        {
            Debug.LogWarning("Stack is null!");
            return;
        }
        if (_index < 1 || _index > _rootStack.Count)
        {
            Debug.LogWarning("Index ranges from 1 to stack count!");
            return;
        }

        Stack<UIBase> _localStack = new Stack<UIBase>();
        bool _isExist = false;
        foreach (var _item in _rootStack)
        {
            if (_item.uiInfo.UIName == uiBase.uiInfo.UIName)
            {
                _isExist = true;
            }
            else
            {
                _localStack.Push(_item);
            }
        }

        if (_isExist)
        {
            _rootStack.Clear();
        }
        else 
        {
            Debug.LogWarning($"There's no {uiBase.uiInfo.UIName} in the stack");
            return;
        }

        for (int i = _localStack.Count + 1; i > 0 ; --i)
        {
            if (i == _index)
            {
                _rootStack.Push(uiBase);
            }
            else
            {
                _rootStack.Push(_localStack.Pop());
            }
        }

    }


}
