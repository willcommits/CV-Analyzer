import React from 'react';

interface CoverLetterDisplayProps {
  coverLetter: string;
  loading?: boolean;
}

const CoverLetterDisplay: React.FC<CoverLetterDisplayProps> = ({ coverLetter, loading }) => {
  if (loading) {
    return (
      <div className="cover-letter-loading">
        <div className="loading-spinner"></div>
        <p>Generating your personalized cover letter...</p>
        <small>This may take a few moments while we analyze your CV and the job description.</small>
      </div>
    );
  }

  if (!coverLetter) {
    return null;
  }

  const copyToClipboard = async () => {
    try {
      await navigator.clipboard.writeText(coverLetter);
      alert('Cover letter copied to clipboard!');
    } catch (err) {
      console.error('Failed to copy text: ', err);
    }
  };

  return (
    <div className="cover-letter-result">
      <div className="result-header">
        <h2>Your Personalized Cover Letter</h2>
        <button className="copy-button" onClick={copyToClipboard}>
          📋 Copy to Clipboard
        </button>
      </div>
      
      <div className="cover-letter-content">
        {coverLetter.split('\n').map((paragraph, index) => (
          <p key={index}>{paragraph}</p>
        ))}
      </div>
      
      <div className="result-footer">
        <small>
          💡 Tip: Review and customize this cover letter to match your personal writing style
          and add any specific details about the company or role.
        </small>
      </div>
    </div>
  );
};

export default CoverLetterDisplay;