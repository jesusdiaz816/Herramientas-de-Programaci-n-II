select * from empleados;

create database TareaConexionBD;
use TareaConexionBD;

CREATE TABLE empleados (

idempleado INTEGER PRIMARY KEY,
apellido VARCHAR(50) NOT NULL,
nombre VARCHAR(50) NOT NULL,
direccion VARCHAR(100) NOT NULL,
email VARCHAR(50) NOT NULL

);

INSERT INTO empleados VALUES
(111, 'Vélez', 'Jaime','Cra 1 #1-1','vj@gmail.com'),
(222, 'Roldan', 'Eliecer','Cra 2 #1-1','re@gmail.com'),
(333, 'Perez', 'Diego','Cra 3 #1-1','pd@gmail.com'),
(444, 'Ochoa', 'Leo','Cra 5 #1-1','ol@gmail.com'),
(555, 'Roa', 'Ana','Cra 6 #1-1','ra@gmail.com');