const express = require("express");
const app = express();

app.post("http://localhost:5074/api/Usuario/LoginUser", (req, res) => {
    res.send("Servidor Node rodando!");
});

app.listen(3000, () => {
    console.log("Servidor iniciado em http://localhost:3000");
});
