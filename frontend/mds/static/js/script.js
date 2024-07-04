document.addEventListener('DOMContentLoaded', function() {
    const signInTab = document.getElementById('signInTab');
    const signUpTab = document.getElementById('signUpTab');
    const tabIndicator = document.getElementById('tabIndicator');

    function updateTabIndicator() {
        if (signUpTab.classList.contains('active')) {
            tabIndicator.style.transform = 'translateX(100%)';
        } else {
            tabIndicator.style.transform = 'translateX(0)';
        }
    }

    if (window.location.pathname.includes('register')) {
        signUpTab.classList.add('active');
        signInTab.classList.remove('active');
    } else {
        signInTab.classList.add('active');
        signUpTab.classList.remove('active');
    }

    updateTabIndicator();

    signInTab.addEventListener('click', () => {
        signInTab.classList.add('active');
        signUpTab.classList.remove('active');
        updateTabIndicator();
    });

    signUpTab.addEventListener('click', () => {
        signUpTab.classList.add('active');
        signInTab.classList.remove('active');
        updateTabIndicator();
    });
});
