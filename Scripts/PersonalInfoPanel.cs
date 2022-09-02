using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Client.UI;

public class PersonalInfoPanel : UIBase
{
    private static string name = "PersonalInfoPanel";
    private static string path = "PersonalInfoPanel";
    public static readonly UIInfo uiInfo = new UIInfo(path, name);

    public PersonalInfoPanel() : base(uiInfo){}
    public override void OnStart()
    {
        base.OnStart();
        UIFunction.GetInstance().GetSingleComponentInChild<Button>(activeObj, "ExitButton").onClick.AddListener(OnExitBtnClicked);

        var _selfDrag = UIFunction.GetInstance().GetSingleComponentInChild<DragItemContainer>(activeObj, activeObj.name);
        _selfDrag.DragEnd += new NoneParamDelegate(DragEndTest);
    }

    public void DragEndTest()
    {
        Debug.Log("这里是测试拖拽结束！");
    }

    public void OnExitBtnClicked()
    {
        UIManager.GetInstance().Pop(false, "PersonalInfo");
    }
}
