using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Client.UI
{
    /// <summary>
    /// 用于拖拽的组件
    /// </summary>
    [ExecuteAlways]
    [SelectionBase]
    [DisallowMultipleComponent]
    public class DragItemContainer : UIBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
    {

        #region Drag Delegate

        public NoneParamDelegate DragStart;
        public NoneParamDelegate DragEnd;

        [Header("拖拽体索引")]
        public int index = 0;
        [Header("拖拽体地位")]
        public int siteIndex = 0;
        [Header("是否设置点击处偏差")]
        public bool _isOffset = true; //是否需要UI与点击处的偏差
        [Header("拖拽物体")]
        public GameObject m_dragObject = null;
        private RectTransform m_dragTransform = null;

        private Vector3 _offPos; //存储鼠标点击位置和UI之间的差值

        private bool _isSelf = false; //是否将自己作为移动目标

        #endregion

        #region Unity Events
        /// <summary>
        /// 寻找目标组件的父物体
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
        /// 拖拽开始时执行
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
        /// 拖拽时执行
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
        /// 拖拽结束后执行
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
        /// 当光标在UI上按下的时候，获取光标与UII中心的相对位置
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
        /// 设置拖拽物体的位置
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
