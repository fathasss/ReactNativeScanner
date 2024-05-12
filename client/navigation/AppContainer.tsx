import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import LoginScreen from '../pages/Login';
import ReaderScreen from '../pages/Reader';

const Stack = createNativeStackNavigator();

const AppNavigationContainer = () => {
  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="Login">
        <Stack.Screen name='Login' component={LoginScreen} options={{headerShown:false}}/>
        <Stack.Screen name='Reader' component={ReaderScreen}/>
      </Stack.Navigator>
    </NavigationContainer>
  );
};

export default AppNavigationContainer;