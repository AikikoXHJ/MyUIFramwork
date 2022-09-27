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

        #region 辅助方法

        /// <summary>
        /// 通过递归给GameObject设置Layer
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
        /// 设置父物体
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
        /// 创建UI元素根节点
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
        /// 创建UI预制体
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
        /// 给Text组件设置默认值
        /// </summary>
        /// <param name="_lbl"></param>
        private static void SetDefaultTextValues(Text _lbl)
        {
            _lbl.name = "AikikoTxt";
            _lbl.color = s_TextColor;
        }

        /// <summary>
        /// 设置默认颜色块
        /// </summary>
        /// <param name="_slider"></param>
        private static void SetDefaultColorTransitionValues(Selectable _slider)
        {
            ColorBlock _colors = _slider.colors;
            //高亮时颜色(光标进入范围时)
            _colors.highlightedColor = new Color(0.882f, 0.882f, 0.882f);
            //按下时颜色
            _colors.pressedColor = new Color(0.698f, 0.698f, 0.698f);
            //禁用时颜色
            _colors.disabledColor = new Color(0.521f, 0.521f, 0.521f);
        }

        #endregion

        #region 实际方法

        /// <summary>
        /// 创建Panel组件
        /// </summary>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public static GameObject CreatePanel(DefaultControls.Resources _resources)
        {
            GameObject _panelRoot = CreateUIElementRoot("Panel", s_ThickElementSize);

            //将 RectTransform 设置为拉伸模式
            RectTransform _rectTransform = _panelRoot.GetComponent<RectTransform>();
            _rectTransform.anchorMin = Vector2.zero;
            _rectTransform.anchorMax = Vector2.one;
            _rectTransform.anchoredPosition = Vector2.zero;
            _rectTransform.sizeDelta = Vector2.zero;

            //添加Panel组件脚本
            _panelRoot.AddComponent<PanelAikiko>();

            //添加背景蒙版
            Image _image = _panelRoot.AddComponent<ImageAikiko>();
            _image.sprite = _resources.background;
            _image.type = Image.Type.Sliced;
            _image.color = s_PanelColor;
            _image.raycastTarget = false;

            return _panelRoot;
        }

        /// <summary>
        /// 创建Button组件
        /// </summary>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public static GameObject CreateButton(DefaultControls.Resources _resources)
        {
            //创建Button根节点
            GameObject _buttonRoot = CreateUIElementRoot("Button", s_ThickElementSize);

            //创建子物体Text组件
            GameObject _childText = new GameObject("Text");
            SetParentAndAlign(_childText, _buttonRoot);

            //设置Button上的Image组件
            Image _image = _buttonRoot.AddComponent<ImageAikiko>();
            _image.sprite = _resources.standard;
            _image.type = Image.Type.Sliced;
            _image.color = s_DefaultSelectableColor;

            Button _button = _buttonRoot.AddComponent<ButtonAikiko>();
            SetDefaultColorTransitionValues(_button);

            //设置子物体Text上的Text组件
            Text _text = _childText.AddComponent<TextAikiko>();
            _text.text = "ButtonAikiko";
            _text.alignment = TextAnchor.MiddleCenter;
            SetDefaultTextValues(_text);
            _text.raycastTarget = false;

            //设置根节点
            RectTransform _rectTransform = _childText.GetComponent<RectTransform>();
            _rectTransform.anchorMin = Vector2.zero;
            _rectTransform.anchorMax = Vector2.one;
            _rectTransform.sizeDelta = Vector2.zero;

            return _buttonRoot;
        }

        /// <summary>
        /// 创建Text组件
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
        /// 创建Image组件
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
        /// 创建RawImage组件
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
            // 创建Slider所需各层
            GameObject _sliderRoot = CreateUIElementRoot("Slider", s_ThinElementSize);

            GameObject _background = CreateUIObject("Background", _sliderRoot);
            GameObject _fillArea = CreateUIObject("Fill Area", _sliderRoot);
            GameObject _fill = CreateUIObject("Fill", _fillArea);
            GameObject _handleArea = CreateUIObject("Handle Slide Area", _sliderRoot);
            GameObject _handle = CreateUIObject("Handle", _handleArea);

            //设置background
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
