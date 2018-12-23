using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace MobaClient
{
    public class GameApp : Singleton<GameApp>
    {

        byte[] buffer ;
        NetworkStream stream;
        TcpClient tcpClient;

        public static bool Quiting { get; private set; } = false;

        public void StartGame()
        {
            tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 6666);

            
            if(tcpClient.Connected)
            {
                stream = tcpClient.GetStream();
                buffer = new byte[tcpClient.ReceiveBufferSize];
                Debug.Log("connect success! ");
                stream.BeginRead(buffer, 0, buffer.Length, OnReceivedData, tcpClient);


                Vector2 input = new Vector2(0.5f, 0.1f);
                Vector2List vectorLst = new Vector2List();
                vectorLst.data.Add(input);
                vectorLst.data.Add(input);

                JsonWrapData data = new JsonWrapData() { protocolName = "moveInput" };
                data.jsonData = JsonUtility.ToJson(vectorLst);
                Debug.Log(data.jsonData);
                SendData(JsonUtility.ToJson(data));
            }
        }

        public void OnUpdate(float deltaTime)
        {

        }

        public void OnLateUpdate(float deltaTime)
        {

        }

        public void OnFixedUpdate(float deltaTime)
        {

        }

        public void OnAppQuit()
        {
            Quiting = true;
        }

        void OnReceivedData(IAsyncResult ar)
        {
            int size = stream.EndRead(ar);
            if(size == 0)
            {
                Debug.Log("server close!");
                return; 
            }

            byte[] newBuffer = new byte[size];
            Buffer.BlockCopy(buffer, 0, newBuffer, 0, size);

            stream.BeginRead(buffer, 0, buffer.Length, OnReceivedData, tcpClient);
        }

        public void SendData(string jsonData)
        {
            byte[] bArray = System.Text.Encoding.Default.GetBytes(jsonData);
            stream.Write(bArray, 0, bArray.Length);
        }

        
    }

    public class Vector2List
    {
        public List<Vector2> data = new List<Vector2>();
    }


    public class JsonWrapData
    {
        public string protocolName;
        public string jsonData;

        public static JsonWrapData WrapData(string proto, object obj)
        {
            JsonWrapData result = new JsonWrapData();
            result.protocolName = proto;
            result.jsonData = JsonUtility.ToJson(obj);
            return result;
        }
    }

}