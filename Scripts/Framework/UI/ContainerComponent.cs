using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

namespace Client.Library
{
    /// <summary>
    /// ���������
    /// </summary>
    public class ContainerComponent : MonoBehaviour
    {
        [Header("Button��ť")]
        [SerializeField]
        private Button[] m_buttons;

        [Header("ImageͼƬ")]
        [SerializeField]
        [Space(10)]
        private Image[] m_images;

        [Header("RawImageͼƬ")]
        [SerializeField]
        [Space(10)]
        private RawImage[] m_rawImages;

        [Header("Text�ı�")]
        [SerializeField]
        [Space(10)]
        private Text[] m_texts;

        [Header("Slider������")]
        [SerializeField]
        [Space(10)]
        private Slider[] m_sliders;

        [Header("Input�����")]
        [SerializeField]
        [Space(10)]
        private InputField[] m_inputFields;

        public Button[] Buttons => m_buttons;

        public Image[] Images => m_images;

        public RawImage[] RawImages => m_rawImages;

        public Text[] Texts => m_texts;

        public Slider[] Sliders => m_sliders;

        public InputField[] InputFields => m_inputFields;

#if UNITY_EDITOR
        /// <summary>
        /// �ж���ͬ��������Ƿ���ͬ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_tar"></param>
        /// <param name="_src"></param>
        /// <returns></returns>
        private bool CheckContinerEqual<T>(T[] _tar, T[] _src) where T : Component
        {
            if (_tar != _src)
            {
                return false;
            }

            if (_tar != null && _src != null)
            {
                return _tar.SequenceEqual(_src);
            }

            return true;
        }
#endif


    }
}
