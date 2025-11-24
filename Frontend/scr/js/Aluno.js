window.onload = function () {
    var logado = JSON.parse(sessionStorage.getItem("Logado"));
    document.getElementById("userBtn").innerText = logado.login.nome;
};

document.getElementById("btnSair").addEventListener("click", function () {
    sessionStorage.clear();
});

const userBtn = document.getElementById("userBtn");
const dropdown = document.getElementById("dropdown");

userBtn.addEventListener("click", () => {
    dropdown.style.display = dropdown.style.display === "flex" ? "none" : "flex";
});

// Fechar ao clicar fora
document.addEventListener("click", (e) => {
    if (!userBtn.contains(e.target) && !dropdown.contains(e.target)) {
        dropdown.style.display = "none";
    }
});