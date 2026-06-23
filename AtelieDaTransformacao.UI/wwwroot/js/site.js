document.addEventListener("DOMContentLoaded", function () {
    // Adiciona feedback visual imediato nos inputs ativos
    const inputs = document.querySelectorAll(".form-control, .form-select");
    inputs.forEach(input => {
        input.addEventListener("focus", () => {
            input.parentElement.classList.add("shadow-sm");
        });
        input.addEventListener("blur", () => {
            input.parentElement.classList.remove("shadow-sm");
        });
    });
});