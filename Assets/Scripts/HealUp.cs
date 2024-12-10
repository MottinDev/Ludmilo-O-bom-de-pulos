using UnityEngine;

public class HealUp : MonoBehaviour
{
    public float healAmount = 20f; // Valor de cura ao coletar o objeto de vida

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.RestoreHealth(healAmount); // Chama o método RestoreHealth do PlayerHealth
                Destroy(gameObject); // Destroi o objeto de vida
            }
        }
    }
}
