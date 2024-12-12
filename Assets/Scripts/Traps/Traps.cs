using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    public float damageAmount = 10f; // Quantidade de dano ao colidir
    public float pushForce = 5f;     // Força para empurrar o player
    public float pushCooldown = 0.5f; // Tempo antes que a trap possa empurrar novamente
    private Dictionary<GameObject, float> pushCooldowns = new Dictionary<GameObject, float>();

    private void Update()
    {
        // Criar uma cópia das chaves para iterar sem problemas
        List<GameObject> keys = new List<GameObject>(pushCooldowns.Keys);

        // Atualizar os valores do dicionário
        foreach (var key in keys)
        {
            pushCooldowns[key] -= Time.deltaTime;

            // Se o cooldown expirou, remove a chave do dicionário
            if (pushCooldowns[key] <= 0f)
            {
                pushCooldowns.Remove(key);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que colidiu é o jogador
        if (other.CompareTag("Player"))
        {
            // Se o player está em cooldown, não aplica força
            if (pushCooldowns.ContainsKey(other.gameObject))
            {
                return;
            }

            // Obtém a referência para o componente PlayerHealth do jogador
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // Se o componente existir, aplica o dano ao jogador
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }

            // Adiciona o efeito de empurrar o jogador
            PlayerMotor playerMotor = other.GetComponent<PlayerMotor>();
            if (playerMotor != null)
            {
                PushPlayer(playerMotor, other.transform);
            }

            // Adiciona o jogador ao cooldown
            pushCooldowns[other.gameObject] = pushCooldown;
        }
    }

    private void PushPlayer(PlayerMotor playerMotor, Transform playerTransform)
    {
        // Calcula a direção do empurrão (do centro da trap para o player)
        Vector3 pushDirection = (playerTransform.position - transform.position).normalized;

        // Move o player diretamente para simular o empurrão
        playerMotor.MoveInstantly(pushDirection * pushForce);
    }
}