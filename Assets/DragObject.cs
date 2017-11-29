using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour {

	Vector3 screenPoint;
	Vector3 offset;
	Vector3 scanPos;

	Transform trans;

	void Start()
	{
		trans = this.transform;
		scanPos = trans.position;

		_sensitivity = 40f;
		_rotation = Vector3.zero;
	}
	void Update()
	{
		SetDirection ();
	}
	void OnMouseDown()
	{
		// rotating flag
		_isRotating = true;

		// store mouse
		_mouseReference = trans.position;

		screenPoint = Camera.main.WorldToScreenPoint(scanPos);

		offset = scanPos - Camera.main.ScreenToWorldPoint(
			new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

		SlingShotManager.instance.aimer.eulerAngles = new Vector3 (0,0,0);
		SlingShotManager.instance.setPath (true);
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		trans.position = curPosition;

		float posX = Mathf.Clamp (trans.position.x,-1.4f,1.4f);
		float posY = Mathf.Clamp (trans.position.y,-3f,-2.36f);

		trans.position = new Vector3 (posX,posY,curPosition.z);
	}
	void OnMouseUp()
	{
		// rotating flag
		_isRotating = false;
		trans.position = scanPos;

		SlingShotManager.instance.throwBall ();
		Invoke ("ResetDirection",1f);
	}

	void ResetDirection()
	{
		SlingShotManager.instance.aimer.eulerAngles = new Vector3 (0,0,0);
		SlingShotManager.instance.setPath (false);
		SlingShotManager.instance.ObjectHolder.GetComponent<Collider> ().enabled = true;
	}

	private float _sensitivity;
	private Vector3 _mouseReference;
	private Vector3 _mouseOffset;
	private Vector3 _rotation;
	private bool _isRotating;

	void SetDirection()
	{
		if(_isRotating)
		{
			// offset
			_mouseOffset = (trans.position - _mouseReference);

			// apply rotation
			_rotation.z = (_mouseOffset.x) * _sensitivity;

			// rotate
			SlingShotManager.instance.aimer.Rotate (_rotation);

			// store mouse
			_mouseReference = trans.position;
		}
	}
}
