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

document.addEventListener("click", (e) => {
    if (!userBtn.contains(e.target) && !dropdown.contains(e.target)) {
        dropdown.style.display = "none";
    }
});
var logado = JSON.parse(sessionStorage.getItem("Logado"));
console.log(logado.login.id);

document.getElementById("formEdita").addEventListener("submit", async function (e) {
    e.preventDefault();

    const nome = document.getElementById("Nome").value;
    const Telefone = document.getElementById("Telefone").value;
    const email = document.getElementById("Email").value;
    const senha = document.getElementById("Senha").value;
    const Confirmasenha = document.getElementById("ConfirmaSenha").value;

    if (senha !== Confirmasenha) {
        alert("As senhas nao coiencidem");
        return;
    }

    if (!confirm("Os dados estão corretos?")) {
        alert("Cancelado!");
        return;
    }
    else {
        var logado = JSON.parse(sessionStorage.getItem("Logado"));
        const id = logado.login.id;
        const response = await fetch(`http://localhost:5074/api/Usuario/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                Nome: nome,
                Email: email,
                Senha: senha,
                Telefone: Telefone
            })
        })
        console.log("STATUS:", response.status);
        if (response.status !== 204) {
            console.log("Erro ao Editar:", response.status);
        } else {

            alert("Dados Salvos!");
            window.location.href = "/Frontend/scr/html/PaginaLogada.html";
            return;
        }

    }
});

async function deleta() {
    if (!confirm("Confirma que deseja apagar usuario?")) {
        alert("Operação cancelada!");
    }
    var logado = JSON.parse(sessionStorage.getItem("Logado"));
    const id = logado.login.id;

    const response = await fetch(`http://localhost:5074/api/Usuario/${id}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
        }
    });

    if (response.status === 204) {
        alert("Usuário apagado com sucesso!");
        sessionStorage.removeItem("Logado");
        window.location.href = "/Frontend/scr/html/Inicio.html";
        return;
    } else {
        console.log("Erro ao deletar:", response.status);
    }
}