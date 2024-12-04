using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovetment : MonoBehaviour
{
    public PlayerCharacter playerCharacter;
    public Animator animator;
    public float speed;
    public float speedRotate = 180f;
    public Transform basePlayerObjectRotate;

    // ���������
    public VariableJoystick movementJoystick;   // ��� ��������
    public VariableJoystick rotationJoystick;   // ��� ��������

    public float detectionRadius = 10f; // ������ ����������� ��������
    public string targetTag = "Enemy"; // ��� ��������, � ������� ����� �������������� ��������

    public Button buttonShoot;

    private CharacterController characterController;  // ������ �� ��������� CharacterController
    private Vector3 directionMove;

    public AudioSource audioSource;
    public AudioClip[] clipFootStap;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerCharacter = GetComponent<PlayerCharacter>();
        characterController = GetComponent<CharacterController>();  // �������� ������ �� CharacterController
        buttonShoot.onClick.AddListener(CheckAndShoot);
    }

    public void TriggerSoundStap()
    {
        int randRange = Random.Range(0,clipFootStap.Length);
        audioSource.clip = clipFootStap[randRange];
        audioSource.Play();
    }

    void FixedUpdate()
    {
        // �������� ��������� � ������ ����������� ���������
        Move();

        // ������� �� ������� ���������
        RotateWithJoystick();
    }

    // ����� ��� �������� ��������� �� ����������� ������� ��������� (������ �� ��� Y)
    void RotateWithJoystick()
    {
        Vector3 directionRotate = new Vector3(rotationJoystick.Horizontal, 0, rotationJoystick.Vertical); // ���������� �������� �� ��� Y

        // ���������, ����� �������� ��� �������
        if (directionRotate.magnitude > 0.1f)
        {
            // ������������ ������ ���� �������� �� Y
            Quaternion targetRotation = Quaternion.LookRotation(directionRotate, Vector3.up);
            Vector3 targetEulerAngles = targetRotation.eulerAngles;

            // ��������� ������ �������� �� Y
            targetEulerAngles.x = 0;
            targetEulerAngles.z = 0;

            // ������������ ������ ������ ��� Y
            basePlayerObjectRotate.rotation = Quaternion.RotateTowards(basePlayerObjectRotate.rotation, Quaternion.Euler(targetEulerAngles), speedRotate * Time.fixedDeltaTime);
        }
    }

    // �������� ��������� � �������������� CharacterController
    void Move()
    {
        // ������� ��������� ������������ ���������� ���������, ���������� �� ��������
        Vector3 directionMove = new Vector3(movementJoystick.Horizontal, 0, movementJoystick.Vertical);

        if (directionMove.magnitude > 0.1f)
        {
            // ����������� ��������� ���������� �������� � ���������� � ������ �������� ��������
            directionMove = basePlayerObjectRotate.TransformDirection(directionMove);
        }

        // ������� ��������� � ������ ������� � ��������
        Vector3 move = directionMove * speed * Time.fixedDeltaTime;

        // ���������� CharacterController ��� ��������, �������� ������ � ������������
        characterController.Move(move);
        animator.SetFloat("Speed", directionMove.magnitude);
    }

    void CheckAndShoot()
    {
        if (playerCharacter.gunSelect != null)
        {
            playerCharacter.gunSelect.GetComponent<Shotting>().Shoot();
            animator.Play("CharacterArmature_Idle_Shoot");
        }
    }

    void OnDrawGizmosSelected()
    {
        // ��������� ������� ����������� � ���������
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
