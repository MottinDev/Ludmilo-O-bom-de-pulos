using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;

    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.LastKnowPos);
        ResetTimers(); // Reinicializa os temporizadores ao entrar no estado.
    }
    
    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
        else
        {
            if (enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance)
            {
                searchTimer += Time.deltaTime;
                moveTimer += Time.deltaTime;
                if (moveTimer > Random.Range(3, 5))
                {
                    enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 10));
                    ResetTimers(); // Reinicializa os temporizadores quando o inimigo se move.
                }
            }
            else
            {
                // Reduza o timer quando o inimigo não pode mais ver o jogador
                searchTimer -= Time.deltaTime;
            }
        }

        if (searchTimer <= 0)
        {
            stateMachine.ChangeState(new PatrolState());
        }
    }
    
    public override void Exit()
    {
        ResetTimers(); // Reinicializa os temporizadores ao sair do estado.
    }

    private void ResetTimers()
    {
        searchTimer = 0;
        moveTimer = 0;
    }
}
