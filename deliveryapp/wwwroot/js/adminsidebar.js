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


// Función para establecer el enlace activo basado en la URL actual
function setActiveNavLink() {
    const currentPath = window.location.pathname;
    let activeFound = false;

    document.querySelectorAll('.nav-link').forEach(link => {
        // Quitar la clase active de todos los enlaces
        link.classList.remove('active');

        // Verificar si la URL del enlace coincide con la URL actual
        if (link.getAttribute('href') && (
            currentPath === link.getAttribute('href') ||
            (currentPath.includes(link.getAttribute('href')) && link.getAttribute('href') !== '/'))) {
            link.classList.add('active');
            localStorage.setItem('activeNavLink', link.getAttribute('href'));
            activeFound = true;
        }
    });

    // Si no se encontró un enlace activo, intentar restaurar desde localStorage
    if (!activeFound && localStorage.getItem('activeNavLink')) {
        document.querySelectorAll('.nav-link').forEach(link => {
            if (link.getAttribute('href') === localStorage.getItem('activeNavLink')) {
                link.classList.add('active');
            }
        });
    }
}

// Establecer el enlace activo al cargar la página
document.addEventListener('DOMContentLoaded', setActiveNavLink);

// Manejar clics en los enlaces de navegación
document.querySelectorAll('.nav-link').forEach(link => {
    link.addEventListener('click', function () {
        document.querySelectorAll('.nav-link').forEach(l => l.classList.remove('active'));
        this.classList.add('active');

        // Guardar el enlace activo en localStorage
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