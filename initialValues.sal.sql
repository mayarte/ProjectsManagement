insert into StudyYears (Year1,Year2) values
(2018,2019),
(2019,2020),
(2020,2021),
(2021,2022),
(2022,2023),
(2023,2024);

insert into Clearances (Name) values 
('Clearances'),
('SystemUserGroups'),
('SystemUsers'),
('StudyYears'),
('ProjectPosts'),
('ProjectDatas'),
('ProposeProject'),
('ProjectList'),
('Home'),
('MyProjects'),
('Projects');

insert into SystemUserGroups(Name) values (N'الادمنية'),(N'الطلبة'),(N'المشرفين');

insert into SystemUsers(Name,Password,ClearanceGroup_ID) values ('admin','admin',1);

insert into SystemUserGroupClearances(SystemUserGroup_ID,Clearance_ID) values (1,1),(1,2),(1,3),(1,4),(1,5),(1,6),(1,7),(1,8),(1,9),(1,10),(1,11);

insert into SystemUserGroupClearances(SystemUserGroup_ID,Clearance_ID) values (2,8),(2,9),(2,10),(2,11);

insert into SystemUserGroupClearances(SystemUserGroup_ID,Clearance_ID) values (3,7),(3,8),(3,9),(3,10),(3,11);