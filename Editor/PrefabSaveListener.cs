using System;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Client.Library
{
    /// <summary>
    /// Prefab保存监听器
    /// </summary>
    public class PrefabSaveListener
    {


        [InitializeOnLoadMethod]
        public static void Test()
        {
            //预制体预览场景打开的时候执行
            PrefabStage.prefabStageOpened += delegate (PrefabStage _prefabStage)
            {
                Debug.Log("Is prefabStageOpened ！！");
            };

            //预制体内容修改的时候执行
            PrefabStage.prefabStageDirtied += delegate (PrefabStage _prefabStage)
            {
                Debug.Log("Is prefabStageDirtied ！！");
            };

            //预制体预览场景关闭的时候执行
            PrefabStage.prefabStageClosing += delegate (PrefabStage _prefabStage)
            {
                Debug.Log("Is prefabStageClosing ！！");
            };

            //将GameObject变为预制体的时候执行
            PrefabUtility.prefabInstanceUpdated = delegate (GameObject _instance)
            {
                Debug.Log("Is prefabInstanceUpdated ！！");
            };
        }
    }
}
