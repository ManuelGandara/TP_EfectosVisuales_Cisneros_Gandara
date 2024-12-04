using UnityEngine;
using UnityEngine.Rendering;

public class DamagePos : MonoBehaviour
{
    [Header("<color=green>Rendering</color>")]
    [SerializeField] private Volume _volume; // Volume asociado
    [SerializeField] private float _triggerDistance = 10f; // Distancia al jugador para activar el efecto

    private Transform _playerTransform;

    private void Start()
    {
        if (GameManager.Instance && GameManager.Instance.player)
        {
            _playerTransform = GameManager.Instance.player.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró el jugador en el GameManager.");
        }
    }

    private void Update()
    {
        if (_playerTransform == null || _volume == null) return;

        // Calcula la distancia al jugador
        float distance = Vector3.Distance(transform.position, _playerTransform.position);

        // Activa o desactiva el volumen según la distancia
        _volume.weight = distance <= _triggerDistance ? 1.0f : 0.0f;
    }
}

