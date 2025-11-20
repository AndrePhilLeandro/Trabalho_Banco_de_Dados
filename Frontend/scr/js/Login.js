document.getElementById("formCompleto").addEventListener("submit", async function (e) {
    e.preventDefault();

    const email = document.getElementById("Email").value;
    const senha = document.getElementById("Senha").value;
    const tipo = document.querySelector('input[name="tipo"]:checked')?.value;
    let verifica = true;
    if (tipo == "aluno") {
        verifica = true;
    }
    else {
        verifica = false;
    }

    if (!email || !senha) {
        alert("Preencha email e senha!");
        return;
    }

    try {
        const response = await fetch("http://localhost:5074/api/Usuario/LoginUser", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                email: email,
                senha: senha,
                ehAluno: verifica
            })
        });
        if (!response.ok) {
            const erro = await response.text();
            alert("Erro: " + erro);
            return;
        }
        const data = await response.json();

        if (data.ativo !== true) {
            alert("Seu usuário está desativado! Não é possível fazer login.");
            return;
        }
        sessionStorage.setItem("Logado", JSON.stringify(data));
        alert("Login feito com sucesso!");
        window.location.href = "/Frontend/scr/html/PaginaLogada.html";

    }
    catch (err) {
        console.error("Erro no fetch:", err);
    }
});
