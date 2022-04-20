CREATE DATABASE db_Classificado
USE db_Classificado


CREATE TABLE Usuario(
	Id INT PRIMARY KEY NOT NULL,
	nome VARCHAR(50) NOT NULL,
	Email VARCHAR(50) NOT NULL,
	Senha VARCHAR(100) NOT NULL,
	Telefone VARCHAR(50) NULL,
)
CREATE TABLE Postagem(
	Id INT PRIMARY KEY NOT NULL,
	Titulo VARCHAR(50) NOT NULL,
	Descricao VARCHAR(200) NOT NULL,
	Foto VARCHAR(200) NOT NULL,
	Fk_Usuario INT NOT NULL,
	FOREIGN KEY (Fk_Usuario) REFERENCES Usuario (Id)
)