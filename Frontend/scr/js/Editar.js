document.getElementById("formCompleto").addEventListener("submit", async function (e) {
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
        const id = logado.id;

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
            window.location.href = "/Frontend/src/html/PaginaLogada.html";
            return;
        }

    }
});