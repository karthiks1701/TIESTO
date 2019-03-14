import time
import zmq
context = zmq.Context()
socket = context.socket(zmq.REP)
socket.bind("tcp://*:5555")
import paho.mqtt.client as mqttClient
import time
#import RPi.GPIO as GPIO




	
 
def on_connect2(client, userdata, flags, rc):
 
    if rc == 0:
 
        print("Connected to broker  ")
        client.subscribe("sim")
     
        global Connected                #Use global variable
        Connected = True                #Signal connection 
 
    else:
 
        print("Connection failed")
 
def on_message2(client, userdata, message):
	a=str(message.payload)
	#print(a)
	#a=a[1:]
	#a=a.split(',')

	#s = ""
	#for i in a :
	#	s+=a+","


	print(a)
	message = socket.recv()
	socket.send(b"Hello")
	

	message = socket.recv()
	socket.send(bytes(a, encoding='utf'))

	#message = socket.recv()
	#socket.send(bytes(a[2], encoding='utf'))
	
	
	



	#message = socket.recv()
	#byte[] msg = Encoding.UTF8.GetBytes("SDFGhj");
	#socket.send(msg);
         
Connected = False   #global variable for the state of the connection
 
broker_address= "192.168.43.235"  #Broker address
port = 1883                         #Broker port
user = "pi"                    #Connection username
password = "spider19"            #Connection password
flag=0
client = mqttClient.Client("Python3")               #create new instance
client.username_pw_set(user, password=password)    #set username and password
client.on_connect= on_connect2                      #attach function to callback
client.on_message= on_message2                    #attach function to callback
 
client.connect(broker_address, port=port)          #connect to broker
client.loop_forever()
