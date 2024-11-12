CREATE DATABASE IF NOT EXISTS sistema_certificado;
USE sistema_certificado;

CREATE TABLE students_db (
  id int NOT NULL AUTO_INCREMENT,
  name varchar(255) NOT NULL,
  nationality varchar(255) NOT NULL,
  state varchar(255) NOT NULL,
  birthDate varchar(255) NOT NULL,
  RG varchar(255) NOT NULL,
  conclusionDate varchar(255) NOT NULL,
  issueDate varchar(255) NOT NULL,
  course varchar(255) NOT NULL,
  workLoad varchar(255) NOT NULL,
  sign varchar(255) NOT NULL,
  role varchar(255) NOT NULL,
  PRIMARY KEY (id)
) 

CREATE TABLE certificate_files_db (
  id int NOT NULL AUTO_INCREMENT,
  name varchar(255) NOT NULL,
  fileName varchar(255) NOT NULL,
  filePath varchar(500) NOT NULL,
  PRIMARY KEY (id)
)
