alter table Reviews
add constraint FK_UserIdinReviews foreign key (userId) references Users(id);