USE RickAdventureGame;
GO

-- Primero desactivar temporalmente las restricciones FK
ALTER TABLE Partidas NOCHECK CONSTRAINT ALL;
GO

-- Borrar datos respetando dependencias (hijo → padre)
DELETE FROM Partidas;
DELETE FROM Jugadores;
GO

-- Reiniciar los contadores de IDENTITY
DBCC CHECKIDENT ('Partidas', RESEED, 0);
DBCC CHECKIDENT ('Jugadores', RESEED, 0);
GO

-- Reactivar las restricciones FK
ALTER TABLE Partidas CHECK CONSTRAINT ALL;
GO
