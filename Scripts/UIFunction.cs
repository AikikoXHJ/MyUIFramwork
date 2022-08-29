using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunction
{

    /// <summary>
    /// UI工具类作为单例全局唯一存在
    /// </summary>
    private static UIFunction instance;

    public static UIFunction GetInstance()
    {
        if (instance == null)
        {
            instance = new UIFunction();
            return instance;
        }
        else
        {
            return instance;
        }
    }

    /// <summary>
    /// 获取场景中的Canvas
    /// </summary>
    public GameObject FindCanvas()
    {
        GameObject _canvasObj = GameObject.FindObjectOfType<Canvas>().gameObject;
        if (_canvasObj == null)
        {
            Debug.LogError("没有在场景中找到Canvas");
            return _canvasObj;
        }
        return _canvasObj;
    }

    /// <summary>
    /// 根据名称获取物体下的子物体
    /// </summary>
    /// <param name="_parent"></param>
    /// <param name="_childName"></param>
    /// <returns></returns>
    public GameObject FindObjectInChild(GameObject _parent, string _childName, bool _includeHide = true)
    {
        Transform[] _transforms = _parent.GetComponentsInChildren<Transform>(_includeHide);
        if (_transforms.Length <= 0)
        {
            Debug.LogWarning($"{_parent.name}物体下没有子物体!!");
            return null;
        }

        foreach (var _child in _transforms)
        {
            if (_child.name == _childName)
            {
                return _child.gameObject;
            }
        }

        Debug.LogWarning($"在{_parent.name}物体中没有找到{_childName}物体！");
        return null;
    }

    /// <summary>
    /// 获取/添加物体上的组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_getObj"></param>
    /// <returns></returns>
    public T GetOrAddComponent<T>(GameObject _getObj) where T : Component
    {
        if (_getObj.GetComponent<T>() == null)
        {
            _getObj.AddComponent(typeof(T));
        }

        return _getObj.GetComponent<T>();
    }


    /// <summary>
    /// 获取子物体的组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_parent"></param>
    /// <param name="_componentName"></param>
    /// <returns></returns>
    public T GetSingleComponentInChild<T>(GameObject _parent, string _componentName, bool _includeHide = true) where T : Component
    {
        Transform[] _parentTrs = _parent.GetComponentsInChildren<Transform>(_includeHide);

        if (_parentTrs.Length <= 0)
        {
            Debug.LogWarning($"{_parent.name}没有子物体");
            return null;
        }
        foreach (var _item in _parentTrs)
        {
            if (_item.gameObject.name == _componentName)
            {
                return _item.gameObject.GetComponent<T>();
            }
        }
        Debug.LogWarning($"{_parent.name}的子物体没有{_componentName}组件");
        return null;
    }

}
