import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';

// Update this to your actual API URL
// For Android emulator: http://10.0.2.2:5000
// For iOS simulator: http://localhost:5000
// For physical device: http://YOUR_IP_ADDRESS:5000
const API_BASE_URL = 'http://10.0.2.2:5000/api';

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor to add auth token
apiClient.interceptors.request.use(
  async (config) => {
    const token = await AsyncStorage.getItem('authToken');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response interceptor for error handling
apiClient.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (error.response?.status === 401) {
      // Token expired or invalid - logout user
      await AsyncStorage.multiRemove(['authToken', 'refreshToken', 'userData']);
    }
    return Promise.reject(error);
  }
);

export default apiClient;
