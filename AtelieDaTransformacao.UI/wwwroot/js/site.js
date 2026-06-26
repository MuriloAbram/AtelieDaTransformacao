
        document.addEventListener("DOMContentLoaded", function () {
    // Adiciona feedback visual imediato nos inputs ativos
    const inputs = document.querySelectorAll(".form-control, .form-select");

    inputs.forEach(input => {
            input.addEventListener("focus", () => {
                // Aplica a classe de controle diretamente no próprio input
                input.classList.add("form-active");
            });

        input.addEventListener("blur", () => {
            // Remove o efeito ao sair do campo
            input.classList.remove("form-active");
        });
    });
});
