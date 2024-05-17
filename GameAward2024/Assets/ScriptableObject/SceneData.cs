using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "ScriptableObjects/SceneData")]
public class SceneData : ScriptableObject
{
	public enum E_SCENE_KIND
	{
		TITLE_SCENE,
		STAGE_SELECT_SCENE,
		GAME_SCENE,
		RESULT_SCENE,
	}

	readonly string[] SCENE_NAMES = {
		"TitleScene",
		"StageSelectScene",
		"GameScene",
		"ResultScene",
	};

	public string GetSceneName(E_SCENE_KIND sceneKind)
	{
		return this.SCENE_NAMES[(int)sceneKind];
	}
}