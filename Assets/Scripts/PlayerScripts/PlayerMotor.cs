using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool isAlive = true;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    // Pulo duplo
    private int jumpCount = 0;
    public int maxJumps = 2;

    // Dash
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;
    private Vector3 currentDashDirection;
    private Vector2 lastInputDirection; // Armazena a última direção de input
    private Vector3 lastWorldDirection; // Armazena a última direção no mundo

    // Efeitos e som
    public AudioSource audioSource;    // Arraste um AudioSource no Inspetor
    public AudioClip dashSound;        // Arraste o áudio do dash
    public GameObject dashEffectPrefab; // Prefab de efeito de partícula

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // Certifique-se de ter um AudioSource no mesmo GameObject
        // Caso contrário: audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        // Se o personagem está no chão, resetar contagem de pulo
        if (isGrounded && playerVelocity.y < 0)
        {
            jumpCount = 0;
            playerVelocity.y = -2f;
        }

        // Atualizar timers do dash
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                // Encerrar dash
                isDashing = false;
            }
        }

        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    public void ProcessMove(Vector2 input)
    {
        if (!isAlive) return;

        // Atualiza última direção de input
        if (input != Vector2.zero)
        {
            lastInputDirection = input;
            // Converte a direção do input local em direção global
            Vector3 moveDir = transform.TransformDirection(new Vector3(input.x, 0, input.y));
            lastWorldDirection = moveDir.normalized;
        }

        if (isDashing)
        {
            // Durante o dash, aplica o movimento continuamente
            controller.Move(currentDashDirection * Time.deltaTime);
        }
        else
        {
            // Movimentação básica quando não está dashando
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;

            controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

            // Aplicar gravidade
            playerVelocity.y += gravity * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }

    public void Jump()
    {
        if (!isAlive) return;

        // Verifica se pode pular: se está no chão ou se ainda não realizou o pulo duplo
        if (jumpCount < maxJumps)
        {
            // Calcular velocidade de pulo
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            jumpCount++;
        }
    }

    public void Dash()
    {
        if (!isAlive) return;

        if (!isDashing && dashCooldownTimer <= 0f)
        {
            isDashing = true;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;

            playerVelocity.y = 0f;

            // Determinar direção do dash
            // Se não há input atual, usa a última direção conhecida
            Vector3 dashDirection;
            if (lastInputDirection != Vector2.zero)
            {
                // Usa a última direção de input no mundo
                dashDirection = lastWorldDirection * dashSpeed;
            }
            else
            {
                // Se não houve input recentemente, dash para frente
                dashDirection = transform.forward * dashSpeed;
            }

            currentDashDirection = dashDirection;

            // Tocar som do dash (caso tenha um áudio configurado)
            if (audioSource != null && dashSound != null)
            {
                audioSource.PlayOneShot(dashSound);
            }

            // Instanciar efeito visual do dash (partículas)
            if (dashEffectPrefab != null)
            {
                GameObject dashEffect = Instantiate(dashEffectPrefab, transform.position, transform.rotation);
                // Caso queira que o efeito siga o player, pode parentar:
                dashEffect.transform.parent = transform;
                // Destruir o efeito após alguns segundos
                Destroy(dashEffect, 2f);
            }
        }
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
        if (!isAlive)
        {
            GetComponent<PlayerMotor>().enabled = false;
        }
    }
}
