using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Объект игрока, за которым будет следовать камера
    public Vector3 offset = new Vector3(0,8,-5); // Смещение камеры относительно игрока

    public float corner = 50f;

    public float followSpeed = 5f; // Скорость следования камеры за игроком
    public float rotationSpeed = 5f; // Скорость поворота камеры

    private Vector3 currentVelocity;

    private void LateUpdate()
    {
        // Плавное движение камеры за игроком
        FollowPlayer();

        // Плавный поворот камеры за игроком
        RotateCamera();
    }

    void FollowPlayer()
    {
        // Плавное движение камеры с использованием Vector3.SmoothDamp
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, followSpeed * Time.deltaTime);
    }

    void RotateCamera()
    {
        Quaternion targetRotation = Quaternion.LookRotation(player.forward);
        Quaternion fixedRotation = Quaternion.Euler(corner, player.rotation.eulerAngles.y, player.rotation.eulerAngles.z);

        transform.rotation = Quaternion.Slerp(transform.rotation, fixedRotation, rotationSpeed * Time.deltaTime);
    }
}
