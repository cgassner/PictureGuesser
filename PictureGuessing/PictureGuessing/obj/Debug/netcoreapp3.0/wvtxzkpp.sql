
CREATE TABLE `Difficulties` (
    `DifficultyScale` float NOT NULL,
    `rows` int NOT NULL,
    `cols` int NOT NULL,
    `revealDelay` float NOT NULL,
    CONSTRAINT `PK_Difficulties` PRIMARY KEY (`DifficultyScale`)
);

CREATE TABLE `Pictures` (
    `Id` char(36) NOT NULL,
    `URL` longtext CHARACTER SET utf8mb4 NULL,
    `Answer` longtext CHARACTER SET utf8mb4 NULL,
    `AnswerLength` int NOT NULL,
    CONSTRAINT `PK_Pictures` PRIMARY KEY (`Id`)
);

CREATE TABLE `Game` (
    `Id` char(36) NOT NULL,
    `DifficultyScale` float NULL,
    `pictureID` char(36) NOT NULL,
    `isFinished` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Game` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Game_Difficulties_DifficultyScale` FOREIGN KEY (`DifficultyScale`) REFERENCES `Difficulties` (`DifficultyScale`) ON DELETE RESTRICT
);

CREATE INDEX `IX_Game_DifficultyScale` ON `Game` (`DifficultyScale`);