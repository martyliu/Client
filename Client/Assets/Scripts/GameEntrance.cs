using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobaClient
{

    public class GameEntrance : MonoBehaviour
    {
        // Update is called once per frame
        void Start()
        {
            GameApp.Instance.StartGame();
        }

        void Update()
        {
            GameApp.Instance.OnUpdate(Time.deltaTime);
        }

        private void OnApplicationQuit()
        {
            GameApp.Instance.OnAppQuit();
        }

        private void LateUpdate()
        {
            GameApp.Instance.OnLateUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            GameApp.Instance.OnFixedUpdate(Time.fixedDeltaTime);
        }
    }
}