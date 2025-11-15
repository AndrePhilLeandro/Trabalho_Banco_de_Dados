const express = require("express");
const app = express();

app.get("/", (req, res) => {
    res.send("Servidor Node rodando!");
});

app.listen(3000, () => {
    console.log("Servidor iniciado em http://localhost:3000");
});
