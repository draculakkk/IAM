CREATE TRIGGER TG_ToLower ON TC_UserInfo FOR INSERT
AS
BEGIN
	UPDATE t SET t.UserID=LOWER(t.UserID)
	FROM dbo.TC_UserInfo t
	INNER JOIN Inserted ON t.UserID=Inserted.UserID 
END
