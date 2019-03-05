DROP TABLE versao;

CREATE TABLE "perguntas" (
    `id` INTEGER PRIMARY KEY AUTOINCREMENT,
    `idAlternativa` INTEGER,
    `descricao` TEXT,
    `alternativaUnica` INTEGER,
    `dissertativa` INTEGER,
    `obrigatoria` INTEGER,
    `tipo` TEXT,
    `ativada` INTEGER DEFAULT 1,
    `removida` INTEGER DEFAULT 0
);

CREATE TABLE "alternativas" (
    `id` INTEGER PRIMARY KEY AUTOINCREMENT,
    `idPergunta` INTEGER NOT NULL,
    `descricao` TEXT NOT NULL,
    `ativada` INTEGER NOT NULL DEFAULT 1,
    `removida` INTEGER NOT NULL DEFAULT 0
);

PRAGMA user_version = 1;