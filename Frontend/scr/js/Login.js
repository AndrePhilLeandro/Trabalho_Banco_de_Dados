document.getElementById("formCompleto").addEventListener("submit", async function (e) {
    e.preventDefault();

    const email = document.getElementById("Email").value;
    const senha = document.getElementById("Senha").value;
    const tipo = document.querySelector('input[name="tipo"]:checked')?.value;
    var verifica;
    if (tipo == "Admin") {
        verifica = 0;
    }
    else if (tipo == "Aluno") {
        verifica = 1;
    }
    else {
        verifica = 2;
    }
    if (!email || !senha) {
        alert("Preencha email e senha!");
        return;
    }

    //return console.log(tipo);

    try {
        const response = await fetch("http://localhost:5074/api/Usuario/LoginUser", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                email: email,
                senha: senha,
                Tipo: verifica
            })
        });
        if (!response.ok) {
            const erro = await response.text();
            alert("Erro: " + erro);
            return;
        }
        const data = await response.json();

        if (data.login.ativo == false) {
            alert("Seu usuário está desativado! Não é possível fazer login.");
            return;
        }
        sessionStorage.setItem("Logado", JSON.stringify(data));
        alert("Login feito com sucesso!");
        if (data.login.tipo == 1) {
            window.location.href = "/Frontend/scr/html/Aluno.html";
        }
        else if (data.login.tipo == 2) {
            window.location.href = "/Frontend/scr/html/Professor.html";
        }
        else{
            window.location.href = "/Frontend/scr/html/Admin.html";
        }
    }
    catch (err) {
        console.error("Erro no fetch:", err);
    }
});
