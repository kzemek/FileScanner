
-- Table: searches
CREATE TABLE searches ( 
    search_id           INTEGER  PRIMARY KEY AUTOINCREMENT,
    startTime           DATETIME,
    endTime             DATETIME,
    processedFilesCount INTEGER 
);


-- Table: files
CREATE TABLE files ( 
    file_id     INTEGER         PRIMARY KEY AUTOINCREMENT,
    fileName    VARCHAR( 256 ),
    fullPath    VARCHAR( 256 ),
    sizeInBytes INTEGER,
    search_id                   REFERENCES searches ( search_id ) ON DELETE CASCADE
                                                                  ON UPDATE CASCADE 
);


-- Table: matches
CREATE TABLE matches ( 
    file_id  INTEGER         REFERENCES files ( file_id ),
    [index]  INTEGER,
    value    VARCHAR( 256 ),
    match_id INTEGER         PRIMARY KEY AUTOINCREMENT 
);

