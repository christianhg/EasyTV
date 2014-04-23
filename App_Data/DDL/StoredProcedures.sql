DROP PROCEDURE sp_InsertTvProvider, sp_GetTvProvider, sp_GetTvProviders, sp_UpdateTvProvider, sp_DeleteTvProvider, sp_InsertPackage, sp_GetPackage, sp_GetPackages, sp_UpdatePackage, sp_DeletePackage, sp_InsertChannel, sp_GetChannel, sp_GetChannels, sp_UpdateChannel, sp_DeleteChannel, sp_InsertPackageStreamingService, sp_GetStreamingServices, sp_DeletePackageStreamingService, sp_InsertStreamingService, sp_GetStreamingService, sp_GetStreamingService, sp_UpdateStreamingService, sp_DeleteStreamingService, sp_InsertExclusionArea, sp_GetExclusionAreas, sp_UpdateExclusionArea, sp_DeleteExclusionArea;

CREATE PROCEDURE sp_InsertTvProvider
	@name nvarchar(50),
	@info nvarchar(200),
	@logo nvarchar(50),
	@address nvarchar(200),
	@phone int,
	@url nvarchar(200)
AS
	INSERT INTO tbl_tvprovider
	(name, info, logo, address, phone, url)
	VALUES
	(@name, @info, @logo, @address, @phone, @url);
GO

CREATE PROCEDURE sp_GetTvProvider
	@pk_id int
AS
	SELECT * FROM tbl_tvProvider WHERE pk_id = @pk_id
GO

CREATE PROCEDURE sp_GetTvProviders
AS
	SELECT * FROM tbl_tvProvider
GO

CREATE PROCEDURE sp_UpdateTvProvider
	@pk_id int,
	@name nvarchar(50),
	@info nvarchar(200),
	@logo nvarchar(50),
	@address nvarchar(200),
	@phone int,
	@url nvarchar(200)
AS
	UPDATE tbl_tvProvider
SET 
	name = @name,
	info = @info,
	logo = @logo,
	address = @address,
	phone = @phone,
	url = @url
WHERE 
	pk_id = @pk_id
GO

CREATE PROCEDURE sp_DeleteTvProvider
	@pk_id int
AS
	DELETE FROM tbl_tvProvider WHERE pk_id = @pk_id
GO

CREATE PROCEDURE sp_InsertExclusionArea
	@highZip int,
	@lowZip int
AS
	INSERT INTO tbl_exclusionArea
	(highZip, lowZip)
	VALUES
	(@highZip, @lowZip)
GO

CREATE PROCEDURE sp_GetExclusionAreas
	@fk_tvProvider_id int
AS
	SELECT * FROM tbl_exclusionArea 
	WHERE fk_tvProvider_id = @fk_tvProvider_id;
GO

CREATE PROCEDURE sp_UpdateExclusionArea
	@pk_id int,
	@highZip int,
	@lowZip int
AS
	UPDATE tbl_exclusionArea
	SET highZip = @highZip, lowZip = @lowZip
	WHERE pk_id = @pk_id;
GO

CREATE PROCEDURE sp_DeleteExclusionArea
	@pk_id int
AS
	DELETE FROM tbl_exclusionArea
	WHERE pk_id = @pk_id;
GO

CREATE PROCEDURE sp_InsertPackage
	@name nvarchar(50),
	@info nvarchar(200),
	@url nvarchar(200),
	@pricePerMonth decimal(8,2),
	@startUpFee decimal(8,2),
	@fk_tvProvider_id int
AS
	INSERT INTO tbl_package
	(name, info, url, pricePerMonth, startUpFee, fk_tvProvider_id)
	VALUES
	(@name, @info, @url, @pricePerMonth, @startUpFee, @fk_tvProvider_id);
GO

CREATE PROCEDURE sp_GetPackage
	@pk_id int
AS
	SELECT * FROM tbl_package WHERE pk_id = @pk_id
GO

CREATE PROCEDURE sp_GetPackages
AS
	SELECT * FROM tbl_package
