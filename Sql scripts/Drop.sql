use traktorsSalesDb
go

alter table Images 
drop constraint fk_images_to_machine;

drop table Machines;
drop table Images;

DROP TABLE dbo.AspNetRoles;
DROP TABLE dbo.AspNetUserClaims;
DROP TABLE dbo.AspNetUserLogins;
DROP TABLE dbo.AspNetUserRoles;
DROP TABLE dbo.AspNetUsers;