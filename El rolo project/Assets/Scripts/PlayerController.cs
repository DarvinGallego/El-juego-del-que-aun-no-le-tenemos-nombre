using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [Header("Variables de salto y movimiento y dash")]
    public float runSpeed = 2;
    public float jumpForce = 5;
    private bool isGrounded;

    [Header("Variables de dash")]
    public float dashSpeed = 10;
    public float dashDuration = 0.5f;
    public float dashCooldown = 1.0f;
    public float dashDistance = 5.0f;
    private bool isDashing;
    private float dashTimeLeft;
    private float lastDashTime;
    private Vector2 dashDirection;

    [Header("Variables de vida")]
    public int vidaPJMax;
    public int vidaPJ;
    public bool fueHerido;

    [Header("Variables de respawn")]
    public Transform spawn;
    public float respawnCD;
    [SerializeField] private float respawnTime;
    public bool murio;

    [Header("Variables Generales")]
    public float empujePJ;
    private Animator animator;
    private Rigidbody2D rb2D;
    [SerializeField] private bool lookRigth;

    [Header("Proyectil")]
    public Transform firePoint;
    public GameObject projectilePrefab;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        animator.SetBool("Ground", isGrounded);
        animator.SetFloat("isJumping", rb2D.velocity.y);

        Vida();
        Respawn();

        if (!fueHerido && !murio) 
        {
            float moveInput = Input.GetAxis("Horizontal");
            animator.SetFloat("isRunning", Math.Abs(moveInput));

            if (moveInput < 0 && lookRigth)
            {
                Giro(moveInput);
            }
            else if (moveInput > 0 && !lookRigth)
            {
                Giro(moveInput);
            }

            Dash(moveInput);
            Shoot();
        }
        else if (fueHerido)
        {
            Golpeado();
        }
    }

    //Dash
    void Dash(float MoveInput)
    {
        // Verificar el dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDashTime + dashCooldown)
        {
            if (!isDashing)
            {
                isDashing = true;
                dashTimeLeft = dashDuration;
                lastDashTime = Time.time;
                dashDirection = new Vector2(MoveInput, 0).normalized; // Dirección del dash
            }
        }

        if (isDashing)
        {
            rb2D.velocity = new Vector2(dashDirection.x * dashSpeed, dashDirection.y * dashSpeed);

            dashTimeLeft -= Time.deltaTime;

            // Limitar la distancia recorrida durante el dash
            if (dashTimeLeft <= 0 || Vector2.Distance(transform.position, (Vector2)transform.position + rb2D.velocity * Time.deltaTime) >= dashDistance)
            {
                isDashing = false;
                rb2D.velocity = Vector2.zero; // Detener el dash al alcanzar la distancia límite
            }
        }
        else
        {
            // Movimiento horizontal normal
            rb2D.velocity = new Vector2(MoveInput * runSpeed, rb2D.velocity.y);

            // Salto
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    //Dispara 
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (projectilePrefab != null && firePoint != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

                // Asegúrate de que el proyectil tenga un script ProjectileController adjunto
                ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
                if (projectileController != null)
                {
                    projectileController.Initialize(); // Inicializar el proyectil
                }
            }
            else
            {
                Debug.LogWarning("Prefab del proyectil o punto de disparo no asignados en el Inspector.");
            }
        }
    }

    //Gira al personaje
    void Giro(float mov)
    {
        lookRigth = !lookRigth;

        if (mov < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (mov > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void Golpeado()
    {
        animator.SetBool("isDamaged", fueHerido);
        transform.Translate(Vector3.left * empujePJ * Time.deltaTime, Space.World);
    }

    public void GolpeadoFin()
    {
        fueHerido = false;
        animator.SetBool("isDamaged", fueHerido);
    }

    void Vida()
    {
        if (vidaPJ <= 0)
        {
            murio = true;
        }
    }

    void Respawn()
    {
        if (murio)
        {
            transform.position = spawn.position;
            vidaPJ = vidaPJMax;

            respawnTime += 1 * Time.deltaTime;

            if (respawnTime > respawnCD)
            {
                murio = false;
                respawnTime = 0;
            }
        }
    }
}