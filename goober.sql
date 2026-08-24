-- Popeye Marina Database Schema
-- KAA5054 Assessment 3 - reference script for recreating the database on another machine
-- Includes the original slip/lease/customer/boat tables plus RentalBoat and
-- BoatHire, which the code (RentalBoatData.cs / BoatHireData.cs) already
-- reads and writes but which were missing from the previously committed script.

CREATE DATABASE PopeyeMarinaDB;
GO
USE PopeyeMarinaDB;
GO

CREATE TABLE Customer (
    CustomerID      INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName    VARCHAR(100) NOT NULL,
    Address         VARCHAR(200) NOT NULL,
    PhoneNo         VARCHAR(20) NOT NULL
);
GO

CREATE TABLE Dock (
    DockID          INT IDENTITY(1,1) PRIMARY KEY,
    Location        VARCHAR(150) NOT NULL,
    HasElectricity  BIT NOT NULL DEFAULT 0,
    HasWater        BIT NOT NULL DEFAULT 0
);
GO

CREATE TABLE Boat (
    StateRegoNo     VARCHAR(15) PRIMARY KEY,
    BoatLength      DECIMAL(5,2) NOT NULL,
    Manufacturer    VARCHAR(100) NOT NULL,
    ModelYear       INT NOT NULL,
    BoatType        VARCHAR(20) NOT NULL CHECK (BoatType IN ('Sailboat','Powerboat')),
    CustomerID      INT NOT NULL,
    CONSTRAINT FK_Boat_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);
GO

CREATE TABLE SailboatDetails (
    StateRegoNo     VARCHAR(15) PRIMARY KEY,
    KeelDepth       DECIMAL(4,2) NOT NULL,
    NumberOfSails   INT NOT NULL,
    MotorType       VARCHAR(50) NULL,
    CONSTRAINT FK_Sailboat_Boat FOREIGN KEY (StateRegoNo) REFERENCES Boat(StateRegoNo)
);
GO

CREATE TABLE PowerboatDetails (
    StateRegoNo     VARCHAR(15) PRIMARY KEY,
    NumberOfEngines INT NOT NULL,
    FuelType        VARCHAR(50) NOT NULL,
    CONSTRAINT FK_Powerboat_Boat FOREIGN KEY (StateRegoNo) REFERENCES Boat(StateRegoNo)
);
GO

CREATE TABLE Slip (
    SlipID          INT IDENTITY(1,1) PRIMARY KEY,
    Width           DECIMAL(5,2) NOT NULL,
    SlipLength      DECIMAL(5,2) NOT NULL,
    DockID          INT NOT NULL,
    IsCovered       BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Slip_Dock FOREIGN KEY (DockID) REFERENCES Dock(DockID)
);
GO

CREATE TABLE SlipCoveredDetails (
    SlipID          INT PRIMARY KEY,
    Height          DECIMAL(4,2) NOT NULL,
    DoorType        VARCHAR(50) NOT NULL,
    CONSTRAINT FK_CoveredSlip_Slip FOREIGN KEY (SlipID) REFERENCES Slip(SlipID)
);
GO

CREATE TABLE Lease (
    LeaseID         INT IDENTITY(1,1) PRIMARY KEY,
    StartDate       DATE NOT NULL,
    EndDate         DATE NULL,
    Amount          DECIMAL(10,2) NOT NULL,
    LeaseType       VARCHAR(20) NOT NULL CHECK (LeaseType IN ('Annual','Daily')),
    SlipID          INT NOT NULL,
    StateRegoNo     VARCHAR(15) NOT NULL,
    CustomerID      INT NOT NULL,
    CreatedDate     DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate    DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Lease_Slip FOREIGN KEY (SlipID) REFERENCES Slip(SlipID),
    CONSTRAINT FK_Lease_Boat FOREIGN KEY (StateRegoNo) REFERENCES Boat(StateRegoNo),
    CONSTRAINT FK_Lease_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);
GO

CREATE TABLE AnnualLeaseDetails (
    LeaseID         INT PRIMARY KEY,
    PayMonthly      BIT NOT NULL DEFAULT 0,
    BalanceDue      DECIMAL(10,2) NOT NULL DEFAULT 0,
    CONSTRAINT FK_AnnualLease_Lease FOREIGN KEY (LeaseID) REFERENCES Lease(LeaseID)
);
GO

CREATE TABLE DailyLeaseDetails (
    LeaseID         INT PRIMARY KEY,
    NumberOfDays    INT NOT NULL,
    CONSTRAINT FK_DailyLease_Lease FOREIGN KEY (LeaseID) REFERENCES Lease(LeaseID)
);
GO

-- ============================================================
-- Boat Hire feature (marina-owned rental boats, separate from
-- customer-owned Boat/Sailboat/Powerboat leasing a slip)
-- ============================================================

CREATE TABLE RentalBoat (
    RentalBoatID    INT IDENTITY(1,1) PRIMARY KEY,
    BoatName        VARCHAR(100) NOT NULL,
    BoatType        VARCHAR(20) NOT NULL CHECK (BoatType IN ('Sailboat','Powerboat')),
    IsActive        BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE BoatHire (
    HireID          INT IDENTITY(1,1) PRIMARY KEY,
    RentalBoatID    INT NOT NULL,
    CustomerID      INT NOT NULL,
    StartDate       DATE NOT NULL,
    EndDate         DATE NOT NULL,
    CreatedDate     DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_BoatHire_RentalBoat FOREIGN KEY (RentalBoatID) REFERENCES RentalBoat(RentalBoatID),
    CONSTRAINT FK_BoatHire_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);
GO

-- ============================================================
-- Sample test data
-- ============================================================

INSERT INTO Customer (CustomerName, Address, PhoneNo) VALUES ('John Smith', '12 Brutus Rd, Brutus', '0400111222');
INSERT INTO Dock (Location, HasElectricity, HasWater) VALUES ('North Dock', 1, 1);
INSERT INTO Boat (StateRegoNo, BoatLength, Manufacturer, ModelYear, BoatType, CustomerID) VALUES ('NSW123AB', 8.5, 'Beneteau', 2020, 'Sailboat', 1);
INSERT INTO SailboatDetails (StateRegoNo, KeelDepth, NumberOfSails, MotorType) VALUES ('NSW123AB', 1.8, 2, 'Inboard');
INSERT INTO Slip (Width, SlipLength, DockID, IsCovered) VALUES (3.5, 10.0, 1, 0);
INSERT INTO Lease (StartDate, EndDate, Amount, LeaseType, SlipID, StateRegoNo, CustomerID) VALUES ('2026-01-01', '2026-12-31', 4800.00, 'Annual', 1, 'NSW123AB', 1);
INSERT INTO AnnualLeaseDetails (LeaseID, PayMonthly, BalanceDue) VALUES (1, 1, 400.00);

INSERT INTO RentalBoat (BoatName, BoatType, IsActive) VALUES ('Sea Breeze', 'Sailboat', 1);
INSERT INTO RentalBoat (BoatName, BoatType, IsActive) VALUES ('Wave Runner', 'Powerboat', 1);
INSERT INTO BoatHire (RentalBoatID, CustomerID, StartDate, EndDate) VALUES (1, 1, '2026-09-01', '2026-09-05');

-- Test SELECT / UPDATE / DELETE
SELECT * FROM Lease;
UPDATE Lease SET Amount = 5000.00 WHERE LeaseID = 1;
-- DELETE FROM Lease WHERE LeaseID = 1; -- example only, don't run against real data
