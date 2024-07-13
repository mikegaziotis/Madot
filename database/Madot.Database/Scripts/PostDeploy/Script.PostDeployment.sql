-- This file contains SQL statements that will be executed after the build script.
DECLARE @seedData INT = (SELECT CAST('$(SeedData)' AS INT));

PRINT '>>> START TEST SQLCMD VARABLES <<<'
PRINT '$(SeedData):' + CAST(@seedData as VARCHAR(10))
PRINT '>>> END TEST SQLCMD VARABLES <<<<'

IF(@seedData=1)
BEGIN
    PRINT('Seeding data');
:r .\SeedData.sql    
END
GO