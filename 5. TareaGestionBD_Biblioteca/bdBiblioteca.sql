CREATE DATABASE Biblioteca;

USE Biblioteca;

CREATE TABLE usuarios(
    nombre VARCHAR(40) PRIMARY KEY,
    password VARCHAR(255) NOT NULL
);

CREATE TABLE libros(
    codigo INT PRIMARY KEY,
    titulo VARCHAR(40) NOT NULL,
    autor VARCHAR(80) NOT NULL
);

CREATE TABLE suscriptores(
    documento VARCHAR(15) PRIMARY KEY,
    nombre VARCHAR(30),
    direccion VARCHAR(30)
);

CREATE TABLE prestamos(
    docsuscriptor VARCHAR(15) NOT NULL,
    codigolibro INT,
    fechaprestamo DATE NOT NULL,
    fechadevolucion DATE,
    PRIMARY KEY (codigolibro, fechaprestamo),
    FOREIGN KEY (docsuscriptor) REFERENCES suscriptores(documento),
    FOREIGN KEY (codigolibro) REFERENCES libros(codigo)
);

insert into usuarios values
 ('admin','123456789');

insert into suscriptores values
 ('111','Juan Perez','Cra 1 #1-1'),
 ('222','Luis Lopez','Cra 2 #1-1'),
 ('333','Ana Herrero','Cra 3 #1-1');

insert into libros values
 (1,'Cien años de soledad','Gabriel García Márquez'),
 (2,'María','Jorge Isaacs'),
 (25,'La Vorágine','José Eustasio Rivera'),
 (42,'La casa de las dos palmas','Manuel Mejía Vallejo');

insert into prestamos values
 ('111',1,'2025-11-10','2025-11-12'),
 ('111',1,'2025-11-15',null),
 ('222',25,'2025-11-10','2025-11-13'),
 ('222',42,'2025-11-10',null),
 ('222',25,'2025-11-15',null),
 ('333',42,'2025-11-02','2025-11-05'),
 ('222',2,'2025-11-02','2025-11-05');

 select * from prestamos;