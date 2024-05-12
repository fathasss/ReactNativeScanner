import React, { useState, useEffect } from 'react';
import { StyleSheet, Text, Pressable, View, Alert } from 'react-native';
import { Camera, CameraView, CameraType } from 'expo-camera';
import axios from 'axios';

interface ReaderScreenProps {
  navigation: any;
  route: any;
}

const Reader: React.FC<ReaderScreenProps> = ({ navigation, route }) => {
  //const [type, setType] = useState(CameraType.back);
  const [hasPermission, setHasPermission] = useState<boolean | null>(null);
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

    let qrData: string = data;

    const [dataPlate, dataRoute] = qrData.includes("-") ? qrData.split("-").map(part => part.trim()) : [qrData.trim(), ""];

    const requestData = {
      EmployeeName: user.EmployeeName,
      InsDate: '2024-05-10T09:16:17.933Z',
      Plate: dataPlate,
      Route: dataRoute,
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
        Alert.alert(
          "Kayıt Başarılı. Uygulama Kapatılacak",
          "Uygulamayı kapatmak istediğinize emin misiniz?",
          [
            { text: "Hayır", onPress: () => console.log("Kapatma iptal edildi"), style: "cancel" },
            {
              text: "Evet", onPress: () => { navigation.navigate('Login') }
            }
          ],
          { cancelable: false }
        );
      })
      .catch(error => {
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
      <CameraView
        onBarcodeScanned={scanned ? undefined : handleBarCodeScanned}
        barcodeScannerSettings={{
          barcodeTypes: ["qr", "pdf417"],
        }}
        style={StyleSheet.absoluteFillObject}
      />
      {scanned && (
        <Pressable style={styles.button} onPress={() => setScanned(false)}>
          <Text style={styles.buttonText}>Tap to Scan Again</Text>
        </Pressable>
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
    backgroundColor: '#007aff',
    padding: 10,
    alignSelf: 'center',
    borderRadius: 10,
    marginBottom: 50,
    width: 150,
    height: 40,
    justifyContent: 'center',
    alignItems: 'center',
  },
  buttonText: {
    color: '#ffffff',
    fontSize: 15,
    fontWeight: 'bold',

  },
  text: {
    alignSelf: 'center',
    marginVertical: 16,
  },
});

export default Reader;
