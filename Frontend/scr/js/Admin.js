window.onload = function () {
    var logado = JSON.parse(sessionStorage.getItem("Logado"));
    document.getElementById("userBtn").innerText = logado.login.ome;
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

/* Listar todos */
let usuarios = []; // lista global usada para filtros

// 1. BUSCAR NA API
async function buscarUsuarios() {
    try {
        const res = await fetch("http://localhost:5074/api/Usuario/MostrarTodos");
        const data = await res.json();

        usuarios = data;
        carregarUsuariosNaTela(usuarios);
    } catch (err) {
        console.error("Erro:", err);
    }
}


// 2. CARREGAR TABELA
function carregarUsuariosNaTela(lista) {

    const tbody = document.getElementById("listaUsuarios");
    tbody.innerHTML = "";

    lista.forEach(u => {

        let situacao;
        let textoBotao;

        if (u.ativo == 1) {
            situacao = "Ativo";
            textoBotao = "Desativar";
        } else {
            situacao = "Inativo";
            textoBotao = "Ativar";
        }


        tbody.innerHTML += `
            <tr>
                <td>${u.id}</td>
                <td>${u.nome}</td>
                <td>${u.cpf}</td>
                <td>${u.data_nascimento}</td>
                <td>${u.telefone}</td>
                <td>${u.email}</td>
                <td>${retornaTipo(u.tipo)}</td>
                <td>${situacao}</td>
                <td>
                    <button onclick="Acao(${u.id},${u.ativo})">
                        ${textoBotao}
                    </button>
                </td>
            </tr>
        `;
    });
}


async function Acao(id, situacao) {
    if (situacao == true) {
        const response = await fetch(`http://localhost:5074/api/Usuario/${id}`, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
            }
        });
        window.location.reload();

    }
    else {
        const response = await fetch(`http://localhost:5074/api/Usuario/${id}`, {
            method: "PATCH",
            headers: {
                "Content-Type": "application/json",
            }
        });
        window.location.reload();
    }
}


function retornaTipo(tipo) {
    switch (tipo) {
        case 0: return "Admin";
        case 1: return "Aluno";
        case 2: return "Professor";
    }
}

buscarUsuarios();
