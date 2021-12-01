alter table Users
add roleId nvarchar(128) not null;
use [aspnet-FitnesSkopjeWebApp-20211109102850]
alter table Users
drop constraint FK_UserRoleId;

alter table Users
add constraint FK_UserRoleId foreign key(roleId) references AspNetRoles(Id);

