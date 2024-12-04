using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] public Vector3 targetPosition;  // ������� ������� ����
    public float moveDuration = 2f;  // �����, �� ������� ������ ��������
    private Vector3 startPosition;   // ��������� �������

    private void Start()
    {
        startPosition = transform.localPosition;  // ���������� ��������� �������
        StartCoroutine(MoveObjectLoop());  // ��������� ��������
    }

    private IEnumerator MoveObjectLoop()
    {
        while (true)  // ����������� ����
        {
            // ������� ������ ����
            yield return StartCoroutine(MoveToPosition(targetPosition));

            // ���������� ������ �������
            yield return StartCoroutine(MoveToPosition(startPosition));
        }
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        float elapsedTime = 0;
        Vector3 initialPosition = transform.localPosition;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;

            // �������� ������������ ����� ��������� � ������� ��������
            transform.localPosition = Vector3.Lerp(initialPosition, target, elapsedTime / moveDuration);

            yield return null;  // ��� �� ���������� �����
        }

        transform.localPosition = target;  // ������ ������� � ����� ��������
    }
}
