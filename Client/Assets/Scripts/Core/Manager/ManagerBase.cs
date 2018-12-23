using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobaClient
{
    public interface IManagerBase 
    {
        void OnStart();
        void OnUpdate(float deltaTime);
    }
}