USE webApp

INSERT INTO Priority(IdPriority, PriorityDesc)
VALUES	('416d3557-4ac1-46ed-a84a-1cfb921f6be8', 'Niski'),
		('4a1e26b0-a159-4130-a1bb-75762551cbf0','Œredni'),
		('06ce6b6a-17fd-4179-a441-c41d8d20ab51','Wysoki');

INSERT INTO Category (IdCategory, CategoryName)
VALUES	('5970cd5b-724e-4c02-9ce5-273ab46835da', 'Telefon'),
	('bb436c0c-9726-4c16-8b82-4a91b81cd000', 'Aplikacja'),
	('7bf28454-1866-4d99-9048-7c71a2f7f17b', 'Drukarka'),
	('a7142dc8-c12a-4542-9127-a3999148b36b', 'Komputer');


INSERT INTO Status (IdStatus, StatusDesc)
VALUES	('CDF4DEF9-2D48-49DB-BEA4-3E69AC6D158F', 'Otwarty'),
	('6B8E4F1F-AD7E-4035-BAC1-4E23422EDE84', 'Anulowane'),
	('77D08315-E2B5-4533-8219-90155C495EC9', 'W realizacji'),
	('09E56212-1812-4715-BA36-EBD840DE31A5', 'Wstrzymany'),
	('D5FA120E-8879-4A13-89F9-C8CF0D59F772', 'Zakoñczony');

INSERT INTO Department(IdDepartment, DepartmentName)
VALUES	('6a28a7b1-6514-4e2e-88ef-1c4d5d398486', 'Marketing'),
		('412b48db-3e91-40b3-918a-03bdedabcbbd', 'Administracja'),
		('7bf0ff26-0aba-45c1-90ed-de3bd892b1a5','IT'),
		('6c066945-8887-4790-a660-5ecd98067458','Logistyka'),
		('cebfec9f-fe71-486b-935d-7068d5cb601f','Ob³suga klienta'),
		('f72069c4-56d8-4ff9-9441-533803d879f2','Rachunkowoœæ'),
		('0b095242-75d3-4994-84ce-3e865cb9582f','Kadry'),
		('07ad63b1-bb51-4195-9888-019a73aa5475','Handlowy');

INSERT INTO AspNetRoles(Id,	Name, NormalizedName)
VALUES	('3aa8b367-c9cd-4e48-8162-31c14837b3e8','Administrator','ADMINISTRATOR'),
('2beaffd7-23dc-4a88-8aa9-cc500081650e','Operator','OPERATOR'),
('ad8ec464-39b0-4efa-ba87-c810324bede7','U¿ytkownik','U¯YTKOWNIK');

INSERT INTO AspNetUsers (Id, FirstName, LastName, IdDepartment, DepartmentIdDepartment, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
VALUES ('A605DDB6-ED26-49B5-4B3B-08D92F28A53C','Rafa³','G³aszczka','00000000-0000-0000-0000-000000000000',NULL,'rglaszczka','RGLASZCZKA','rafal.glaszczka@outlook.com','RAFAL.GLASZCZKA@OUTLOOK.COM',1,'AQAAAAEAACcQAAAAEDseKzuK6jlDSQZNXsnJJQI3vvYye53vfZYwOhBscn65Zt5pzFbncXX4t1XP4nbFlw==','WH5BKJE7WVAL3C2CAA74S4LXJJEIRT5Y','efac4ef3-d46b-4533-9537-3ed325b35c12',NULL,0,0,NULL,1,0),
	  ('0681C107-4B0D-469E-B3BB-08D92F2A1C6E','Jan','Testowy','00000000-0000-0000-0000-000000000000',NULL,'jtestowy','JTESTOWY','raf9600@hotmail.com','RAF9600@HOTMAIL.COM',1,'AQAAAAEAACcQAAAAEP1mWddtiWxP7UOoQ9UETAAgEqZN49W3e4kNiRmcCTiB8u0mTW3Yt6GxaPcsgrNaxA==','QLPUX65IUOVUHLZ6FBOZ4MIBR2IWSXFQ','76949175-bc08-4783-bda5-ff68784d8572',NULL,0,0,NULL,1,0);

INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES ('A605DDB6-ED26-49B5-4B3B-08D92F28A53C','3AA8B367-C9CD-4E48-8162-31C14837B3E8'),
		('0681C107-4B0D-469E-B3BB-08D92F2A1C6E','AD8EC464-39B0-4EFA-BA87-C810324BEDE7');
