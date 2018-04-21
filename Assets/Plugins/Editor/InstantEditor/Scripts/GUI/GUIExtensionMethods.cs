#if UNITY_EDITOR
namespace DeadMosquito.InstantEditor
{
	using UnityEngine;

	public static class GUIExtensionMethods
	{
		public static bool HasMouseInside(this Rect rect)
		{
			return rect.Contains(Event.current.mousePosition);
		}
	}
}
#endif