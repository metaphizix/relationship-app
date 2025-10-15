# Mobile App Configuration Guide

## Quick Setup

### 1. Install Dependencies

```bash
cd mobile
npm install
```

### 2. Configure API Endpoint

Edit `src/services/apiClient.ts` and set the correct API URL:

#### For Development on Android Emulator:
```typescript
const API_BASE_URL = 'http://10.0.2.2:5000/api';
```

#### For Development on iOS Simulator:
```typescript
const API_BASE_URL = 'http://localhost:5000/api';
```

#### For Testing on Physical Device:
```typescript
// Replace with your computer's IP address
const API_BASE_URL = 'http://192.168.1.XXX:5000/api';
```

**To find your IP address:**
- Windows: `ipconfig` (look for IPv4 Address)
- macOS/Linux: `ifconfig` or `ip addr`

### 3. Start the Backend

```bash
cd backend/RelationshipApp.Api
dotnet run
```

Backend should be running on:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

### 4. Start the Mobile App

```bash
cd mobile
npm start
```

Then press:
- `a` for Android
- `i` for iOS (macOS only)
- `w` for Web

## Testing the App

### Complete User Flow

1. **Register First User**
   - Open app → Tap "Sign Up"
   - Enter: Email, Password, Display Name
   - Tap "Sign Up"
   - Should navigate to "Create Couple" screen

2. **Create Couple**
   - Tap "Create Couple"
   - Note the 6-character code (e.g., "ABC123")
   - Tap "Continue to App"

3. **Register Second User (on another device/emulator)**
   - Open app → Tap "Sign Up"
   - Create another account
   - Should navigate to "Create Couple" screen

4. **Join Couple**
   - Tap "Or join an existing couple"
   - Enter the 6-character code from Step 2
   - Tap "Join Couple"
   - Should navigate to Dashboard

5. **View Dashboard**
   - See both partners listed
   - See couple code
   - Ready for future features!

## Troubleshooting

### Cannot Connect to Backend

**Symptom**: "Network Error" or "Connection refused"

**Solutions**:

1. **Check Backend is Running**
   ```bash
   curl http://localhost:5000/
   # Should return: {"message":"Relationship App API is running!","version":"1.0.0"}
   ```

2. **Check API URL in apiClient.ts**
   - Android Emulator must use `10.0.2.2` (not `localhost`)
   - Physical device must use computer's IP address

3. **Allow Firewall Access**
   - Windows: Allow dotnet.exe through firewall
   - macOS: System Preferences → Security & Privacy → Firewall

4. **For Physical Device**:
   - Ensure device is on same WiFi network
   - Backend must listen on all interfaces (it does by default)

### TypeScript Errors

```bash
npm install --save-dev @types/react typescript
```

### Module Not Found

```bash
rm -rf node_modules package-lock.json
npm install
```

### Expo Start Issues

```bash
npx expo start --clear
```

## Development Tips

### Hot Reload

Changes to JS/TS files will automatically reload. Shake device or press `Cmd+D` (iOS) / `Cmd+M` (Android) to open developer menu.

### Debugging

- **Console Logs**: View in Metro Bundler terminal
- **React DevTools**: Press `Cmd+D` → "Debug Remote JS"
- **Network Requests**: Use React Native Debugger or Flipper

### State Management

- **Auth State**: Managed by `AuthContext`
- **API Data**: Use TanStack Query for caching
- **Local Storage**: AsyncStorage for persistence

## Adding New Features

### Add a New Screen

1. Create `src/screens/NewScreen.tsx`:
```typescript
import React from 'react';
import { View, StyleSheet } from 'react-native';
import { Text } from 'react-native-paper';

export default function NewScreen() {
  return (
    <View style={styles.container}>
      <Text>New Screen</Text>
    </View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, padding: 24 },
});
```

2. Add to `src/navigation/AppNavigator.tsx`:
```typescript
import NewScreen from '../screens/NewScreen';

// Inside navigator:
<Stack.Screen name="New" component={NewScreen} />
```

3. Navigate to it:
```typescript
navigation.navigate('New');
```

### Add a New API Service

1. Create `src/services/newService.ts`:
```typescript
import apiClient from './apiClient';

export const newService = {
  async getData(): Promise<any> {
    const response = await apiClient.get('/endpoint');
    return response.data;
  },
};
```

2. Use in component:
```typescript
import { newService } from '../services/newService';
import { useQuery } from '@tanstack/react-query';

const { data, isLoading } = useQuery({
  queryKey: ['dataKey'],
  queryFn: newService.getData,
});
```

## Environment Variables

For production, create `.env` file:

```env
API_BASE_URL=https://your-api.com/api
```

Install dotenv:
```bash
npm install react-native-dotenv
```

Use in code:
```typescript
import { API_BASE_URL } from '@env';
```

## Building for Production

### Android APK

```bash
eas build --platform android --profile preview
```

### iOS IPA

```bash
eas build --platform ios --profile preview
```

### Configure EAS

```bash
npm install -g eas-cli
eas login
eas build:configure
```

## Performance Optimization

- Use `React.memo` for expensive components
- Implement `FlatList` for long lists
- Use `useMemo` and `useCallback` wisely
- Enable Hermes engine (default in Expo)

## Next Steps

1. Implement remaining screens (Moods, Memories, Likes, etc.)
2. Add real-time features with SignalR
3. Add image upload for memory board
4. Implement push notifications
5. Add offline support
6. Write tests with Jest and React Native Testing Library

## Resources

- [Expo Documentation](https://docs.expo.dev/)
- [React Native Paper](https://callstack.github.io/react-native-paper/)
- [React Navigation](https://reactnavigation.org/)
- [TanStack Query](https://tanstack.com/query/latest)

## Support

For issues or questions, check:
1. Mobile README.md
2. Backend README.md
3. PROJECT_SUMMARY.md
4. QUICK_START.md
