using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Reflection;
using UnityEngine.SceneManagement;
using HarmonyLib;
using UnityEngine;
using UnityEditor;
using UnityModManagerNet;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace RandomTweaks {
	public static class Copy {
		public static T DeepCopy<T>(this T obj) {
			using (var ms = new MemoryStream()) {
				var formatter = new BinaryFormatter();
				formatter.Serialize(ms, obj);
				ms.Position = 0;

				return (T)formatter.Deserialize(ms);
			}

		}
	}
		class RandomTweaks {
		public static int FontLoc = -1;
		public static Font Font = RDConstants.data.latinFont;
		public static List<Font> FontList = new List<Font> {
			RDConstants.data.latinFont,
			RDConstants.data.koreanFont,
			RDConstants.data.japaneseFont,
			RDConstants.data.chineseFont,
			RDConstants.data.legacyFont,
			//RDConstants.data.arialFont,
			//RDConstants.data.comicSansMSFont,
			//RDConstants.data.courierNewFont,
			//RDConstants.data.georgiaFont,
			//RDConstants.data.impactFont,
			//RDConstants.data.timesNewRomanFont
			};
		public static Dictionary<String, Font> OSFonts = new Dictionary<String, Font> {
			};
		public static List<string> FontNames = new List<string> {
			"Default",
			"Korean Font",
			"Japanese Font",
			"Chinese Font",
			"Legacy Font",
			//"Arial",
			//"Comic Sans",
			//"Courier",
			//"Georgia",
			//"Impact",
			//"Times New Roman",
			"Other Font",
		};
		public static UnityModManager.ModEntry.ModLogger Logger { get; private set; }
		public static string ModVersion = "0.1.1";
		public static Settings settings;

		internal static void Setup(UnityModManager.ModEntry modEntry) {
			Harmony harmony = new Harmony(modEntry.Info.Id);
			harmony.PatchAll();
			Logger = modEntry.Logger;
			settings = UnityModManager.ModSettings.Load<Settings>(modEntry);
			modEntry.OnGUI = new Action<UnityModManager.ModEntry>(OnGUI);
			modEntry.OnSaveGUI = new Action<UnityModManager.ModEntry>(OnSaveGUI);
			Behavior.PlayingUI.GaugeTextureInside.LoadImage(File.ReadAllBytes("Mods\\RandomTweaks\\GaugeInside.png"));
			Behavior.PlayingUI.GaugeTextureOutside.LoadImage(File.ReadAllBytes("Mods\\RandomTweaks\\GaugeOutside.png"));
			
			//L.og("Font Generation Done");
			if (settings.FontIndex == 5) {
				if (new List<string>(Font.GetOSInstalledFontNames()).Contains(settings.CustomFontName)) {
					Font = Font.CreateDynamicFontFromOSFont(settings.CustomFontName, 1);
				}
			} else {
				Font = FontList[settings.FontIndex];
            }
		}
		public static string FontName = "";
		public static Font FontTmp;
		public static Vector2 scrollPos;
		public static int LastFontLoc = -1;
		private static void OnGUI(UnityModManager.ModEntry modEntry) {

			//Checkpoint
			GUILayout.Label("Restart at Checkpoint");
			settings.DisableRestartAtCheckpoint = GUILayout.Toggle(settings.DisableRestartAtCheckpoint, "Disable Restart at Checkpoint");
			
			//Checkpoint
			GUILayout.Label("Play CLS in Editor");
			settings.PlayCLSInEditor = GUILayout.Toggle(settings.PlayCLSInEditor, "Enable Editor Playing (Ctrl+Shift+P)");

			//Overload Gauge
			GUILayout.Label("Overload Gauge");
			settings.EnableOverloadGauge = GUILayout.Toggle(settings.EnableOverloadGauge, "Enable Overload Gauge");

			// Custom Fonts
			GUILayout.Label("Custom Fonts");
			settings.FontIndex = GUILayout.SelectionGrid(settings.FontIndex, FontNames.ToArray(), 6);
			//L.og(Font.settings.fontSize);
			if (settings.FontIndex == 5) {
				if (FontLoc != -1) LastFontLoc = FontLoc;
				GUILayout.Label("Font Name");
				FontName = GUILayout.TextField(FontLoc == -1 ? FontName : Font.GetOSInstalledFontNames()[FontLoc], GUILayout.Width(300));
				scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.MinWidth(300), GUILayout.Height(500));
				int FontLocChange = GUILayout.SelectionGrid(FontLoc, Font.GetOSInstalledFontNames(), 1, GUILayout.Width(200));
				if (LastFontLoc != FontLocChange) FontLoc = FontLocChange; else FontLoc = -1;
				GUILayout.EndScrollView();
				GUILayout.Label("Font Size");
				string fTmp = (GUILayout.TextField(settings.fontSize.ToString()));
				settings.fontSize = float.TryParse(fTmp, out float tmp) ? float.Parse(fTmp) : settings.fontSize;
				GUILayout.Label("Font Line Spacing");
				string fTmp2 = (GUILayout.TextField(settings.lineSpace.ToString()));
				settings.lineSpace = float.TryParse(fTmp2, out tmp) ? float.Parse(fTmp2) : settings.lineSpace;
				if (!OSFonts.ContainsKey(FontName)) OSFonts[FontName] = Font.CreateDynamicFontFromOSFont(FontName, 16);
				FontTmp = OSFonts[FontName];
			} else {
				FontTmp = FontList[settings.FontIndex];
			}
			if (GUILayout.Button("Change Font")) {
				L.og("asdf");
				if (FontTmp == null) {
					Font = Font.CreateDynamicFontFromOSFont(Font.GetOSInstalledFontNames()[FontLoc], 16); //OSFonts[FontLoc];
				} else {
					Font = FontTmp;
				}
				settings.CustomFontName = Font.name;
				L.og(Font.name);
				//BBManager.instance.font = RandomTweaks.Font;
				typeof(RDString).GetProperty("fontData").SetValue(null, Settings.GetFontData());
				GCS.sceneToLoad = SceneManager.GetActiveScene().name;
				scrController.instance.StartLoadingScene(WipeDirection.StartsFromRight);
				//Patch.ChangeFont.UpdateFont();
			}
			if (GUILayout.Button("Quit ADOFAI")) System.Windows.Forms.Application.Exit();
		}

		private static void OnSaveGUI(UnityModManager.ModEntry modEntry) {
			settings.Save(modEntry);
		}
	}
}
public static class L {
	public static void og(object log) {
		UnityModManager.Logger.Log(log.ToString());
    }
}