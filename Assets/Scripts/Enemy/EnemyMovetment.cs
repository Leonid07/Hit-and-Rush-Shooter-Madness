using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class EnemyMovetment : MonoBehaviour
{
    public Transform player; // Ссылка на Transform игрока
    public PlayerCharacter character;
    private NavMeshAgent agent; // Ссылка на компонент NavMeshAgent
    public float damage = 10;
    public float attackDistance = 5.0f; // Дистанция атаки
    public float attackDelay = 2.0f; // Задержка атаки

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Получаем компонент NavMeshAgent
        player = GameObject.Find("Player").transform;
        character = player.GetComponent<PlayerCharacter>();
        StartCoroutine(AttackPlayer());
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position); // Устанавливаем позицию игрока как цель для NavMeshAgent
        }
    }
    IEnumerator AttackPlayer()
    {
        while (true)
        {
            if (player != null)
            {
                float distance = Vector3.Distance(transform.position, player.position);
                if (distance <= attackDistance)
                {

                }
            }
            yield return new WaitForSeconds(attackDelay);
        }
    }
}
