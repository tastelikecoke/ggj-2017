using UnityEngine;
using System.Collections;

public enum EnemyColor {

	Red, Yellow, Blue,
	Orange, Green, Violet

}


public static class EnemyColorExtensions {

	public static EnemyColor GetEnemyColorFromPlayerNumber(int number) {
		return number == 1 ? EnemyColor.Red : (number == 2 ? EnemyColor.Yellow : EnemyColor.Blue);
	}

	public static int GetDifficulty(this EnemyColor color) {
		switch (color) {
		case EnemyColor.Red:
		case EnemyColor.Yellow:
		case EnemyColor.Blue:
			return 1;
		case EnemyColor.Orange:
		case EnemyColor.Green:
		case EnemyColor.Violet:
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
		case EnemyColor.Blue:
			return Color.blue;
		case EnemyColor.Orange:
			return new Color(1f, 0.5f, 0f);
		case EnemyColor.Green:
			return new Color(0f, 1f, 0f);
		case EnemyColor.Violet:
			return new Color(1f, 0f, 1f);
		}
		return Color.black;
	}

}