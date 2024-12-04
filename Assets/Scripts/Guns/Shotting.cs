using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotting : MonoBehaviour
{
    public GameObject[] prefubBullet;
    public float range = 100;
    public int bulletsPerShot = 1;
    public float bulletSpeed = 20;

    public Transform spawnPointBullet;

    Ray shootRay = new Ray();
    RaycastHit hit;

    public void Shoot()
    {
        shootRay.origin = spawnPointBullet.position;
        shootRay.direction = spawnPointBullet.TransformDirection(Vector3.forward);
        Debug.DrawRay(shootRay.origin, shootRay.direction * range, Color.green, 2.0f);
        FireBulletsInCone(shootRay.direction);
    }
    void FireBulletsInCone(Vector3 direction)
    {
        for (int i = 0; i < bulletsPerShot; i++)
        {
            // Создаем пулю
            int rand = UnityEngine.Random.Range(0, prefubBullet.Length);
            GameObject bullet = Instantiate(prefubBullet[rand], spawnPointBullet.position, Quaternion.identity);

            bullet.GetComponent<Bullet>().StarCorutineMoveBullet(bullet, direction);
        }
    }
}