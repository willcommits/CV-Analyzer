export interface CvAnalysisRequest {
  cvFile: File;
  jobDescription: string;
}

export interface CvAnalysisResponse {
  coverLetter: string;
  success: boolean;
  errorMessage?: string;
}