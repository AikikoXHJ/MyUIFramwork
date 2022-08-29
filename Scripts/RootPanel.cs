using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RootPanel : UIBase
{
    private static string name = "RootPanel";
    private static string path = "RootPanel";
    public static readonly UIInfo uiInfo = new UIInfo(path, name);

    public RootPanel() : base(uiInfo)
    {

    }

    public override void OnStart()
    {
        base.OnStart();
        UIFunction.GetInstance().GetSingleComponentInChild<Button>(activeObj, "PersonalMainButton").onClick.AddListener(OnPersonalBtnClicked);
        UIFunction.GetInstance().GetSingleComponentInChild<Button>(activeObj, "SystemMainButton").onClick.AddListener(OnSystemBtnClicked);
    }

    public override void OnOpen()
    {
        base.OnOpen();
    }

    public override void OnHide()
    {
        base.OnHide();
    }

    public override void OnDestory()
    {
        base.OnDestory();
    }

    #region 个人信息按钮相关

    public void OnPersonalBtnClicked()
    {
        GameObject _btnList = UIFunction.GetInstance().FindObjectInChild(activeObj, "PersonalBtnList");
        if (_btnList.activeSelf)
        {
            _btnList.SetActive(false);
        }
        else
        {
            _btnList.SetActive(true);
            UIFunction.GetInstance().GetSingleComponentInChild<Button>(_btnList, "InfoButton").onClick.AddListener(OnInfoBtnClicked);
            UIFunction.GetInstance().GetSingleComponentInChild<Button>(_btnList, "EquipButton").onClick.AddListener(EquipBtnClicked);
        }
    }

    public void OnInfoBtnClicked()
    {
        Debug.Log("属性按钮被点击");
        UIManager.GetInstance().Push(new PersonalInfoPanel(), "PersonalInfo");
    }

    public void EquipBtnClicked()
    {
        Debug.Log("装备按钮被点击");
        UIManager.GetInstance().Push(new PersonalEquipPanel(), "PersonalInfo");
    }
    #endregion

    #region 系统设置按钮相关
    public void OnSystemBtnClicked()
    {
        GameObject _btnList = UIFunction.GetInstance().FindObjectInChild(activeObj, "SystemBtnList");
        if (_btnList.activeSelf)
        {
            _btnList.SetActive(false);
        }
        else
        {
            _btnList.SetActive(true);
            UIFunction.GetInstance().GetSingleComponentInChild<Button>(_btnList, "ExitButton").onClick.AddListener(ExitBtnClicked);
            UIFunction.GetInstance().GetSingleComponentInChild<Button>(_btnList, "ConfigButton").onClick.AddListener(ConfigBtnClicked);
        }
    }

    public void ExitBtnClicked()
    {
        Debug.Log("退出游戏按钮被点击");
    }

    public void ConfigBtnClicked()
    {
        Debug.Log("配置按钮被点击");
    }

    #endregion
}
