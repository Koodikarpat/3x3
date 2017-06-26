using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

namespace Networking 
{

	public class testingtesting : MonoBehaviour {

	// Use this for initialization
		void Start () {
	}
	
	// Update is called once per frame
		void Update () {
		
		}
		public void Connect ()
		{
			Client client = new Client("172.20.146.40");
			client.Connect ();
			Thread.Sleep (1000);
			client.Disconnect ();
		}
	}
}