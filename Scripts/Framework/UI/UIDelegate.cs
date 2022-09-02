using UnityEngine;
using UnityEngine.EventSystems;

namespace Client.UI
{
    /// <summary>
    /// 拖拽功能带参的委托
    /// </summary>
    public delegate void DragDelegate(int id, int site);

    /// <summary>
    /// 拖拽功能无参委托
    /// </summary>
    public delegate void NoneParamDelegate();
}