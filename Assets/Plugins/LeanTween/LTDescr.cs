﻿using System;
using UnityEngine;


public interface LTDescr
{
	bool toggle { get; set; }
	bool useEstimatedTime { get; set; }
	bool useFrames { get; set; }
	bool useManualTime { get; set; }
	bool hasInitiliazed { get; set; }
	bool hasExtraOnCompletes { get; }
	bool hasPhysics { get; set; }
	bool onCompleteOnRepeat { get; set; }
	bool onCompleteOnStart { get; set; }
	bool useRecursion { get; set; }
    float ratioPassed { get; set; }
	float passed { get; set; }
	float delay { get; set; }
	float time { get; set; }
	float lastVal { get; set; }
	int loopCount { get; set; }
	uint counter { get; set; }
	float direction { get; set; }
	float directionLast { get; set; }
	float overshoot { get; set; }
	float period { get; set; }
	bool destroyOnComplete { get; set; }
	Transform trans { get; set; }
	Vector3 from { get; set; }
	Vector3 to { get; set; }

	TweenAction type { get; set; }
	LeanTweenType tweenType { get; set; }

	LeanTweenType loopType { get; set; }
	bool hasUpdateCallback { get; set; }

	LTDescrOptional optional { get; set; }

	LTDescrImpl.EaseTypeDelegate easeMethod { get; set; }
	LTDescrImpl.ActionMethodDelegate easeInternal {get; set; }
	LTDescrImpl.ActionMethodDelegate initInternal {get; set; }
	[System.Obsolete("Use 'LeanTween.cancel( id )' instead")]
	LTDescr cancel(UnityEngine.GameObject gameObject);
	int uniqueId { get; }
	int id { get; }
	#if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_0_1 && !UNITY_4_1 && !UNITY_4_2
	SpriteRenderer spriteRen{ get; set; }
	#endif
	bool update2();
	void callOnCompletes();
	LTDescr pause();
	void reset();
	LTDescr resume();
	LTDescr setAudio(object audio);
	LTDescr setAxis(UnityEngine.Vector3 axis);
	LTDescr setDelay(float delay);
	LTDescr setDestroyOnComplete(bool doesDestroy);
	LTDescr setDiff(UnityEngine.Vector3 diff);
	LTDescr setDirection(float direction);
	LTDescr setEase(LeanTweenType easeType);
	LTDescr setEaseLinear();
	LTDescr setEaseSpring();
	LTDescr setEaseOutQuad();
	LTDescr setEaseInQuad();
	LTDescr setEaseInOutQuad();
	LTDescr setEaseInCubic();
	LTDescr setEaseOutCubic();
	LTDescr setEaseInOutCubic();
	LTDescr setEaseInQuart();
	LTDescr setEaseOutQuart();
	LTDescr setEaseInOutQuart();
	LTDescr setEaseInQuint();
	LTDescr setEaseOutQuint();
	LTDescr setEaseInOutQuint();
	LTDescr setEaseInSine();
	LTDescr setEaseOutSine();
	LTDescr setEaseInOutSine();
	LTDescr setEaseInExpo();
	LTDescr setEaseOutExpo();
	LTDescr setEaseInOutExpo();
	LTDescr setEaseInCirc();
	LTDescr setEaseOutCirc();
	LTDescr setEaseInOutCirc();
	LTDescr setEaseInBounce();
	LTDescr setEaseOutBounce();
	LTDescr setEaseInOutBounce();
	LTDescr setEaseInBack();
	LTDescr setEaseOutBack();
	LTDescr setEaseInOutBack(); 
	LTDescr setEaseInElastic(); 
	LTDescr setEaseOutElastic();
	LTDescr setEaseInOutElastic();
	LTDescr setEasePunch();
	LTDescr setEaseShake();
	LTDescr setEase(UnityEngine.AnimationCurve easeCurve);

	LTDescr setFrom(float from);
	LTDescr setFrom(UnityEngine.Vector3 from);
	LTDescr setFromColor(UnityEngine.Color col);
	LTDescr setHasInitialized(bool has);
	LTDescr setId(uint id);

