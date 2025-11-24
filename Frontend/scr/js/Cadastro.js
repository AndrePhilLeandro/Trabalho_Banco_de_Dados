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

document.getElementById("formCadastro").addEventListener("submit", async function (e) {
    e.preventDefault();

    const nome = document.getElementById("Nome").value;
    const cpf = document.getElementById("Cpf").value;
    const Data_nascimento = document.getElementById("Data_nascimento").value;
    const Telefone = document.getElementById("Telefone").value;
    const email = document.getElementById("Email").value;
    const senha = document.getElementById("Senha").value;
    const Confirmasenha = document.getElementById("ConfirmaSenha").value;
    const tipo = document.querySelector('input[name="tipo"]:checked')?.value;
    let T;
    if (tipo === "Aluno") {
        T = 1;
    }
    else if (tipo === "Professor") {
        T = 2;
    }
    else {
        T = 0;
    }
    if (senha !== Confirmasenha) {
        alert("As senhas nao coiencidem");
        return;
    }
    alert("Confirme se os dados estão corretos");

    if (!confirm("Os dados estão corretos?")) {
        return;
    }

    fetch("http://localhost:5074/api/Usuario/CadastroUser", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            Nome: nome,
            Cpf: cpf,
            Data_nascimento: Data_nascimento,
            Telefone: Telefone,
            Email: email,
            Senha: senha,
            Tipo: T
        })
    })
        .then(async response => {
            if (response.status === 409) {
                alert("Cadastro ja Existe!");
                return;
            }

            if (!response.ok) {
                alert("Erro no servidor. Tente novamente.");
                return;
            }

            alert("Cadastro Realizado com Sucesso!");
            window.location.href = "/Frontend/scr/html/Admin.html";
        })
        .catch(error => {
            console.error("Erro:", error);
            alert("Erro ao conectar ao servidor.");
        });
});
