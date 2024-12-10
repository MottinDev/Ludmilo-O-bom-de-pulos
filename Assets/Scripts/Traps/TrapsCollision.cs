using UnityEngine;

public class TrapsCollision : MonoBehaviour
{
    public float damageAmount = 10f; // Quantidade de dano ao colidir

    // Este método é chamado quando ocorre uma colisão física com outro Collider
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se a colisão envolve o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtém a referência para o componente PlayerHealth do jogador
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // Se o componente existir, aplica o dano ao jogador
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
