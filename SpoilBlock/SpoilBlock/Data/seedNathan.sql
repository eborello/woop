
INSERT INTO WOOPUser(ASPNetIdentityId, Username)
values ('ccef5498-206c-4679-bc5d-a0f2c31a51f6', 'nathancamuso@gmail.com');

INSERT INTO Media(IMDBID, Title, Description)
values ('tt0110912', 'Pulp Fiction', '(1994)');

INSERT INTO WOOPUser(ASPNetIdentityId, Username)
values ('c6320664-1e71-4c92-877f-0d49efedda2e', 'SampleUser1');

INSERT INTO Media(IMDBID, Title, Description, Image)
values ('tt1375666', 'Inception', 'A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O., but his tragic past may doom the project and his team to disaster.', 'https://imdb-api.com/images/original/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_Ratio0.6800_AL_.jpg');

INSERT into WOOPUserMedia(BlockageLevel, UserID, MediaID)
VALUES (1,1,3)