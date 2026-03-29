-- PostgreSQL Database Schema for Gym Management System

-- Drop tables if they exist
DROP TABLE IF EXISTS "Payments";
DROP TABLE IF EXISTS "Members";
DROP TABLE IF EXISTS "Plans";
DROP TABLE IF EXISTS "Trainers";

-- 1. Trainers Table
CREATE TABLE "Trainers" (
    "TrainerId" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Specialization" VARCHAR(100) NOT NULL
);

-- 2. Plans Table
CREATE TABLE "Plans" (
    "PlanId" SERIAL PRIMARY KEY,
    "PlanName" VARCHAR(100) NOT NULL,
    "DurationMonths" INT NOT NULL,
    "Price" DECIMAL(10, 2) NOT NULL
);

-- 3. Members Table
CREATE TABLE "Members" (
    "MemberId" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Age" INT NOT NULL,
    "Phone" VARCHAR(20) NOT NULL,
    "JoinDate" DATE NOT NULL DEFAULT CURRENT_DATE,
    "TrainerId" INT NULL,
    "PlanId" INT NOT NULL,
    FOREIGN KEY ("TrainerId") REFERENCES "Trainers"("TrainerId") ON DELETE SET NULL,
    FOREIGN KEY ("PlanId") REFERENCES "Plans"("PlanId") ON DELETE RESTRICT
);

-- 4. Payments Table
CREATE TABLE "Payments" (
    "PaymentId" SERIAL PRIMARY KEY,
    "MemberId" INT NOT NULL,
    "Amount" DECIMAL(10, 2) NOT NULL,
    "PaymentDate" DATE NOT NULL DEFAULT CURRENT_DATE,
    "Status" VARCHAR(50) NOT NULL,
    FOREIGN KEY ("MemberId") REFERENCES "Members"("MemberId") ON DELETE CASCADE
);

-- Insert Sample Data
INSERT INTO "Trainers" ("Name", "Specialization") VALUES 
('John Doe', 'Bodybuilding'),
('Jane Smith', 'Cardio & Yoga');

INSERT INTO "Plans" ("PlanName", "DurationMonths", "Price") VALUES 
('Basic Monthly', 1, 50.00),
('Premium Quarterly', 3, 135.00),
('Annual VIP', 12, 500.00);

INSERT INTO "Members" ("Name", "Age", "Phone", "TrainerId", "PlanId") VALUES 
('Alice Johnson', 25, '555-0101', 1, 2),
('Bob Williams', 32, '555-0202', 2, 1);

INSERT INTO "Payments" ("MemberId", "Amount", "Status") VALUES 
(1, 135.00, 'Paid'),
(2, 50.00, 'Paid');
