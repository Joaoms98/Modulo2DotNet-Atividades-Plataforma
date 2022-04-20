
INSERT INTO Usuario
VALUES
(1,'jvm1800@gmail.com','coelhinhorosa','João victor'),
(2,'Igonago@gmail.com','vilageofthesunchine','George ducke'),
(3,'Franknotgay@gmail.com','incaroads?', 'Frank zappa')

SELECT * FROM Usuario

INSERT INTO Postagem
VALUES
(1,'MY NEW SONG (BOGO SORT)','New song the band FEPASA SUPERMERCADOS','lhfjlahfdjkjdgfg5456s1d584fd56s4f64.png',1),
(2,'I like buy a new Saxfone, somewere sell to me?','','',2),
(3,'Hello folks, it is my first post','','dskdjkaljdasjdsjhfdhwqj544adf44s585.png',3),
(4,'Where found one beatiful guitar for my collection?','','',1)

SELECT * FROM Postagem

INSERT INTO Grupos
VALUES
(1,'Group for a musicians',1,1)

SELECT * FROM Grupos

SELECT * FROM Postagem
where Fk_Criador LIKE 1