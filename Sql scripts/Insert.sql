use traktorsSalesDb
go

set identity_insert Machines on
insert into Machines(Id, Model, MainImage, [Description], Price)
	values 
		(1, '3CX', '/Content/Images\1.1.jfif', 'Some description for first machine', 10000),
		(2, '4CX', '/Content/Images\2.1.jfif', 'Some description for second machine', 15000)
set identity_insert Machines off

set identity_insert Images on
insert into Images(Id, ImagePath, MachineId)
	values
		(1, '/Content/Images\1.2.jfif', 1),
		(2, '/Content/Images\1.3.jfif', 1),
		(3, '/Content/Images\1.4.jfif', 1),
		(4, '/Content/Images\1.5.jfif', 1),
		(5, '/Content/Images\2.2.jfif', 2),
		(6, '/Content/Images\2.3.jfif', 2),
		(7, '/Content/Images\2.4.jfif', 2),
		(8, '/Content/Images\2.5.jfif', 2)
set identity_insert Images off


insert into AspNetUsers(Id, Hometown, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName)
	values('10fb244f-ffe7-4fcf-860e-77edb029787d', NULL, 'm.maykher@mail.ru', 0, 'AJWAE/VtYQK20PbOL1PdGSV39RjiQHE9MC781ou3CSbf06tCkKe5t2+wVN9odMoouQ==', '4ce0b04a-6fdf-4484-b1a0-6296c392c29f', NULL, 0, 0, NULL, 0, 0, 'Admin');
