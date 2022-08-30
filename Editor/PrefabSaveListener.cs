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
        private static bool m_prefabModifyLock = false;

        [InitializeOnLoadMethod]
        public static void StartInitializeOnLoadMethod()
        {
            //Ԥ����Ԥ�������򿪵�ʱ��ִ��
            PrefabStage.prefabStageOpened += delegate (PrefabStage _prefabStage)
            {
                Debug.Log($"Ԥ����{_prefabStage.name}��Ԥ������������ ����");
            };

            //Ԥ���������޸ĵ�ʱ��ִ��
            PrefabStage.prefabStageDirtied += delegate (PrefabStage _prefabStage)
            {
                Debug.Log($"Ԥ����{_prefabStage.name}��Ԥ���������޸��� ����");
            };

            //Ԥ����Ԥ�������رյ�ʱ��ִ��
            PrefabStage.prefabStageClosing += delegate (PrefabStage _prefabStage)
            {
                Debug.Log($"Ԥ����{_prefabStage.name}��Ԥ���������ر� ����");

                var _gameObject = _prefabStage.prefabContentsRoot;

                //�ж��Ƿ���UIԤ����
                var _prefabPath = _prefabStage.assetPath;

                if (true) //�ж�Ԥ����ĵ�ַ_prefabPath�Ƿ�����˹涨�ļ�����
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

                    //��Ӷ�Ӧ�������ؽű�

                    if (_modified)
                    {
                        //����Ԥ����
                        PrefabUtility.SaveAsPrefabAsset(_gameObject, _prefabPath);
                    }

                }

            };

            //��GameObject��ΪԤ�����ʱ��ִ��
            PrefabUtility.prefabInstanceUpdated = delegate (GameObject _instance)
            {
                Debug.Log($"Ԥ����{_instance.name}�����˸ı� ����");

                if (m_prefabModifyLock)
                {
                    return;
                }

                m_prefabModifyLock = true;

                //�ж��Ƿ���UIԤ����
                var _prefabPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(_instance);

                if (true)//�ж�Ԥ����ĵ�ַ_prefabPath�Ƿ�����˹涨�ļ�����
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
                        //���½ڵ�
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
