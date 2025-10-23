import type { CvAnalysisRequest, CvAnalysisResponse } from '../types';

const API_BASE_URL = import.meta.env.PROD 
  ? 'https://cv-analyzer-6gsn.onrender.com/api'
  : 'http://localhost:5161/api';

export class ApiService {
  static async analyzeCv(request: CvAnalysisRequest): Promise<CvAnalysisResponse> {
    try {
      const formData = new FormData();
      formData.append('cvFile', request.cvFile);
      formData.append('jobDescription', request.jobDescription);

      const response = await fetch(`${API_BASE_URL}/analyze-cv`, {
        method: 'POST',
        body: formData,
      });

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(`HTTP ${response.status}: ${errorText}`);
      }

      const result: CvAnalysisResponse = await response.json();
      return result;
    } catch (error) {
      console.error('API Error:', error);
      throw error;
    }
  }
}