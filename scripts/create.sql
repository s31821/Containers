CREATE TABLE ContainerType (
                               ID INT PRIMARY KEY IDENTITY(1,1),
                               Name NVARCHAR(100) NOT NULL
);
GO

-- Create Containers table
CREATE TABLE Containers (
                            ID INT PRIMARY KEY IDENTITY(1,1),
                            ContainerTypeId INT NOT NULL,
                            IsHazardous BIT NOT NULL,
                            Name NVARCHAR(100) NOT NULL,
                            FOREIGN KEY (ContainerTypeId) REFERENCES ContainerType(ID)
);