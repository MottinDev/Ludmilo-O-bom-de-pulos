using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/AnimationConfig")]
public class AnimationConfig : ScriptableObject
{
    public RuntimeAnimatorController animatorController; // Controlador de anima��o para o inimigo
    public string patrolParameter = "isPatroling"; // Par�metro para anima��o de patrulha
    public string chaseParameter = "isPlayerFound"; // Par�metro para anima��o de persegui��o
    public string attackParameter = "isAttacking"; // Par�metro para anima��o de ataque
}
