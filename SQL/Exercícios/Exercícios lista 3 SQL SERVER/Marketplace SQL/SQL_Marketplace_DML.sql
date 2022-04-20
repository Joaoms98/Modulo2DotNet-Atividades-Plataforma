INSERT INTO Usuario
VALUES
(1, 'Vitor','joano@gmail.com','5454666','rua das margaridas,564-SP'),
(2, 'joao victor','jvm1800@gmail.com','123456','rua das piriri,168-SP'),
(3, 'Joaozinho piriri','jailson@gmail.com','321654','rua das anta,165-MT')

SELECT*FROM Usuario

INSERT INTO Vendedor
VALUES
(1, 'Vitor','joano@gmail.com','5454666','11996896545'),
(2, 'joao victor','jvm1800@gmail.com','123456','11996986548454'),
(3, 'Joaozinho piriri','jailson@gmail.com','321654','119968877894')

SELECT*FROM Vendedor

INSERT INTO Produtos
VALUES
(1,'BICLETA MOUNTAIN BIKE', 'Bike pra andar na neve do Brasil',350.00,5,2),
(2,'Barraca para se abrigar no meio do mato', 'Barraca camuflada com um bucaro no chão para servir de banheiro',400.00,15,1),
(3,'Óculos de sol para tempos nublados', 'OBS=Não usar em tempos ensolarados',100.00,10,1)

SELECT*FROM Produtos

INSERT INTO Carrinho
VALUES
(1,2,3,1),
(2,1,3,2),
(3,3,2,3)

SELECT*FROM Carrinho

Select *FROM Produtos
Where Valor BETWEEN 300 and 500