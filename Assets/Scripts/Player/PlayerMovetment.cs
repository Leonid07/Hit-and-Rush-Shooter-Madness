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

    // Джойстики
    public VariableJoystick movementJoystick;   // Для движения
    public VariableJoystick rotationJoystick;   // Для поворота

    public float detectionRadius = 10f; // Радиус обнаружения объектов
    public string targetTag = "Enemy"; // Тег объектов, к которым будет поворачиваться персонаж

    public Button buttonShoot;

    private CharacterController characterController;  // Ссылка на компонент CharacterController
    private Vector3 directionMove;

    public AudioSource audioSource;
    public AudioClip[] clipFootStap;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerCharacter = GetComponent<PlayerCharacter>();
        characterController = GetComponent<CharacterController>();  // Получаем ссылку на CharacterController
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
        // Движение персонажа с учетом направления джойстика
        Move();

        // Поворот по второму джойстику
        RotateWithJoystick();
    }

    // Метод для поворота персонажа по направлению второго джойстика (только по оси Y)
    void RotateWithJoystick()
    {
        Vector3 directionRotate = new Vector3(rotationJoystick.Horizontal, 0, rotationJoystick.Vertical); // Игнорируем вращение по оси Y

        // Проверяем, чтобы джойстик был сдвинут
        if (directionRotate.magnitude > 0.1f)
        {
            // Рассчитываем только угол поворота по Y
            Quaternion targetRotation = Quaternion.LookRotation(directionRotate, Vector3.up);
            Vector3 targetEulerAngles = targetRotation.eulerAngles;

            // Оставляем только вращение по Y
            targetEulerAngles.x = 0;
            targetEulerAngles.z = 0;

            // Поворачиваем только вокруг оси Y
            basePlayerObjectRotate.rotation = Quaternion.RotateTowards(basePlayerObjectRotate.rotation, Quaternion.Euler(targetEulerAngles), speedRotate * Time.fixedDeltaTime);
        }
    }

    // Движение персонажа с использованием CharacterController
    void Move()
    {
        // Двигаем персонажа относительно глобальных координат, независимо от поворота
        Vector3 directionMove = new Vector3(movementJoystick.Horizontal, 0, movementJoystick.Vertical);

        if (directionMove.magnitude > 0.1f)
        {
            // Преобразуем локальные координаты движения в глобальные с учетом текущего поворота
            directionMove = basePlayerObjectRotate.TransformDirection(directionMove);
        }

        // Двигаем персонажа с учётом времени и скорости
        Vector3 move = directionMove * speed * Time.fixedDeltaTime;

        // Используем CharacterController для движения, учитывая физику и столкновения
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
        // Отрисовка радиуса обнаружения в редакторе
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
