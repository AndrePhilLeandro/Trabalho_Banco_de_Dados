document.getElementById("formCompleto").addEventListener("submit", async function (e) {
    e.preventDefault();

    const nome = document.getElementById("Nome").value;
    const cpf = document.getElementById("Cpf").value;
    const Data_nascimento = document.getElementById("Data_nascimento").value;
    const Telefone = document.getElementById("Telefone").value;
    const email = document.getElementById("Email").value;
    const senha = document.getElementById("Senha").value;
    const tipo = document.querySelector('input[name="tipo"]:checked')?.value;

    fetch("http://localhost:5074/api/Usuario/CadastroUser", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            Nome: nome,
            Cpf: cpf,
            Data_nascimento: Data_nascimento,
            Telefone: Telefone,
            Email: email,
            Senha: senha,
            EhAluno: tipo === "aluno"
        })
    })
        .then(response => response.json())
        .then(data => {
            console.log("Resposta:", data);
        })
        .catch(erro => console.error("Erro:", erro));
});
