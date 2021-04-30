using System;
using UnityEngine.SceneManagement;
using System.Reflection;
using System.Collections.Generic;
using UnityModManagerNet;
using UnityEngine;

namespace RandomTweaks {
	public class Settings : UnityModManager.ModSettings, IDrawable {
		public string CustomFontName;
		public bool CustomFontEnabled;
		public float fontSize = 0.75f;
		public float lineSpace = 0.85f;
		public int FontIndex;
		public bool EnableOverloadGauge;
		public bool DisableRestartAtCheckpoint;
		public bool PlayCLSInEditor;
		public override void Save(UnityModManager.ModEntry modEntry) {
			UnityModManager.ModSettings.Save<Settings>(this, modEntry);
		}
		public static FontData GetFontData() {
			int index = RandomTweaks.settings.FontIndex;
			return GetFontData(index);
		}
		public static FontData GetFontData(int index) {
			FontData result = default(FontData);
			RDConstants data = RDConstants.data;
			if (index == 1) {
				result.fontScale = 1.25f;
				result.lineSpacing = 0.75f;
			} else if (index == 2) {
				result.fontScale = 0.7f;
				result.lineSpacing = 1.1f;
			} else if (index == 3) {
				result.fontScale = 0.82f;
				result.lineSpacing = 1.1f;
			} else if (index == 4) {
				result.fontScale = 1.25f;
				result.lineSpacing = 0.75f;
			} else {
				result.fontScale = RandomTweaks.settings.fontSize;
				result.lineSpacing = RandomTweaks.settings.lineSpace;
			}
			result.font = RandomTweaks.Font;
			return result;
		}

		public static FontData GetFontData(Font font) {
			FontData result = default(FontData);
			RDConstants data = RDConstants.data;
			if (font == data.koreanFont) {
				result.fontScale = 1.25f;
				result.lineSpacing = 0.75f;
			} else if (font == data.japaneseFont) {
				result.fontScale = 0.7f;
				result.lineSpacing = 1.1f;
			} else if (font == data.chineseFont) {
				result.fontScale = 0.82f;
				result.lineSpacing = 1.1f;
			} else if (font == data.legacyFont) {
				result.fontScale = 1.25f;
				result.lineSpacing = 0.75f;
			} else if (font.fontNames[0].Contains("Avenir")) {
				result.fontScale = 1f;
				result.lineSpacing = 1f;
			} else {
				result.fontScale = RandomTweaks.settings.fontSize;
				result.lineSpacing = RandomTweaks.settings.lineSpace;
			}
			result.font = RandomTweaks.Font;
			return result;
		}

		public void OnChange() {
		}

	}
}
