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

        private void Update()
        {
            SendCharacterStatus();
            
        }

        void SendCharacterStatus()
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.z);
            StartCoroutine(SendDataCor(pos));
        }

        public float delay = 100.0f;
        public float randomValue = 15.0f;
        IEnumerator SendDataCor(Vector2 pos)
        {
            var dd = Random.Range(delay - randomValue, delay + randomValue);
            yield return new WaitForSeconds( dd / 1000.0f);
            GameApp.Instance.SendData("status", pos);
        }

        void FixedUpdate()
        {
            float x;
            float y;
            GameApp.Instance.InputManager.GetInputData(out x, out y);
            if (x == 0 && y == 0)
            {
                Debug.Log(_rigidBody.velocity);
                return;
            }
                
            _rigidBody.velocity = new Vector3(x * Time.fixedDeltaTime * speed , _rigidBody.velocity.y, y * Time.fixedDeltaTime * speed);
        }

        private void LateUpdate()
        {
            //Debug.Log("clear late update");
            //GameApp.Instance.InputManager.ClearInputDate();
        }
    }
}