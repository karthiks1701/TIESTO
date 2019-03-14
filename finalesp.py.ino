#include <ESP8266WiFi.h>
#include <PubSubClient.h>

const char* ssid = "Karthik";                     
const char* password = "8778813014";
const char* mqttServer = "192.168.43.235";        //IP address of the mqtt broker 
const int mqttPort = 1883;                        //port number of the broker 
const char* mqttUser = "pi";                      
const char* mqttPassword = "spider19";


WiFiClient espClient;
PubSubClient client(espClient);

void setup() 
{
  int h = 0;
  Serial.begin(115200);

  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) 
  {
    delay(500);
    Serial.println("Connecting to WiFi..");
   
  }
  Serial.println("Connected to the WiFi network");

  client.setServer(mqttServer, mqttPort);
  client.setCallback(callback);

  while (!client.connected()) {
    Serial.println("Connecting to MQTT...");

    if (client.connect("ESP8266Client1", mqttUser, mqttPassword)) {

      Serial.println("connected");

    } else {

      Serial.print("failed with state ");
      Serial.print(client.state());
      delay(2000);

    }
  }
  client.subscribe("test1");


}

void callback(char* topic, byte* payload, unsigned int length) {
  String a[20];
  Serial.print("Message arrived in topic: ");
  Serial.println(topic);

  Serial.print("Message:");
  for (int i = 0; i < length; i++) {
    Serial.print((char)payload[i]);
  }

  Serial.println();
  Serial.println("-----------------------");

  Serial.println("scan start");

  // WiFi.scanNetworks will return the number of networks found
  int n = WiFi.scanNetworks();
  Serial.println("scan done");
  if (n == 0)
    Serial.println("no networks found");
  else
  {
    Serial.print(n);
    Serial.println("networks found");
    int k = 0;
    int z=-1;
    
      // Print SSID and RSSI for each network found
      //Serial.print(i + 1);
      //Serial.print(": ");
      //Serial.print(WiFi.SSID(i));
      //Serial.print(" (");
      //Serial.print(WiFi.RSSI(i));
      //Serial.print(")");
      //Serial.println((WiFi.encryptionType(i) == ENC_TYPE_NONE) ? " " : "*");
      delay(10);
       int i=0;
      for (int j=0;j<n;j++)
        {
          for (int k=0;k<i;k++)
               {
                   
                   if ( WiFi.SSID(j) == a[k])
                   {
                         z=1;
                         break;
                   }
               }
                            if(z!=1)
                            {
                          String h  = ","+WiFi.SSID(j) + "," + WiFi.RSSI(j)+",";
                          if(WiFi.SSID(j).toInt()<10 && WiFi.SSID(j).toInt()>0)
                          {a[i++]=WiFi.SSID(j);
                          char hh1[30];
                          Serial.println(h);
                          h.toCharArray(hh1, 30);
                          
                          //Serial.println(hh1);
                    
                          client.publish("lane1", hh1);
                          z=0;
                          }
          
                        }   
               
                
          }
        
      
      

  }
}


void loop() 
{
   client.loop();
}
