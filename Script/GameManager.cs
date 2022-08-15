using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager uiManager;
    public UIManager uiManagerRoot { get => uiManager; }

    /// <summary>
    /// 游戏管理类，单例，全局唯一
    /// </summary>
    private static GameManager instance;

    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("获取GameManager实例失败");
            return instance;
        }
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        uiManager = new UIManager();
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        uiManagerRoot.uiCanvasObj = UIFunction.GetInstance().FindCanvas();

        //加载主界面
        uiManagerRoot.PushUIBase(new RootPanel(), "Main");

    }
}
