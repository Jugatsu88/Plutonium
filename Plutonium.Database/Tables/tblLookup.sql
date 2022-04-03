CREATE TABLE [tblLookup] (
    "id" INTEGER NOT NULL CONSTRAINT "PK_tblLookup" PRIMARY KEY AUTOINCREMENT,
    "LookupName" TEXT NULL,
    "Code" TEXT NULL,
    "Desc" TEXT NULL,
	"Ordering" INTEGER NULL,
	"IsActive" BIT NULL
)

