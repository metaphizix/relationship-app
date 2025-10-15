import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { useAuth } from '../contexts/AuthContext';
import { ActivityIndicator, View } from 'react-native';

// Auth Screens
import LoginScreen from '../screens/LoginScreen';
import RegisterScreen from '../screens/RegisterScreen';

// Onboarding Screens
import CreateCoupleScreen from '../screens/CreateCoupleScreen';
import JoinCoupleScreen from '../screens/JoinCoupleScreen';

// Main Screens
import DashboardScreen from '../screens/DashboardScreen';

const Stack = createNativeStackNavigator();

export default function AppNavigator() {
  const { user, loading } = useAuth();

  if (loading) {
    return (
      <View style={{ flex: 1, justifyContent: 'center', alignItems: 'center' }}>
        <ActivityIndicator size="large" />
      </View>
    );
  }

  return (
    <NavigationContainer>
      {!user ? (
        // Auth Stack
        <Stack.Navigator
          screenOptions={{
            headerShown: false,
          }}
        >
          <Stack.Screen name="Login" component={LoginScreen} />
          <Stack.Screen name="Register" component={RegisterScreen} />
        </Stack.Navigator>
      ) : (
        // Main App Stack
        <Stack.Navigator>
          <Stack.Screen
            name="CreateCouple"
            component={CreateCoupleScreen}
            options={{ title: 'Setup' }}
          />
          <Stack.Screen
            name="JoinCouple"
            component={JoinCoupleScreen}
            options={{ title: 'Join Couple' }}
          />
          <Stack.Screen
            name="Main"
            component={DashboardScreen}
            options={{ title: 'Dashboard' }}
          />
        </Stack.Navigator>
      )}
    </NavigationContainer>
  );
}
