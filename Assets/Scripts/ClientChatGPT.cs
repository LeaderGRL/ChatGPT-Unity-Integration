using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
public class ClientChatGPT : MonoBehaviour
{
    private static ClientChatGPT Instance;

    private UdpClient client;
    private string ask;
    private string answer;
    string[] lines;
    private string NPCAnswer;
    private string[] choices;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            newConnection();
        }
        else
        {
            Destroy(gameObject);
        }
        client = new UdpClient(5600);
    }

    public static ClientChatGPT GetInstance()
    {
        return Instance;
    }

    void newConnection()
    {
        client = new UdpClient(5600);
        try
        {
            client.Connect("127.0.0.1", 5500);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    void send()
    {
        try
        {
            byte[] sendBytes = Encoding.ASCII.GetBytes(ask);
            client.Send(sendBytes, sendBytes.Length);
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 5500);
            byte[] receiveBytes = client.Receive(ref remoteEndPoint);
            string receivedString = Encoding.ASCII.GetString(receiveBytes);
            print("Message received from the server \n " + receivedString);

            answer = receivedString;
            lines = answer.Split('\n');
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
        send();
    }

    public string GetAnswer()
    {
        return answer;
    }

    private void SetNPCAnswer()
    {
        int colonIndex = lines[0].IndexOf(':');
        NPCAnswer = lines[0].Substring(colonIndex + 1).Trim().Replace("\"", "");
        //NPCAnswer = GetAnswer().Split(':')[1].Trim();
    }

    public string GetNPCAnswer()
    {
        Debug.Log("NPCAnswer: " + NPCAnswer);
        return NPCAnswer;
    }

    private void SetChoices()
    {
        //GetAnswer().Split(':');
        choices = new string[lines.Length - 1];
        for (int i = 1; i < lines.Length; i++)
        {
            int colonIndex = lines[i].IndexOf(':');
            choices[i - 1] = lines[i].Substring(colonIndex + 1).Trim().Replace("\"", "");
        }
        Debug.Log("Choices: " + choices);
    }

    public string[] GetChoices()
    {
        Debug.Log("Choices: " + choices);
        return choices;
    }


}