using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Pagination
{
	internal class PassThroughDragEvents : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IEventSystemHandler
	{
		public List<GameObject> Targets;

		public List<string> DesiredTargetTypes;

		private Dictionary<string, Dictionary<MonoBehaviour, MethodInfo>> m_Events = new Dictionary<string, Dictionary<MonoBehaviour, MethodInfo>>();

		private static List<string> eventTypes = new List<string> { "OnBeginDrag", "OnEndDrag", "OnDrag" };

		private Vector2 m_dragStartPosition = default(Vector2);

		private Vector2 m_dragEndPosition = default(Vector2);

		private Vector2 m_delta = default(Vector2);

		private bool m_dragging;

		public bool PassThroughHorizontalDragEvents = true;

		public bool PassThroughVerticalDragEvents = true;

		private void Start()
		{
			Initialise();
		}

		public void Initialise()
		{
			m_Events.Clear();
			if (Targets == null || Targets.Count == 0 || DesiredTargetTypes == null || DesiredTargetTypes.Count == 0)
			{
				return;
			}
			foreach (string eventType in eventTypes)
			{
				foreach (GameObject target in Targets)
				{
					if (target == null)
					{
						continue;
					}
					MonoBehaviour[] components = target.GetComponents<MonoBehaviour>();
					MonoBehaviour[] array = components;
					foreach (MonoBehaviour monoBehaviour in array)
					{
						Type type = monoBehaviour.GetType();
						if (!DesiredTargetTypes.Contains(type.Name))
						{
							continue;
						}
						MethodInfo method = type.GetMethod(eventType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
						if (method != null)
						{
							if (!m_Events.ContainsKey(eventType))
							{
								m_Events.Add(eventType, new Dictionary<MonoBehaviour, MethodInfo>());
							}
							m_Events[eventType].Add(monoBehaviour, method);
						}
					}
				}
			}
		}

		private void Update()
		{
			if (m_dragging)
			{
				m_dragStartPosition = Input.mousePosition;
			}
		}

		public void OnEndDrag(PointerEventData data)
		{
			if (!m_dragging)
			{
				return;
			}
			m_dragging = false;
			m_dragEndPosition = Input.mousePosition;
			m_delta = m_dragEndPosition - m_dragStartPosition;
			data.delta = m_delta;
			if (!m_Events.ContainsKey("OnEndDrag"))
			{
				return;
			}
			foreach (KeyValuePair<MonoBehaviour, MethodInfo> item in m_Events["OnEndDrag"])
			{
				item.Value.Invoke(item.Key, new object[1] { data });
			}
		}

		public void OnBeginDrag(PointerEventData data)
		{
			DragEventAnalysis dragEventAnalysis = new DragEventAnalysis(data);
			if ((!PassThroughHorizontalDragEvents || dragEventAnalysis.DragPlane != 0) && (!PassThroughVerticalDragEvents || dragEventAnalysis.DragPlane != DragEventAnalysis.eDragPlane.Vertical))
			{
				return;
			}
			m_dragging = true;
			if (!m_Events.ContainsKey("OnBeginDrag"))
			{
				return;
			}
			foreach (KeyValuePair<MonoBehaviour, MethodInfo> item in m_Events["OnBeginDrag"])
			{
				item.Value.Invoke(item.Key, new object[1] { data });
			}
		}

		public void OnDrag(PointerEventData data)
		{
			if (!m_dragging || !m_Events.ContainsKey("OnDrag"))
			{
				return;
			}
			foreach (KeyValuePair<MonoBehaviour, MethodInfo> item in m_Events["OnDrag"])
			{
				item.Value.Invoke(item.Key, new object[1] { data });
			}
		}
	}
}
