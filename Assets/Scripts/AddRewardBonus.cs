using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddRewardBonus : MonoBehaviour
{
    public ParticleSystem partice_1;
    public ParticleSystem particle_2;
    public ParticleSystem particle_3;

    public TMP_Text textCoin;

    public void ActivePanel()
    {
        gameObject.SetActive(true);
        StartCoroutine(StartAnim());
    }

    public IEnumerator StartAnim()
    {
        partice_1.Play();
        particle_2.Play();
        particle_3.Play();
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
