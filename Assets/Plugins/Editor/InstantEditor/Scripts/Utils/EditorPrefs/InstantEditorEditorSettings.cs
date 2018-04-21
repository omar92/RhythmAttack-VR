#if UNITY_EDITOR
namespace DeadMosquito.InstantEditor
{
	using System;
	using UnityEditor;
	using UnityEngine;

	public static class InstantEditorEditorSettings
	{
		public static readonly EditorPrefsString InstantEditorHomeFolder =
			new EditorPrefsString("DeadMosquito.InstantEditor.InstantEditorHomeFolder." + ProjectName, "Location Folder",
				"Assets/Plugins/Editor/InstantEditor");

		public static readonly EditorPrefsIntSlider OffsetInProjectView =
			new EditorPrefsIntSlider("DeadMosquito.InstantEditor.OffsetInProjectView." + ProjectName,
				"Offset in Project View", 0, 0, 100);

		public static readonly EditorPrefsIntSlider FontSize =
			new EditorPrefsIntSlider("DeadMosquito.InstantEditor.FontSize." + ProjectName,
				"Note Font Size", 12, 8, 32);
			
		public static readonly EditorPrefsIntSlider WindowWidth =
			new EditorPrefsIntSlider("DeadMosquito.InstantEditor.WindowWidth." + ProjectName,
				"Window width", 1024, 320, 2400);
		
		public static readonly EditorPrefsIntSlider WindowHeight =
			new EditorPrefsIntSlider("DeadMosquito.InstantEditor.WindowHeight." + ProjectName,
				"Window height", 768, 240, 2400);
		
		public static readonly EditorPrefsEnum EditorColor =
			new EditorPrefsEnum("DeadMosquito.InstantEditor.EditorColor." + ProjectName,
				"Background color", NoteColor.Clean);

		static string ProjectName
		{
			get
			{
				var s = Application.dataPath.Split('/');
				var p = s[s.Length - 2];
				return p;
			}
		}

		[PreferenceItem("InstantEditor")]
		public static void EditorPreferences()
		{
			EditorGUILayout.HelpBox(
				"Change this setting to new location of InstantEditor if you move the folder around in your project.",
				MessageType.Warning);
			InstantEditorHomeFolder.Draw();
			EditorGUILayout.Space();
			OffsetInProjectView.Draw();
			FontSize.Draw();
			EditorGUILayout.Space();
			WindowWidth.Draw();
			WindowHeight.Draw();
			EditorColor.Draw();

			GUILayout.FlexibleSpace();
			EditorGUILayout.LabelField("Version 1.1.1", EditorStyles.miniLabel);
		}

		public abstract class EditorPrefsItem<T>
		{
			public T DefaultValue;
			public string Key;
			public string Label;

			public EditorPrefsItem(string key, string label, T defaultValue)
			{
				if (string.IsNullOrEmpty(key))
				{
					throw new ArgumentNullException("key");
				}

				Key = key;
				Label = label;
				DefaultValue = defaultValue;
			}

			public abstract T Value { get; set; }
			public abstract void Draw();

			public static implicit operator T(EditorPrefsItem<T> s)
			{
				return s.Value;
			}
		}
		
		public class EditorPrefsEnum : EditorPrefsItem<NoteColor>
		{
			public EditorPrefsEnum(string key, string label, NoteColor defaultValue)
				: base(key, label, defaultValue)
			{
			}

			public override NoteColor Value
			{
				get { return (NoteColor) EditorPrefs.GetInt(Key, (int) DefaultValue); }
				set { EditorPrefs.SetInt(Key, (int) value); }
			}

			public override void Draw()
			{
				Value = (NoteColor) EditorGUILayout.EnumPopup(Label, Value);
			}
		}

		public class EditorPrefsInt : EditorPrefsItem<int>
		{
			public EditorPrefsInt(string key, string label, int defaultValue)
				: base(key, label, defaultValue)
			{
			}

			public override int Value
			{
				get { return EditorPrefs.GetInt(Key, DefaultValue); }
				set { EditorPrefs.SetInt(Key, value); }
			}

			public override void Draw()
			{
				Value = EditorGUILayout.IntField(Label, Value);
			}
		}

		public class EditorPrefsIntSlider : EditorPrefsInt
		{
			readonly int _leftValue;
			readonly int _rightValue;

			public EditorPrefsIntSlider(string key, string label, int defaultValue, int leftValue, int rightValue)
				: base(key, label, defaultValue)
			{
				_leftValue = leftValue;
				_rightValue = rightValue;
			}

			public override void Draw()
			{
				Value = EditorGUILayout.IntSlider(Label, Value, _leftValue, _rightValue);
			}
		}

		public class EditorPrefsString : EditorPrefsItem<string>
		{
			public EditorPrefsString(string key, string label, string defaultValue)
				: base(key, label, defaultValue)
			{
			}

			public override string Value
			{
				get { return EditorPrefs.GetString(Key, DefaultValue); }
				set { EditorPrefs.SetString(Key, value); }
			}

			public override void Draw()
			{
				Value = EditorGUILayout.TextField(Label, Value);
			}
		}

		public class EditorPrefsBool : EditorPrefsItem<bool>
		{
			public EditorPrefsBool(string key, string label, bool defaultValue)
				: base(key, label, defaultValue)
			{
			}

			public override bool Value
			{
				get { return EditorPrefs.GetBool(Key, DefaultValue); }
				set { EditorPrefs.SetBool(Key, value); }
			}

			public override void Draw()
			{
				Value = EditorGUILayout.Toggle(Label, Value);
			}
		}

		public class EditorPrefsColor : EditorPrefsItem<Color>
		{
			readonly string A;
			readonly string B;
			readonly string G;
			readonly string R;

			public EditorPrefsColor(string key, string label, Color defaultValue)
				: base(key, label, defaultValue)
			{
				R = Key + "_R";
				G = Key + "_G";
				B = Key + "_B";
				A = Key + "_A";
			}

			public override Color Value
			{
				get
				{
					if (EditorPrefs.GetBool(Key, false))
					{
						return new Color(
							EditorPrefs.GetFloat(R, 1),
							EditorPrefs.GetFloat(G, 1),
							EditorPrefs.GetFloat(B, 1),
							EditorPrefs.GetFloat(A, 1));
					}
					return DefaultValue;
				}
				set
				{
					EditorPrefs.SetBool(Key, true);
					EditorPrefs.SetFloat(Key + "_R", value.r);
					EditorPrefs.SetFloat(Key + "_G", value.g);
					EditorPrefs.SetFloat(Key + "_B", value.b);
					EditorPrefs.SetFloat(Key + "_A", value.a);
				}
			}

			public override void Draw()
			{
				Value = EditorGUILayout.ColorField(Label, Value);
			}
		}
	}
}

#endif