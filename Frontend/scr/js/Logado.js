async function deleta() {
    if (!confirm("Confirma que deseja apagar usuario?")) {
        alert("Operação cancelada!");
    }
    var logado = JSON.parse(sessionStorage.getItem("Logado"));
    const id = logado.id;

    const response = await fetch(`http://localhost:5074/api/Usuario/${id}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
        }
    });

    if (response.status === 204) {
        alert("Usuário apagado com sucesso!");
        sessionStorage.removeItem("Logado");
        window.location.href = "/Frontend/src/html/Inicio.html";
        return;
    } else {
        console.log("Erro ao deletar:", response.status);
    }
}
