using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWatchUI : MonoBehaviour
{
    internal Transform target;

	public void Awake() {
		transform.localScale = Vector3.one;
	}

	public void Update () {
        if (target == null)
            return;
		transform.localPosition = WorldToCanvas (target.position);
	}
	
	private static Vector2 WorldToCanvas(Vector3 worldPosition, Canvas canvas = null, Camera camera = null)
	{
		if (camera == null) {
			camera = Camera.main;
		}
		if (canvas == null) {
			canvas = GameManager.Instance.MainCanvas;
		}
		
		var viewport_position = camera.WorldToViewportPoint(worldPosition);
		var canvas_rect = canvas.GetComponent<RectTransform>();

		return new Vector2((viewport_position.x * canvas_rect.sizeDelta.x) - (canvas_rect.sizeDelta.x * 0.5f),
			(viewport_position.y * canvas_rect.sizeDelta.y) - (canvas_rect.sizeDelta.y * 0.5f));
	}
}
