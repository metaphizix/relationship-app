import apiClient from './apiClient';

export interface CoupleResponse {
  id: string;
  code: string;
  createdAt: string;
  members: CoupleMember[];
}

export interface CoupleMember {
  userId: string;
  displayName: string;
  role: string;
  joinedAt: string;
}

export const coupleService = {
  async createCouple(): Promise<CoupleResponse> {
    const response = await apiClient.post<CoupleResponse>('/couples', {});
    return response.data;
  },

  async joinCouple(code: string): Promise<CoupleResponse> {
    const response = await apiClient.post<CoupleResponse>(
      '/couples/00000000-0000-0000-0000-000000000000/join',
      { code }
    );
    return response.data;
  },

  async getMyCouple(): Promise<CoupleResponse> {
    const response = await apiClient.get<CoupleResponse>('/couples/my-couple');
    return response.data;
  },

  async getCoupleById(id: string): Promise<CoupleResponse> {
    const response = await apiClient.get<CoupleResponse>(`/couples/${id}`);
    return response.data;
  },
};
