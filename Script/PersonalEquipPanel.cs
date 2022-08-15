using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalEquipPanel : UIBase
{
    private static string name = "PersonalEquipPanel";
    private static string path = "PersonalEquipPanel";
    public static readonly UIInfo uiInfo = new UIInfo(path, name);

    public PersonalEquipPanel() : base(uiInfo){}

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
