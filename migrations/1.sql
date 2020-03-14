ALTER TABLE `usuario` ADD COLUMN `nome` VARCHAR;
ALTER TABLE `usuario` ADD COLUMN `ativo` INTEGER;

UPDATE `usuario` SET `ativo` = 1;

ALTER TABLE `perguntas`
  ADD COLUMN `codigoImportacao` VARCHAR;
  
ALTER TABLE `perguntas`
  ADD COLUMN `colunasAlternativas` INTEGER;

UPDATE `perguntas` SET `colunasAlternativas` =  1;

PRAGMA user_version = 2;