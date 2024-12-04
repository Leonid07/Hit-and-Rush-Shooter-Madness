using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{
    public int health = 10;
    public ParticleSystem explosition;
    public Timer timer;
    void Update()
    {
        if (health <= 0)
        {
            if (explosition != null)
            {
                ParticleSystem part = Instantiate(explosition, transform.position, Quaternion.identity);
            }
            timer.Check();
            Destroy(gameObject);
        }
    }
}