	LTDescr setMoveX();
	LTDescr setMoveY();
	LTDescr setMoveZ();
	LTDescr setMoveLocalX();
	LTDescr setMoveLocalY();
	LTDescr setMoveLocalZ();
	LTDescr setMoveCurved();
	LTDescr setMoveCurvedLocal();
	LTDescr setMoveSpline();
	LTDescr setMoveSplineLocal();
	LTDescr setScaleX();
	LTDescr setScaleY();
	LTDescr setScaleZ();
	LTDescr setRotateX();
	LTDescr setRotateY();
	LTDescr setRotateZ();
	LTDescr setRotateAround();
	LTDescr setRotateAroundLocal();
	LTDescr setAlpha();
	LTDescr setTextAlpha();
	LTDescr setAlphaVertex();
	LTDescr setColor();
	LTDescr setCallbackColor();
	LTDescr setTextColor();
	LTDescr setCanvasAlpha();
	LTDescr setCanvasGroupAlpha();
	LTDescr setCanvasColor();
	LTDescr setCanvasMoveX();
	LTDescr setCanvasMoveY();
	LTDescr setCanvasMoveZ();
	LTDescr setCanvasRotateAround();
	LTDescr setCanvasRotateAroundLocal();
	LTDescr setCanvasPlaySprite();
	LTDescr setCallback();
	LTDescr setValue3();
	LTDescr setMove();
	LTDescr setMoveLocal();
	LTDescr setMoveToTransform();
	LTDescr setRotate();
	LTDescr setRotateLocal();
	LTDescr setScale();
	LTDescr setGUIMove();
	LTDescr setGUIMoveMargin();
	LTDescr setGUIScale();
	LTDescr setGUIAlpha();
	LTDescr setGUIRotate();
	LTDescr setDelayedSound();
	LTDescr setCanvasMove();
	LTDescr setCanvasScale();
	LTDescr setCanvasSizeDelta();

	LTDescr setIgnoreTimeScale(bool useUnScaledTime);
	LTDescr setSpeed( float speed );
	LTDescr setLoopClamp();
	LTDescr setLoopClamp(int loops);
	LTDescr setLoopCount(int loopCount);
	LTDescr setLoopOnce();
	LTDescr setLoopPingPong();
	LTDescr setLoopPingPong(int loops);
	LTDescr setLoopType(LeanTweenType loopType);
	LTDescr setOnComplete(Action onComplete);
	LTDescr setOnComplete(Action<object> onComplete);
	LTDescr setOnComplete(Action<object> onComplete, object onCompleteParam);
	LTDescr setOnCompleteOnRepeat(bool isOn);
	LTDescr setOnCompleteOnStart(bool isOn);
	LTDescr setOnCompleteParam(object onCompleteParam);
	LTDescr setOnStart(Action onStart);
	LTDescr setOnUpdate(Action<float, object> onUpdate, object onUpdateParam = null);
	LTDescr setOnUpdate(Action<float> onUpdate);
	LTDescr setOnUpdate(Action<UnityEngine.Color> onUpdate);
	LTDescr setOnUpdate(Action<UnityEngine.Color,object> onUpdate );
	LTDescr setOnUpdate(Action<UnityEngine.Vector2> onUpdate, object onUpdateParam = null);
	LTDescr setOnUpdate(Action<UnityEngine.Vector3, object> onUpdate, object onUpdateParam = null);
	LTDescr setOnUpdate(Action<UnityEngine.Vector3> onUpdate, object onUpdateParam = null);
	LTDescr setOnUpdateColor(Action<UnityEngine.Color> onUpdate);
	LTDescr setOnUpdateColor(Action<UnityEngine.Color,object> onUpdate );
	LTDescr setOnUpdateObject(Action<float, object> onUpdate);
	LTDescr setOnUpdateParam(object onUpdateParam);
	LTDescr setOnUpdateRatio(Action<float, float> onUpdate);
	LTDescr setOnUpdateVector2(Action<UnityEngine.Vector2> onUpdate);
	LTDescr setOnUpdateVector3(Action<UnityEngine.Vector3> onUpdate);
	LTDescr setOrientToPath(bool doesOrient);
	LTDescr setOrientToPath2d(bool doesOrient2d);
	LTDescr setOvershoot(float overshoot);
	LTDescr setPath(LTBezierPath path);
	LTDescr setPeriod(float period);
	LTDescr setPoint(UnityEngine.Vector3 point);
	LTDescr setRect(LTRect rect);
	LTDescr setRect(UnityEngine.Rect rect);
	LTDescr setRepeat(int repeat);
	LTDescr setRecursive(bool useRecursion);
	#if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_0_1 && !UNITY_4_1 && !UNITY_4_2 && !UNITY_4_3 && !UNITY_4_5
	LTDescr setRect(UnityEngine.RectTransform rect);
	LTDescr setSprites(UnityEngine.Sprite[] sprites);
	LTDescr setFrameRate(float frameRate);
	#endif
	LTDescr setTime(float time);
	LTDescr setTo(UnityEngine.Transform to);
	LTDescr setTo(UnityEngine.Vector3 to);
	LTDescr setUseEstimatedTime(bool useEstimatedTime);
	LTDescr setUseFrames(bool useFrames);
	LTDescr setUseManualTime(bool useManualTime);
	string ToString();
}

