-- Retail Price Adjustment & Promotion Control System
-- Database schema (SQL Server)
 

CREATE TABLE Category (
    CategoryId          INT IDENTITY PRIMARY KEY,
    CategoryName        NVARCHAR(100) NOT NULL,
    CategoryDescription NVARCHAR(255) NULL
);
 
CREATE TABLE Product (
    ProductId            INT IDENTITY PRIMARY KEY,
    ProductCode          NVARCHAR(20) NOT NULL UNIQUE,
    ProductName          NVARCHAR(150) NOT NULL,
    CategoryId            INT NOT NULL FOREIGN KEY REFERENCES Category(CategoryId),
    UnitCost              DECIMAL(10,2) NOT NULL,
    CurrentSellingPrice   DECIMAL(10,2) NOT NULL,
    MinimumSellingPrice   DECIMAL(10,2) NOT NULL
);
 
CREATE TABLE PriceAdjustment (
    AdjustmentId          INT IDENTITY PRIMARY KEY,
    ProductId              INT NOT NULL FOREIGN KEY REFERENCES Product(ProductId),
    AdjustmentType          NVARCHAR(10) NOT NULL,
    AdjustmentPercentage    DECIMAL(5,2) NOT NULL,
    PreviousPrice           DECIMAL(10,2) NOT NULL,
    NewPrice                DECIMAL(10,2) NOT NULL,
    AdjustmentDate          DATETIME NOT NULL DEFAULT GETDATE(),
    Reason                  NVARCHAR(255) NOT NULL,
    ApprovedBy              NVARCHAR(100) NOT NULL
);
 
-- Sample seed data for testing / screenshots (Question 3 evidence)
INSERT INTO Category (CategoryName, CategoryDescription)
VALUES ('Electronics', 'TVs, audio, computing and accessories'),
       ('Homeware', 'Kitchen, dining and household items');
 
INSERT INTO Product (ProductCode, ProductName, CategoryId, UnitCost, CurrentSellingPrice, MinimumSellingPrice)
VALUES ('PRD-2045', 'Wireless Bluetooth Speaker', 1, 550.00, 1000.00, 700.00),
       ('PRD-3010', 'Non-Stick Frying Pan 28cm', 2, 180.00, 350.00, 250.00);
 
-- Example SELECT with JOIN, matching Question 2(c)
SELECT p.ProductCode, p.ProductName, c.CategoryName,
       p.CurrentSellingPrice, p.MinimumSellingPrice,
       pa.PreviousPrice, pa.NewPrice, pa.AdjustmentType, pa.AdjustmentDate
FROM PriceAdjustment pa
JOIN Product p  ON pa.ProductId = p.ProductId
JOIN Category c ON p.CategoryId = c.CategoryId
ORDER BY pa.AdjustmentDate DESC;
