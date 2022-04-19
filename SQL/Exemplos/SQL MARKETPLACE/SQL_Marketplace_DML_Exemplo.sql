--DML
--INSERT
--SELECT
--UPDATE
--DELETE

USE db_marketplace
INSERT INTO Usuario
VALUES
(3, 'Vitor','joano@gmail.com','5454666','rua das margaridas,564-SP'),
(1, 'joao victor','jvm1800@gmail.com','123456','rua das piriri,168-SP'),
(2, 'Joaozinho piriri','jailson@gmail.com','321654','rua das anta,165-SP')

INSERT INTO Categoria
VALUES
('Animais'),
('Humanos')

INSERT INTO Produtos
VALUES
('Shampoo segura cabelo', 'Shampoo anti-queda', 15.25,1,2),
('Sabonete Dog', 'Sabonete para uso do seu cachorrinho',2.75,1,1),
('Sabonete Cat', 'Sabonete para uso do seu Gato',5.75,1,1)

INSERT INTO Compras
VALUES
(1,2),
(2,1)

SELECT*FROM Categoria
SELECT*FROM Produtos
SELECT * FROM Usuario

SELECT * FROM Usuario
Where Nome LIKE '%Joao%'

Select *FROM Produtos
Where Preco BETWEEN 2 and 15

UPDATE Usuario
SET Nome = 'Joao vitor'
Where Id = 1
