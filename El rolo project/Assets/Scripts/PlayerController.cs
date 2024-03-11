using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Variables de salto, movimiento y dash")]
    public float runSpeed = 2;
    public float jumpForce = 5;
    public float dashSpeed = 10;
    public float dashDuration = 0.5f; 
    public float dashCooldown = 1.0f;
    public float dashDistance = 5.0f;
    public bool damage_;
    public int empuje;
    [Header ("Proyectil")]
    public Transform firePoint;  // El punto desde donde se disparará el proyectil
    public GameObject projectilePrefab;  // Prefab del proyectil

    private Rigidbody2D rb2D;
    private bool isGrounded;
    private bool isDashing;
    private bool lookRigth = true;
    private float dashTimeLeft;
    private float lastDashTime;
    private Vector2 dashDirection;
    private Animator animator;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public void Damage*()
    {
        if (damage_)
        {
            transform.Translate(Vector3.rigth*empuje*time.deltaTime,space.world)
            
            LeroyFake.Shoot = false;
        }
    }
    public void Finis_Damage()
    {
        damage_ = false;
    }

    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        animator.SetBool("Ground", isGrounded);
        animator.SetFloat("isJumping", rb2D.velocity.y);

        float moveInput = Input.GetAxis("Horizontal");
        animator.SetFloat("isRunning", Math.Abs(moveInput));

        if(moveInput < 0 && lookRigth)
        {
            Giro();
        }
        else if (moveInput > 0 && !lookRigth)
        {
            Giro();
        }

        // Verificar el dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDashTime + dashCooldown)
        {
            if (!isDashing)
            {
                isDashing = true;
                dashTimeLeft = dashDuration;
                lastDashTime = Time.time;
                dashDirection = new Vector2(moveInput, 0).normalized; // Dirección del dash
            }
        }

        // Dash
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
            rb2D.velocity = new Vector2(moveInput * runSpeed, rb2D.velocity.y);

            // Salto
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        // Disparo
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Shoot();
        }
    }

    void Shoot()
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

    //Gira al personaje
    void Giro()
    {
        lookRigth = !lookRigth;
        Vector3 scale = transform.localScale;
        scale.x *= -1; 
        transform.localScale = scale;
    }
    void fixedupdate()
    {
        if (!damage_)
        (
            moveInput();


        )
        
    }
    
}