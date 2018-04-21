#if UNITY_EDITOR
namespace DeadMosquito.InstantEditor
{
	using System;
	using UnityEditor;
	using UnityEngine;

	public sealed class NoteTextArea
	{
		readonly Action<string> _onTextUpdated;

		Vector2 _scroll = Vector2.zero;
		readonly bool _isTooMuchText;
		string _text;

		public string Text
		{
			set { _text = value; }
		}

		public static NoteTextArea CreateTooMuchText(string croppedText)
		{
			return new NoteTextArea(true, croppedText, null);
		}

		public static NoteTextArea Create(string initialText, Action<string> onTextUpdated)
		{
			return new NoteTextArea(false, initialText, onTextUpdated);
		}

		NoteTextArea(bool isTooMuchText, string initialText, Action<string> onTextUpdated)
		{
			_isTooMuchText = isTooMuchText;
			_text = initialText;
			_onTextUpdated = onTextUpdated;
		}

		public void OnGUI(Rect rect, Colors.NoteColorCollection colors)
		{
			DrawNoteBackground(rect, colors.main);


			GUILayout.BeginArea(GetTextAreaRect(rect));
			EditorGUILayout.BeginVertical();
			GUI.skin = Assets.Styles.Skin;

			_scroll = EditorGUILayout.BeginScrollView(_scroll);
			EditorGUI.BeginChangeCheck();
			
			if (_isTooMuchText)
			{
				GUI.enabled = false;
			}
			Assets.Styles.TextArea.fontSize = InstantEditorEditorSettings.FontSize;
			_text = EditorGUILayout.TextArea(_text, Assets.Styles.TextArea);

			if (EditorGUI.EndChangeCheck())
			{
				_onTextUpdated(_text);
			}
			EditorGUILayout.EndScrollView();

			GUI.skin = null;

			EditorGUILayout.EndVertical();
			GUILayout.EndArea();

			GUI.enabled = true;
		}

		static void DrawNoteBackground(Rect rect, Color backgroundColor)
		{
			GUIUtils.ColorRect(rect, backgroundColor, Color.clear);
		}

		static Rect GetTextAreaRect(Rect noteRect)
		{
			const float h = NoteHeader.Height;
			return new Rect(noteRect.x, noteRect.y + h, noteRect.width, noteRect.height - h);
		}
	}
}
#endif