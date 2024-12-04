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
    [SerializeField] private GameObject vfxObject; // Objeto vac�o con los VFX dentro
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
        // Verifica si Torch y Body est�n involucrados en la colisi�n
        if ((collision.gameObject == Torch && collision.collider.gameObject == Body) ||
            (collision.gameObject == Body && collision.collider.gameObject == Torch))
        {
            ActivateVFX(); // Activa el VFX
        }
    }

    private void Update()
    {
        // Verifica continuamente si los colisionadores se est�n intersectando
        if (!vfxActivated && Torch.GetComponent<Collider>().bounds.Intersects(Body.GetComponent<Collider>().bounds))
        {
            ActivateVFX();
        }
    }

    private void ActivateVFX()
    {
        if (vfxObject != null)
        {
            vfxObject.SetActive(true); // Activa el objeto vac�o con los VFX dentro
            vfxActivated = true; // Asegura que no se vuelva a activar innecesariamente
        }
        else
        {
            Debug.LogWarning("No se asign� un objeto vac�o con VFX al GameManager.");
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
