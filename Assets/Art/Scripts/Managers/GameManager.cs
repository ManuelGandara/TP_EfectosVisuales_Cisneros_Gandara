using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    public GameObject player;
    public GameObject Body; // Objeto del cuerpo
    public GameObject Torch; // Objeto de la antorcha
    [SerializeField] private GameObject vfxObject; // Objeto vacío con los VFX dentro
    private bool vfxActivated = false; // Bandera para evitar activar el VFX varias veces

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

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si Torch y Body están involucrados en la colisión
        if ((collision.gameObject == Torch && collision.collider.gameObject == Body) ||
            (collision.gameObject == Body && collision.collider.gameObject == Torch))
        {
            ActivateVFX(); // Activa el VFX
        }
    }

    private void Update()
    {
        // Verifica continuamente si los colisionadores se están intersectando
        if (!vfxActivated && Torch.GetComponent<Collider>().bounds.Intersects(Body.GetComponent<Collider>().bounds))
        {
            ActivateVFX();
        }
    }

    private void ActivateVFX()
    {
        if (vfxObject != null)
        {
            vfxObject.SetActive(true); // Activa el objeto vacío con los VFX dentro
            vfxActivated = true; // Asegura que no se vuelva a activar innecesariamente
        }
        else
        {
            Debug.LogWarning("No se asignó un objeto vacío con VFX al GameManager.");
        }
    }
}



#endregion

/*private FirstPersonController _player;
public FirstPersonController Player
{
    get { return _player; }
    set { _player = value; }
}*/
