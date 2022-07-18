/*
 * Copyright (C) 2016, Jaguar Land Rover
 * This program is licensed under the terms and conditions of the
 * Mozilla Public License, version 2.0.  The full text of the
 * Mozilla Public License is at https://www.mozilla.org/MPL/2.0/
 */

using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class NetworkSettings
{
    public string hostIp;
    public string clientIp;
    public string altClientIP;
    public string eventClientIP;

    public NetworkSettings()
    {
        //hostIp = "127.0.0.1";
        //clientIp = "192.168.178.29";
        //altClientIP = "127.0.0.1";

        hostIp = "192.168.50.199";
        clientIp = "192.168.50.164";
        eventClientIP = "127.0.0.1";
        altClientIP = "192.168.50.123";


        //hostIp = "127.0.0.1";
        //clientIp = "127.0.0.1";
        //eventClientIP = "127.0.0.1";
        //altClientIP = "127.0.0.1";

    }

    public NetworkSettings(string host, string client)
    {
        hostIp = host;
        clientIp = client;
    }

    public void SaveSettings(string path)
    {
        var serializer = new XmlSerializer(typeof(NetworkSettings));

        using (var filestream = new FileStream(path, FileMode.Create))
        {
            var writer = new System.Xml.XmlTextWriter(filestream, System.Text.Encoding.Unicode);
            serializer.Serialize(writer, this);
        }
    }

    public static NetworkSettings LoadSettings(string path)
    {
        try
        {
            XmlSerializer ser = new XmlSerializer(typeof(NetworkSettings));
            TextReader reader = new StreamReader(path);
            return (NetworkSettings)ser.Deserialize(reader);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("error reading network settings, reverting to default: " + e.Message);
            return new NetworkSettings();
        }

    }
}

public class NetworkController : PersistentUnitySingleton<NetworkController>
{

    public bool isMaster = true;

    public event System.Action<int> OnInitConsole;
    public event System.Action<int> OnSelectCar;

    /// <summary>
    /// Converted to 2018.2 networkTransport protocoll by David Goedicke.
    /// Please direct any complains, ideas or thoughts directly at me or open an issue on Github
    /// da.goedicke@gmail.com
    /// 
    /// Based on : https://docs.unity3d.com/ScriptReference/Networking.NetworkTransport.Connect.html
    /// </summary>
    int connectionId;
    int channelId;
    int hostId;
    public static NetworkSettings settings;
    bool connected = false;

    Queue outQueue = new Queue();
    private IEnumerator sender;

    protected override void Awake()
    {
        base.Awake();
        if (_instance != this)
            return;

        settings = NetworkSettings.LoadSettings(Application.dataPath + Path.DirectorySeparatorChar + "network_settings");

        // Init Transport using default values.
        NetworkTransport.Init();

        // Create a connection config and add a Channel.
        ConnectionConfig config = new ConnectionConfig();
        channelId = config.AddChannel(QosType.Reliable);

        // Create a topology based on the connection config.
        HostTopology topology = new HostTopology(config, 10);

        // Create a host based on the topology we just created, and bind the socket to port 12345.
        hostId = NetworkTransport.AddHost(topology, 25552); ///changed

        if (ShowBuild.GetBuildType() == "CONSOLE")
            isMaster = false;

        if (isMaster)
        {
            byte error;
            connectionId = NetworkTransport.Connect(hostId, NetworkController.settings.clientIp, 25552, 0, out error);
            Debug.Log(connectionId + ":connection" + hostId + ":hostId" + channelId + ":channelId and ip:\t" + NetworkController.settings.clientIp);
            Debug.Log(error);

            sender = sendToClient(2.0f);
            StartCoroutine(sender);
            //  Network.InitializeServer(1, 25552, false);
        }
        else
        {

            byte error;
            connectionId = NetworkTransport.Connect(hostId, NetworkController.settings.hostIp, 25552, 0, out error);
            Debug.Log(connectionId + ":connection" + hostId + ":hostId" + channelId + ":channelId and ip:\t" + NetworkController.settings.clientIp);
            Debug.Log(error);
            //We used to connect to the server  in this new case its the other way arround.
            //Debug.Log("connecting: " + Network.Connect(NetworkController.settings.hostIp, 25552));
        }

    }


