using System;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Client.Library
{
    /// <summary>
    /// Prefab���������
    /// </summary>
    public class PrefabSaveListener
    {


        [InitializeOnLoadMethod]
        public static void Test()
        {
            //Ԥ����Ԥ�������򿪵�ʱ��ִ��
            PrefabStage.prefabStageOpened += delegate (PrefabStage _prefabStage)
            {
                Debug.Log("Is prefabStageOpened ����");
            };

            //Ԥ���������޸ĵ�ʱ��ִ��
            PrefabStage.prefabStageDirtied += delegate (PrefabStage _prefabStage)
            {
                Debug.Log("Is prefabStageDirtied ����");
            };

            //Ԥ����Ԥ�������رյ�ʱ��ִ��
            PrefabStage.prefabStageClosing += delegate (PrefabStage _prefabStage)
            {
                Debug.Log("Is prefabStageClosing ����");
            };

            //��GameObject��ΪԤ�����ʱ��ִ��
            PrefabUtility.prefabInstanceUpdated = delegate (GameObject _instance)
            {
                Debug.Log("Is prefabInstanceUpdated ����");
            };
        }
    }
}
