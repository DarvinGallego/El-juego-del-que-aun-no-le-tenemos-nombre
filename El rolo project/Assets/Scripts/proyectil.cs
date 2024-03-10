using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        // Mover el proyectil hacia adelante
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobar la etiqueta del objeto con el que colisiona
        if (!other.CompareTag("Player"))
        {
            // Si el proyectil colisiona con otro objeto (que no sea el jugador),
            // puedes agregar lógica adicional aquí (por ejemplo, hacer que el enemigo tome daño).

            // Destruir el proyectil al colisionar con un objeto que no es el jugador
            Destroy(gameObject);
        }
    }

    public void Initialize()
    {
        // Método de inicialización, puedes añadir lógica aquí según tus necesidades
    }
}


