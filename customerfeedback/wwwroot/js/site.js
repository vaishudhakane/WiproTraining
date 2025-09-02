// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function() {
    // Star rating functionality
    const stars = document.querySelectorAll('.star');
    const ratingInput = document.getElementById('ratingValue');
    
    stars.forEach(star => {
        star.addEventListener('click', function() {
            const rating = parseInt(this.getAttribute('data-value'));
            ratingInput.value = rating;
            
            // Update star appearance
            stars.forEach(s => {
                const starValue = parseInt(s.getAttribute('data-value'));
                if (starValue <= rating) {
                    s.classList.add('active');
                } else {
                    s.classList.remove('active');
                }
            });
        });
        
        star.addEventListener('mouseenter', function() {
            const hoverRating = parseInt(this.getAttribute('data-value'));
            stars.forEach(s => {
                const starValue = parseInt(s.getAttribute('data-value'));
                if (starValue <= hoverRating) {
                    s.style.color = '#ffd700';
                } else {
                    s.style.color = '#e2e8f0';
                }
            });
        });
        
        star.addEventListener('mouseleave', function() {
            const currentRating = parseInt(ratingInput.value) || 0;
            stars.forEach(s => {
                const starValue = parseInt(s.getAttribute('data-value'));
                if (starValue <= currentRating) {
                    s.style.color = '#ffd700';
                } else {
                    s.style.color = '#e2e8f0';
                }
            });
        });
    });
    
    // Form validation enhancement
    const form = document.querySelector('.feedback-form');
    form.addEventListener('submit', function(e) {
        if (ratingInput.value === '0') {
            e.preventDefault();
            alert('Please select a rating before submitting.');
        }
    });
    
    // Initialize stars based on any existing rating
    const initialRating = parseInt(ratingInput.value) || 0;
    if (initialRating > 0) {
        stars.forEach(star => {
            const starValue = parseInt(star.getAttribute('data-value'));
            if (starValue <= initialRating) {
                star.classList.add('active');
                star.style.color = '#ffd700';
            }
        });
    }
});
