#if UNITY_EDITOR
namespace DeadMosquito.InstantEditor
{
	using UnityEditor;
	using UnityEngine;

	[InitializeOnLoad]
	public static class InstantEditor
	{
		static InstantEditor()
		{
			EditorApplication.projectWindowItemOnGUI += AddEditIcon;
		}

		static void AddEditIcon(string guid, Rect selectionRect)
		{
			AddEditButton(guid, selectionRect);
			EditorApplication.RepaintProjectWindow();
		}

		static void AddEditButton(string guid, Rect rect)
		{
			var iconRect = GUIUtils.GetProjectViewIconRect(rect);

			var isInFocus = rect.HasMouseInside();
			var isFile = FileUtils.IsFile(AssetDatabase.GUIDToAssetPath(guid));

			if (isInFocus && isFile)
			{
				DrawEditButton(iconRect, guid);
			}
		}

		static void DrawEditButton(Rect iconRect, string guid)
		{
			if (GUI.Button(iconRect, string.Empty, GUI.skin.button))
			{
				ShowEditor(iconRect, guid);
			}
			GUI.Label(iconRect, "E", Assets.Styles.PlusLabel);
		}

		static void ShowEditor(Rect iconRect, string guid)
		{
			PopupWindow.Show(iconRect, new InstantEditorWindowContent(guid));
		}
	}
}
#endif