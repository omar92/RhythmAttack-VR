#if UNITY_EDITOR
namespace DeadMosquito.InstantEditor
{
	using System;
	using UnityEditor;
	using UnityEngine;

	public sealed class NoteHeader
	{
		public const string TooMuchTextMessage = "This file is too large to be quick-edited.";
		public const float Height = 32f;

		readonly Action _onCloseBtnClick;
		readonly Action _onSaveBtnClick;
		readonly Action _onRestore;
		readonly bool _isFileTooBig;
		readonly string _title;


		public NoteHeader(bool isFileTooBig, string title, Action onClose, Action onSave, Action onRestore)
		{
			_onCloseBtnClick = onClose;
			_onSaveBtnClick = onSave;
			_onRestore = onRestore;
			_isFileTooBig = isFileTooBig;
			_title = title;
		}

		public void OnGUI(Rect rect, Colors.NoteColorCollection colors)
		{
			var headerRect = GetHeaderRect(rect);
			GUIUtils.ColorRect(headerRect, colors.header, Color.clear);

			EditorGUILayout.LabelField(_isFileTooBig ? TooMuchTextMessage : _title, Assets.Styles.HeaderFileNameText);

			if (!_isFileTooBig)
			{
				DrawSaveButton(headerRect);
				DrawRestoreButton(headerRect);
			}
			DrawColorPickerButton(headerRect);
		}

		void DrawRestoreButton(Rect headerRect)
		{
			if (RestoreButton(headerRect))
			{
				_onRestore();
			}
		}

		void DrawSaveButton(Rect headerRect)
		{
			if (SaveButton(headerRect))
			{
				_onSaveBtnClick();
			}
		}

		void DrawColorPickerButton(Rect headerRect)
		{
			if (CloseButton(headerRect))
			{
				_onCloseBtnClick();
			}
		}

		static bool SaveButton(Rect headerRect)
		{
			return GUIUtils.TextureButton(GetSaveBtnRect(headerRect), Assets.Textures.SaveTexture, "Save changes and overwrite file");
		}

		static bool CloseButton(Rect headerRect)
		{
			return GUIUtils.TextureButton(GetCloseBtnRect(headerRect), Assets.Textures.CloseTexture, "Close editor");
		}

		static bool RestoreButton(Rect headerRect)
		{
			return GUIUtils.TextureButton(GetRestoreBtnRect(headerRect), Assets.Textures.RestoreTexture, "Restore original text");
		}

		#region rects

		static Rect GetHeaderRect(Rect noteRect)
		{
			var headerRect = new Rect(noteRect.x, noteRect.y, noteRect.width, Height);
			return headerRect;
		}

		static Rect GetSaveBtnRect(Rect headerRect)
		{
			return new Rect(headerRect.width - headerRect.height, headerRect.y, headerRect.height, headerRect.height);
		}

		static Rect GetCloseBtnRect(Rect headerRect)
		{
			return new Rect(0, headerRect.y, headerRect.height, headerRect.height);
		}

		static Rect GetRestoreBtnRect(Rect headerRect)
		{
			return new Rect(headerRect.width - 2 * headerRect.height, headerRect.y, headerRect.height, headerRect.height);
		}

		static Rect GetTitleRect(Rect headerRect)
		{
			return headerRect;
//			return new Rect(headerRect.width - 2 * headerRect.height, headerRect.y, headerRect.height, headerRect.height);
		}

		#endregion
	}
}

#endif