## OnlineShop.Data:
	Add-Migration -Name "MigrationName" -Context "FsaDbContext" -StartupProject "OnlineShop.API" -Project "OnlineShop.Data"
	Update-Database -StartupProject "OnlineShop.API" -Project "OnlineShop.Data"
	Update-Database -StartupProject "OnlineShop.API" -Project "OnlineShop.Data" -Migration "MigrationName"
	Remove-Migration -StartupProject "OnlineShop.API" -Project "OnlineShop.Data"