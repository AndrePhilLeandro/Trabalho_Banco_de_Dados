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
    carregarDiscplina();
    carregarProfessores();
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

//////////////// Carrega Discplina

async function carregarDiscplina() {

    const resp = await fetch("http://localhost:5074/api/Disciplina/MostraDiscplina");
    const Discplina = await resp.json();

    const container = document.getElementById("Discplina");

    if (!container) {
        console.error("ERRO: #Discplina não existe no DOM!");
        return;
    }

    Discplina.forEach(Discplina => {
        const label = document.createElement("label");
        label.style.display = "flex";
        label.style.gap = "6px";
        label.style.margin = "4px 0";

        label.innerHTML = `
            <input type="radio" name="DiscplinaSelecionado" value="${Discplina.id_disciplina}"
>
            ${Discplina.nome}
        `;
        container.appendChild(label);
    });
}


////////////////////// Carrega Professores
async function carregarProfessores() {
    var alunos = {};
    const resp = await fetch("http://localhost:5074/api/Usuario/MostrarTodos");
    const Professores = await resp.json();

    const container = document.getElementById("professores");

    if (!container) {
        console.error("ERRO: #professor não existe no DOM!");
        return;
    }
    Professores.forEach(p => {
        if (p == 1) {
            alunos = p;
        }
        if (p.tipo == 2) {
            const label = document.createElement("label");
            label.style.display = "flex";
            label.style.gap = "6px";
            label.style.margin = "4px 0";

            label.innerHTML = `
            <input type="radio" name="ProfessorSelecionado" value="${p.id}"
>
            ${p.nome}
        `;
            container.appendChild(label);
        }
    });
}


//////////////////// Pega alunos 
async function carregarAlunos() {
    const resp = await fetch("http://localhost:5074/api/Usuario/MostrarTodos");
    const lista = await resp.json();

    const selecao = lista
        .filter(u => u.tipo === 1)
        .map(u => u.id);
    return selecao;
}


//////////////////////// cadastrar turma
document.getElementById("FormCadastroTurma").addEventListener("submit", async function (e) {
    e.preventDefault();

    var semestre = document.getElementById("Semestre").value;
    var ano = document.getElementById("Ano").value;
    var cursoId = document.querySelector("input[name='cursoSelecionado']:checked")?.value;
    var DiscplinaId = document.querySelector("input[name='DiscplinaSelecionado']:checked")?.value;
    var ProfessorId = document.querySelector("input[name='ProfessorSelecionado']:checked")?.value;
    var lista = await carregarAlunos();


    fetch("http://localhost:5074/api/Turma/CadastrarTurma", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            semestre: semestre,
            ano: ano,
            id_disciplinaFK: DiscplinaId,
            id_cursoFK: cursoId,
            id_professorFK: ProfessorId,
            alunos: lista,
            ativo: true
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
            document.getElementById("FormCadastroTurma").reset();
        });
});
