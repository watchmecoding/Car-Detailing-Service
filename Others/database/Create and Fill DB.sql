Create database DBCarDetailing

GO

use DBCarDetailing;

CREATE TABLE tbVehicleType (
    VehicleId INT IDENTITY NOT NULL,
    VehicleName NVARCHAR(50) NULL,
    VehicleClass NVARCHAR (50) NULL,
	CONSTRAINT PK_VehicleId PRIMARY KEY (VehicleId)
);

CREATE TABLE tbCompany (
    CompanyId INT IDENTITY NOT NULL,
    CompanyName NVARCHAR(50) NULL,
    CompanyAddress NVARCHAR (50) NULL,
	CONSTRAINT PK_CompanyId PRIMARY KEY (CompanyId)
);

CREATE TABLE tbCustomer (
    CustomerId INT IDENTITY NOT NULL,
	VehicleId INT NULL,
    CustomerName NVARCHAR(50) NULL,
    CustomerPhone NVARCHAR(50) NULL,
    CustomerCarNumber NVARCHAR(50) NULL,
    CustomerCarModel NVARCHAR(50) NULL,
    CustomerAddress TEXT NULL,
    CustomerPoints INT NULL,
	CONSTRAINT PK_CustomerId PRIMARY KEY (CustomerId),
	CONSTRAINT FK_Customers_VehicleType FOREIGN KEY (VehicleId) REFERENCES tbVehicleType (VehicleId)
);

CREATE TABLE tbService (
    ServiceId INT IDENTITY NOT NULL,
    ServiceName NVARCHAR(50) NULL,
    ServicePrice DECIMAL(18,2) NULL,
	CONSTRAINT PK_ServiceId PRIMARY KEY (ServiceId)
);

