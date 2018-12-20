using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobaClient
{

    public class GameEntrance : MonoBehaviour
    {
        GameApp game;
        // Start is called before the first frame update
        void Start()
        {
            game = new GameApp();
            game.StartGame();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}