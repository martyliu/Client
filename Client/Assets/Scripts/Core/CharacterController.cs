using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobaClient
{

    public class CharacterController : MonoBehaviour
    {
        public float speed = 1.0f;
        Rigidbody _rigidBody;

         
        void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();


        }

        
        void LateUpdate()
        {
            float x;
            float y;
            GameApp.Instance.InputManager.GetAndClearInputData(out x, out y);

            //Debug.Log("x: " + x + ", Y: " + y);
            //if(x != 0 || y != 0)
            {
                _rigidBody.velocity = new Vector3(x * speed * Time.deltaTime, 0, y * speed * Time.deltaTime);
            }
        }
    }
}