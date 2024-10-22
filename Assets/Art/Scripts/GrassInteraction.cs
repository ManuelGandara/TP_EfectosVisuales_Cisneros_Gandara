using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassInteraction : MonoBehaviour
{
    [Header("<color=green>Rendering</color>")]
    [SerializeField] private string _sqrDistName = "_SqrDistance";


    private float _sqrDist = 0.0f;

    private Renderer[] _renderers;
    private Material[] _materials;

    void Start()
    {
        _renderers = GetComponentsInChildren<Renderer>();

        _materials = new Material[_renderers.Length];

        for (int i = 0; i < _renderers.Length; i++) 
        {
            _materials[i] = _renderers[i].material;
        }
    }

    void Update()
    {
        _sqrDist = Vector3.SqrMagnitude(transform.position - GameManager.Instance.transform.position);

        for (int i = 0; i < _materials.Length; i++) 
        {
            _materials[i].SetFloat(_sqrDistName, _sqrDist);
        }
    }
}
