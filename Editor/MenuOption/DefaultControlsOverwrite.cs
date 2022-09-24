using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UI
{
    public class DefaultControlsOverwrite
    {
        private const float kWidth = 160f;
        private const float kThickHeight = 30f;
        private const float kThinHeight = 20f;
        private static Vector2 s_ThickElementSize = new Vector2(kWidth, kThickHeight);
        private static Vector2 s_ThinElementSize = new Vector2(kWidth, kThinHeight);
        private static Vector2 s_ImageElementSize = new Vector2(100f, 100f);
        private static Color s_DefaultSelectableColor = new Color(1f, 1f, 1f, 1f);
        private static Color s_PanelColor = new Color(1f, 1f, 1f, 0.392f);
        private static Color s_TextColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);

        #region ��������

        /// <summary>
        /// ͨ���ݹ��GameObject����Layer
        /// </summary>
        /// <param name="_obj"></param>
        /// <param name="_layer"></param>
        private static void SetLayerRecursively(GameObject _obj, int _layer)
        {
            _obj.layer = _layer;
            Transform _t = _obj.transform;
            for (int i = 0; i < _t.childCount; i++)
            {
                SetLayerRecursively(_t.GetChild(i).gameObject, _layer);
            }
        }

        /// <summary>
        /// ���ø�����
        /// </summary>
        /// <param name="_child"></param>
        /// <param name="_parent"></param>
        private static void SetParentAndAlign(GameObject _child, GameObject _parent)
        {
            if (_parent == null)
            {
                return;
            }

            _child.transform.SetParent(_parent.transform, false);
            SetLayerRecursively(_child, _parent.layer);
        }

        /// <summary>
        /// ����UIԪ�ظ��ڵ�
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_size"></param>
        /// <returns></returns>
        private static GameObject CreateUIElementRoot(string _name, Vector2 _size)
        {
            GameObject _child = new GameObject(_name);
            RectTransform _rectTransform = _child.AddComponent<RectTransform>();
            _rectTransform.sizeDelta = _size;
            return _child;
        }

        /// <summary>
        /// ����UIԤ����
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_parent"></param>
        /// <returns></returns>
        private GameObject CreateUIObject(string _name, GameObject _parent)
        {
            GameObject _obj = new GameObject(_name);
            _obj.AddComponent<RectTransform>();
            SetParentAndAlign(_obj, _parent);
            return _obj;
        }

        /// <summary>
        /// ��Text�������Ĭ��ֵ
        /// </summary>
        /// <param name="_lbl"></param>
        private static void SetDefaultTextValues(Text _lbl)
        {
            _lbl.name = "AikikoTxt";
            _lbl.text = "AikikoTxtComponent";
            _lbl.color = s_TextColor;
        }

        /// <summary>
        /// ����Ĭ����ɫ��
        /// </summary>
        /// <param name="_slider"></param>
        private static void SetDefaultColorTransitionValues(Selectable _slider)
        {
            ColorBlock _colors = _slider.colors;
            //����ʱ��ɫ(�����뷶Χʱ)
            _colors.highlightedColor = new Color(0.882f, 0.882f, 0.882f);
            //����ʱ��ɫ
            _colors.pressedColor = new Color(0.698f, 0.698f, 0.698f);
            //����ʱ��ɫ
            _colors.disabledColor = new Color(0.521f, 0.521f, 0.521f);
        }

        #endregion

        #region ʵ�ʷ���

        /// <summary>
        /// ����Panel���
        /// </summary>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public static GameObject CreatePanel(DefaultControls.Resources _resources)
        {
            GameObject _panelRoot = CreateUIElementRoot("Panel", s_ThickElementSize);

            //�� RectTransform ����Ϊ����ģʽ
            RectTransform _rectTransform = _panelRoot.GetComponent<RectTransform>();
            _rectTransform.anchorMin = Vector2.zero;
            _rectTransform.anchorMax = Vector2.one;
            _rectTransform.anchoredPosition = Vector2.zero;
            _rectTransform.sizeDelta = Vector2.zero;

            //���Panel����ű�
            _panelRoot.AddComponent<PanelAikiko>();

            //��ӱ����ɰ�
            Image _image = _panelRoot.AddComponent<ImageAikiko>();
            _image.sprite = _resources.background;
            _image.type = Image.Type.Sliced;
            _image.color = s_PanelColor;
            _image.raycastTarget = false;

            return _panelRoot;
        }

        #endregion



    }
}
