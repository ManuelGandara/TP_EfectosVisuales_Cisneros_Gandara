using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenManager : MonoBehaviour
{
    #region Singleton
    public static FullScreenManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("<color=#7B2FBC>PP</color>")]
    [SerializeField] private Material _M_Damage;
    public Material M_Damage
    { 
        get { return _M_Damage; }
        set { _M_Damage = value; }
    }
    [SerializeField] private string _distanceName = "_DistDist";
    public string DistanceName 
    { 
        get { return _distanceName; }
    }

    
}
