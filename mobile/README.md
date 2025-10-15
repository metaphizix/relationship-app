# Relationship App - Mobile Frontend

React Native (Expo) mobile application for the Relationship App.

## Features

✅ User Authentication (Login/Register)  
✅ Couple Management (Create/Join with invite codes)  
✅ Modern Material Design UI (React Native Paper)  
⏳ Dashboard with couple information  
⏳ Mood Tracking (Coming Soon)  
⏳ Memory Board (Coming Soon)  
⏳ Likes & Dislikes (Coming Soon)  

## Tech Stack

- **React Native** with Expo
- **React Native Paper** - Material Design components
- **React Navigation** - Navigation
- **TanStack Query** - Data fetching and caching
- **Axios** - API communication
- **AsyncStorage** - Local storage
- **TypeScript** - Type safety

## Prerequisites

- Node.js 18+ and npm
- Expo CLI
- iOS Simulator (macOS) or Android Emulator
- Expo Go app on physical device (optional)

## Installation

```bash
cd mobile
npm install
```

## Configuration

Update the API URL in `src/services/apiClient.ts`:

```typescript
// For Android Emulator
const API_BASE_URL = 'http://10.0.2.2:5000/api';

// For iOS Simulator
const API_BASE_URL = 'http://localhost:5000/api';

// For Physical Device (replace with your computer's IP)
const API_BASE_URL = 'http://192.168.1.XXX:5000/api';
```

## Running the App

### Start Metro Bundler

```bash
npm start
```

### Run on Android

```bash
npm run android
```

### Run on iOS (macOS only)

```bash
npm run ios
```

### Run on Web

```bash
npm run web
```

## Project Structure

```
mobile/
├── src/
│   ├── screens/           # Screen components
│   │   ├── LoginScreen.tsx
│   │   ├── RegisterScreen.tsx
│   │   ├── CreateCoupleScreen.tsx
│   │   ├── JoinCoupleScreen.tsx
│   │   └── DashboardScreen.tsx
│   ├── components/        # Reusable components
│   ├── contexts/          # React contexts
│   │   └── AuthContext.tsx
│   ├── services/          # API services
│   │   ├── apiClient.ts
│   │   ├── authService.ts
│   │   └── coupleService.ts
│   └── navigation/        # Navigation setup
│       └── AppNavigator.tsx
├── App.js                 # Root component
└── package.json
```

## Features Overview

### Authentication

- Email/password registration
- Login with JWT tokens
- Token persistence with AsyncStorage
- Automatic logout on token expiration

### Couple Management

- Create new couple with unique invite code
- Join existing couple using code
- View couple information and members

### UI/UX

- Material Design 3 theming
- Responsive layouts
- Loading states and error handling
- Pull-to-refresh functionality

## Testing

Make sure your backend is running on `http://localhost:5000` before testing the mobile app.

### Test Flow

1. **Start Backend**:
   ```bash
   cd backend/RelationshipApp.Api
   dotnet run
   ```

2. **Start Mobile App**:
   ```bash
   cd mobile
   npm start
   ```

3. **Test Registration**:
   - Open the app
   - Tap "Sign Up"
   - Create an account

4. **Test Couple Creation**:
   - After login, create a couple
   - Note the invite code

5. **Test Joining**:
   - Register another user
   - Join using the invite code

## API Integration

The app communicates with the ASP.NET Core backend API:

- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login
- `POST /api/couples` - Create couple
- `POST /api/couples/{id}/join` - Join couple
- `GET /api/couples/my-couple` - Get current user's couple

## Troubleshooting

### Cannot connect to API

- **Android Emulator**: Use `http://10.0.2.2:5000`
- **iOS Simulator**: Use `http://localhost:5000`
- **Physical Device**: Use your computer's IP address (e.g., `http://192.168.1.100:5000`)
- Ensure backend is running and accessible

### Module not found errors

```bash
npm install
npx expo start --clear
```

### TypeScript errors

```bash
npm install --save-dev @types/react typescript
```

## Coming Soon

- [ ] Mood tracking with calendar view
- [ ] Memory board with photos
- [ ] Likes & Dislikes with reveal
- [ ] Personality tests
- [ ] Real-time notifications with SignalR
- [ ] Goals and progress tracking
- [ ] Anonymous feedback
- [ ] AI coach suggestions

## Development

### Add a new screen

1. Create file in `src/screens/`
2. Add route to `src/navigation/AppNavigator.tsx`
3. Create service functions if needed in `src/services/`

### Add a new API service

1. Create file in `src/services/`
2. Import `apiClient` and define TypeScript interfaces
3. Export service functions

## License

This project is for educational purposes.
