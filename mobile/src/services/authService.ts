import apiClient from './apiClient';
import AsyncStorage from '@react-native-async-storage/async-storage';

export interface RegisterData {
  email: string;
  password: string;
  displayName: string;
}

export interface LoginData {
  email: string;
  password: string;
}

export interface AuthResponse {
  userId: string;
  email: string;
  displayName: string;
  token: string;
  refreshToken: string;
}

export const authService = {
  async register(data: RegisterData): Promise<AuthResponse> {
    const response = await apiClient.post<AuthResponse>('/auth/register', data);
    await this.saveAuthData(response.data);
    return response.data;
  },

  async login(data: LoginData): Promise<AuthResponse> {
    const response = await apiClient.post<AuthResponse>('/auth/login', data);
    await this.saveAuthData(response.data);
    return response.data;
  },

  async logout(): Promise<void> {
    await AsyncStorage.multiRemove(['authToken', 'refreshToken', 'userData']);
  },

  async saveAuthData(data: AuthResponse): Promise<void> {
    await AsyncStorage.setItem('authToken', data.token);
    await AsyncStorage.setItem('refreshToken', data.refreshToken);
    await AsyncStorage.setItem('userData', JSON.stringify({
      userId: data.userId,
      email: data.email,
      displayName: data.displayName,
    }));
  },

  async getAuthToken(): Promise<string | null> {
    return await AsyncStorage.getItem('authToken');
  },

  async getUserData(): Promise<any> {
    const data = await AsyncStorage.getItem('userData');
    return data ? JSON.parse(data) : null;
  },

  async isAuthenticated(): Promise<boolean> {
    const token = await this.getAuthToken();
    return !!token;
  },
};
