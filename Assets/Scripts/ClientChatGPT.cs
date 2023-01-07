using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
public class ClientChatGPT : MonoBehaviour
{
    private static ClientChatGPT Instance;
    private string ask;
    private string answer;
    private string NPCAnswer;
    private string[] choices;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static ClientChatGPT GetInstance()
    {
        return Instance;
    }
    void UDPConnection()
    {
        UdpClient client = new UdpClient(5600);
        try
        {
            client.Connect("127.0.0.1", 5500);
            byte[] sendBytes = Encoding.ASCII.GetBytes(ask);
            client.Send(sendBytes, sendBytes.Length);
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 5500);
            byte[] receiveBytes = client.Receive(ref remoteEndPoint);
            string receivedString = Encoding.ASCII.GetString(receiveBytes);
            print("Message received from the server \n " + receivedString);

            answer = receivedString;
            SetNPCAnswer();
            SetChoices();
        }
        catch (Exception e)
        {
            print("Exception thrown " + e.Message);
        }
    }
    
    public void ReadContext(string s)
    {
        ask = s;
        UDPConnection();
    }

    public string GetAnswer()
    {
        return answer;
    }

    private void SetNPCAnswer()
    {
       NPCAnswer = GetAnswer().Split(':')[1].Trim();
    }

    public string GetNPCAnswer()
    {
        Debug.Log("NPCAnswer: " + NPCAnswer);
        return NPCAnswer;
    }

    private void SetChoices()
    {
        GetAnswer().Split(':').CopyTo(choices, 2);
    }

    public string[] GetChoices()
    {
        Debug.Log("Choices: " + choices);
        return choices;
    }


}