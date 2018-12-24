CREATE DATABASE `cliente_tatoo` /*!40100 DEFAULT CHARACTER SET utf8 */;

CREATE USER 'cliente_tatoo'@'%' IDENTIFIED BY 'clitatoo@3409'; 
FLUSH PRIVILEGES;
GRANT SELECT ON `endereco`.* TO 'cliente_tatoo'@'%'; 
GRANT ALTER, ALTER ROUTINE, CREATE, CREATE ROUTINE, CREATE TEMPORARY TABLES, CREATE VIEW, DELETE, DROP, EVENT, EXECUTE, INDEX, INSERT, LOCK TABLES, REFERENCES, SELECT, SHOW VIEW, TRIGGER, UPDATE ON `cliente_tatoo`.* TO 'cliente_tatoo'@'%' WITH GRANT OPTION;

CREATE TABLE `usuario` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login` varchar(50) DEFAULT '',
  `senha` varchar(50) DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `clientes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(255) DEFAULT '',
  `cpf` char(11) DEFAULT '',
  `dataNascimento` date DEFAULT NULL,
  `cep` char(8) DEFAULT '',
  `tipoLogradouro` varchar(15) DEFAULT '',
  `logradouro` varchar(255) DEFAULT '',
  `numero` char(20) DEFAULT '',
  `bairro` varchar(255) DEFAULT '',
  `complemento` varchar(100) DEFAULT '',
  `idCidade` int(11) DEFAULT NULL,
  `uf` char(2) DEFAULT '',
  `telefone` varchar(11) DEFAULT NULL,
  `celular` varchar(11) DEFAULT NULL,
  `email` varchar(255) DEFAULT '',
  `idTermoResponsabilidade` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `termo_responsabilidade` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `termo` text,
  `dataCadastro` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `perguntas` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `descricao` varchar(255) DEFAULT '',
  `dissertativa` tinyint(1) DEFAULT '0',
  `isSubPergunta` tinyint(4) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `respostas` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `idpergunta` int(11) DEFAULT '0',
  `descricao` varchar(255) DEFAULT '',
  `idSubPergunta` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `sessao` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `idCliente` int(11) DEFAULT '0',
  `valor` decimal(10,0) DEFAULT NULL,
  `dataExecucao` date DEFAULT NULL,
  `parametros` varchar(255) DEFAULT '',
  `disparos` varchar(255) DEFAULT '',
  `observacao` text,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;