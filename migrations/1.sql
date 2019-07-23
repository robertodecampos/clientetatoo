ALTER TABLE `usuario` ADD COLUMN `nome` VARCHAR;
ALTER TABLE `usuario` ADD COLUMN `ativo` INTEGER;

UPDATE `usuario` SET `ativo` = 1;

PRAGMA user_version = 2;