CREATE TABLE tbExpense (
    ExpenseId INT IDENTITY NOT NULL,
    ExpenseName NVARCHAR(50) NULL,
    ExpenseCost DECIMAL(18,2) NULL,
	ExpenseDate DATE NULL,
	CompanyId INT DEFAULT 1,
	CONSTRAINT PK_ExpenseId PRIMARY KEY (ExpenseId),
	CONSTRAINT FK_Expenses_tbCompany FOREIGN KEY (CompanyId) REFERENCES tbCompany (CompanyId)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE tbEmployee (
    EmployeeId INT IDENTITY NOT NULL,
    EmployeeName NVARCHAR(50) NULL,
    EmployeePhone NVARCHAR(50) NULL,
    EmployeeAddress TEXT NULL,
    EmployeeDOB NVARCHAR(50) NULL,
    EmployeeGender NVARCHAR(50) NULL,
    EmployeeRole NVARCHAR(50) NULL,
    EmployeeSalary DECIMAL(18, 2) NULL,
    EmployeePassword NVARCHAR(50) NULL,
	CompanyId INT NULL,
    CONSTRAINT PK_tbEmployee PRIMARY KEY CLUSTERED (EmployeeId ASC),
	CONSTRAINT FK_Company_tbCompany FOREIGN KEY (CompanyId) REFERENCES tbCompany (CompanyId)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE tbCashReg (
    CashId INT IDENTITY NOT NULL,
    TransactionNumber NVARCHAR(50) NULL,
    CustomerId INT NULL,
    ServiceId INT NULL,
    VehicleId INT NULL,
    Price DECIMAL(18,2) NULL,
    CashDate DATE NULL,
    CashStatus NVARCHAR(50) DEFAULT 'Pending',
	EmployeeId INT NULL,
	CONSTRAINT PK_CashId PRIMARY KEY (CashId),
	CONSTRAINT FK_Customer_tbCustomer FOREIGN KEY (CustomerId) REFERENCES tbCustomer (CustomerId)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT FK_Service_tbService FOREIGN KEY (ServiceId) REFERENCES tbService (ServiceId)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT FK_Vehicle_tbVehicleType FOREIGN KEY (VehicleId) REFERENCES tbVehicleType (VehicleId)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT FK_Employee_tbEmployee FOREIGN KEY (EmployeeId) REFERENCES tbEmployee (EmployeeId)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

GO

use DBCarDetailing;

INSERT INTO tbVehicleType (VehicleName, VehicleClass)
VALUES
    ('Спорткар', '1'),
    ('Седан', '1'),
    ('Купе', '1'),
    ('Хетчбек', '1'),
    ('Джип', '2'),
	('Пікап', '2'),
    ('Мінівен', '2'),
    ('Мікроавтобус', '3');


INSERT INTO tbCompany (CompanyName, CompanyAddress)
VALUES
    ('Washhh up?!', 'Somewhere in Kyiv');


INSERT INTO tbService (ServiceName, ServicePrice)
VALUES
    ('Поверхнева мийка', 3500.00),
    ('Поліровка кузову воском', 30000.00),
    ('Машинна поліровка кузову', 5000.00),
	('Поліровка фар', 12000.00),
    ('Поліровка лобового скла', 6000.00),
    ('Полірока дисків', 6000.00),
    ('Глубока чиста сидінь', 5000.00),
    ('Чистка стелі та підлоги', 10000.00),
    ('Суха чистка килимків', 5000.00),
    ('Чистка дверних панелей', 6000.00),
    ('Чистка підкапотного простору', 10000.00),
    ('ВИДАЛЕННЯ ПЛЯМ КРОВІ!!!', 100000.00),
    ('Не повідомляти про кров в поліцію', 100000.00);


INSERT INTO tbCustomer (VehicleId, CustomerName, CustomerPhone, CustomerCarNumber, CustomerCarModel, CustomerAddress, CustomerPoints)
VALUES
    (6, 'Антон Д.', '0965412154', 'AA1285КН', 'Honda', '-', 11),
    (1, 'Роман К.', '0985463122', 'AМ2294КХ', 'Audi', '-', 10),
    (1, 'Степан В.', '0965874253', 'ТУ1285KN', 'Porsche', '-', 7),
    (3, 'Дмитро Л.', '0985654752', 'БВ3542ММ', 'Mercedes', '-', 5),
    (3, 'Олексій Н.', '0968544721', 'AГ3333ГД', 'BMW', '-', 10),
    (1, 'Анастасія Г.', '0958745521', 'AВ1010КН', 'Mitsubishi', '-', 10);


INSERT INTO tbEmployee (EmployeeName, EmployeePhone, EmployeeAddress, EmployeeDOB, EmployeeGender, EmployeeRole, EmployeeSalary, EmployeePassword, CompanyId)
VALUES
    ('VitaliyV', '095331301', 'Київ', '2000-02-01', 'Male', 'Manager', 500000.00, '123', 1),
    ('EkaterynaMMD', '096543214', 'Одеса', '2000-02-01', 'Male', 'Manager', 350000.00, '123', 1),
	('Mr.Cashier', '096543214', 'Львів', '2000-02-01', 'Male', 'Cashier', 350000.00, '123', 1),
    ('Mr.Someone', '09654456', 'Київ', '2000-02-01', 'Male', 'Worker', 250000.00, NULL, 1),
    ('AlexanderII', '098745632', 'Кривий Ріг', '2000-02-01', 'Female', 'Worker', 250000.00, NULL, 1),
    ('Mr.Drister', '098754212', 'Суми', '2002-02-10', 'Male', 'Supervisor', 350000.00, NULL, 1),
    ('Васьок', '2332342355', 'Житомир', '2002-12-09', 'Male', 'Worker', 55555.00, NULL, 1),
    ('qwe', '22222', 'Ірпінь', '1991-04-05', 'Male', 'Cashier', 50000.00, '123', 1),
	('TheBoss', '0999999999', 'Глеваха', '1991-01-01', 'Male', 'Director', 500000.00, '123', 1);


INSERT INTO tbCashReg (TransactionNumber, CustomerId, ServiceId, VehicleId, Price, CashDate, CashStatus, EmployeeId)
VALUES
    ('202402024001', 2, 1, 6, 3500.00, '2024-12-02', 'Pending', 1),
    ('202402024002', 3, 3, 1, 30000.00, '2024-12-02', 'Pending', 2),
    ('202402024002', 3, 1, 1, 3500.00, '2024-12-02', 'Pending', 3),
    ('202402024003', 3, 3, 1, 30000.00, '2024-12-02', 'Pending', 3),
    ('202402024003', 3, 4, 1, 5000.00, '2024-12-02', 'Pending', 2),
    ('202402024004', 2, 1, 6, 3500.00, '2024-12-02', 'Pending', 1),
    ('202402024004', 2, 3, 6, 30000.00, '2024-12-02', 'Pending', 1),
    ('202402024005', 2, 1, 6, 3500.00, '2024-12-02', 'Pending', 1),
    ('202402024005', 2, 3, 6, 30000.00, '2024-12-02', 'Pending', 2),
    ('202402024006', 2, 1, 6, 3500.00, '2024-12-02', 'Pending', 3),
    ('202402024006', 2, 3, 6, 30000.00, '2024-12-02', 'Pending', 3),
    ('202402024007', 3, 1, 1, 3500.00, '2024-12-02', 'Pending', 2),
    ('202402024007', 3, 3, 1, 30000.00, '2024-12-02', 'Pending', 3),
    ('202402024008', 3, 1, 1, 3500.00, '2024-12-02', 'Pending', 1),
    ('202402024008', 3, 3, 1, 30000.00, '2024-12-02', 'Pending', 1),
    ('202402024009', 2, 1, 6, 3500.00, '2024-12-02', 'Pending', 3),
    ('202402024009', 2, 3, 6, 30000.00, '2024-12-02', 'Pending', 2),
    ('202402024010', 2, 1, 6, 3500.00, '2024-12-02', 'Pending', 2),
    ('202402024010', 2, 3, 6, 30000.00, '2024-12-02', 'Pending', 3),
    ('202402024011', 2, 1, 6, 7000.00, '2024-12-02', 'Sold', 1),
    ('202402024011', 2, 3, 6, 60000.00, '2024-12-02', 'Sold', 3),
    ('202402024012', 3, 1, 1, 3500.00, '2024-12-02', 'Sold', 2),
    ('202402024012', 3, 3, 1, 30000.00, '2024-12-02', 'Sold', 2),
    ('202402024012', 3, 4, 1, 5000.00, '2024-12-02', 'Sold', 2),
    ('202402024013', 2, 1, 6, 7000.00, '2024-12-02', 'Sold', 3),
    ('202402024014', 4, 1, 1, 3500.00, '2024-12-02', 'Sold', 1),
    ('202402024015', 4, 1, 1, 3500.00, '2024-12-02', 'Sold', 2),
    ('202402024015', 4, 3, 1, 30000.00, '2024-12-02', 'Sold', 1),
    ('202402024016', 5, 1, 3, 3500.00, '2024-12-02', 'Sold', 3),
    ('202402024016', 5, 3, 3, 30000.00, '2024-12-02', 'Sold', 3),
    ('202402024017', 3, 1, 1, 3500.00, '2024-12-02', 'Sold', 2),
    ('202402024018', 2, 1, 6, 7000.00, '2024-12-02', 'Sold', 2),
    ('202402024019', 4, 1, 1, 3500.00, '2024-12-02', 'Sold', 3),
    ('202402024020', 4, 1, 1, 3500.00, '2024-12-02', 'Sold', 1),
    ('202402024021', 2, 1, 6, 7000.00, '2024-12-02', 'Sold', 1),
    ('202402024022', 2, 1, 6, 7000.00, '2024-12-02', 'Sold', 3),
    ('202402024023', 6, 1, 3, 3500.00, '2024-12-02', 'Sold', 2),
    ('202412104002', 4, 4, 1, 5000.00, '2024-12-10', 'Sold', 1);


INSERT INTO tbExpense (ExpenseName, ExpenseCost, ExpenseDate, CompanyId)
VALUES
    ('Сертифікація якості від "AutoGlory+"', 150000.00, '2024-11-30', 1),
    ('Євро-ліцензія на ведення бізнесу', 45000.00, '2024-11-30', 1),
    ('Страховка обладнання', 1450000.00, '2024-11-30', 1),
    ('Діагностичне обладнання', 300000.00, '2024-12-01', 1),
    ('Плата за оренду приміщення', 80000.00, '2024-12-01', 1),
    ('Розхідні матеріали', 30000.00, '2024-12-01', 1),
    ('Зарплатня співробітникам', 500000.00, '2024-11-30', 1);

