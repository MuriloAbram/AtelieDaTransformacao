document.addEventListener("DOMContentLoaded", function () {
    // Feedback visual nos inputs
    const inputs = document.querySelectorAll(".form-control, .form-select");
    inputs.forEach(input => {
        input.addEventListener("focus", () => input.classList.add("form-active"));
        input.addEventListener("blur", () => input.classList.remove("form-active"));
    });

    // Toggle do hamburguer: usa Bootstrap se disponível, senão fallback manual
    const toggler = document.querySelector('.navbar-toggler[data-bs-target="#mainNavbar"]');
    const collapseEl = document.getElementById('mainNavbar');

    if (toggler && collapseEl) {
        toggler.addEventListener('click', function () {
            if (window.bootstrap && typeof window.bootstrap.Collapse === 'function') {
                // usa a API do Bootstrap (não atrapalha se o Bootstrap já estiver funcionando)
                const instance = bootstrap.Collapse.getOrCreateInstance(collapseEl);
                instance.toggle();
            } else {
                // fallback simples
                collapseEl.classList.toggle('show');
                const expanded = this.getAttribute('aria-expanded') === 'true';
                this.setAttribute('aria-expanded', String(!expanded));
            }
        });
    }
});