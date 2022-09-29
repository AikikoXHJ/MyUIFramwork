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

        /// <summary>
        /// 创建Slider组件
        /// </summary>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public static GameObject CreateSlider(DefaultControls.Resources _resources)
        {
            //创建Slider所需各层
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

            //设置Fill Area 进度条父节点
            RectTransform _fillAreaRect = _fillArea.GetComponent<RectTransform>();
            _fillAreaRect.anchorMin = new Vector2(0, 0.25f);
            _fillAreaRect.anchorMax = new Vector2(1, 0.75f);
            _fillAreaRect.anchoredPosition = new Vector2(-5, 0);
            _fillAreaRect.sizeDelta = new Vector2(-20, 0);

            //设置Fill 进度条
            Image _fillImage = _fill.AddComponent<ImageAikiko>();
            _fillImage.sprite = _resources.standard;
            _fillImage.type = Image.Type.Sliced;
            _fillImage.color = s_DefaultSelectableColor;
            RectTransform _fillRect = _fill.GetComponent<RectTransform>();
            _fillRect.sizeDelta = new Vector2(10, 0);

            //设置Handle Area 拖动按钮父节点
            RectTransform _handleAreaRect = _handleArea.GetComponent<RectTransform>();
            _handleAreaRect.sizeDelta = new Vector2(-20, 0);
            _handleAreaRect.anchorMin = new Vector2(0, 0);
            _handleAreaRect.anchorMax = new Vector2(1, 1);

            //设置Handle 拖动按钮
            Image _handleImage = _handle.AddComponent<ImageAikiko>();
            _handleImage.sprite = _resources.knob;
            _handleImage.color = s_DefaultSelectableColor;
            RectTransform _handleRect = _handle.GetComponent<RectTransform>();
            _handleRect.sizeDelta = new Vector2(20, 0);

            //设置slider组件
            Slider _slider = _sliderRoot.AddComponent<SliderAikiko>();
            _slider.fillRect = _fill.GetComponent<RectTransform>();
            _slider.handleRect = _handle.GetComponent<RectTransform>();
            _slider.targetGraphic = _handleImage;
            _slider.direction = Slider.Direction.LeftToRight;
            SetDefaultColorTransitionValues(_slider);

            return _sliderRoot;
        }

        /// <summary>
        /// 创建Scrollbar组件
        /// </summary>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public static GameObject CreateScrollbar(DefaultControls.Resources _resources)
        {
            //创建Scrollbar所需层级
            GameObject _scrollbarRoot = CreateUIElementRoot("Scrollbar", s_ThinElementSize);

            GameObject _sliderArea = CreateUIObject("Sliding Area", _scrollbarRoot);
            GameObject _handle = CreateUIObject("Handle", _sliderArea);

            //设置背景
            Image _backgroundImage = _scrollbarRoot.AddComponent<ImageAikiko>();
            _backgroundImage.sprite = _resources.background;
            _backgroundImage.type = Image.Type.Sliced;
            _backgroundImage.color = s_DefaultSelectableColor;

            //设置Handle Area 活动按钮父节点
            RectTransform _sliderAreaRect = _sliderArea.GetComponent<RectTransform>();
            _sliderAreaRect.sizeDelta = new Vector2(-20, -20);
            _sliderAreaRect.anchorMin = Vector2.zero;
            _sliderAreaRect.anchorMax = Vector2.one;

            //设置Handle 滑动按钮
            Image _handleImage = _handle.AddComponent<ImageAikiko>();
            _handleImage.sprite = _resources.standard;
            _handleImage.type = Image.Type.Sliced;
            _handleImage.color = s_DefaultSelectableColor;
            RectTransform _handleRect = _handle.GetComponent<RectTransform>();
            _handleRect.sizeDelta = new Vector2(20, 20);

            //设置Scrollbar组件
            Scrollbar _scrollbar = _scrollbarRoot.AddComponent<ScrollbarAikiko>();
            _scrollbar.handleRect = _handleRect;
            _scrollbar.targetGraphic = _handleImage;
            SetDefaultColorTransitionValues(_scrollbar);

            return _scrollbarRoot;
        }

        /// <summary>
        /// 创建Toggle组件
        /// </summary>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public static GameObject CreateToggle(DefaultControls.Resources _resources)
        {
            //创建Toggle所需层级
            GameObject _toggleRoot = CreateUIElementRoot("Toggle", s_ThinElementSize);

            GameObject _background = CreateUIObject("Background", _toggleRoot);
            GameObject _checkmark = CreateUIObject("Checkmark", _toggleRoot);
            GameObject _childLabel = CreateUIObject("Label", _toggleRoot);

            //设置勾选框背景图
            Image _backgroundImage = _background.AddComponent<ImageAikiko>();
            _backgroundImage.sprite = _resources.standard;
            _backgroundImage.type = Image.Type.Sliced;
            _backgroundImage.color = s_DefaultSelectableColor;
            RectTransform _backgroundRect = _background.GetComponent<RectTransform>();
            _backgroundRect.anchorMin = new Vector2(0f, 1f);
            _backgroundRect.anchorMax = new Vector2(0f, 1f);
            _backgroundRect.anchoredPosition = new Vector2(0, -10);
            _backgroundRect.sizeDelta = new Vector2(kThickHeight, kThinHeight);

            //设置勾选图片
            Image _checkmarkImage = _checkmark.AddComponent<ImageAikiko>();
            _checkmarkImage.sprite = _resources.checkmark;
            RectTransform _checkmarkRect = _checkmark.GetComponent<RectTransform>();
            _checkmarkRect.anchorMin = new Vector2(0.5f, 0.5f);
            _checkmarkRect.anchorMax = new Vector2(0.5f, 0.5f);
            _checkmarkRect.anchoredPosition = Vector2.zero;
            _checkmarkRect.sizeDelta = new Vector2(20f, 20f);

            //设置Text组件
            Text _text = _childLabel.AddComponent<TextAikiko>();
            _text.text = "Toggle";
            SetDefaultTextValues(_text);
            _text.raycastTarget = false;
            RectTransform _labelRect = _childLabel.GetComponent<RectTransform>();
            _labelRect.anchorMin = new Vector2(0f, 0f);
            _labelRect.anchorMax = new Vector2(1f, 1f);
            _labelRect.offsetMin = new Vector2(23f, 1f);
            _labelRect.offsetMax = new Vector2(-5f, -2f);

            //设置Toggle组件
            Toggle _toggle = _toggleRoot.AddComponent<ToggleAikiko>();
            _toggle.isOn = true;
            _toggle.graphic = _checkmarkImage;
            _toggle.targetGraphic = _backgroundImage;
            SetDefaultColorTransitionValues(_toggle);

            return _toggleRoot;
        }

        /// <summary>
        /// 创建InputField组件
        /// </summary>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public static GameObject CreateInputField(DefaultControls.Resources _resources)
        {
            //创建InputField所需层级
            GameObject _inputFieldRoot = CreateUIElementRoot("InputField", s_ThickElementSize);

            GameObject _childPlaceholder = CreateUIObject("Placeholder", _inputFieldRoot);
            GameObject _childText = CreateUIObject("Text", _inputFieldRoot);

            //设置Text组件
            Text _text = _childText.AddComponent<TextAikiko>();
            _text.text = "";
            _text.supportRichText = false;
            SetDefaultTextValues(_text);
            _text.raycastTarget = false;
            RectTransform _textRect = _childText.GetComponent<RectTransform>();
            _textRect.anchorMin = Vector2.zero;
            _textRect.anchorMax = Vector2.one;
            _textRect.sizeDelta = Vector2.zero;
            _textRect.offsetMin = new Vector2(10, 6);
            _textRect.offsetMax = new Vector2(-10, -7);

            //设置Placeholder组件
            Text _placeholder = _childPlaceholder.AddComponent<TextAikiko>();
            _placeholder.text = "Enter text...";
            _placeholder.fontStyle = FontStyle.Italic; //设置斜体
            //将placeholder设置为半透明
            Color _placeholderColor = _text.color;
            _placeholderColor.a *= 0.5f;
            _placeholder.color = _placeholderColor;
            _placeholder.raycastTarget = false;
            RectTransform _placeholderRect = _childPlaceholder.GetComponent<RectTransform>();
            _placeholderRect.anchorMin = Vector2.zero;
            _placeholderRect.anchorMax = Vector2.one;
            _placeholderRect.sizeDelta = Vector2.zero;
            _placeholderRect.offsetMin = new Vector2(10, 6);
            _placeholderRect.offsetMax = new Vector2(-10, -7);

            //设置InputField组件
            Image _backgroundImage = _inputFieldRoot.AddComponent<ImageAikiko>();
            _backgroundImage.sprite = _resources.inputField;
            _backgroundImage.type = Image.Type.Sliced;
            _backgroundImage.color = s_DefaultSelectableColor;

            InputField _inputField = _inputFieldRoot.AddComponent<InputFieldAiko>();
            SetDefaultColorTransitionValues(_inputField);
            _inputField.textComponent = _text;
            _inputField.placeholder = _placeholder;

            return _inputFieldRoot;
        }

        /// <summary>
        /// 创建Dropdown组件
        /// </summary>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public static GameObject CreateDropdown(DefaultControls.Resources _resources)
        {
            //创建Dropdown所需层级
            GameObject _dropdownRoot = CreateUIElementRoot("Dropdown", s_ThickElementSize);

            GameObject _label = CreateUIObject("Label", _dropdownRoot); //标题
            GameObject _arrow = CreateUIObject("Arrow", _dropdownRoot); //下拉框图标
            GameObject _template = CreateUIObject("Template", _dropdownRoot);
            GameObject _viewport = CreateUIObject("Viewport", _template);
            GameObject _content = CreateUIObject("Content", _viewport);
            GameObject _item = CreateUIObject("Item", _content);
            GameObject _itemBackground = CreateUIObject("Item Background", _item);
            GameObject _itemCheckmark = CreateUIObject("Item Checkmark", _item);
            GameObject _itemLabel = CreateUIObject("Item Label", _item);

            //子控件
            GameObject _scrollbar = CreateScrollbar(_resources);
            _scrollbar.name = "Scrollbar";
            SetParentAndAlign(_scrollbar, _template);
            Scrollbar _scrollbarScrollbar = _scrollbar.GetComponent<ScrollbarAikiko>();
            _scrollbarScrollbar.SetDirection(Scrollbar.Direction.BottomToTop, true);
            RectTransform _scrollbarRect = _scrollbar.GetComponent<RectTransform>();
            _scrollbarRect.anchorMin = Vector2.right;
            _scrollbarRect.anchorMax = Vector2.one;
            _scrollbarRect.pivot = Vector2.one;
            _scrollbarRect.sizeDelta = new Vector2(_scrollbarRect.sizeDelta.x, 0);

            //设置Item
            Text _itemLabelText = _itemLabel.AddComponent<TextAikiko>();
            SetDefaultTextValues(_itemLabelText);
            _itemLabelText.alignment = TextAnchor.MiddleLeft;
            _itemLabelText.raycastTarget = false;

            Image _itemBackgroundImage = _itemBackground.AddComponent<ImageAikiko>();
            _itemBackgroundImage.color = new Color32(245, 245, 245, 255);

            Image _itemCheckmarkImage = _itemCheckmark.AddComponent<ImageAikiko>();
            _itemCheckmarkImage.sprite = _resources.checkmark;

            //设置Item里的勾选框Toggle组件
            Toggle _itemToggle = _item.AddComponent<ToggleAikiko>();
            _itemToggle.targetGraphic = _itemBackgroundImage;
            _itemToggle.graphic = _itemCheckmarkImage;
            _itemToggle.isOn = true;

            //设置Template
            Image _templateImage = _template.AddComponent<ImageAikiko>();
            _templateImage.sprite = _resources.standard;
            _templateImage.type = Image.Type.Sliced;

            ScrollRect _templateScrollRect = _template.AddComponent<ScrollRectAikiko>();
            _templateScrollRect.content = (RectTransform)_content.transform;
            _templateScrollRect.viewport = (RectTransform)_viewport.transform;
            _templateScrollRect.horizontal = false;
            _templateScrollRect.movementType = ScrollRect.MovementType.Clamped;
            _templateScrollRect.verticalScrollbar = _scrollbarScrollbar;
            _templateScrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            _templateScrollRect.verticalScrollbarSpacing = -3;

            Mask _scrollRectMask = _viewport.AddComponent<MaskAikiko>();
            _scrollRectMask.showMaskGraphic = false;

            Image _viewportImage = _viewport.AddComponent<ImageAikiko>();
            _viewportImage.sprite = _resources.mask;
            _viewportImage.type = Image.Type.Sliced;

        }

        #endregion



    }
}
