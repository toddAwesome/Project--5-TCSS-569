using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
public class leftThighMotion : MonoBehaviour {

    SerialPort serial = new SerialPort("COM4", 115200);
    
    ArrayList gyroListX = new ArrayList();
    ArrayList gyroListY = new ArrayList();
    ArrayList gyroListZ = new ArrayList();
    // Use this for initialization
    void Start()
    {
        serial.Open();
    }

    // Update is called once per frame
    void Update()
    {
        if (!serial.IsOpen)
        {
            serial.Open();
        }
        string[] accelerometer = serial.ReadLine().Split(',');
        string[] gyroscope = serial.ReadLine().Split(',');
        string[] mag = serial.ReadLine().Split(',');

        transform.Translate(float.Parse(accelerometer[0]) / 10000, //x
                            float.Parse(accelerometer[1]) / 10000, //y
                            float.Parse(accelerometer[2]) / 10000); //z
        float magX = float.Parse(mag[0]);
        float magY = float.Parse(mag[1]);
        float magZ = float.Parse(mag[2]);

        float gyroscopeX = float.Parse(gyroscope[0]);
        gyroListX.Add(gyroscopeX);
        if (gyroListX.Count > 5)
        {
            gyroListX.RemoveAt(0); 
        }

        float gyroscopeY = float.Parse(gyroscope[1]);
        gyroListY.Add(gyroscopeY);
        if (gyroListY.Count > 5) {
            gyroListY.RemoveAt(0);
        }
        float gyroscopeZ = float.Parse(gyroscope[2]);
        gyroListY.Add(gyroscopeY);
        if (gyroListZ.Count > 5) {
            gyroListZ.RemoveAt(0);
        }

        float countX = 0; 
        foreach (float valueX in gyroListX) {

        }

        float countY = 0;
        foreach (float valueY in gyroListY)
        {
            
        }
        float gY = countY / gyroListY.Count;

        float countZ = 0;
        foreach (float valueZ in gyroListZ)
        {
            countZ += valueZ;
        }
        float gZ = countX / gyroListZ.Count;

        Quaternion rot = Quaternion.Euler(gX, gY, gZ);

        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 6.0f);
    }
}
