using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("-------Sounds-------")]
    [SerializeField] private AudioSource loseSound;
    [SerializeField] private AudioSource takeHitSound;

    [Header("-------Player-------")]
    [SerializeField] private float maxHealth;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Camera _camera;
    private float _health;
    private float _speed;
    private Vector3 _inputAxis;

    [Header("-------Other--------")]
    [SerializeField] private GameObject loseWindow;
    [SerializeField] private Weapon[] weapons;

    private const float MULTIPLY=0.03f;
    private const float MAX_SPEED = 8f;
    private const float MIN_SPEED = 0;

    private void Start()
    {
        _health = maxHealth;
        Cursor.lockState = CursorLockMode.Locked;
        _camera = Camera.main;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _inputAxis.x = Input.GetAxisRaw("Horizontal");
        _inputAxis.z = Input.GetAxisRaw("Vertical");
        if (_inputAxis.Equals(Vector3.zero))
        {
            ChangeSpeed(-20);
        }
        else
        {
            ChangeSpeed(30);
        }
        ChooseWeapon();
    }

    private void FixedUpdate()
    {
        MoveAndRotatePlayer();
    }

    private void MoveAndRotatePlayer()
    {
        var _cameraEulers = _camera.transform.eulerAngles;
        _cameraEulers.x = 0;
        _cameraEulers.z = 0;
        if (!_inputAxis.Equals(Vector3.zero))
        {
            transform.localEulerAngles = _cameraEulers;
            _rigidbody.MovePosition(transform.position+(transform.forward*_inputAxis.z+transform.right*_inputAxis.x)*_speed*MULTIPLY);
        }
    }
    
    private void ChooseWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DisactivateWeapons();
            weapons[0].gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DisactivateWeapons();
            weapons[1].gameObject.SetActive(true);
        }
    }
    private void DisactivateWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(true);
        }
    }
    private void ChangeSpeed(float sign)
    {
        _speed += Time.deltaTime*sign;
        _speed = Mathf.Clamp(_speed, MIN_SPEED,MAX_SPEED);
        //_animator.SetFloat("Movement", _speed);
    }
    public void ApplyDamage(float damage)
    {
        takeHitSound.Play();
        _health -= damage;
        if (_health <= 0)
        {
            loseSound.Play();
            //_animator.SetTrigger("Die");
        }
    }

    public void Die()
    {
        loseWindow.SetActive(true);
    }
}
