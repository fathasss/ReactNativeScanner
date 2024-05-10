import React, { useState, useEffect } from 'react';
import { StyleSheet, Text, TouchableOpacity, View } from 'react-native';
import { Camera, CameraType } from 'expo-camera/legacy';
import axios, { AxiosRequestConfig } from 'axios';
import { NetworkInfo } from "react-native-network-info";


interface ReaderScreenProps {
  route: any;
}

const Reader: React.FC<ReaderScreenProps> = ({ route }) => {
  const [type, setType] = useState(CameraType.back); //CameraType front or back
  const [hasPermission, setHasPermission] = useState<boolean | null>(null); // android camera permission
  const [scanned, setScanned] = useState<boolean>(false);
  const { user } = route.params;

  useEffect(() => {
    (async () => {
      const { status } = await Camera.requestCameraPermissionsAsync();
      setHasPermission(status === 'granted');
    })();
  }, []);

  const handleBarCodeScanned = async ({ type, data }: { type: string; data: string }) => {
    setScanned(true);

    /*
    const requestData = {
      "EmployeeName": user.EmployeeName,
      "InsDate": new Date().toLocaleString(),
      "Plate": data,
      "Route": data,
      "UserIp": "ReactApp"
    };

    const config = {
      method: 'post',
      url: 'https://mpluslifeapi.cmcplanet.com/api/MobileLogger/LoggerCreate',
      headers: {
        accept: 'text/plain',
        Authorization: `Bearer ${user.AccessToken}`,
        'Content-Type': 'application/json'
      },
      data: requestData
    };

    console.log(config);

    axios.request(config)
      .then((response) => {
        alert(JSON.stringify(response.data));
        console.log(JSON.stringify(response.data));
      })
      .catch((error) => {
        alert(data + " -- > Error : " + error);
        console.log(error);
      });
      */

    const requestData = {
      EmployeeName: user.EmployeeName,
      InsDate: '2024-05-10T09:16:17.933Z',
      Plate: data,
      Route: data,
      UserIp: 'ReactApplication'
    };

    const config = {
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${user.AccessToken}`
      }
    };

    axios.post('https://mpluslifeapi.cmcplanet.com/api/MobileLogger/LoggerCreate', requestData, config)
      .then(response => {     
        console.log('Response:', response);
        alert("Kayıt başarılı!");
      })
      .catch(error => {
        console.error('Error:', error);
        alert(data + '-->' + error);
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
    <View style={[styles.container, { backgroundColor: '#f5f5f5' }]}>
      <Camera
        style={[styles.camera, { margin: 20 }]}
        type={type}
        onBarCodeScanned={scanned ? undefined : handleBarCodeScanned}
      />
      {scanned && (
        <TouchableOpacity
          style={[
            styles.button,
            {
              borderRadius: 10,
              padding: 10,
              backgroundColor: '#007aff',
              marginBottom: 50
            },
          ]}
          onPress={() => setScanned(false)}
        >
          <Text
            style={[
              styles.buttonText,
              { color: '#ffffff', fontWeight: 'bold', fontSize: 16 },
            ]}
          >
            Tap to Scan Again
          </Text>
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

export default Reader;
