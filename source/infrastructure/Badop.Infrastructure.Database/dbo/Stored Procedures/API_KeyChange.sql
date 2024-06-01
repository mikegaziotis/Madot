CREATE PROCEDURE [dbo].[API_KeyChange]
	@Name varchar(50),
	@NewHashedKey varchar(64),
	@OldHashedKey varchar(64) = NULL,
	@Force bit = 0,
    @UserId varchar(100) = NULL
AS 
BEGIN

	UPDATE [dbo].[Api]
	SET
      [Sha256HashedKey] = @NewHashedKey,
      [LastModifiedBy] = @UserId,
      [LastModifiedDate] = GETUTCDATE()
	WHERE [Name] = @Name AND [Sha256HashedKey] = IIF(@Force = 0, @OldHashedKey, [Sha256HashedKey])

	SELECT CAST(IIF(@@ROWCOUNT = 1, 1, 0) as bit) Success
END