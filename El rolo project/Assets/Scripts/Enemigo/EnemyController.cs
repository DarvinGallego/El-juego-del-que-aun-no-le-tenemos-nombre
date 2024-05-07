using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [Header("Variables de distancia al jugador")]
    public Transform PJ;
    [SerializeField] private float distancia;
    [Range(0, 2)][SerializeField] private float distanciaLimite;

    [Header("Variables de tiempo de patrullaje")]
    [SerializeField] private int rutina;
    [SerializeField] private float cronometro;

    [Header("Variables de patrullaje en plataformas")]
    public LayerMask abajo;
    public LayerMask enfrente;
    public float distanciaAbajo;
    public float distanciaEnfrente;
    public Transform NocionA;
    public Transform NocionE;
    public bool infoAbajo;
    public bool infoEnfrente;

    [Header("Variables de ataque")]
    [Range(0, 1)][SerializeField] private float rangoAtaque;
    public Collider2D hit;
    [SerializeField] private bool puedeAtacar;

    [Header("Variables de daño recibido")]
    public int vidaMax;
    public int vida;
    public bool recibioDaño;
    [SerializeField] private float empuje;

    [Header("Variables generales")]
    public float velocidad;
    public float velocidadLimite;
    [SerializeField] private int direccionGiro;
    [SerializeField] private bool lookLeft;
    public EstadoEnemigo estado = EstadoEnemigo.Patrullando;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(CronometroPatrullar());
    }

    // Update is called once per frame
    void Update()
    {
        infoEnfrente = Physics2D.Raycast(NocionE.position, transform.right * -1, distanciaEnfrente, enfrente);
        infoAbajo = Physics2D.Raycast(NocionA.position, transform.up * -1, distanciaAbajo, abajo);
        distancia = Vector2.Distance(transform.position, PJ.position);
        Salud();

        if (!recibioDaño)
        {
            if (distancia < distanciaLimite && distancia > rangoAtaque)
            {
                estado = EstadoEnemigo.Persiguiendo;
            }
            else if (distancia > distanciaLimite)
            {
                estado = EstadoEnemigo.Patrullando;
            }
            else if (distancia < rangoAtaque)
            {
                estado = EstadoEnemigo.Atacando;
            }
        }
        else
        {
            estado = EstadoEnemigo.Herido;
        }

        switch (estado)
        {
            case EstadoEnemigo.Patrullando:
                Patrullar();
                break;
            case EstadoEnemigo.Persiguiendo:
                Perseguir();    
                break;
            case EstadoEnemigo.Atacando:
                Atacar();
                break;
            case EstadoEnemigo.Herido:
                Herido();
                break;
        }
    }

    public void Patrullar()
    {
        if (infoEnfrente || !infoAbajo)
        {
            lookLeft = !lookLeft;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
        else
        {
            switch (rutina)
            {
                case 0:
                    animator.SetBool("walk", false);
                    break;

                case 1:
                    lookLeft = Random.value < 0.5f;
                    rutina++;
                    break;

                case 2:
                    switch (lookLeft)
                    {
                        case true:
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            transform.Translate(Vector3.left * velocidad * Time.deltaTime);
                            break;

                        case false:
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                            transform.Translate(Vector3.left * velocidad * Time.deltaTime);
                            break;
                    }
                    animator.SetBool("walk", true);
                    break;
            }
        }
    }

    public void Perseguir()
    {
        if (transform.position.x < PJ.position.x)
        {
            lookLeft = false;
        }
        else
        {
            lookLeft = true;
        }

        switch (lookLeft)
        {
            case false:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.position = Vector2.MoveTowards(transform.position, PJ.position, velocidadLimite * Time.deltaTime);
                break;

            case true:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.position = Vector2.MoveTowards(transform.position, PJ.position, velocidadLimite * Time.deltaTime);
                break;
        }

        animator.SetBool("walk", true);     
    }

    public void Herido()
    {
        animator.SetBool("damaged", recibioDaño);
        animator.SetBool("walk", false);
        animator.SetBool("atack", false);

        if (transform.position.x < PJ.position.x)
        {
            lookLeft = false;
        }
        else
        {
            lookLeft = true;
        }

        switch (lookLeft)
        {
            case false:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.Translate(Vector3.left * empuje * Time.deltaTime, Space.World);
                break;

            case true:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(Vector3.right * empuje * Time.deltaTime, Space.World);
                break;
        }
    }

    public void Salud()
    {
        if (vida <= 0)
        {
            transform.gameObject.SetActive(false);
            vida = vidaMax;
        }
    }

    public void FinHerido()
    {
        recibioDaño = false;
        animator.SetBool("damaged", recibioDaño);
    }

    public void Atacar()
    {
        animator.SetBool("walk", false);
        animator.SetBool("atack", true);
    }
    
    public void PuñoInicio()
    {
        hit.enabled = true;
    }

    public void PuñoFin()
    {
        hit.enabled = false;
        animator.SetBool("walk", true);
        animator.SetBool("atack", false);
    }

    IEnumerator CronometroPatrullar()
    {
        while (true)
        {
            rutina = Random.Range(0, 2);
            yield return new WaitForSeconds(cronometro);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(NocionA.transform.position, NocionA.transform.position + transform.up * -1 * distanciaAbajo);
        Gizmos.DrawLine(NocionE.transform.position, NocionE.transform.position + transform.right * -1 * distanciaEnfrente);

    }
}