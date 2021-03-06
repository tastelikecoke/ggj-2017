using UnityEngine;
using System.Collections;

public static class Mathfx {

	public const float TAU = Mathf.PI * 2f;

	public static bool IsAngleBetween(float a, float b, float angle) {
		a = ConvertToSmallestAngle(a);
		b = ConvertToSmallestAngle(b);
//		Debug.Log(a + " > " + b + " > " + angle);
		if (b < a) {
			b += 360f;
			angle = ClampToPositiveAngle(angle);
			return angle > a && angle < b;
		} else {
			angle = ConvertToSmallestAngle(angle);
			return angle > a && angle < b;
		}
	}

	public static float ConvertToSmallestAngle(float a) {
		if (a < -180f) {
			return a + 360f;
		}
		if (a > 180f) {
			return a - 360f;
		}
		return a;
	}

	public static float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n) {
		// angle in [0,180]
		float angle = Vector3.Angle(a, b);
		float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(a, b)));
		
		// angle in [-179,180]
		float signed_angle = angle * sign;
		
		// angle in [0,360] (not used but included here for completeness)
		//float angle360 =  (signed_angle + 180) % 360;
		
		return signed_angle;
	}

	public static float ClampAngle(float angle, float min, float max) {
		if (angle < 90 || angle > 270) {       // if angle in the critic region...
			if (angle > 180) {
				angle -= 360;
			}  // convert all angles to -180..+180
			if (max > 180) {
				max -= 360;
			}
			if (min > 180) {
				min -= 360;
			}
		}    
		return Mathf.Clamp(angle, min, max);
	}

	public static float ClampToPositiveAngle(float angle) {
		while (angle < 0) {
			angle += 360;
		}  // if angle negative, convert to 0..360
		while (angle > 360f) {
			angle -= 360f;
		}
		return angle;
	}


	public static float ClockwiseAngle(Vector3 a, Vector3 b, Vector3 n) {
		return Mathf.Atan2(
			Vector3.Dot(n, Vector3.Cross(a, b)),
			Vector3.Dot(a, b)) * Mathf.Rad2Deg;
	}
}
