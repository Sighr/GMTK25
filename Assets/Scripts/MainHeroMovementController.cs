using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainHeroMovementController : MonoBehaviour
{
	private InputAction moveForward;
	private InputAction moveBackward;
	private InputAction rotateRight;
	private InputAction rotateLeft;
	
	private bool movingForward = false;
	private bool movingBackward = false;
	private bool rotatingRight = false;
	private bool rotatingLeft = false;

	[Header("Movement Settings")]
	[SerializeField] private float forwardMoveSpeed;
	[SerializeField] private float backwardMoveSpeed;
	[SerializeField] private float rotationSpeed;
	
	[Header("Configuration")]
	[SerializeField] private Rigidbody rb;
	
	private void OnEnable()
	{
		moveForward = InputSystem.actions.FindAction("MoveForward");
		moveBackward = InputSystem.actions.FindAction("MoveBackward");
		rotateRight = InputSystem.actions.FindAction("RotateRight");
		rotateLeft = InputSystem.actions.FindAction("RotateLeft");

		moveForward.performed += OnMoveForwardStarted;
		moveBackward.performed += OnMoveBackwardStarted;
		rotateRight.performed += OnRotateRightStarted;
		rotateLeft.performed += OnRotateLeftStarted;
		
		moveForward.canceled += OnMoveForwardEnded;
		moveBackward.canceled += OnMoveBackwardEnded;
		rotateRight.canceled += OnRotateRightEnded;
		rotateLeft.canceled += OnRotateLeftEnded;
	}

	private void OnDisable()
	{
		moveForward.performed -= OnMoveForwardStarted;
		moveBackward.performed -= OnMoveBackwardStarted;
		rotateRight.performed -= OnRotateRightStarted;
		rotateLeft.performed -= OnRotateLeftStarted;
		
		moveForward.canceled -= OnMoveForwardEnded;
		moveBackward.canceled -= OnMoveBackwardEnded;
		rotateRight.canceled -= OnRotateRightEnded;
		rotateLeft.canceled -= OnRotateLeftEnded;
	}

	private void OnMoveForwardStarted(InputAction.CallbackContext context)
	{
		movingForward = true;
	}
	
	private void OnMoveForwardEnded(InputAction.CallbackContext context)
	{
		movingForward = false;
	}

	private void OnMoveBackwardStarted(InputAction.CallbackContext context)
	{
		movingBackward = true;
	}
	
	private void OnMoveBackwardEnded(InputAction.CallbackContext context)
	{
		movingBackward = false;
	}

	private void OnRotateRightStarted(InputAction.CallbackContext context)
	{
		rotatingRight = true;
	}
	
	private void OnRotateRightEnded(InputAction.CallbackContext context)
	{
		rotatingRight = false;
	}

	private void OnRotateLeftStarted(InputAction.CallbackContext context)
	{
		rotatingLeft = true;
	}
	
	private void OnRotateLeftEnded(InputAction.CallbackContext context)
	{
		rotatingLeft = false;
	}

	private void FixedUpdate()
	{
		Vector3 position = rb.position;
		Vector3 rotation = rb.rotation.eulerAngles;
		if (movingForward)
		{
			position += forwardMoveSpeed * Time.fixedDeltaTime * transform.forward;
		}
		else if (movingBackward)
		{
			position -= backwardMoveSpeed * Time.fixedDeltaTime * transform.forward;
		}

		if (rotatingRight)
		{
			rotation += rotationSpeed * Time.fixedDeltaTime * transform.up;
		}
		else if (rotatingLeft)
		{
			rotation -= rotationSpeed * Time.fixedDeltaTime * transform.up;
		}
		rb.Move(position, Quaternion.Euler(rotation));
	}
}
