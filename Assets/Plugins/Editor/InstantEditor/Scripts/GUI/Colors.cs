#if UNITY_EDITOR
namespace DeadMosquito.InstantEditor
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	public static class Colors
	{
		public static readonly Color Darken = new Color(0, 0, 0, 0.1f);
		public static readonly Color DarkenABit = new Color(0, 0, 0, 0.04f);

		static readonly Dictionary<NoteColor, NoteColorCollection> _noteColors;

		static Colors()
		{
			var size = Enum.GetValues(typeof(NoteColor)).Length;
			_noteColors = new Dictionary<NoteColor, NoteColorCollection>(size);
			InitColors();

			Values = Enum.GetValues(typeof(NoteColor)).Cast<NoteColor>().ToArray();
		}

		public static NoteColor[] Values { get; private set; }

		static void InitColors()
		{
			_noteColors[NoteColor.Lemon] = new NoteColorCollection(LemonBg, LemonHeader, LemonOutline);
			_noteColors[NoteColor.Grass] = new NoteColorCollection(GrassBg, GrassHeader, GrassOutline);
			_noteColors[NoteColor.SkyBlue] = new NoteColorCollection(SkyBlueBg, SkyBlueHeader, SkyBlueOutline);
			_noteColors[NoteColor.Amethyst] = new NoteColorCollection(AmethystBg, AmethystHeader, AmethystOutline);
			_noteColors[NoteColor.Rose] = new NoteColorCollection(RoseBg, RoseHeader, RoseOutline);
			_noteColors[NoteColor.Clean] = new NoteColorCollection(CleanBg, CleanHeader, CleanOutline);
		}

		public static NoteColorCollection ColorById(NoteColor color)
		{
			if (_noteColors.ContainsKey(color))
			{
				return _noteColors[color];
			}

			throw new ArgumentException("Color not present in dictionary: " + color);
		}

		public struct NoteColorCollection
		{
			public Color main;
			public Color header;
			public Color chooserOutline;

			public NoteColorCollection(Color main, Color header, Color outline)
			{
				this.main = main;
				this.header = header;
				chooserOutline = outline;
			}
		}

		#region yellow

		static readonly Color LemonHeader = new Color(0.976f, 0.953f, 0.631f, 1.000f);
		static readonly Color LemonBg = new Color(0.980f, 0.965f, 0.741f, 1.000f);
		static readonly Color LemonOutline = new Color(0.890f, 0.910f, 0.451f, 1.000f);

		#endregion

		#region green

		static readonly Color GrassHeader = new Color(0.722f, 0.875f, 0.663f, 1.000f);
		static readonly Color GrassBg = new Color(0.769f, 0.886f, 0.722f, 1.000f);
		static readonly Color GrassOutline = new Color(0.702f, 0.827f, 0.647f, 1.000f);

		#endregion

		#region blue

		static readonly Color SkyBlueHeader = new Color(0.627f, 0.867f, 0.925f, 1.000f);
		static readonly Color SkyBlueBg = new Color(0.686f, 0.878f, 0.925f, 1.000f);
		static readonly Color SkyBlueOutline = new Color(0.494f, 0.718f, 0.753f, 1.000f);

		#endregion

		#region purple

		static readonly Color AmethystHeader = new Color(0.784f, 0.702f, 0.855f, 1.000f);
		static readonly Color AmethystBg = new Color(0.812f, 0.737f, 0.863f, 1.000f);
		static readonly Color AmethystOutline = new Color(0.682f, 0.624f, 0.714f, 1.000f);

		#endregion

		#region pink

		static readonly Color RoseHeader = new Color(0.953f, 0.722f, 0.773f, 1.000f);
		static readonly Color RoseBg = new Color(0.957f, 0.753f, 0.796f, 1.000f);
		static readonly Color RoseOutline = new Color(0.769f, 0.608f, 0.671f, 1.000f);

		#endregion

		#region white

		static readonly Color CleanHeader = new Color(0.957f, 0.957f, 0.957f, 1.000f);
		static readonly Color CleanBg = new Color(0.980f, 0.980f, 0.980f, 1.000f);
		static readonly Color CleanOutline = new Color(0.804f, 0.804f, 0.804f, 1.000f);

		#endregion
	}
}
#endif