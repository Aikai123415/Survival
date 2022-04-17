using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private float sensitivity = 3;
	[SerializeField] private float limit = 40;
	[SerializeField] private Transform target;
	[SerializeField] private Vector3 offset;
	private RaycastHit _hit;
	private Vector3 _mouseAxis;

	void Update()
	{
		_mouseAxis.x -= Input.GetAxis("Mouse Y");
		_mouseAxis.y += Input.GetAxis("Mouse X");
		_mouseAxis.x = Mathf.Clamp(_mouseAxis.x, -limit, limit);

		transform.eulerAngles = _mouseAxis * sensitivity;
		transform.position = transform.rotation * offset + target.position;
		Debug.Log(transform.rotation);
		if (Physics.Linecast(target.position, transform.position, out _hit))
		{
			transform.position = _hit.point;
		}
	}
}
