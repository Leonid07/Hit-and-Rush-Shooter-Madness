using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [Header("��������� ��� ������")]
    public Transform gunsSlot;// ��������� ��������� ������
    public GameObject gunSelect;

    public void WormGun(GameObject wormGun)
    {
        gunSelect = Instantiate(wormGun, gunsSlot.position, gunsSlot.rotation, gunsSlot.transform);
    }
}