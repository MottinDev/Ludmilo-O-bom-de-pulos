using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/AnimationConfig")]
public class AnimationConfig : ScriptableObject
{
    public RuntimeAnimatorController animatorController; // Controlador de animação para o inimigo
    public string patrolParameter = "isPatroling"; // Parâmetro para animação de patrulha
    public string chaseParameter = "isPlayerFound"; // Parâmetro para animação de perseguição
    public string attackParameter = "isAttacking"; // Parâmetro para animação de ataque
}
