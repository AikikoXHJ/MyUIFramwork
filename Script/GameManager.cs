using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager uiManager;
    public UIManager uiManagerRoot { get => uiManager; }

    /// <summary>
    /// ��Ϸ�����࣬������ȫ��Ψһ
    /// </summary>
    private static GameManager instance;

    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("��ȡGameManagerʵ��ʧ��");
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

        //����������
        uiManagerRoot.PushUIBase(new RootPanel(), "Main");

    }
}
