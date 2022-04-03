CREATE TABLE [MenuItems] (
    "id" INTEGER NOT NULL CONSTRAINT "PK_MenuItem" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Url" TEXT NULL,
    "ParentId" INT NULL,
    "Ordering" INT NULL,
    "IsVisible" BIT NULL,
    "IsActive" BIT NULL
)