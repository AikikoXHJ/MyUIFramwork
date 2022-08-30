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
        private static bool m_prefabModifyLock = false;

        [InitializeOnLoadMethod]
        public static void StartInitializeOnLoadMethod()
        {
            //预制体预览场景打开的时候执行
            PrefabStage.prefabStageOpened += delegate (PrefabStage _prefabStage)
            {
                Debug.Log($"预制体{_prefabStage.name}的预览场景被打开了 ！！");
            };

            //预制体内容修改的时候执行
            PrefabStage.prefabStageDirtied += delegate (PrefabStage _prefabStage)
            {
                Debug.Log($"预制体{_prefabStage.name}的预览场景被修改了 ！！");
            };

            //预制体预览场景关闭的时候执行
            PrefabStage.prefabStageClosing += delegate (PrefabStage _prefabStage)
            {
                Debug.Log($"预制体{_prefabStage.name}的预览场景被关闭 ！！");

                var _gameObject = _prefabStage.prefabContentsRoot;

                //判断是否是UI预制体
                var _prefabPath = _prefabStage.assetPath;

                if (true) //判断预制体的地址_prefabPath是否放在了规定文件夹内
                {
                    var _container = _gameObject.GetComponent<ContainerComponent>();

                    var _modified = false;

                    if (_container == null)
                    {
                        _container = _gameObject.AddComponent<ContainerComponent>();

                        _container.FillComponents();

                        _modified = true;
                    }
                    else if (_container.FillComponents())
                    {
                        _modified = true;
                    }

                    //添加对应组件的相关脚本

                    if (_modified)
                    {
                        //保存预制体
                        PrefabUtility.SaveAsPrefabAsset(_gameObject, _prefabPath);
                    }

                }

            };

            //将GameObject变为预制体的时候执行
            PrefabUtility.prefabInstanceUpdated = delegate (GameObject _instance)
            {
                Debug.Log($"预制体{_instance.name}发生了改变 ！！");

                if (m_prefabModifyLock)
                {
                    return;
                }

                m_prefabModifyLock = true;

                //判断是否是UI预制体
                var _prefabPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(_instance);

                if (true)//判断预制体的地址_prefabPath是否放在了规定文件夹内
                {
                    var _container = _instance.GetComponent<ContainerComponent>();

                    if (_container == null)
                    {
                        _container = _instance.AddComponent<ContainerComponent>();

                        _container.FillComponents();

                        PrefabUtility.ApplyPrefabInstance(_instance, InteractionMode.AutomatedAction);
                    }
                    else
                    {
                        //更新节点
                        var _root = PrefabUtility.LoadPrefabContents(_prefabPath);

                        _container = _root.GetComponent<ContainerComponent>();

                        if (_container.FillComponents())
                        {
                            PrefabUtility.SaveAsPrefabAsset(_root, _prefabPath);
                        }

                        PrefabUtility.UnloadPrefabContents(_root);
                    }

                }

            };
        }
    }
}
