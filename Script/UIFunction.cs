using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunction
{

    /// <summary>
    /// UI��������Ϊ����ȫ��Ψһ����
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
    /// ��ȡ�����е�Canvas
    /// </summary>
    public GameObject FindCanvas()
    {
        GameObject _canvasObj = GameObject.FindObjectOfType<Canvas>().gameObject;
        if (_canvasObj == null)
        {
            Debug.LogError("û���ڳ������ҵ�Canvas");
            return _canvasObj;
        }
        return _canvasObj;
    }

    /// <summary>
    /// �������ƻ�ȡ�����µ�������
    /// </summary>
    /// <param name="_parent"></param>
    /// <param name="_childName"></param>
    /// <returns></returns>
    public GameObject FindObjectInChild(GameObject _parent, string _childName, bool _includeHide = true)
    {
        Transform[] _transforms = _parent.GetComponentsInChildren<Transform>(_includeHide);
        if (_transforms.Length <= 0)
        {
            Debug.LogWarning($"{_parent.name}������û��������!!");
            return null;
        }

        foreach (var _child in _transforms)
        {
            if (_child.name == _childName)
            {
                return _child.gameObject;
            }
        }

        Debug.LogWarning($"��{_parent.name}������û���ҵ�{_childName}���壡");
        return null;
    }

    /// <summary>
    /// ��ȡ/��������ϵ����
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
    /// ��ȡ����������
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
            Debug.LogWarning($"{_parent.name}û��������");
            return null;
        }
        foreach (var _item in _parentTrs)
        {
            if (_item.gameObject.name == _componentName)
            {
                return _item.gameObject.GetComponent<T>();
            }
        }
        Debug.LogWarning($"{_parent.name}��������û��{_componentName}���");
        return null;
    }

}
