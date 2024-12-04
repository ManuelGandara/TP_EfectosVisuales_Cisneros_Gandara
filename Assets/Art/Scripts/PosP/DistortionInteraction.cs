using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortionInteraction : MonoBehaviour
{
    [Header("<color=green>Rendering</color>")]
    [SerializeField] private string _sqrDistName = "_DistDist";

    private float _DistDist = 0.0f;

    private Renderer[] _renderers;
    [SerializeField] Material _fire;
    [SerializeField] GameObject _chimenea;

    private void Start()
    {
        
    }

    private void Update()
    {
        _DistDist = Vector3.SqrMagnitude(_chimenea.transform.position - GameManager.Instance.player.transform.position);

        if (_DistDist <= 100)
        {
            _fire.SetFloat(_sqrDistName, 0);
        }
        else
        {
            _fire.SetFloat (_sqrDistName, 1);
        }

    }
}
