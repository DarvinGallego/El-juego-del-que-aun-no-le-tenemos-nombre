using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class ProjectileController : MonoBehaviour
{
    public float speed;
    [SerializeField] private float timeDestroy;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        StartCoroutine(Destruir());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigos"))
        {
            Debug.Log("Golpeo al enemigo");
            other.GetComponent<EnemyController>().recibioDaño = true;
            other.GetComponent<EnemyController>().vida--;
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Golpeo al jugador");
            other.GetComponent<PlayerController>().fueHerido = true;
            other.GetComponent<PlayerController>().vidaPJ--;
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    public void Initialize()
    {
        // Método de inicialización, puedes añadir lógica aquí según tus necesidades
    }

    IEnumerator Destruir()
    {
        yield return new WaitForSeconds(timeDestroy);
        Destroy(gameObject);
    }
}