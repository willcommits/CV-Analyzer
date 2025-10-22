import React, { useState } from 'react';
import FileUpload from './components/FileUpload';
import CoverLetterDisplay from './components/CoverLetterDisplay';
import { ApiService } from './services/api';
import type { CvAnalysisResponse } from './types';
import './App.css';

function App() {
  const [selectedFile, setSelectedFile] = useState<File | undefined>();
  const [jobDescription, setJobDescription] = useState('');
  const [loading, setLoading] = useState(false);
  const [result, setResult] = useState<CvAnalysisResponse | null>(null);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!selectedFile) {
      setError('Please select a CV file');
      return;
    }
    
    if (!jobDescription.trim()) {
      setError('Please enter a job description');
      return;
    }

    setLoading(true);
    setError(null);
    setResult(null);

    try {
      const response = await ApiService.analyzeCv({
        cvFile: selectedFile,
        jobDescription: jobDescription.trim()
      });
      
      setResult(response);
    } catch (err) {
      console.error('Error:', err);
      setError(err instanceof Error ? err.message : 'An unexpected error occurred');
    } finally {
      setLoading(false);
    }
  };

  const handleNewAnalysis = () => {
    setSelectedFile(undefined);
    setJobDescription('');
    setResult(null);
    setError(null);
  };

  return (
    <div className="app">
      <header className="app-header">
        <h1>CV-to-Cover Letter Generator</h1>
        <p>Upload your CV and job description to generate a personalized cover letter</p>
      </header>

      <main className="app-main">
        {!result && (
          <form onSubmit={handleSubmit} className="analysis-form">
            <div className="form-section">
              <label htmlFor="cv-upload">
                <h2>1. Upload Your CV</h2>
              </label>
              <FileUpload 
                onFileSelect={setSelectedFile}
                selectedFile={selectedFile}
              />
            </div>

            <div className="form-section">
              <label htmlFor="job-description">
                <h2>2. Enter Job Description</h2>
              </label>
              <textarea
                id="job-description"
                className="job-description-input"
                placeholder="Paste the job description, requirements, and any specific details about the role you're applying for..."
                value={jobDescription}
                onChange={(e) => setJobDescription(e.target.value)}
                rows={10}
                required
              />
            </div>

            {error && (
              <div className="error-message">
                <strong>Error:</strong> {error}
              </div>
            )}

            <button 
              type="submit" 
              className="submit-button"
              disabled={loading}
            >
              {loading ? 'Generating Cover Letter...' : 'Generate Cover Letter'}
            </button>
          </form>
        )}

        {loading && (
          <CoverLetterDisplay loading={true} coverLetter="" />
        )}

        {result && (
          <div>
            {result.success ? (
              <CoverLetterDisplay coverLetter={result.coverLetter} />
            ) : (
              <div className="error-message">
                <strong>Error:</strong> {result.errorMessage}
              </div>
            )}
            <button 
              onClick={handleNewAnalysis}
              className="new-analysis-button"
            >
              Create Another Cover Letter
            </button>
          </div>
        )}
      </main>

      <footer className="app-footer">
        <p>Built with React, .NET, and OpenAI • Always review and customize generated content</p>
      </footer>
    </div>
  );
}

export default App;