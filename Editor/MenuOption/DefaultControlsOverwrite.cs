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

        /// <summary>
        /// ����Slider���
        /// </summary>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public static GameObject CreateSlider(DefaultControls.Resources _resources)
        {
            //����Slider�������
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

            //����Fill Area ���������ڵ�
            RectTransform _fillAreaRect = _fillArea.GetComponent<RectTransform>();
            _fillAreaRect.anchorMin = new Vector2(0, 0.25f);
            _fillAreaRect.anchorMax = new Vector2(1, 0.75f);
            _fillAreaRect.anchoredPosition = new Vector2(-5, 0);
            _fillAreaRect.sizeDelta = new Vector2(-20, 0);

            //����Fill ������
            Image _fillImage = _fill.AddComponent<ImageAikiko>();
            _fillImage.sprite = _resources.standard;
            _fillImage.type = Image.Type.Sliced;
            _fillImage.color = s_DefaultSelectableColor;
            RectTransform _fillRect = _fill.GetComponent<RectTransform>();
            _fillRect.sizeDelta = new Vector2(10, 0);

            //����Handle Area �϶���ť���ڵ�
            RectTransform _handleAreaRect = _handleArea.GetComponent<RectTransform>();
            _handleAreaRect.sizeDelta = new Vector2(-20, 0);
            _handleAreaRect.anchorMin = new Vector2(0, 0);
            _handleAreaRect.anchorMax = new Vector2(1, 1);

            //����Handle �϶���ť
            Image _handleImage = _handle.AddComponent<ImageAikiko>();
            _handleImage.sprite = _resources.knob;
            _handleImage.color = s_DefaultSelectableColor;
            RectTransform _handleRect = _handle.GetComponent<RectTransform>();
            _handleRect.sizeDelta = new Vector2(20, 0);

            //����slider���
            Slider _slider = _sliderRoot.AddComponent<SliderAikiko>();
            _slider.fillRect = _fill.GetComponent<RectTransform>();
            _slider.handleRect = _handle.GetComponent<RectTransform>();
            _slider.targetGraphic = _handleImage;
            _slider.direction = Slider.Direction.LeftToRight;
            SetDefaultColorTransitionValues(_slider);

            return _sliderRoot;
        }

        /// <summary>
        /// ����Scrollbar���
        /// </summary>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public static GameObject CreateScrollbar(DefaultControls.Resources _resources)
        {
            //����Scrollbar����㼶
            GameObject _scrollbarRoot = CreateUIElementRoot("Scrollbar", s_ThinElementSize);

            GameObject _sliderArea = CreateUIObject("Sliding Area", _scrollbarRoot);
            GameObject _handle = CreateUIObject("Handle", _sliderArea);

            //���ñ���
            Image _backgroundImage = _scrollbarRoot.AddComponent<ImageAikiko>();
            _backgroundImage.sprite = _resources.background;
            _backgroundImage.type = Image.Type.Sliced;
            _backgroundImage.color = s_DefaultSelectableColor;

            //����Handle Area ���ť���ڵ�
            RectTransform _sliderAreaRect = _sliderArea.GetComponent<RectTransform>();
            _sliderAreaRect.sizeDelta = new Vector2(-20, -20);
            _sliderAreaRect.anchorMin = Vector2.zero;
            _sliderAreaRect.anchorMax = Vector2.one;

            //����Handle ������ť
            Image _handleImage = _handle.AddComponent<ImageAikiko>();
            _handleImage.sprite = _resources.standard;
            _handleImage.type = Image.Type.Sliced;
            _handleImage.color = s_DefaultSelectableColor;
            RectTransform _handleRect = _handle.GetComponent<RectTransform>();
            _handleRect.sizeDelta = new Vector2(20, 20);

            //����Scrollbar���
            Scrollbar _scrollbar = _scrollbarRoot.AddComponent<ScrollbarAikiko>();
            _scrollbar.handleRect = _handleRect;
            _scrollbar.targetGraphic = _handleImage;
            SetDefaultColorTransitionValues(_scrollbar);

            return _scrollbarRoot;
        }

        /// <summary>
        /// ����Toggle���
        /// </summary>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public static GameObject CreateToggle(DefaultControls.Resources _resources)
        {
            //����Toggle����㼶
            GameObject _toggleRoot = CreateUIElementRoot("Toggle", s_ThinElementSize);

            GameObject _background = CreateUIObject("Background", _toggleRoot);
            GameObject _checkmark = CreateUIObject("Checkmark", _toggleRoot);
            GameObject _childLabel = CreateUIObject("Label", _toggleRoot);

            //���ù�ѡ�򱳾�ͼ
            Image _backgroundImage = _background.AddComponent<ImageAikiko>();
            _backgroundImage.sprite = _resources.standard;
            _backgroundImage.type = Image.Type.Sliced;
            _backgroundImage.color = s_DefaultSelectableColor;
            RectTransform _backgroundRect = _background.GetComponent<RectTransform>();
            _backgroundRect.anchorMin = new Vector2(0f, 1f);
            _backgroundRect.anchorMax = new Vector2(0f, 1f);
            _backgroundRect.anchoredPosition = new Vector2(0, -10);
            _backgroundRect.sizeDelta = new Vector2(kThickHeight, kThinHeight);

            //���ù�ѡͼƬ
            Image _checkmarkImage = _checkmark.AddComponent<ImageAikiko>();
            _checkmarkImage.sprite = _resources.checkmark;
            RectTransform _checkmarkRect = _checkmark.GetComponent<RectTransform>();
            _checkmarkRect.anchorMin = new Vector2(0.5f, 0.5f);
            _checkmarkRect.anchorMax = new Vector2(0.5f, 0.5f);
            _checkmarkRect.anchoredPosition = Vector2.zero;
            _checkmarkRect.sizeDelta = new Vector2(20f, 20f);

            //����Text���
            Text _text = _childLabel.AddComponent<TextAikiko>();
            _text.text = "Toggle";
            SetDefaultTextValues(_text);
            _text.raycastTarget = false;
            RectTransform _labelRect = _childLabel.GetComponent<RectTransform>();
            _labelRect.anchorMin = new Vector2(0f, 0f);
            _labelRect.anchorMax = new Vector2(1f, 1f);
            _labelRect.offsetMin = new Vector2(23f, 1f);
            _labelRect.offsetMax = new Vector2(-5f, -2f);

            //����Toggle���
            Toggle _toggle = _toggleRoot.AddComponent<ToggleAikiko>();
            _toggle.isOn = true;
            _toggle.graphic = _checkmarkImage;
            _toggle.targetGraphic = _backgroundImage;
            SetDefaultColorTransitionValues(_toggle);

            return _toggleRoot;
        }

        /// <summary>
        /// ����InputField���
        /// </summary>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public static GameObject CreateInputField(DefaultControls.Resources _resources)
        {
            //����InputField����㼶
            GameObject _inputFieldRoot = CreateUIElementRoot("InputField", s_ThickElementSize);

            GameObject _childPlaceholder = CreateUIObject("Placeholder", _inputFieldRoot);
            GameObject _childText = CreateUIObject("Text", _inputFieldRoot);

            //����Text���
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

            //����Placeholder���
            Text _placeholder = _childPlaceholder.AddComponent<TextAikiko>();
            _placeholder.text = "Enter text...";
            _placeholder.fontStyle = FontStyle.Italic; //����б��
            //��placeholder����Ϊ��͸��
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

            //����InputField���
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
        /// ����Dropdown���
        /// </summary>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public static GameObject CreateDropdown(DefaultControls.Resources _resources)
        {
            //����Dropdown����㼶
            GameObject _dropdownRoot = CreateUIElementRoot("Dropdown", s_ThickElementSize);

            GameObject _label = CreateUIObject("Label", _dropdownRoot); //����
            GameObject _arrow = CreateUIObject("Arrow", _dropdownRoot); //������ͼ��
            GameObject _template = CreateUIObject("Template", _dropdownRoot);
            GameObject _viewport = CreateUIObject("Viewport", _template);
            GameObject _content = CreateUIObject("Content", _viewport);
            GameObject _item = CreateUIObject("Item", _content);
            GameObject _itemBackground = CreateUIObject("Item Background", _item);
            GameObject _itemCheckmark = CreateUIObject("Item Checkmark", _item);
            GameObject _itemLabel = CreateUIObject("Item Label", _item);

            //�ӿؼ�
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

            //����Item
            Text _itemLabelText = _itemLabel.AddComponent<TextAikiko>();
            SetDefaultTextValues(_itemLabelText);
            _itemLabelText.alignment = TextAnchor.MiddleLeft;
            _itemLabelText.raycastTarget = false;

            Image _itemBackgroundImage = _itemBackground.AddComponent<ImageAikiko>();
            _itemBackgroundImage.color = new Color32(245, 245, 245, 255);

            Image _itemCheckmarkImage = _itemCheckmark.AddComponent<ImageAikiko>();
            _itemCheckmarkImage.sprite = _resources.checkmark;

            //����Item��Ĺ�ѡ��Toggle���
            Toggle _itemToggle = _item.AddComponent<ToggleAikiko>();
            _itemToggle.targetGraphic = _itemBackgroundImage;
            _itemToggle.graphic = _itemCheckmarkImage;
            _itemToggle.isOn = true;

            //����Template
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
