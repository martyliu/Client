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
        private List<IManagerBase> _managerList  = new List<IManagerBase>();

        public InputManager InputManager { get; private set; }

        byte[] buffer ;
        NetworkStream stream;
        TcpClient tcpClient;

        public static bool Quiting { get; private set; } = false;

        public void Init()
        {
            InputManager = new InputManager();
            _managerList.Add(InputManager);
        }

        public void StartGame()
        {

            foreach (var m in _managerList)
                m.OnStart();
            tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 6666);


            if (tcpClient.Connected)
            {
                stream = tcpClient.GetStream();
                buffer = new byte[tcpClient.ReceiveBufferSize];
                Debug.Log("connect success! ");
                stream.BeginRead(buffer, 0, buffer.Length, OnReceivedData, tcpClient);
            }
        }

        public void OnUpdate(float deltaTime)
        {
            int handleCount = 0;
            for(int i = 0; ;)
            {
                if (message.Count == 0 || handleCount >= 50)
                    break;

                var msg = message[i];
                // --todo  未处理粘包
                message.RemoveAt(0);
                HandleMsg(msg);
                
                handleCount++;
            }
            

            foreach (var m in _managerList)
                m.OnUpdate(deltaTime);
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

            message.Add(System.Text.Encoding.Default.GetString( newBuffer ));

            stream.BeginRead(buffer, 0, buffer.Length, OnReceivedData, tcpClient);
        }

        List<string> message = new List<string>();

        void HandleMsg(string buffer)
        {
            var wrapData = JsonUtility.FromJson<JsonWrapData>(buffer);
            if (wrapData.protocolName == "status")
            {
                var data = JsonUtility.FromJson<Vector2>(wrapData.jsonData);
                GameObject.FindObjectOfType<SvrMoveController>().UpdateStatus(data.x, data.y);
            }
        }

        public void SendData(string protocol, object toSendData)
        {
            JsonWrapData data = new JsonWrapData() { protocolName = protocol };
            data.jsonData = JsonUtility.ToJson(toSendData);

            byte[] bArray = System.Text.Encoding.Default.GetBytes(JsonUtility.ToJson( data));
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