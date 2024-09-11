CREATE TABLE `users` (
    `id` INT NOT NULL AUTO_INCREMENT,
    `user_name` VARCHAR(50) NOT NULL DEFAULT '0',
    `password` VARCHAR(130) NOT NULL DEFAULT '0',
    `full_name` VARCHAR(120) NOT NULL,
    `refresh_token` VARCHAR(500) DEFAULT '0',
    `refresh_token_expiry_time` DATETIME DEFAULT NULL,
    PRIMARY KEY (`id`),
    UNIQUE KEY `user_name_unique` (`user_name`)
)
ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
