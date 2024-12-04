using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    public GameObject player;
    public GameObject Body;
    public GameObject Torch;
    [SerializeField] private GameObject vfxObject;

    // Cambiamos VisualEffect por GameObject
    [SerializeField] private GameObject emptyObject1;
    [SerializeField] private GameObject emptyObject2;
    [SerializeField] private GameObject emptyObject3;
    [SerializeField] private GameObject emptyObject4;
    [SerializeField] private GameObject emptyObject5;
    [SerializeField] private GameObject emptyObject6;
    [SerializeField] private GameObject emptyObject7;

    private bool vfxActivated = false;
    public bool onElectricity = false;
    public bool onFire = false;

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
        if ((collision.gameObject == Torch && collision.collider.gameObject == Body) ||
            (collision.gameObject == Body && collision.collider.gameObject == Torch))
        {
            ActivateVFXFire();
        }
    }

    private void Update()
    {
        if (!vfxActivated && Torch.GetComponent<Collider>().bounds.Intersects(Body.GetComponent<Collider>().bounds))
        {
            ActivateVFXFire();
            onFire = true;
        }

        if (onElectricity == true)
        {
            ActivateEmptyObjectsElectricity();
        }
    }

    private void ActivateVFXFire()
    {
        if (vfxObject != null)
        {
            vfxObject.SetActive(true);
            vfxActivated = true;
        }
        else
        {
            Debug.LogWarning("No se asignó un objeto vacío con VFX al GameManager.");
        }
    }

    private void ActivateEmptyObjectsElectricity()
    {
        StartCoroutine(ActivateEmptyObjectsSequence());
    }

    private IEnumerator ActivateEmptyObjectsSequence()
    {
        // Activamos los Empty Objects uno por uno con una pausa de 1 segundo
        emptyObject1.SetActive(true);
        emptyObject2.SetActive(true);
        Debug.Log("Empty Object 1 activado!");
        yield return new WaitForSeconds(1f);

        emptyObject3.SetActive(true);
        emptyObject4.SetActive(true);
        emptyObject5.SetActive(true);
        Debug.Log("Empty Object 2 activado!");
        yield return new WaitForSeconds(1f);

        emptyObject6.SetActive(true);
        emptyObject7.SetActive(true);
        Debug.Log("Empty Object 3 activado!");
    }
}
#endregion
