using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCombatEnemy : MonoBehaviour
{
    public float disparoECD;
    public float distanciaApuntar;
    public bool enRango;
    public bool puedeDisparar;
    public LayerMask disparar;
    public Transform puntoDisparo;
    public GameObject proyectilE;
    private EnemyController EnemyC;

    // Start is called before the first frame update
    void Start()
    {
        EnemyC = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        enRango = Physics2D.Raycast(puntoDisparo.position, transform.right * -1, distanciaApuntar, disparar);
        DisparoE();
    }

    void DisparoE()
    {
        if (proyectilE != null && puntoDisparo != null)
        {
            if (enRango && !puedeDisparar && EnemyC.estado == EstadoEnemigo.Persiguiendo)
            {
                puedeDisparar = true;
                GameObject projectile = Instantiate(proyectilE, puntoDisparo.position, puntoDisparo.rotation);

                // Asegúrate de que el proyectil tenga un script ProjectileController adjunto
                ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
                if (projectileController != null)
                {
                    projectileController.Initialize(); // Inicializar el proyectil
                }
                StartCoroutine(DisparoEnemigoCD());
            }
        }
        else
        {
            Debug.LogWarning("Prefab del proyectil o punto de disparo no asignados en el Inspector.");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(puntoDisparo.transform.position, puntoDisparo.transform.position + transform.right * -1 * distanciaApuntar);
    }

    IEnumerator DisparoEnemigoCD()
    {
        yield return new WaitForSeconds(disparoECD);
        puedeDisparar = false;
    }
}