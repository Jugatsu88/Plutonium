CREATE TABLE [BatchFiles] (
    "id" INTEGER NOT NULL CONSTRAINT "PK_BatchFile" PRIMARY KEY AUTOINCREMENT,
    "FileName" TEXT NOT NULL,
    "Contents" TEXT NULL
)