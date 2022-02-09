CREATE TABLE LogoModels
(
    Id serial PRIMARY KEY,
    Name VARCHAR (100) NOT NULL 
);

CREATE TABLE PdfModels
(
    Id serial PRIMARY KEY,
    Name VARCHAR (100) NOT NULL,
    AddressLine1 VARCHAR (100) NOT NULL,
    AddressLine2 VARCHAR (100) NOT NULL,
    PostCode VARCHAR (100) NOT NULL,
    TextInput VARCHAR NOT NULL,
    FilePath VARCHAR NOT NULL,
    Logo VARCHAR NOT NULL
);

ALTER TABLE LogoModels OWNER TO pdfuser;

INSERT INTO LogoModels(Id, Name) VALUES( 1, 'PwC');
INSERT INTO LogoModels(Id, Name) VALUES( 2, 'Lloyds');
INSERT INTO LogoModels(Id, Name) VALUES( 3, 'Santander');