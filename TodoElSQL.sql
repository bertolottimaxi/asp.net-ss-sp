
CREATE DATABASE DB2

CREATE TABLE usuario(
idUsuario int primary key,
pass varchar(500)
)

CREATE TABLE Personal_info(
dni int primary key,
nombreYapellido varchar(100)
)

insert into usuario values(1234,'p1234')
insert into usuario values(5678,'p56785')

select * from usuario

insert into Personal_info values(34453573,'Bertolotti Maximiliano')
insert into Personal_info values(40123456,'Echagüe Pascual')
insert into Personal_info values(33222111,'Roberto Funes')
insert into Personal_info values(35435117,'Pazos Andrés')
insert into Personal_info values(42123456,'Laprida Narciso')

CREATE PROCEDURE ValidarUsuario(
@Id int,
@Pass varchar(500)
)
AS
BEGIN
	if(exists(select * from usuario where idUsuario = @Id and pass = @Pass))
		select idUsuario from usuario where idUsuario = @Id and pass = @Pass
	else 
		select '0'
END




CREATE PROCEDURE AgregarAgente(
@Dni int,
@NombreyApellido VARCHAR(100)
)
AS
BEGIN
	INSERT INTO Personal_info values(@Dni,@NombreyApellido)
END


INSERT INTO Personal_info values(12345,'abcde')
select * from Personal_info

exec AgregarAgente 121212,'ababab'

