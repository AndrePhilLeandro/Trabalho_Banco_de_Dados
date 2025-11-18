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

    console.log(email,senha,verifica);

    // Validação simples
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

        // Se a API retornou erro (401, 422, 404)
        if (!response.ok) {
            const erro = await response.text();
            alert("Erro: " + erro);
            return;
        }

        const token = await response.text(); // sua API retorna uma STRING


        // Salvar token
        sessionStorage.setItem("Logado", token);

        alert("Login feito com sucesso!");
        window.location.href = "https://www.youtube.com";

    }
    catch (err) {
        console.error("Erro no fetch:", err);
    }
});
