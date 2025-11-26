window.onload = function () {
    var logado = JSON.parse(sessionStorage.getItem("Logado"));
    document.getElementById("userBtn").innerText = logado.nome;
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


document.addEventListener("DOMContentLoaded", () => {
    carregarCursos();
});

async function carregarCursos() {

    const resp = await fetch("http://localhost:5074/api/Curso/MostraCursos");
    const cursos = await resp.json();

    const container = document.getElementById("cursos");

    if (!container) {
        console.error("ERRO: #cursos não existe no DOM!");
        return;
    }

    cursos.forEach(curso => {
        const label = document.createElement("label");
        label.style.display = "flex";
        label.style.gap = "6px";
        label.style.margin = "4px 0";

        label.innerHTML = `
            <input type="radio" name="cursoSelecionado" value="${curso.id_curso}"
>
            ${curso.nome}
        `;
        container.appendChild(label);
    });
}


////////////////// Cadastrar Discplina

document.getElementById("FormCadastroDisciplina").addEventListener("submit", async function (e) {
    e.preventDefault();

    var nome = document.getElementById("Nome").value;
    var carga = document.getElementById("Carga_horaria").value;
    var cursoId = document.querySelector("input[name='cursoSelecionado']:checked")?.value;

    fetch("http://localhost:5074/api/Disciplina/Cadastro_Disciplina", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            Nome: nome,
            Carga_horaria: carga,
            Id_cursoFK: cursoId,
            Ativo: true
        })
    })
        .then(async response => {
            if (response.status === 409) {
                alert("Cadastro já existe!");
                return;
            }

            if (!response.ok) {
                alert("Erro no servidor. Tente novamente.");
                return;
            }

            alert("Cadastro Realizado com Sucesso!");
            document.getElementById("FormCadastroDisciplina").reset();
        });
});
