#if UNITY_EDITOR
namespace DeadMosquito.InstantEditor
{
	using System.IO;
	using UnityEditor;
	using UnityEngine;

	public class InstantEditorWindowContent : PopupWindowContent
	{
		const int CharacterLimit = 16000;

		readonly NoteHeader _header;
		readonly NoteTextArea _textArea;
		readonly string _filePath;

		readonly string _originalText;
		string _currentText;

		public InstantEditorWindowContent(string guid)
		{
			_filePath = AssetDatabase.GUIDToAssetPath(guid);
			_originalText = File.ReadAllText(_filePath);
			_currentText = _originalText;

			var isTooLarge = _originalText.Length > CharacterLimit;
			if (isTooLarge)
			{
				_originalText = _originalText.Substring(0, CharacterLimit);
				_originalText += "\n...\n\n<...etc...>";
			}

			_header = new NoteHeader(isTooLarge, Path.GetFileName(_filePath), OnCloseButtonClick, OnSaveButtonClick, OnRestoreButtonClick);
			_textArea = isTooLarge ? NoteTextArea.CreateTooMuchText(_originalText) : NoteTextArea.Create(_originalText, OnTextUpdated);
		}

		public override Vector2 GetWindowSize()
		{
			return new Vector2(InstantEditorEditorSettings.WindowWidth, InstantEditorEditorSettings.WindowHeight);
		}

		public override void OnGUI(Rect rect)
		{
			var c = Colors.ColorById(InstantEditorEditorSettings.EditorColor);
			_textArea.OnGUI(rect, c);
			_header.OnGUI(rect, c);

			editorWindow.Repaint();
		}

		void OnTextUpdated(string text)
		{
			_currentText = text;
		}

		void OnCloseButtonClick()
		{
			editorWindow.Close();
		}

		void OnRestoreButtonClick()
		{
			GUIUtility.keyboardControl = 0; // remove focus from text field
			
			_currentText = _originalText;
			_textArea.Text = _currentText;
		}

		void OnSaveButtonClick()
		{
			editorWindow.Close();

			if (_currentText == _originalText)
			{
				return;
			}

			File.WriteAllText(_filePath, _currentText);
			AssetDatabase.Refresh();
		}

		public override void OnOpen()
		{
		}

		public override void OnClose()
		{
		}
	}
}

#endif