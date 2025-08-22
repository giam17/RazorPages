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

    const sel = document.getElementById('filtro');
    if (sel && sel.dataset && sel.dataset.current) {
        const wanted = sel.dataset.current.toLowerCase();
        for (const opt of sel.options) {
            if (opt.value.toLowerCase() === wanted) {
                sel.value = opt.value;
                break;
            }
        }
    }
})();

