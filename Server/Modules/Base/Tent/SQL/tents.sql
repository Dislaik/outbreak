CREATE TABLE IF NOT EXISTS `tents` (
    `Identifier` VARCHAR(50) NOT NULL,
	`Inventory` LONGTEXT NOT NULL,
	`Position` LONGTEXT NOT NULL,

	PRIMARY KEY (`Identifier`)
);