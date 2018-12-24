using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobaClient
{
    public class SvrMoveController : MonoBehaviour
    {
        class InputData
        {
            public float x;
            public float y;
        }

        Rigidbody _rigidbody;
        InputData _inputData = new InputData();

        CharacterMoveController moveController;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            moveController = FindObjectOfType<CharacterMoveController>();
        }

        public void UpdateStatus(float posX, float posZ)
        {
            transform.position = new Vector3(posX, transform.position.y, posZ);
        }


        private void FixedUpdate()
        {
            //_rigidbody.velocity = new Vector3(_inputData.x * Time.fixedDeltaTime * moveController.speed, 0, _inputData.y * Time.fixedDeltaTime * moveController.speed);
        }


    }
}