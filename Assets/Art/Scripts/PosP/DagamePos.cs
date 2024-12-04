using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePos : MonoBehaviour
{
    [Header("<color=green>Rendering</color>")]
    [SerializeField] private string _sqrDistName = "_DistDist";

    private float _DistDist = 0.0f;

    private Renderer[] _renderers;
    [SerializeField] Material _damage;
    [SerializeField] GameObject _body;

    private void Start()
    {

    }

    private void Update()
    {
        //_DistDist = Vector3.SqrMagnitude(_body.transform.position - GameManager.Instance.player.transform.position);

       /* if (_DistDist <= 100 && GameManager.Instance.onFire == true)
        {
            _damage.SetFloat(_sqrDistName, 0);
        }
        else
        {
            _damage.SetFloat(_sqrDistName, 1);
        }*/
        if (Input.GetKeyDown(KeyCode.R))
        {
            FullScreenManager.Instance.M_Damage.SetFloat(FullScreenManager.Instance.DistanceName, 0);
            _damage.SetFloat("_DistDist", 0);
        }
    }
}
