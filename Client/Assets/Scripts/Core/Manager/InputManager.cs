using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobaClient
{

    public class InputManager : IManagerBase
    {
        class InputData
        {
            public float x;
            public float y;

            public void Clear()
            {
                x = 0;
                y = 0;
            }
        }

        InputData _inputData;

        public void OnStart()
        {
            _inputData = new InputData();
        }

        public void OnUpdate(float deltaTime)
        {
            var hori = Input.GetAxisRaw("Horizontal");
            var vert = Input.GetAxisRaw("Vertical");

            if (vert == 0 && hori == 0)
                return;

            var x = hori;
            var y = vert;

            _inputData.x += x;
            _inputData.y += y;
        }

        public void GetAndClearInputData(out float x, out float y)
        {
            x = _inputData.x;
            y = _inputData.y;

            _inputData.Clear();
        }
    }
}