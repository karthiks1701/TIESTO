    using UnityEngine;

public class HelloClient : MonoBehaviour
{
    private HelloRequester _helloRequester;
    public TrafficLightAI trafficLightAI;

    private void Start()
    {
        _helloRequester = new HelloRequester();//trafficLightAI);
        _helloRequester.Start();
    }

    private void OnDestroy()
    {
        _helloRequester.Stop();
    }
}