using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Behaviours")]
    [SerializeField] private Transform _headTransform;

    [Header("Inputs")]
    [SerializeField] private KeyCode _interactKey = KeyCode.E; // Tecla para la interacción general
    [SerializeField] private KeyCode _dropKey = KeyCode.Q; // Tecla para soltar objetos
    [SerializeField] private KeyCode _rotateBridgeKey = KeyCode.F; // Tecla para rotar el puente
    [Range(50.0f, 1000.0f)][SerializeField] private float _mouseSensitivity = 300.0f;
    private bool _isActive = false;
    [SerializeField] GameObject _fireInteraction;
    [SerializeField] Material _material;

    [Header("Physics")]
    [SerializeField] private LayerMask _intMask;
    [SerializeField] private LayerMask _movMask;
    [SerializeField] private float _intRayDist = 2.0f;
    [SerializeField] private float _movRayDist = 0.75f;
    [SerializeField] private float _movSpeed = 5.0f;

    [SerializeField] private Rigidbody _heldObject;
    [SerializeField] private float _throwForce = 5.0f;

    [SerializeField] private string _sqrDistName = "_DistDist";
    private Renderer[] _renderers;

    private float _DistDist = 0.0f;

    private float _xAxis, _zAxis, _inputMouseX, _inputMouseY, _mouseX;
    private Vector3 _dir;

    private Camera _camera;
    private FirstPersonCamera _camControl;
    private Rigidbody _rb;

    private Ray _intRay, _movRay;
    private RaycastHit _intHit;

    [Header("Bridge Rotation Settings")]
    [SerializeField] private Transform bridge; // Asigna el cubo en forma de rectángulo
    [SerializeField] private Transform emptyObject; // Asigna el empty object para la rotación
    [SerializeField] private float rotationAngle = 45f; // Ángulo de rotación
    [SerializeField] private float rotationDuration = 1f; // Duración de la rotación en segundos
    private Quaternion targetRotation;
    private bool isRotating = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Start()
    {
        if (!GameManager.Instance)
        {
            Debug.LogError($"No GameManager found on scene.");
        }

        _camera = Camera.main;

        _camControl = Camera.main.GetComponent<FirstPersonCamera>();
        _camControl.Head = _headTransform;

        if (_heldObject != null)
        {
            _heldObject.isKinematic = true;
        }

        // Inicializa la rotación del puente
        targetRotation = emptyObject.localRotation * Quaternion.Euler(rotationAngle, 0, 0);
    }

    private void Update()
    {
        _xAxis = Input.GetAxis($"Horizontal");
        _zAxis = Input.GetAxis($"Vertical");
        _dir = (transform.right * _xAxis + transform.forward * _zAxis).normalized;

        _inputMouseX = Input.GetAxisRaw($"Mouse X");
        _inputMouseY = Input.GetAxisRaw($"Mouse Y");

        if (_inputMouseX != 0 || _inputMouseY != 0)
        {
            Rotation(_inputMouseX, _inputMouseY);
        }

        if (Input.GetKeyDown(_interactKey))
        {
            OnInteract();
        }

        if (Input.GetKeyDown(_dropKey))
        {
            DropObject();
        }

        // Rotación del puente al presionar F
        if (Input.GetKeyDown(_rotateBridgeKey) && !isRotating)
        {
            StartCoroutine(RotateBridgeCoroutine()); // Rota el puente cuando presionas la tecla F
            GameManager.Instance.onElectricity = true;
        }

        if (_isActive == true)
        {
            _material.SetFloat(_sqrDistName, 0);
        }
        else
        {
            _material.SetFloat(_sqrDistName, 1);
        }
    }

    private void FixedUpdate()
    {
        if (_xAxis != 0 || _zAxis != 0 && !IsBlocked(_dir))
        {
            Movement(_dir);
        }
    }

    private bool IsBlocked(Vector3 dir)
    {
        _movRay = new Ray(transform.position, dir);

        return Physics.Raycast(_movRay, _movRayDist, _movMask);
    }

    private void Movement(Vector3 dir)
    {
        _rb.MovePosition(transform.position + dir * _movSpeed * Time.fixedDeltaTime);
    }

    private void Rotation(float x, float y)
    {
        _mouseX += x * _mouseSensitivity * Time.deltaTime;

        if (_mouseX >= 360 || _mouseX <= -360)
        {
            _mouseX -= 360 * Mathf.Sign(_mouseX);
        }

        y *= _mouseSensitivity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0f, _mouseX, 0f);

        _camControl?.Rotation(_mouseX, y);
    }

    public void OnInteract()
    {
        _isActive = !_isActive;
        _fireInteraction.SetActive(_isActive);
    }

    private void DropObject()
    {
        if (_heldObject == null) return;

        _heldObject.transform.parent = null;

        _heldObject.isKinematic = false;

        Vector3 forwardForce = transform.forward * _throwForce;
        _heldObject.AddForce(forwardForce, ForceMode.Impulse);

        _heldObject = null;
    }

    // Corutina que rota el puente de forma suave
    private IEnumerator RotateBridgeCoroutine()
    {
        isRotating = true;
        float elapsedTime = 0f;
        Quaternion initialRotation = emptyObject.localRotation;

        // Realiza la rotación de forma suave
        while (elapsedTime < rotationDuration)
        {
            emptyObject.localRotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegura que la rotación final sea la correcta
        emptyObject.localRotation = targetRotation;
        isRotating = false;
    }
}


