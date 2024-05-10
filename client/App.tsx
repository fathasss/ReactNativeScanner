import React from 'react';

import { SafeAreaView } from 'react-native';
import AppContainer from './navigation/AppContainer';

export default function App() {
  return (
    <SafeAreaView style={{ flex: 1 }}>
      <AppContainer />
    </SafeAreaView>   
  );
}
