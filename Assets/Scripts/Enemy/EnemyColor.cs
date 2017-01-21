using UnityEngine;
using System.Collections;

public enum EnemyColor {

	Red, Yellow,
	Orange

}


public static class EnemyColorsExtensions {

	public static int GetDifficulty(this EnemyColor color) {
		switch (color) {
		case EnemyColor.Red:
		case EnemyColor.Yellow:
			return 1;
		case EnemyColor.Orange:
			return 2;
		}
		return 0;
	}

	public static Color GetColor(this EnemyColor color) {
		switch (color) {
		case EnemyColor.Red:
			return Color.red;
		case EnemyColor.Yellow:
			return Color.yellow;
		case EnemyColor.Orange:
			return new Color(1f, 0.5f, 0f);
		}
		return Color.black;
	}

}