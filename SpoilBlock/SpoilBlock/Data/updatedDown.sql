ALTER TABLE [WOOPUserMedia] DROP CONSTRAINT [Fk_WOOPUserMedia_User_ID];
ALTER TABLE [WOOPUserMedia] DROP CONSTRAINT [Fk_WOOPUserMedia_Media_ID];
ALTER TABLE [Media] DROP CONSTRAINT [Unique_IMDBID];

DROP TABLE [WOOPUser];
DROP TABLE [Media];
DROP TABLE [WOOPUserMedia];