GO

CREATE PROCEDURE sp_UpdatePackage
	@pk_id int,
	@name nvarchar(50),
	@info nvarchar(200),
	@url nvarchar(200),
	@pricePerMonth decimal(8,2),
	@startUpFee decimal(8,2),
	@fk_tvProvider_id int
AS
	UPDATE tbl_package
SET
	name = @name,
	info = @info,
	url = @url,
	pricePerMonth = @pricePerMonth,
	startUpFee = @startUpFee,
	fk_tvProvider_id = @fk_tvProvider_id
WHERE
	pk_id = @pk_id
GO

CREATE PROCEDURE sp_DeletePackage
	@pk_id int
AS
	DELETE FROM tbl_package WHERE pk_id = @pk_id
GO

CREATE PROCEDURE sp_InsertChannel
	@name nvarchar(50),
	@info nvarchar(200),
	@logo nvarchar(50)
AS
	INSERT INTO tbl_channel
	(name, info, logo)
VALUES
	(@name, @info, @logo);
GO

CREATE PROCEDURE sp_GetChannel
	@pk_id int
AS
	SELECT * FROM tbl_channel WHERE pk_id = @pk_id
GO

CREATE PROCEDURE sp_GetChannels
AS
	SELECT * FROM tbl_channel
GO

CREATE PROCEDURE sp_UpdateChannel
	@pk_id int,
	@name nvarchar(50),
	@info nvarchar(200),
	@logo nvarchar(50)
AS
	UPDATE tbl_channel
SET
	name = @name,
	info = @info,
	logo = @logo
WHERE
	pk_id = @pk_id
GO

CREATE PROCEDURE sp_DeleteChannel
	@pk_id int
AS
	DELETE FROM tbl_channel WHERE pk_id = @pk_id
GO

CREATE PROCEDURE sp_GetPackageChannels
	@fk_package_id int
AS
	SELECT c.pk_id, c.name, c.info, c.logo 
	FROM tbl_channel as c 
	INNER JOIN tbl_package_channel AS pc 
	ON pc.fk_channel_id = c.pk_id 
	WHERE pc.fk_package_id = @fk_package_id;
GO

CREATE PROCEDURE sp_InsertPackageStreamingService 
	@fk_package_id int, 
	@fk_streamingService_id int
AS
	INSERT INTO tbl_PackageStreamingService
	(fk_package_id, fk_streamingService_id)
	VALUES 
	(@fk_package_id, @fk_streamingService_id)
GO

CREATE PROCEDURE sp_GetStreamingServices 
	@fk_package_id int
AS
	SELECT p.*, ss.* FROM tbl_streamingService AS ss
	INNER JOIN tbl_package_streamingService AS pss 
	ON pss.fk_package_id = @fk_package_id
	INNER JOIN tbl_package AS p 
	ON p.pk_id = pss.fk_package_id
	WHERE pss.fk_package_id = @fk_package_id
GO

CREATE PROCEDURE sp_DeletePackageStreamingService
	@fk_package_id int, 
	@fk_streamingService_id int
AS
	DELETE FROM tbl_package_streamingService
	WHERE fk_package_id = @fk_package_id AND fk_streamingService_id = @fk_streamingService_id
GO

CREATE PROCEDURE sp_InsertStreamingService (@name nvarchar(50))
as
insert into tbl_streamingService values(@name)
GO

CREATE PROCEDURE sp_GetStreamingService (@pk_id int)
as
select * from tbl_streamingService where pk_id = @pk_id
GO

CREATE PROCEDURE sp_GetStreamingService (@pk_id int)
as
select * from tbl_streamingService
GO

CREATE PROCEDURE sp_UpdateStreamingService (@pk_id int, @name nvarchar(50))
as
update tbl_streamingService 
set name = @name
where pk_id = @pk_id
GO

CREATE PROCEDURE sp_DeleteStreamingService(@pk_id int)
as
delete from tbl_streamingService
where pk_id = @pk_id
GO