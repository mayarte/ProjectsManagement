insert into StudyYears (Year1,Year2) values
(2018,2019),
(2019,2020),
(2020,2021),
(2021,2022),
(2022,2023),
(2023,2024);

insert into Clearances (Name) values ('Clearances'),('UserGroups'),('Users'),('Students');

insert into SystemUserGroups(Name) values (N'الادمنية'),(N'الطلبة'),(N'المشرفين');

insert into ClearanceUserGroups(UserGroup_ID,Clearance_ID) values (1,1),(1,2),(1,3),(1,4);

insert into SystemUsers(Name,Password,ClearanceGroup_ID) values ('admin','admin',1);

