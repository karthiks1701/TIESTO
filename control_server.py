import paho.mqtt.client as mqttClient
import time
import RPi.GPIO as GPIO

GPIO.setmode(GPIO.BOARD)
GPIO.setup(3,GPIO.OUT)  #lane1-green
GPIO.setup(7,GPIO.OUT)  #lane2-green
GPIO.setup(5,GPIO.OUT)  #lane1-red
GPIO.setup(11,GPIO.OUT) #lane2-red

lane1green=3   
lane1red=5
lane2green=7
lane2red=11

lane1 =[]       #lists lane1 and lane2 used to store the SSID of a wifi hotspot with it's RSSI value present in the respective lanes
lane2 =[]


def traffic():      #computes the real time traffic densities present in the two lanes
     l1=0
     l2=0
     m=0

     if len(lane1)>len(lane2) :
         for i in range(len(lane1)):
            for k in range(len(lane2)):
                if lane2[k][1]==lane1[i][1]:
                    if int(lane2[k][2])>int(lane1[i][2]):
                         l2+=1
                         m+=1
                    elif int(lane1[i][2])>int(lane2[k][2]):
                         l1+=1
                         m+=1
                
         if m<len(lane1):
               l1=l1+len(lane1)-m
         if m<len(lane2):
               l2=l2+len(lane2)-m
         
         
         
         
     elif len(lane2)>len(lane1) :
         for i in range(len(lane2)):
            for k in range(len(lane1)):
                if lane2[i][1]==lane1[k][1]:
                    if int(lane2[i][2])>int(lane1[k][2]):
                         l2+=1
                         m+=1
                    elif int(lane1[k][2])>int(lane2[i][2]):
                         l1+=1
                         m+=1
                         
         if m<len(lane1):
               l1=l1+len(lane1)-m
         if m<len(lane2):
               l2=l2+len(lane2)-m
         
                
     elif len(lane1)==len(lane2) :
         for i in range(len(lane1)):
            for k in range(len(lane2)):
                if lane2[k][1]==lane1[i][1]:
                    if int(lane2[k][2])>int(lane1[i][2]):
                         l2+=1
                         m+=1
                    elif int(lane1[i][2])>int(lane2[k][2]):
                         l1+=1
                         m+=1
         if m<len(lane1):
               l1=l1+len(lane1)-m
         if m<len(lane2):
               l2=l2+len(lane2)-m
                    
              
     
     if l1>l2:
           GPIO.output(lane1green,GPIO.HIGH)
           GPIO.output(lane2red,GPIO.HIGH)
           GPIO.output(lane1red,GPIO.LOW)
           GPIO.output(lane2green,GPIO.LOW)
           
                     
     elif l2>l1:
           GPIO.output(lane1red,GPIO.HIGH)
           GPIO.output(lane2green,GPIO.HIGH)
           GPIO.output(lane1green,GPIO.LOW)
           GPIO.output(lane2red,GPIO.LOW)
     
     print(l1)
     print(l2)
     
 
def on_connect2(client, userdata, flags, rc):
 
    if rc == 0:
 
        print("Connected to broker  ")
        client.subscribe("lane1")
        client.subscribe("lane2")
        global Connected                #Use global variable
        Connected = True                #Signal connection 
 
    else:
 
        print("Connection failed")
 
def on_message2(client, userdata, message):
    if (flag==1):
        a=str(message.payload)
        a=a[1:]
        lane1.append(a.split(","))
    if (flag==2):
        b=str(message.payload)
        b=b[1:]
        lane2.append(b.split(","))
         
Connected = False   #global variable for the state of the connection
 
broker_address= "192.168.43.235"        #Broker address
port = 1883                             #Broker port
user = "pi"                             #Connection username
password = "spider19"                   #Connection password
flag=0
client = mqttClient.Client("Python2")               #create new instance
client.username_pw_set(user, password=password)     #set username and password
client.on_connect= on_connect2                      #attach function to callback
client.on_message= on_message2                      #attach function to callback
 
client.connect(broker_address, port=port)          #connect to broker
client.loop_start()
while 1:
    flag=1
    client.publish("test1","hello")
    time.sleep(4)                                  #waiting time to receive all the values
    print(lane1)
    print("end of values from esp_1")
    flag=2
    client.publish("test2","hello")
    time.sleep(4)
    print(lane2)
    print("end of values from esp_2")
    traffic()
    lane1=[]
    lane2=[]
    
    
