using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainHeroMovementController : MonoBehaviour
{
	private static readonly int IsLiftedAnimatorProperty = Animator.StringToHash("IsLifted");
	private InputAction moveForward;
	private InputAction moveBackward;
	private InputAction rotateRight;
	private InputAction rotateLeft;
	private InputAction lift;
	
	private bool movingForward = false;
	private bool movingBackward = false;
	private bool rotatingRight = false;
	private bool rotatingLeft = false;

	private bool isLifted;
	private bool IsLifted
	{
		get => isLifted;
		set
		{
			anim.SetBool(IsLiftedAnimatorProperty, value);
			isLifted = value;
		}
	}

	[Header("Movement Settings")]
	[SerializeField] private float forwardMoveSpeed;
	[SerializeField] private float backwardMoveSpeed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float maxAngleToDrive;
	
	[Header("Configuration")]
	[SerializeField] private Rigidbody rb;
	[SerializeField] private Animator anim;
	[SerializeField] private CustomEvent heroStuck;


	private void OnEnable()
	{
		moveForward = InputSystem.actions.FindAction("MoveForward");
		moveBackward = InputSystem.actions.FindAction("MoveBackward");
		rotateRight = InputSystem.actions.FindAction("RotateRight");
		rotateLeft = InputSystem.actions.FindAction("RotateLeft");
		lift = InputSystem.actions.FindAction("lift");

		moveForward.performed += OnMoveForwardStarted;
		moveBackward.performed += OnMoveBackwardStarted;
		rotateRight.performed += OnRotateRightStarted;
		rotateLeft.performed += OnRotateLeftStarted;
		lift.performed += OnLiftToggled;
		
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
		lift.performed -= OnLiftToggled;
		
		moveForward.canceled -= OnMoveForwardEnded;
		moveBackward.canceled -= OnMoveBackwardEnded;
		rotateRight.canceled -= OnRotateRightEnded;
		rotateLeft.canceled -= OnRotateLeftEnded;
		
		movingForward = false;
		movingBackward = false;
		rotatingRight = false;
		rotatingLeft = false;
		IsLifted = false;
	}

	private void OnLiftToggled(InputAction.CallbackContext obj)
	{
		IsLifted = !IsLifted;
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

	private void Update()
	{
		if (Vector3.Angle(Vector3.down, -transform.up) > maxAngleToDrive)
		{
			enabled = false;
			StartCoroutine(Stuck());
			return;
		}

		if (!movingForward && !movingBackward && !rotatingRight && !rotatingLeft)
		{
			return;
		}
		
		Vector3 position = rb.position;
		Vector3 rotation = rb.rotation.eulerAngles;
		if (movingForward)
		{
			position += forwardMoveSpeed * Time.deltaTime * transform.forward;
		}
		else if (movingBackward)
		{
			position -= backwardMoveSpeed * Time.deltaTime * transform.forward;
		}

		if (rotatingRight)
		{
			rotation += rotationSpeed * Time.deltaTime * transform.up;
		}
		else if (rotatingLeft)
		{
			rotation -= rotationSpeed * Time.deltaTime * transform.up;
		}
		rb.Move(position, Quaternion.Euler(rotation));
	}

	private IEnumerator Stuck()
	{
		yield return new WaitForSeconds(4f);
		heroStuck.Fire();
	}
}