    public void Update()
    {
        if (!isMaster && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        Debug.developerConsoleVisible = true;


        int outHostId;
        int outConnectionId;
        int outChannelId;
        byte[] buffer = new byte[1024];
        int receivedSize;
        byte error;

        //Set up the Network Transport to receive the incoming message, and decide what type of event
        NetworkEventType eventType = NetworkTransport.Receive(out outHostId, out outConnectionId, out outChannelId, buffer, buffer.Length, out receivedSize, out error);

        switch (eventType)
        {
            case NetworkEventType.ConnectEvent:
                {
                    if (outHostId == hostId &&
                        outConnectionId == connectionId &&
                        (NetworkError)error == NetworkError.Ok)
                    {
                        OnConnect(outHostId, outConnectionId, (NetworkError)error);
                    }
                    break;
                }
            case NetworkEventType.DisconnectEvent:
                {
                    if (outHostId == hostId &&
                        outConnectionId == connectionId)
                    {
                        OnDisconnect(outHostId, outConnectionId, (NetworkError)error);
                    }
                    break;
                }


            //This case is called if the event type is a data event, like the serialized message
            case NetworkEventType.DataEvent:
                {
                    Debug.Log("We just got Data in");

                    //Call the function to deal with the received data
                    OnData(outHostId, outConnectionId, outChannelId, buffer, receivedSize, (NetworkError)error);

                    break;
                }

            case NetworkEventType.Nothing:
                break;

            default:
                //Output the error
                Debug.LogError("Unknown network message type received: " + eventType);
                break;
        }


    }

    //This function is called when a connection is detected
    void OnConnect(int hostID, int connectionID, NetworkError error)
    {
        Debug.Log("OnConnect(hostId = " + hostID + ", connectionId = "
            + connectionID + ", error = " + error.ToString() + ")");
        connected = true;
    }
    void OnDisconnect(int hostID, int connectionID, NetworkError error)
    {
        Debug.Log("OnDisonnect(hostId = " + hostID + ", connectionId = "
            + connectionID + ", error = " + error.ToString() + ")");
        connected = false;
    }

    //This function is called when data is sent
    void OnData(int hostId, int connectionId, int channelId, byte[] data, int size, NetworkError error)
    {
        //Here the message being received is deserialized and output to the console
        Stream serializedMessage = new MemoryStream(data);
        BinaryFormatter formatter = new BinaryFormatter();
        string message = formatter.Deserialize(serializedMessage).ToString();
        Debug.Log(message);
        string[] splitString = message.Split(',');
        if (splitString.Length < 1)
        {
            Debug.LogError("ThisShould not happen I need more infor to change the screen");
        }
        if (splitString[0].Equals("i"))
        {

            OnInitConsole(int.Parse(splitString[1]));
        }
        else if (splitString[0].Equals("s"))
        {

            OnSelectCar(int.Parse(splitString[1]));
        }
        else
        {
            Debug.Log("I got something I am not sure if that works");
        }


        //Output the deserialized message as well as the connection information to the console
        Debug.Log("OnData(hostId = " + hostId + ", connectionId = "
            + connectionId + ", channelId = " + channelId + ", data = "
            + message + ", size = " + size + ", error = " + error.ToString() + ")");


    }

    private IEnumerator sendToClient(float waitTime)
    {
        while (true)
        {
            if (outQueue.Count > 0 && connected)
            {
                string outputstring = outQueue.Dequeue() as string;
                byte error;
                byte[] buffer = new byte[1024];
                Stream message = new MemoryStream(buffer);
                BinaryFormatter formatter = new BinaryFormatter();
                //Serialize the message

                formatter.Serialize(message, outputstring);
                Debug.Log(connectionId + ":connection" + hostId + ":hostId" + channelId + ":channelId");
                //Send the message from the "client" with the serialized message and the connection information

                NetworkTransport.Send(hostId, connectionId, channelId, buffer, (int)message.Position, out error);
                //If there is an error, output message error to the console
                if ((NetworkError)error != NetworkError.Ok)
                {
                    Debug.Log("Message send error: " + (NetworkError)error);
                    Debug.Log("reque again send message ");
                    outQueue.Enqueue(outputstring);
                }
            }

            else if (outQueue.Count > 0 && !connected)
            {
                Debug.Log("We where trying to send somewthing while not beeing connected. This is not good. will try again next frame");
                yield return new WaitForSeconds(waitTime / 10);

            }
            else
            {
                yield return new WaitForSeconds(waitTime);
            }

        }
    }

    private void sendToConsole(string ID, int num)
    {
        string outString = ID + "," + num.ToString();
        outQueue.Enqueue(outString);


    }
    public void SelectCar(int car)
    {
        sendToConsole("s", car);
    }

    public void InitConsole(int consoleNum)
    {
        sendToConsole("i", consoleNum);
    }

#pragma warning disable 0618
#pragma warning restore 0618
}
