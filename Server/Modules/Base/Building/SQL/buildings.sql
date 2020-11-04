CREATE TABLE IF NOT EXISTS `buildings` (
    `Identifier` VARCHAR(50) NOT NULL,
	`Model` VARCHAR(50) NOT NULL,
	`Position` LONGTEXT NOT NULL,

	PRIMARY KEY (`Identifier`)
);