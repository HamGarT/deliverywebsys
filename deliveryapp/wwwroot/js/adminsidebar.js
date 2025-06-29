function toggleSidebar() {
    const sidebar = document.getElementById('sidebar');
    const mainContent = document.getElementById('mainContent');
    const toggleIcon = document.getElementById('toggleIcon');

    sidebar.classList.toggle('collapsed');
    mainContent.classList.toggle('expanded');

    if (sidebar.classList.contains('collapsed')) {
        toggleIcon.classList.remove('fa-chevron-left');
        toggleIcon.classList.add('fa-chevron-right');
    } else {
        toggleIcon.classList.remove('fa-chevron-right');
        toggleIcon.classList.add('fa-chevron-left');
    }
}

function toggleMobileSidebar() {
    const sidebar = document.getElementById('sidebar');
    sidebar.classList.toggle('show');
}



function setActiveNavLink() {
    const currentPath = window.location.pathname;
    let activeFound = false;

    document.querySelectorAll('.nav-link').forEach(link => {
      
        link.classList.remove('active');

        if (link.getAttribute('href') && (
            currentPath === link.getAttribute('href') ||
            (currentPath.includes(link.getAttribute('href')) && link.getAttribute('href') !== '/'))) {
            link.classList.add('active');
            localStorage.setItem('activeNavLink', link.getAttribute('href'));
            activeFound = true;
        }
    });

  
    if (!activeFound && localStorage.getItem('activeNavLink')) {
        document.querySelectorAll('.nav-link').forEach(link => {
            if (link.getAttribute('href') === localStorage.getItem('activeNavLink')) {
                link.classList.add('active');
            }
        });
    }
}


document.addEventListener('DOMContentLoaded', setActiveNavLink);


document.querySelectorAll('.nav-link').forEach(link => {
    link.addEventListener('click', function () {
        document.querySelectorAll('.nav-link').forEach(l => l.classList.remove('active'));
        this.classList.add('active');

        if (this.getAttribute('href')) {
            localStorage.setItem('activeNavLink', this.getAttribute('href'));
        }
    });
});

document.addEventListener('click', function (e) {
    const sidebar = document.getElementById('sidebar');
    const mobileToggle = document.querySelector('.mobile-toggle');

    if (window.innerWidth <= 768 &&
        !sidebar.contains(e.target) &&
        !mobileToggle.contains(e.target) &&
        sidebar.classList.contains('show')) {
        sidebar.classList.remove('show');
    }
});

window.addEventListener('resize', function () {
    const sidebar = document.getElementById('sidebar');
    if (window.innerWidth > 768) {
        sidebar.classList.remove('show');
    }
});