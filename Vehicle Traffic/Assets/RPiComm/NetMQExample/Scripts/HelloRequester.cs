using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;


/// <summary>
///     Example of requester who only sends Hello. Very nice guy.
///     You can copy this class and modify Run() to suits your needs.
///     To use this class, you just instantiate, call Start() when you want to start and Stop() when you want to stop.
/// </summary>
public class HelloRequester : RunAbleThread
{
    //public TrafficLightAI trafficLightAI;
    public string message = null;
    public static bool gotMessage = false;
    public static string permanentMessage2, permanentMessage1;
    private int ctr = 0;
    //public HelloRequester(TrafficLightAI trafficLightAI)
    //{
    //    this.trafficLightAI = trafficLightAI;
    //}
    /// <summary>
    ///     Request Hello message to server and receive message back. Do it 10 times.
    ///     Stop requesting when Running=false.
    /// </summary>
    protected override void Run()
    {
        ForceDotNet.Force(); // this line is needed to prevent unity freeze after one use, not sure why yet
        using (RequestSocket client = new RequestSocket())
        {
            client.Connect("tcp://localhost:5555");

            for (int i = 0;  Running; i++)
            {
                Debug.Log("Sending Hello");
                client.SendFrame("Hello");
                // ReceiveFrameString() blocks the thread until you receive the string, but TryReceiveFrameString()
                // do not block the thread, you can try commenting one and see what the other does, try to reason why
                // unity freezes when you use ReceiveFrameString() and play and stop the scene without running the server
//                string message = client.ReceiveFrameString();
//                Debug.Log("Received: " + message);
                gotMessage = false;
                while (Running)
                {
                    gotMessage = client.TryReceiveFrameString(out message); // this returns true if it's successful
                    if (gotMessage) break;
                }
                var p = 0;
                if (gotMessage)
                {
                    //trafficLightAI = GameObject.FindGameObjectWithTag("TrafficLightHolder").GetComponent<TrafficLightAI>();
                    //trafficLightAI.getMessage(message);
                    //Debug.Log("Received int " + int.TryParse(message, out p));
                    
                    Debug.Log("Received string " + message);

                    if(message.Length >= 6)
                    {
                        int[] a = new int[] { (int)char.GetNumericValue(message[3]), (int)char.GetNumericValue(message[5]) };

                        CreateCars.getTheMessage(a);
                    }
                    
                    /*
                    if (ctr == 0)
                    {
                        permanentMessage1 = message;
                    }
                    
                    else if (ctr == 2)
                    {
                        permanentMessage2 = message;
                        ctr = 0;
                    }
                    ctr++;
                    */
                }
            }
        }

        NetMQConfig.Cleanup(); // this line is needed to prevent unity freeze after one use, not sure why yet
    }

    public static int[] getMessage1()
    {
        int[] a = new int[] { permanentMessage1[3], permanentMessage1[5] };
        return a;
    }

    public static bool getGotMessage()
    {
        return gotMessage;
    }

    public static string getMessage2()
    {
        return permanentMessage2;
    }

}