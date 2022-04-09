CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS recipes(
  id INT AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  title TEXT NOT NULL,
  subtitle TEXT NOT NULL,
  category TEXT NOT NULL,
  imgUrl TEXT NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id)
) DEFAULT charset utf8 COMMENT '';
INSERT INTO
  recipes (title, subtitle, category, imgUrl, creatorId)
VALUES
  (
    'Tams Tabbouleh',
    'Complicated',
    'Mediterranean',
    'https: / / cookieandkate.com / images / 2019 / 07 / best - tabbouleh - recipe -3.jpg',
    '624f510d7a16ad15a236c950'
  );
SELECT
  *
FROM
  recipes;
SELECT
  r.*,
  a.*
FROM
  recipes r
  JOIN accounts a
WHERE
  a.id = r.creatorId;
DELETE FROM
  recipes
WHERE
  id = 2
LIMIT
  1;
UPDATE
  recipes
SET
  title = "Brocolli Cheddar",
  subtitle = "Its great"
WHERE
  id = 1;
CREATE TABLE IF NOT EXISTS ingredients(
    recipeId INT NOT NULL,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name TEXT NOT NULL,
    quantity TEXT NOT NULL,
    FOREIGN KEY (recipeId) REFERENCES recipes(id)
  ) DEFAULT charset utf8 COMMENT '';
INSERT INTO
  ingredients(name, quantity, recipeId)
VALUES
  ('Egg', 'One Egg', 2);
SELECT
  *
FROM
  ingredients;
SELECT
  i.*,
  r.*
FROM
  ingredients i
  JOIN recipes r
WHERE
  i.recipeId = r.id;
DELETE FROM
  ingredients
WHERE
  recipeId = 1
LIMIT
  1;