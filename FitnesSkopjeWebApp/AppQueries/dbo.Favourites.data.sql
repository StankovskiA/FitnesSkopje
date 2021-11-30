use [aspnet-FitnesSkopjeWebApp-20211109102850];
alter table Favourites 
add constraint FK_UserId foreign key (userId) references Users (id);