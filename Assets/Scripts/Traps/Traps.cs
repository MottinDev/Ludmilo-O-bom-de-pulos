using UnityEngine;

public class Traps : MonoBehaviour
{
    public float damageAmount = 10f; // Quantidade de dano ao colidir

    // Este método é chamado quando outro objeto entra no trigger do Collider deste objeto
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que colidiu é o jogador
        if (other.CompareTag("Player"))
        {
            // Obtém a referência para o componente PlayerHealth do jogador
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // Se o componente existir, aplica o dano ao jogador
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
