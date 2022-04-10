CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
SELECT
  *
FROM
  accounts;
CREATE TABLE IF NOT EXISTS recipes(
    id INT AUTO_INCREMENT primary key,
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
    'Drakes Cake',
    'Meh',
    'Dessert',
    'https: / / cookieandkate.com / images / 2019 / 07 / best - tabbouleh - recipe -3.jpg',
    '624f510d7a16ad15a236c950'
  );
-- Recipes
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
    id INT AUTO_INCREMENT primary key,
    name VARCHAR(255) NOT NULL,
    quantity VARCHAR(255) NOT NULL,
    recipeId INT NOT NULL,
    FOREIGN KEY (recipeId) REFERENCES recipes(id)
  ) DEFAULT charset utf8 COMMENT '';
-- Ingredients
  DROP TABLE ingredients;
INSERT INTO
  ingredients(name, quantity, recipeId)
VALUES
  ('Feta', 'One', 2);
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
  r.id = i.recipeId;
DELETE FROM
  ingredients
WHERE
  recipeId = 1
LIMIT
  1;
CREATE TABLE IF NOT EXISTS steps(
    id INT AUTO_INCREMENT primary key,
    position INT,
    body TEXT,
    recipeId INT NOT NULL,
    FOREIGN KEY(recipeId) REFERENCES recipes(id)
  ) default charset utf8 COMMENT '';
SELECT
  *
FROM
  steps;
DROP TABLE steps;
INSERT INTO
  steps (position, body, recipeId)
VALUES
  (1, "Cut Cheese", 2);
SELECT
  s.*,
  r.*
FROM
  steps s
  JOIN recipes r
WHERE
  r.id = s.recipeId;
SELECT
  *
FROM
  ingredients i
WHERE
  i.recipeId = 2;
DELETE FROM
  steps
WHERE
  id = 18
LIMIT
  1;
CREATE TABLE IF NOT EXISTS favorites(
    id INT AUTO_INCREMENT NOT NULL primary key,
    accountId VARCHAR(255) NOT NULL,
    recipeId INT NOT NULL,
    FOREIGN KEY (accountID) REFERENCES accounts(id) ON DELETE CASCADE,
    FOREIGN KEY (recipeID) REFERENCES recipes(id) ON DELETE CASCADE
  ) default charset utf8 COMMENT '';