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
        private static GameObject CreateUIObject(string _name, GameObject _parent)
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

        /// <summary>
        /// ����Button���
        /// </summary>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public static GameObject CreateButton(DefaultControls.Resources _resources)
        {
            //����Button���ڵ�
            GameObject _buttonRoot = CreateUIElementRoot("Button", s_ThickElementSize);

            //����������Text���
            GameObject _childText = new GameObject("Text");
            SetParentAndAlign(_childText, _buttonRoot);

            //����Button�ϵ�Image���
            Image _image = _buttonRoot.AddComponent<ImageAikiko>();
            _image.sprite = _resources.standard;
            _image.type = Image.Type.Sliced;
            _image.color = s_DefaultSelectableColor;

            Button _button = _buttonRoot.AddComponent<ButtonAikiko>();
            SetDefaultColorTransitionValues(_button);

            //����������Text�ϵ�Text���
            Text _text = _childText.AddComponent<TextAikiko>();
            _text.text = "ButtonAikiko";
            _text.alignment = TextAnchor.MiddleCenter;
            SetDefaultTextValues(_text);
            _text.raycastTarget = false;

            //���ø��ڵ�
            RectTransform _rectTransform = _childText.GetComponent<RectTransform>();
            _rectTransform.anchorMin = Vector2.zero;
            _rectTransform.anchorMax = Vector2.one;
            _rectTransform.sizeDelta = Vector2.zero;

            return _buttonRoot;
        }

        /// <summary>
        /// ����Text���
        /// </summary>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public static GameObject CreateText(DefaultControls.Resources _resources)
        {
            GameObject _textRoot = CreateUIElementRoot("Text", s_ThickElementSize);

            Text _text = _textRoot.AddComponent<TextAikiko>();
            _text.text = "New AikikoText";
            SetDefaultTextValues(_text);
            _text.raycastTarget = false;

            return _textRoot;
        }

        /// <summary>
        /// ����Image���
        /// </summary>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public static GameObject CreateImage(DefaultControls.Resources _resources)
        {
            GameObject _imageRoot = CreateUIElementRoot("Image", s_ImageElementSize);

            Image _imageAikiko = _imageRoot.AddComponent<ImageAikiko>();
            _imageAikiko.sprite = _resources.standard;
            _imageAikiko.type = Image.Type.Sliced;
            _imageAikiko.color = s_DefaultSelectableColor;
            _imageAikiko.raycastTarget = false;

            return _imageRoot;
        }

        /// <summary>
        /// ����RawImage���
        /// </summary>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public static GameObject CreateRawImage(DefaultControls.Resources _resources)
        {
            GameObject _rawImageRoot = CreateUIElementRoot("RawImage", s_ImageElementSize);

            _rawImageRoot.AddComponent<RawImageAikiko>();

            return _rawImageRoot;
        }

        public static GameObject CreateSlider(DefaultControls.Resources _resources)
        {
            // ����Slider�������
            GameObject _sliderRoot = CreateUIElementRoot("Slider", s_ThinElementSize);

            GameObject _background = CreateUIObject("Background", _sliderRoot);
            GameObject _fillArea = CreateUIObject("Fill Area", _sliderRoot);
            GameObject _fill = CreateUIObject("Fill", _fillArea);
            GameObject _handleArea = CreateUIObject("Handle Slide Area", _sliderRoot);
            GameObject _handle = CreateUIObject("Handle", _handleArea);

            //����background
            Image _backgroundImage = _background.AddComponent<ImageAikiko>();
            _backgroundImage.sprite = _resources.background;
            _backgroundImage.type = Image.Type.Sliced;
            _backgroundImage.color = s_DefaultSelectableColor;
            RectTransform _backgroundRect = _background.GetComponent<RectTransform>();
            _backgroundRect.anchorMin = new Vector2(0, 0.25f);
            _backgroundRect.anchorMax = new Vector2(1, 0.75f);
            _backgroundRect.sizeDelta = new Vector2(0, 0);



            return _sliderRoot;
        }

        #endregion



    }
}
