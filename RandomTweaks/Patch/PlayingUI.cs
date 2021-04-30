using HarmonyLib;
using UnityEngine;

namespace RandomTweaks.Patch {
	class PlayingUI {
		private static GameObject _gameObject;
		private static Behavior.PlayingUI _mainBehavior;
		private static bool IsPlaying = false;
		internal static bool UI = false;
		[HarmonyPatch(typeof(scnEditor), "Start")]
		private static class ResetUI {
			public static void Postfix() {
				UI = false;
			}
		}
		[HarmonyPatch(typeof(scrController), "Start")]
		private static class CtrResetUI {
			public static void Postfix() {
				UI = false;
			}
		}
		[HarmonyPatch(typeof(scrController), "Update")]
		private static class ControllerUIPatch {
			private static void Prefix() {
				if (!Startup.IsEnabled || !RandomTweaks.settings.EnableOverloadGauge || !scrController.isGameWorld) {
					DestroyUI();
					return;
				}

				if (!UI) {
					StartUI();
				}
			}
		}

		[HarmonyPatch(typeof(scnEditor), "Update")]
		private static class EditorUIPatch {
			private static void Prefix(scnEditor __instance) {
				if (GCS.standaloneLevelMode) IsPlaying = true;
				if (!Startup.IsEnabled || !RandomTweaks.settings.EnableOverloadGauge || !IsPlaying) {
					DestroyUI();
					return;
				}

				if (!UI) {
					StartUI();
				}
			}
		}

		[HarmonyPatch(typeof(scnEditor), "Play")]
		private static class EditorPlayingUIPatch {
			private static void Prefix(scnEditor __instance) {
				IsPlaying = true;
			}
		}

		[HarmonyPatch(typeof(scnEditor), "SwitchToEditMode")]
		private static class EditorEditorUIPatch {
			private static void Prefix(scnEditor __instance) {
				IsPlaying = false;
			}
		}

		[HarmonyPatch(typeof(scrFailBar), "Update")]
		internal static class setOverloadGauge {
			public static void Postfix(scrFailBar __instance) {
				Behavior.PlayingUI.OverloadGauge = __instance.value;
			}
		}
		//commands
		public static void DestroyUI() {
			if (_gameObject == null) {
				return;
			}
			Object.DestroyImmediate(_gameObject);
			Object.DestroyImmediate(_mainBehavior);
			_gameObject = null;
			_mainBehavior = null;
			UI = false;
		}
		public static void StartUI() {
			_gameObject = new GameObject();
			_mainBehavior = _gameObject.AddComponent<Behavior.PlayingUI>();
			UI = true;
		}
	}
}