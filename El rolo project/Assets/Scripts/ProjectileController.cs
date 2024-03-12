using UnityEngine;
using UnityEngine.Video;

public class ProjectileController : MonoBehaviour
{
    public float speed;
    [SerializeField] private float timeLimit;
    [SerializeField] private float timeDestroy;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        Destruir();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobar la etiqueta del objeto con el que colisiona
        if (other.CompareTag("Enemigos"))
        {
            // Si el proyectil colisiona con otro objeto (que no sea el jugador),
            // puedes agregar lógica adicional aquí (por ejemplo, hacer que el enemigo tome daño).

            Debug.Log("Golpeo al enemigo");
            other.GetComponent<EnemyController>().recibioDaño = true;
            other.GetComponent<EnemyController>().vida--;
            Destroy(gameObject);
        }
    }

    public void Initialize()
    {
        // Método de inicialización, puedes añadir lógica aquí según tus necesidades
    }
    
    void Destruir()
    {
        timeDestroy += 1 * Time.deltaTime;

        if (timeDestroy >= timeLimit)
        {
            Destroy(gameObject);
        }
    }
}