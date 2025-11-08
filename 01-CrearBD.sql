-- ============================================
-- RICK ADVENTURE GAME - DATABASE CREATION
-- ============================================

CREATE DATABASE RickAdventureGame;
GO

USE RickAdventureGame;
GO

-- ============================================
-- 1. TABLA JUGADORES
-- ============================================
CREATE TABLE Jugadores (
    IdJugador INT PRIMARY KEY IDENTITY(1,1),
    NombreJugador NVARCHAR(50) NOT NULL,
    PuntajeTotal INT DEFAULT 0,
    NivelMaximoAlcanzado INT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE()
);
GO

-- ============================================
-- 2. TABLA NIVELES
-- ============================================
CREATE TABLE Niveles (
    IdNivel INT PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Dificultad NVARCHAR(20) NOT NULL
);
GO

-- ============================================
-- 3. TABLA PARTIDAS
-- ============================================
CREATE TABLE Partidas (
    IdPartida INT PRIMARY KEY IDENTITY(1,1),
    IdJugador INT NOT NULL,
    IdNivel INT NOT NULL,
    Puntaje INT NOT NULL,
    Completado BIT DEFAULT 0,
    Fecha DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IdJugador) REFERENCES Jugadores(IdJugador),
    FOREIGN KEY (IdNivel) REFERENCES Niveles(IdNivel)
);
GO

-- Insertar niveles de prueba
-- Estos hay que definir un nombre para cada nivel
INSERT INTO Niveles (IdNivel, Nombre, Dificultad) VALUES
(1, 'Nivel 1 - Prueba', 'Fácil'),
(2, 'Nivel 2 - Prueba', 'Media'),
(3, 'Nivel 3 - Prueba', 'Difícil');
GO