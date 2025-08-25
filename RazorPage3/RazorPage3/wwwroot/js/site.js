// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

(function () {
    const links = document.querySelectorAll('.sidebar-nav .nav-link');
    const panels = document.querySelectorAll('.panel');

    links.forEach(link => {
        link.addEventListener('click', (e) => {
            e.preventDefault();
            links.forEach(a => a.classList.remove('active'));
            link.classList.add('active');
            const target = link.getAttribute('data-target');
            panels.forEach(p => p.classList.remove('active'));
            const el = document.querySelector(target);
            if (el) el.classList.add('active');
        });
    });

    const selFiltro = document.getElementById('filtro');
    if (selFiltro && selFiltro.dataset && selFiltro.dataset.current) {
        const wanted = selFiltro.dataset.current.toLowerCase();
        for (const opt of selFiltro.options) {
            if (opt.value.toLowerCase() === wanted) {
                selFiltro.value = opt.value;
                break;
            }
        }
    }

    const selTam = document.getElementById('tamano');
    if (selTam && selTam.dataset && selTam.dataset.current) {
        const wanted = String(selTam.dataset.current);
        for (const opt of selTam.options) {
            if (opt.value === wanted) {
                selTam.value = opt.value;
                break;
            }
        }
    }

    const btnCrear = document.getElementById('btnCrearTarea');
    if (btnCrear) {
        btnCrear.addEventListener('click', function (e) {
            e.preventDefault();
            const linkForm = document.querySelector('.sidebar-nav .nav-link[data-target="#panel-form"]');
            if (linkForm) linkForm.click();
        });
    }
})();