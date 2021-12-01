alter table Users
add roleId nvarchar(128) not null;

alter table Users
add constraint FK_UserRoleId foreign key(roleId) references AspNetRoles(Id);

