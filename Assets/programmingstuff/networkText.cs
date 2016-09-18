using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Text;
using System;

public class networkText : MonoBehaviour {

	public string serverIP = "45.55.194.211";
	public System.Int32 serverPort;

	//Thread tcpThread;
	TcpClient tcpClient;
	NetworkStream theStream;
	string thePolice = "das";

	//static int bufferSize = 512;
	byte[] data = new byte[1024];
	string receiveMsg = "";

	bool ipconfiged = false;
	bool conReady = false;

	// Use this for initialization
	void changeText(string te){

		TextMesh t = (TextMesh)gameObject.GetComponent(typeof(TextMesh));
		t.text = te;
	}
	void SendCallback(IAsyncResult ar) {

		changeText ("fucksit4");
		receiveData ();
	}

	void Start ()
	{
		readTCPInfo(); 
		theStream.ReadTimeout = 1238989712;
		theStream.BeginRead(data, 0, data.Length, new AsyncCallback(HasRead), tcpClient);
	}

	void HasRead(IAsyncResult ar){
		// Read data from the remote device.
		int bytesRead = tcpClient.Client.EndReceive(ar);
		receiveMsg = System.Text.Encoding.ASCII.GetString(data, 0, bytesRead);
		thePolice = receiveMsg;

		theStream.BeginRead(data, 0, data.Length, new AsyncCallback(HasRead), tcpClient);
		//receiveMsg;
		//changeText (receiveMsg);
	}
	// Update is called once per frame
	int m = 0;
	void Update () {
		changeText (thePolice + m);
		m++;
		if (thePolice != "") {
			changeText (thePolice);
		}
	}
	void readTCPInfo()
	{
		serverPort = 3001;
		ipconfiged = true;

		Debug.Log("server ip: " + serverIP + "    server port: " + serverPort);

		setupTCP();
	}

	public void setupTCP()
	{
		try
		{
			if(ipconfiged)
			{
				tcpClient = new TcpClient();
				IPAddress ip = IPAddress.Parse("45.55.194.211");
				tcpClient.Client.Connect(ip, serverPort);
				theStream = tcpClient.GetStream();

				Debug.Log("Successfully created TCP client and open the NetworkStream.");

				conReady = true;

			}
		}
		catch(Exception e)
		{
			changeText ("fucksita" +serverIP+ e);
			Debug.Log("Unable to connect...");
			Debug.Log("Reason: " + e);
		}
	}

	public void receiveData()
	{
		if(!conReady)
		{
			Debug.Log("connection not ready...");
			return;
		}

		int numberOfBytesRead = 0;

		if(theStream.CanRead)
		{
			try
			{
				//data available always false?
				//Debug.Log("data availability:  " + theStream.DataAvailable);

				numberOfBytesRead = theStream.Read(data, 0, data.Length);  
				receiveMsg = System.Text.Encoding.ASCII.GetString(data, 0, numberOfBytesRead);
				TextMesh t = (TextMesh)gameObject.GetComponent(typeof(TextMesh));
				t.text = receiveMsg;
				Debug.Log("receive msg:  " + receiveMsg);
			}
			catch(Exception e)
			{
				Debug.Log("Error in NetworkStream: " + e);
			}
		}

		receiveMsg = "";
	}

	public void maintainConnection()
	{
		if(!theStream.CanRead)
		{
			setupTCP();
		}
	}
	public void closeConnection()
	{
		if(!conReady) return;

		theStream.Close();
		conReady = false;
	}

}
