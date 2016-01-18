CREATE TABLE [dbo].CheckInCheckOut
(
	[Id] BIGINT NOT NULL PRIMARY KEY, 
    [CheckInTime] DATETIME NULL, 
    [CheckOutTime] DATETIME NULL, 
    [MachineNoIn] TINYINT NULL, 
    [MachineNoOut] TINYINT NULL, 
    [UserEnrollNumber] INT NOT NULL, 
    [StartWorkingTime] SMALLDATETIME NULL, 
    [EndWorkingTime] SMALLDATETIME NULL, 
    [ShiftID] INT NULL
)
