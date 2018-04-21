#if UNITY_EDITOR
namespace DeadMosquito.InstantEditor
{
	using System.IO;
	using UnityEditor;
	using UnityEngine;

	public static class Assets
	{
		public static class Styles
		{
			public static readonly GUIStyle TextArea;
			public static readonly GUIStyle HeaderFileNameText;
			public static readonly GUIStyle PlusLabel;
			public static readonly GUIStyle TooBigMessageText;

			public static readonly GUISkin Skin;

			static Styles()
			{
				var skinPath = Path.Combine(InstantEditorEditorSettings.InstantEditorHomeFolder, "Assets/ScrollGUISkin.asset");
				Skin = AssetDatabase.LoadAssetAtPath<GUISkin>(skinPath);

				if (Skin == null)
				{
					Debug.LogError(
						"Could not load GUI skin. Did you move InstantEditor folder around in your project? Go to Preferences -> InstantEditor and update the location of InstantEditor folder");
				}
				
				var fontPath = Path.Combine(InstantEditorEditorSettings.InstantEditorHomeFolder, "Assets/SourceCodePro-Regular.ttf");
				var font = AssetDatabase.LoadAssetAtPath<Font>(fontPath);

				TextArea = new GUIStyle(EditorStyles.textArea)
				{
					normal = {background = null, textColor = Color.black},
					active = {background = null},
					focused = {background = null},
					wordWrap = false,
					richText = false,
					font = font,
					stretchHeight = true,
					stretchWidth = true
				};
				HeaderFileNameText = new GUIStyle(EditorStyles.boldLabel)
				{
					alignment = TextAnchor.MiddleCenter,
					stretchHeight = true,
					stretchWidth = true, 
					fixedHeight = NoteHeader.Height,
				};
				PlusLabel = new GUIStyle(EditorStyles.boldLabel)
				{
					padding = new RectOffset(0, 1, 0, 2),
					margin = new RectOffset(),
					alignment = TextAnchor.MiddleCenter,
					stretchHeight = true,
					stretchWidth = true
				};
				TooBigMessageText = EditorStyles.boldLabel;
				TooBigMessageText.normal.textColor = Color.black;
				TooBigMessageText.alignment = TextAnchor.MiddleCenter;
				TooBigMessageText.stretchHeight = true;
			}
		}

		public static class Textures
		{
			public static readonly Texture2D CloseTexture;
			public static readonly Texture2D RestoreTexture;
			public static readonly Texture2D SaveTexture;

			static Textures()
			{
				CloseTexture = GetTexture("ic_close");
				RestoreTexture = GetTexture("ic_restore");
				SaveTexture = GetTexture("ic_save");
			}

			static Texture2D GetTexture(string name)
			{
				return AssetDatabase.LoadAssetAtPath<Texture2D>(GetTexturePath(name));
			}

			static string GetTexturePath(string name)
			{
				var relativePath = Path.Combine("Assets/GUI", name + ".png");
				return Path.Combine(InstantEditorEditorSettings.InstantEditorHomeFolder, relativePath);
			}
		}
	}
}

#endif