document.addEventListener("DOMContentLoaded", function () {

    // Captura o botão do WhatsApp na tela de detalhes
    const whatsappBtn = document.querySelector(".whatsapp-btn");

    if (whatsappBtn) {
        whatsappBtn.addEventListener("click", function (e) {
            e.preventDefault();

            // Pega a URL formatada pelo C# que está no atributo 'data-url'
            const targetUrl = this.getAttribute("data-url");

            if (targetUrl && targetUrl.trim() !== "") {
                // Abre o WhatsApp em uma nova aba com a mensagem montada
                window.open(targetUrl, "_blank", "noopener,noreferrer");
            } else {
                alert("O artesão não disponibilizou um número de contato válido para este produto.");
            }
        });
    }
});