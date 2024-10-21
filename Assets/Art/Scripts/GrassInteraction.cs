using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("<color=red>Rendering</color>")]
    [SerializeField] private string _sqrDisName = "_SqrDistance";

    private float _sqrDist = 0.0f;

    private Renderer[] _renders;
    private Material[] _materials;

    private void Start()
    {
        _renders = GetComponentsInChildren<Renderer>();

        _materials = new Material[_renders.Length];

        for (int i = 0; i < _renders.Length; i++)
        {
            _materials[i] = _renders[i].material;
        }

    }

    private void Update()
    {
       
    }
}
