DROP TABLE versao;

CREATE TABLE "perguntas" (
    `id` INTEGER PRIMARY KEY AUTOINCREMENT,
    `idResposta` INTEGER,
    `descricao` TEXT,
    `respostaUnica` INTEGER,
    `respostaDissertativa` INTEGER,
    `obrigatoria` INTEGER,
    `tipo` TEXT,
    `ativada` INTEGER DEFAULT 1,
    `removida` INTEGER DEFAULT 0
);

CREATE TABLE "respostas" (
    `id` INTEGER PRIMARY KEY AUTOINCREMENT,
    `idPergunta` INTEGER NOT NULL,
    `descricao` TEXT NOT NULL,
    `especificar` INTEGER
);

PRAGMA user_version = 1;