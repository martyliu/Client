using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobaClient
{

    public class CharacterMoveController : MonoBehaviour
    {
        public float speed = 1.0f;
        Rigidbody _rigidBody;

         
        void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();

        }

        
        void FixedUpdate()
        {
            float x;
            float y;
            GameApp.Instance.InputManager.GetInputData(out x, out y);

            _rigidBody.velocity = new Vector3(x * Time.fixedDeltaTime * speed , _rigidBody.velocity.y, y * Time.fixedDeltaTime * speed);
            //Debug.Log(x + ", " + y);
            //Debug.Log("x: " + x + ", Y: " + y);
            //if(x != 0 || y != 0)
            {
                //_rigidBody.velocity = new Vector3(x * speed * Time.fixedDeltaTime, _rigidBody.velocity.y, y * speed * Time.fixedDeltaTime);
            }

            //Debug.Log(_rigidBody.velocity);
        }

        private void LateUpdate()
        {
            //Debug.Log("clear late update");
            //GameApp.Instance.InputManager.ClearInputDate();
        }
    }
}