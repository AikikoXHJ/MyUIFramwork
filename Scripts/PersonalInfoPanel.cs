using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    public void OnExitBtnClicked()
    {
        UIManager.GetInstance().PopUIBase(false, "PersonalInfo");
    }
}
