using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Client.UI
{
    /// <summary>
    /// ������ק�����
    /// </summary>
    [ExecuteAlways]
    [SelectionBase]
    [DisallowMultipleComponent]
    public class DragItemContainer : UIBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
    {

        #region Drag Delegate

        public NoneParamDelegate DragStart;
        public NoneParamDelegate DragEnd;

        [Header("��ק������")]
        public int index = 0;
        [Header("��ק���λ")]
        public int siteIndex = 0;
        [Header("�Ƿ����õ����ƫ��")]
        public bool _isOffset = true; //�Ƿ���ҪUI��������ƫ��
        [Header("��ק����")]
        public GameObject m_dragObject = null;
        private RectTransform m_dragTransform = null;

        private Vector3 _offPos; //�洢�����λ�ú�UI֮��Ĳ�ֵ

        private bool _isSelf = false; //�Ƿ��Լ���Ϊ�ƶ�Ŀ��

        #endregion

        #region Unity Events
        /// <summary>
        /// Ѱ��Ŀ������ĸ�����
        /// </summary>
        public static T FindInParents<T>(GameObject _go) where T : Component
        {
            if (_go == null) return null;

            var _component = _go.GetComponent<T>();

            if (_component != null)
            {
                return _component;
            }

            var t = _go.transform.parent;
            while (t != null && _component == null)
            {
                _component = t.gameObject.GetComponent<T>();
                t = t.parent;
            }
            return _component;
        }

        #endregion

        /// <summary>
        /// ��ק��ʼʱִ��
        /// </summary>
        public void OnBeginDrag(PointerEventData eventData)
        {
            var _canvas = FindInParents<Canvas>(gameObject);
            if (_canvas == null) return;
            if (!m_dragObject)
            {
                m_dragObject = this.gameObject;
                _isSelf = true;
            }
            else if (m_dragObject == this.gameObject)
            {
                _isSelf = true;
            }

            if (!_isSelf)
            {
                m_dragObject.SetActive(true);
                m_dragObject.transform.position = gameObject.transform.position;
            }
            m_dragObject.transform.SetAsLastSibling();

            m_dragTransform = transform as RectTransform;
            if (!m_dragObject.GetComponent<CanvasGroup>())
            {
                var _group = m_dragObject.AddComponent<CanvasGroup>();
                _group.blocksRaycasts = false;
            }
            SetDraggedPosition(eventData);
        }

        /// <summary>
        /// ��קʱִ��
        /// </summary>
        public void OnDrag(PointerEventData eventData)
        {
            if (!m_dragObject.activeSelf)
            {
                return;
            }
            SetDraggedPosition(eventData);

            PointerEventData _eventDataCurPosition = new PointerEventData(EventSystem.current);
            _eventDataCurPosition.position = eventData.pressEventCamera.WorldToScreenPoint(m_dragObject.transform.position);

            List<RaycastResult> _result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_eventDataCurPosition, _result);

            if (_result.Count <= 0)
            {
                return;
            }

            DragItemContainer _obj = null;
            for (int i = 0; i < _result.Count; ++i)
            {
                _obj = FindInParents<DragItemContainer>(_result[i].gameObject);
                if (_obj)
                { 
                    break;
                }
            }

            if (!_obj)
            {
                return;
            }
        }

        /// <summary>
        /// ��ק������ִ��
        /// </summary>
        public void OnEndDrag(PointerEventData eventData)
        {
            if (!m_dragObject.activeSelf)
            {
                return;
            }
            if (!_isSelf)
            {
                m_dragObject.SetActive(false);
            }
            DragEnd?.Invoke();
        }

        /// <summary>
        /// �������UI�ϰ��µ�ʱ�򣬻�ȡ�����UII���ĵ����λ��
        /// </summary>
        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isOffset)
            {
                Vector3 _globalMousePos;
                if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.GetComponent<RectTransform>(), eventData.position, eventData.enterEventCamera, out _globalMousePos))
                {
                    _offPos = transform.GetComponent<RectTransform>().position - _globalMousePos;
                }
            }
        }

        /// <summary>
        /// ������ק�����λ��
        /// </summary>
        /// <param name="eventData"></param>
        private void SetDraggedPosition(PointerEventData eventData)
        {
            if (eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
            {
                m_dragTransform = eventData.pointerEnter.transform as RectTransform;
            }

            var _rectTransform = m_dragObject.GetComponent<RectTransform>();
            Vector3 _globalMousePos;

            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_dragTransform, eventData.position, eventData.pressEventCamera, out _globalMousePos))
            {
                _rectTransform.position = new Vector3(_globalMousePos.x + _offPos.x, _globalMousePos.y + _offPos.y, _globalMousePos.z);
                _rectTransform.rotation = m_dragTransform.rotation;
            }

        }

        
    }
}
