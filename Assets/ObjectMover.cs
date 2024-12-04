using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] public Vector3 targetPosition;  // Целевая позиция вниз
    public float moveDuration = 2f;  // Время, за которое объект движется
    private Vector3 startPosition;   // Начальная позиция

    private void Start()
    {
        startPosition = transform.localPosition;  // Запоминаем стартовую позицию
        StartCoroutine(MoveObjectLoop());  // Запускаем корутину
    }

    private IEnumerator MoveObjectLoop()
    {
        while (true)  // Бесконечный цикл
        {
            // Двигаем объект вниз
            yield return StartCoroutine(MoveToPosition(targetPosition));

            // Возвращаем объект обратно
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

            // Линейная интерполяция между начальной и целевой позицией
            transform.localPosition = Vector3.Lerp(initialPosition, target, elapsedTime / moveDuration);

            yield return null;  // Ждём до следующего кадра
        }

        transform.localPosition = target;  // Точная позиция в конце анимации
    }
}
