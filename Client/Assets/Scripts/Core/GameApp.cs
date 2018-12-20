using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace MobaClient
{
    public class GameApp 
    {
        #region Singleton
        private static GameApp _instance = null;
        public static GameApp Instance {
            get
            {
                if (_instance == null)
                    _instance = new GameApp();
                return _instance;
            }
        }

        #endregion Singleton

        byte[] buffer ;
        NetworkStream stream;
        TcpClient tcpClient;
        public void StartGame()
        {
            tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 6666);

            
            if(tcpClient.Connected)
            {
                stream = tcpClient.GetStream();
                buffer = new byte[tcpClient.ReceiveBufferSize];
                stream.BeginRead(buffer, 0, buffer.Length, OnReceivedData, tcpClient);
            }
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