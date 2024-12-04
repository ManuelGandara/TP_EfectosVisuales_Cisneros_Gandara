using UnityEngine;
using UnityEngine.VFX;

public class ActivateVFXWithLever : MonoBehaviour
{
    [SerializeField] private VisualEffect vfxGraph1;
    [SerializeField] private VisualEffect vfxGraph2;
    [SerializeField] private VisualEffect vfxGraph3;
    [SerializeField] private VisualEffect vfxGraph4; // Arrastra tus VFX Graph aquí en el inspector.
    [SerializeField] private Transform leverHandle; // Arrastra el mango de la palanca.
    [SerializeField] private float activationAngle = -30f; // Ángulo de activación en grados.

    private bool isActivated = false;
    private bool isDragging = false;
    private Quaternion initialRotation;

    private void Start()
    {
        // Guarda la rotación inicial de la palanca.
        initialRotation = leverHandle.localRotation;
    }

    private void Update()
    {
        // Detectar clic para iniciar arrastre.
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.transform == leverHandle)
            {
                isDragging = true;
            }
        }

        // Soltar clic para finalizar arrastre.
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // Arrastrar y mover la palanca.
        if (isDragging)
        {
            DragLever();
        }

        // Calcula el ángulo actual de la palanca.
        float currentAngle = Quaternion.Angle(initialRotation, leverHandle.localRotation);

        // Activa el VFX cuando la palanca baje lo suficiente.
        if (currentAngle >= activationAngle && !isActivated)
        {
            ActivateVFX();
        }
    }

    private void DragLever()
    {
        // Obtén la posición del ratón en el mundo y ajusta la rotación de la palanca.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 direction = hit.point - leverHandle.position;
            direction.y = 0; // Bloquea movimiento en el eje Y.
            leverHandle.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void ActivateVFX()
    {
        isActivated = true;
        vfxGraph1.Play();
        vfxGraph2.Play();
        vfxGraph3.Play();
        vfxGraph4.Play();
        Debug.Log("VFX activado!");
    }
}
