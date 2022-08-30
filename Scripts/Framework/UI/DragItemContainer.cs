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
    public class DragItemContainer : UIBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {

        public GameObject m_dragObject = null;
        private RectTransform m_dragTransform = null;

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

        public void OnBeginDrag(PointerEventData eventData)
        {
            var _canvas = FindInParents<Canvas>(gameObject);
            if (_canvas == null) return;
            m_dragObject.SetActive(true);
            m_dragObject.transform.SetAsLastSibling();
            m_dragObject.transform.position = gameObject.transform.position;

            m_dragTransform = transform as RectTransform;
            if (!m_dragObject.GetComponent<CanvasGroup>())
            {
                var _group = m_dragObject.AddComponent<CanvasGroup>();
                _group.blocksRaycasts = false;
            }

            var _rect = m_dragObject.GetComponent<RectTransform>();
            Vector3 _globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_dragTransform, eventData.position, eventData.pressEventCamera, out _globalMousePos))
            {
                float _diffX = _globalMousePos.x - _rect.position.x;
                float _diffY = _globalMousePos.y - _rect.position.y;
            }

           //throw new System.NotImplementedException();
        }

        public void OnDrag(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnDrop(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}
