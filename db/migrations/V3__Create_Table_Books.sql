﻿CREATE TABLE books (
  id INT AUTO_INCREMENT PRIMARY KEY,
  author LONGTEXT,
  launch_date DATETIME(6) NOT NULL,
  price DECIMAL(10,2) NOT NULL,
  title LONGTEXT
);
