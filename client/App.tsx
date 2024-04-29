import React, { useState, useEffect } from 'react';
import { StyleSheet, Text, TouchableOpacity, View } from 'react-native';
import { Camera, CameraType } from 'expo-camera';
import axios, { AxiosRequestConfig } from 'axios';

export default function App() {
  const [type, setType] = useState(CameraType.back); //CameraType front or back
  const [hasPermission, setHasPermission] = useState<boolean | null>(null); // android camera permission
  const [scanned, setScanned] = useState<boolean>(false);

  useEffect(() => {
    (async () => {
      const { status } = await Camera.requestCameraPermissionsAsync();
      setHasPermission(status === 'granted');
    })();
  }, []);

  const handleBarCodeScanned = async ({ type, data }: { type: string; data: string }) => {
    setScanned(true);

    const requestData = JSON.stringify({
      "name": "application_v1",
      "readData": data,
      "userIp": "192.168.1.1"
    });

    const config: AxiosRequestConfig = {
      method: 'POST',
      maxBodyLength: Infinity,
      url: 'https://localhost:7281/api/log/Create',
      headers: {
        'accept': '*/*',
        'Content-Type': 'application/json'
      },
      data: requestData
    };

    //Example FreeAPI

    // const config: AxiosRequestConfig = {
    //   method: 'GET',
    //   maxBodyLength: Infinity,
    //   url: 'https://coffee.alexflipnote.dev/random.json',
    //   headers: { }
    // };

    //java -jar bundletool-all-1.16.0.jar build-apks --bundle=application-e765c56b-cc05-4e74-9d23-45aeab3f9d47.aab --output=scanner.apks --mode=universal

    axios.request(config)
      .then((response) => {
        alert(JSON.stringify(response.data));
        console.log(JSON.stringify(response.data));
      })
      .catch((error) => {
        alert(data + " -- > Error : " + error);
        console.log(error);
      });
  };

  if (hasPermission === null) {
    return <View />;
  }

  if (hasPermission === false) {
    return (
      <View style={styles.container}>
        <Text style={styles.text}>Camera permission not granted</Text>
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <Camera
        style={styles.camera}
        type={type}
        onBarCodeScanned={scanned ? undefined : handleBarCodeScanned}
      />
      {scanned && (
        <TouchableOpacity style={styles.button} onPress={() => setScanned(false)}>
          <Text style={styles.buttonText}>Tap to Scan Again</Text>
        </TouchableOpacity>
      )}
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    flexDirection: 'column',
    justifyContent: 'flex-end',
  },
  camera: {
    flex: 1,
  },
  button: {
    backgroundColor: 'blue',
    padding: 20,
    alignSelf: 'center',
  },
  buttonText: {
    color: 'white',
    fontSize: 16,
  },
  text: {
    alignSelf: 'center',
    marginVertical: 16,
  },
});
