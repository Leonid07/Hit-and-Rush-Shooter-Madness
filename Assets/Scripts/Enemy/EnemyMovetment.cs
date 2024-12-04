using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class EnemyMovetment : MonoBehaviour
{
    public Transform player; // ������ �� Transform ������
    public PlayerCharacter character;
    private NavMeshAgent agent; // ������ �� ��������� NavMeshAgent
    public float damage = 10;
    public float attackDistance = 5.0f; // ��������� �����
    public float attackDelay = 2.0f; // �������� �����

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // �������� ��������� NavMeshAgent
        player = GameObject.Find("Player").transform;
        character = player.GetComponent<PlayerCharacter>();
        StartCoroutine(AttackPlayer());
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position); // ������������� ������� ������ ��� ���� ��� NavMeshAgent
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
