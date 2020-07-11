create database tatooreport;

use tatooreport;

create table user(
    id INT(11) AUTO_INCREMENT PRIMARY KEY NOT NULL,
    cpf CHAR(11),
    `name` VARCHAR(255),
    surname VARCHAR(255),
    email VARCHAR(255),
    phone VARCHAR(255),
    mobilePhone VARCHAR(255),
    normalizedName VARCHAR(255),
    passwordHash VARCHAR(255)
);

create table role(
    id INT(11) AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `name` VARCHAR(255),
    normalizedName VARCHAR(255),
    removed TINYINT(4) DEFAULT 0
);

create table user_role(
    id INT(11) AUTO_INCREMENT PRIMARY KEY NOT NULL,
    userId INT(11) NOT NULL,
    roleId INT(11) NOT NULL,
    removed TINYINT(4) DEFAULT 0
);

create table branch_network(
    id INT(11) AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `name` VARCHAR(255) 
);

create table branch_network_user(
	id INT(11) AUTO_INCREMENT PRIMARY KEY NOT NULL,
    branchNetworkId INT(11) NOT NULL,
    userId INT(11) NOT NULL
);

create table branch(
    id INT(11) AUTO_INCREMENT PRIMARY KEY NOT NULL,
    branchNetworkId INT(11) NOT NULL,
    `name` VARCHAR(255),
    addressPostalCode CHAR(8),
    addressStreetType VARCHAR(20),
    addressStreet VARCHAR(255),
    addressComplement VARCHAR(255),
    addressNumber VARCHAR(20),
    addressDistrict VARCHAR(100),
    addressCity VARCHAR(255),
    addressState VARCHAR(30)
);