using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MythicalBattles
{
    [RequireComponent(typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;

        private Controls _controls;
        private Animator _animator;
        
        private Vector2 _moveDirection;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _animator.SetBool("Shoot", false);
        }

        private void OnEnable()
        {
            if (_controls == null)
                _controls = new Controls();
            
            _controls.Player.Enable();
        }

        public void OnDisable()
        {
            _controls.Player.Disable();
        }

        private void FixedUpdate()
        {
            _moveDirection = _controls.Player.Move.ReadValue<Vector2>();
            
            Move();
        }

        private void Move()
        {
            if (_moveDirection.sqrMagnitude < 0.1f)
            {
                _animator.SetBool("Move", false);
                return;
            }
            
            if(_animator.GetBool("Move") == false)
                _animator.SetBool("Move", true);

            float rotationAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            Vector3 move = _moveDirection.magnitude * _moveSpeed * transform.forward;

            transform.Translate(move, Space.World);
        }
    }
}