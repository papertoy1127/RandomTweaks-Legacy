using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RandomTweaks.Patch {
	class EditorTweaks {
		[HarmonyPatch(typeof(CustomLevel), "Play")]
		private static class CheckpointStartPatch {
			public static bool Prefix(CustomLevel __instance, int seqID) {
				if (seqID == 0 || !RandomTweaks.settings.DisableRestartAtCheckpoint) {
					return true;
				}
				__instance.Play(0);
				return false;
			}
		}

		/*[HarmonyPatch(typeof(scrLevelMaker), "MakeLevel")]
		private static class asdfpatch {
			public static bool MakeLevel(scrLevelMaker __instance, ref List<scrFloor> __result) {
				GameObject gameObject = GameObject.Find("Floor Container");
				if (gameObject == null) {
					gameObject = new GameObject("Floor Container");
				}
				if (__instance.listFloors.Count > 0) {
					foreach (scrFloor scrFloor in __instance.listFloors) {
						if (scrFloor != null) {
							UnityEngine.Object.DestroyImmediate(scrFloor.gameObject);
						}
					}
					scrConductor.instance.onBeats = null;
				}
				__instance.listFloors = new List<scrFloor>();
				string text = __instance.leveldata;
				Vector3 vector = Vector3.zero;
				bool flag = true;
				float num = 1f;
				scrFloor component = UnityEngine.Object.Instantiate<GameObject>(__instance.floor, Vector3.zero, Quaternion.identity).GetComponent<scrFloor>();
				__instance.listFloors.Add(component);
				component.gameObject.transform.parent = gameObject.transform;
				component.hasLit = true;
				component.entryangle = 4.71238899230957;
				component.name = "0/FloorR";
				int num2 = 1;
				for (int i = 0; i < text.Length; i++) {
					double radius = (double)scrController.instance.startRadius;
					double num3 = 0.0;
					Mathf.Sin(0.7853982f);
					Mathf.Sin(0.5235988f);
					Mathf.Sin(0.5235988f);
					bool isEditor = Application.isEditor;
					char c = text[i];
					if (c <= 'q') {
						if (c != '!') {
							switch (c) {
								case '5':
									num3 = scrMisc.incrementAngle(__instance.listFloors.Last<scrFloor>().entryangle, 1.8849555253982544);
									break;
								case '6':
									num3 = scrMisc.incrementAngle(__instance.listFloors.Last<scrFloor>().entryangle, -1.8849555253982544);
									break;
								case '7':
									num3 = scrMisc.incrementAngle(__instance.listFloors.Last<scrFloor>().entryangle, 2.243994951248169);
									break;
								case '8':
									num3 = scrMisc.incrementAngle(__instance.listFloors.Last<scrFloor>().entryangle, -2.243994951248169);
									break;
								case '9':
									num3 = scrMisc.incrementAngle(__instance.listFloors.Last<scrFloor>().entryangle, 3.665191411972046);
									break;
								case 'A':
									num3 = 1.832595705986023;
									break;
								case 'B':
									num3 = 2.6179938316345215;
									break;
								case 'C':
									num3 = 2.356194496154785;
									break;
								case 'D':
									num3 = 3.1415927410125732;
									break;
								case 'E':
									num3 = 0.7853981852531433;
									break;
								case 'F':
									num3 = 3.665191411972046;
									break;
								case 'G':
									num3 = 5.759586334228516;
									break;
								case 'H':
									num3 = 5.235987663269043;
									break;
								case 'J':
									num3 = 1.0471975803375244;
									break;
								case 'L':
									num3 = 4.71238899230957;
									break;
								case 'M':
									num3 = 2.094395160675049;
									break;
								case 'N':
									num3 = 4.188790321350098;
									break;
								case 'Q':
									num3 = 5.4977874755859375;
									break;
								case 'R':
									num3 = 1.5707963705062866;
									break;
								case 'T':
									num3 = 0.5235987901687622;
									break;
								case 'U':
									num3 = 0.0;
									break;
								case 'V':
									num3 = 3.4033920764923096;
									break;
								case 'W':
									num3 = 4.974188327789307;
									break;
								case 'Y':
									num3 = 2.879793167114258;
									break;
								case 'Z':
									num3 = 3.9269909858703613;
									break;
								case '[':
									if (isEditor) {
										bool flag2 = false;
										int num4 = i + 1;
										bool flag3 = false;
										if (i + 1 <= text.Length && text[i + 1] == '*') {
											flag3 = true;
											num4++;
										}
										while (i + 1 <= text.Length && !flag2) {
											i++;
											if (text[i] == ']') {
												flag2 = true;
											}
										}
										string s = text.Substring(num4, i - num4).Replace(" ", "");
										float num5 = 0f;
										if (float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out num5)) {
											float num6 = 0.017453292f * num5;
											num3 = (flag3 ? scrMisc.incrementAngle(__instance.listFloors.Last<scrFloor>().entryangle, (double)num6) : ((double)num6));
										}
									}
									break;
								case 'h':
									num3 = scrMisc.incrementAngle(__instance.listFloors.Last<scrFloor>().entryangle, 2.094395160675049);
									break;
								case 'j':
									num3 = scrMisc.incrementAngle(__instance.listFloors.Last<scrFloor>().entryangle, -2.094395160675049);
									break;
								case 'o':
									num3 = 0.2617993950843811;
									break;
								case 'p':
									num3 = 1.3089969158172607;
									break;
								case 'q':
									num3 = 6.021385669708252;
									break;
							}
						} else {
							num3 = (double)((float)__instance.listFloors.Last<scrFloor>().entryangle);
						}
					} else if (c != 't') {
						if (c != 'x') {
							if (c == 'y') {
								num3 = scrMisc.incrementAngle(__instance.listFloors.Last<scrFloor>().entryangle, (double)(1.0471976f * (float)(flag ? -1 : 1)));
							}
						} else {
							num3 = 4.450589656829834;
						}
					} else {
						num3 = scrMisc.incrementAngle(__instance.listFloors.Last<scrFloor>().entryangle, (double)(1.0471976f * (float)(flag ? 1 : -1)));
					}
					Vector3 vectorFromAngle = scrMisc.getVectorFromAngle(num3, radius);
					vector += vectorFromAngle;
					if (__instance.listFloors.Count > 0) {
						__instance.listFloors[__instance.listFloors.Count - 1].exitangle = num3;
					}
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(__instance.floor, vector, default(Quaternion));
					gameObject2.name = (i + 1).ToString() + "/Floor" + text[i].ToString();
					gameObject2.gameObject.transform.parent = gameObject.transform;
					scrFloor component2 = gameObject2.GetComponent<scrFloor>();
					__instance.listFloors.Last<scrFloor>().nextfloor = component2;
					__instance.listFloors.Add(component2);
					component2.direction = text[i];
					component2.seqID = num2;
					component2.entryangle = (num3 + 3.1415927410125732) % 6.2831854820251465;
					if (text[i] == '!') {
						__instance.listFloors[num2 - 1].midSpin = true;
					}
					bool flag4 = true;
					while (flag4 && i < text.Length - 1 && !"UDLRQEZCthTFGByjHJMN!56789[qWVYAxop".Contains(text[i + 1])) {
						i++;
						flag4 = true;
						if (text[i] == 'S') {
							component2.speed = 0.25f;
						} else if (text[i] == 'X') {
							component2.speed = 0.5f;
						} else if (text[i] == 'O') {
							component2.speed = 2f;
						} else if (text[i] == 'P') {
							component2.speed = 4f;
						} else if (text[i] == '>') {
							component2.iconsprite.sprite = scrController.instance.sprIconRabbit;
							component2.floorIcon = FloorIcon.Rabbit;
							component2.isspecial = true;
							num *= 2f;
							if (__instance.lm2.BigTiles) {
								component2.iconsprite.transform.Rotate(Vector3.back, 180f);
							}
						} else if (text[i] == '*') {
							component2.iconsprite.sprite = scrController.instance.sprIconDoubleRabbit;
							RDBaseDll.printem("or __instance?? " + i);
							component2.floorIcon = FloorIcon.DoubleRabbit;
							component2.isspecial = true;
							num *= 4f;
							if (__instance.lm2.BigTiles) {
								component2.iconsprite.transform.Rotate(Vector3.back, 180f);
							}
						} else if (text[i] == '_') {
							component2.iconsprite.sprite = scrController.instance.sprIconRabbit;
							component2.floorIcon = FloorIcon.Rabbit;
							component2.isspecial = true;
							num /= 0.75f;
							if (__instance.lm2.BigTiles) {
								component2.iconsprite.transform.Rotate(Vector3.back, 180f);
							}
						} else if (text[i] == '<') {
							component2.iconsprite.sprite = scrController.instance.sprIconSnail;
							component2.floorIcon = FloorIcon.Snail;
							component2.isspecial = true;
							num /= 2f;
							if (__instance.lm2.BigTiles) {
								component2.iconsprite.transform.Rotate(Vector3.back, 180f);
							}
						} else if (text[i] == '%') {
							component2.iconsprite.sprite = scrController.instance.sprIconDoubleSnail;
							component2.floorIcon = FloorIcon.DoubleSnail;
							component2.isspecial = true;
							num /= 4f;
							if (__instance.lm2.BigTiles) {
								component2.iconsprite.transform.Rotate(Vector3.back, 180f);
							}
						} else if (text[i] == '-') {
							component2.iconsprite.sprite = scrController.instance.sprIconSnail;
							component2.floorIcon = FloorIcon.Snail;
							component2.isspecial = true;
							num *= 0.75f;
							if (__instance.lm2.BigTiles) {
								component2.iconsprite.transform.Rotate(Vector3.back, 180f);
							}
						} else if (text[i] == '/') {
							component2.changeDir = true;
							component2.floorIcon = FloorIcon.Swirl;
							flag = !flag;
						}
					}
					component2.isCCW = !flag;
					component2.speed = num;
					if (i == text.Length - 1 && scrController.isGameWorld) {
						component2.isportal = true;
						component2.levelnumber = -1;
					}
					num2++;
				}
				if (__instance.is3D) {
					Floors = new List<scrFloor>(__instance.listFloors.ToArray().DeepCopy<scrFloor[]>());
					__result = __instance.listFloors;
					return false;
				}
				__instance.lm2 = __instance.GetComponent<scrLevelMaker2>();
				int num7 = 0;
				__instance.listFloors.Last<scrFloor>().exitangle = __instance.listFloors.Last<scrFloor>().entryangle + 3.1415927410125732;
				for (int j = 0; j < __instance.listFloors.Count; j++) {
					int num8;
					for (num8 = Mathf.FloorToInt((float)UnityEngine.Random.Range(0, __instance.lm2.arrBend.Length)); num8 == num7; num8 = Mathf.FloorToInt((float)UnityEngine.Random.Range(0, __instance.lm2.arrBend.Length))) {
					}
					num7 = num8;
					scrFloor scrFloor2 = __instance.listFloors[j];
					scrFloor2.sprBend = __instance.lm2.arrBend[num8];
					scrFloor2.sprStraight = __instance.lm2.arrStraight[num8];
					scrFloor2.styleNum = 0;
					if (__instance.isgameworld) {
						scrFloor2.UpdateAngle(true);
					}
					scrFloor2.SetTileColor(__instance.lm2.tilecolor);
					int num9 = 100 + (__instance.listFloors.Count - j);
					num9 *= 2;
					scrFloor2.floorRenderer.renderer.sortingOrder = num9;
					scrFloor2.iconsprite.sortingOrder = num9 + 2;
					scrFloor2.outlineSprite.sortingOrder = num9 + 1;
					scrFloor2.topglow.sortingOrder = num9 + 3;
					scrFloor2.startPos = scrFloor2.transform.position;
					scrFloor2.startRot = scrFloor2.transform.rotation.eulerAngles;
					scrFloor2.tweenRot = scrFloor2.startRot;
					scrFloor2.offsetPos = Vector3.zero;
				}
				__result = __instance.listFloors;
				return false;
			}
		}*/

		[HarmonyPatch(typeof(scrUIController), "WipeToBlack")]
		private static class OfficialCheckpointStartPatch {
			public static void Prefix(scrController __instance) {
				L.og(GCS._checkpointNum);
				//L.og(_currentSeqID);
				if ( RandomTweaks.settings.DisableRestartAtCheckpoint) {
					GCS._checkpointNum = 0;
				}
			}
		}

		[HarmonyPatch(typeof(scnCLS), "Start")]
		private static class ClsStartPatch {
			public static void Postfix() {
				PlayEditorLevelPatch.IsNotPlayingAlone = true;
			}
		}

		[HarmonyPatch(typeof(scnEditor), "Update")]
		private static class PlayEditorLevelPatch {
			public static bool IsNotPlayingAlone;
			public static bool Prefix(scnEditor __instance, ref bool ___showingPopup) {

				bool flag = (!(__instance.eventSystem.currentSelectedGameObject != null) || !(__instance.eventSystem.currentSelectedGameObject.GetComponent<InputField>() != null)) && !___showingPopup && RandomTweaks.settings.PlayCLSInEditor;
				if (flag) {
					if (Input.GetKeyDown(KeyCode.P) && ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) && ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))) && !GCS.standaloneLevelMode) {
						if (__instance.levelPath == null) {
							__instance.ShowPopup(true, scnEditor.PopupType.SaveBeforeLevelExport);
							return true;
						}
						GCS.standaloneLevelMode = true;
						PlayEditorLevelPatch.IsNotPlayingAlone = false;
						GCS.customLevelPaths = CustomLevel.GetWorldPaths(__instance.levelPath, false, true);
						SceneManager.LoadScene("scnEditor");
						return false;
					} else {
						if (Input.GetKeyDown(KeyCode.Escape)) {
							if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
								GCS.standaloneLevelMode = false;
								GCS.customLevelPaths = null;
								__instance.SwitchToEditMode(false);
								return false;
							} else { }
						}
					}
				}
				return true;
			}
		}
	}
}
