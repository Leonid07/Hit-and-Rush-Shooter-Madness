using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    public float bulletSpeed;

    public GameObject soundObject;

    private void Start()
    {
        Instantiate(soundObject, transform.position, Quaternion.identity);
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyCharacter>().health -= damage;
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Промах");
        }
    }
    public void StarCorutineMoveBullet(GameObject bullet, Vector3 direction)
    {
        StartCoroutine(MoveBullet(bullet, direction));
    }
    public IEnumerator MoveBullet(GameObject bullet, Vector3 direction)
    {
        while (bullet != null)
        {
            bullet.transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
    }
}
