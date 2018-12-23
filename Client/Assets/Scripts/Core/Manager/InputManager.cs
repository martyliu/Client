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
            var hori = 0.0f;
            if(Input.GetKey(KeyCode.D))
            {
                hori += 1;
            }else if(Input.GetKey(KeyCode.A))
            {
                hori += -1;
            }


            var vert = 0.0f;
            if(Input.GetKey(KeyCode.W))
            {
                vert += 1;
            }else if(Input.GetKey(KeyCode.S))
            {
                vert += -1;
            }

            //if (hori == 0 && vert == 0)
                //return;
            if (hori != _inputData.x || vert != _inputData.y)
            {
                _inputData.x = hori;
                _inputData.y = vert;
            }
        }

        public void GetInputData(out float x, out float y)
        {
            x = _inputData.x;
            y = _inputData.y;
        }

        public void ClearInputDate()
        {
            _inputData.Clear();
        }
    }
}