-- CreateEnum
CREATE TYPE "Role" AS ENUM ('ADMIN', 'MX', 'PILOT', 'GUEST');

-- CreateEnum
CREATE TYPE "LicenseType" AS ENUM ('MX', 'PILOT', 'NONE');

-- CreateEnum
CREATE TYPE "ToolType" AS ENUM ('GAUGE', 'ELECTRICAL', 'TOOL', 'CABLE', 'OTHER');

-- CreateEnum
CREATE TYPE "ToolStatus" AS ENUM ('SERVICABLE', 'UNSERVICABLE', 'OUT_FOR_CALIBRATION');

-- CreateTable
CREATE TABLE "User" (
    "id" TEXT NOT NULL,
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" TIMESTAMP(3) NOT NULL,
    "email" TEXT NOT NULL,
    "hashedPassword" TEXT NOT NULL,
    "firstName" TEXT NOT NULL,
    "lastName" TEXT NOT NULL,
    "licenseType" "LicenseType" NOT NULL DEFAULT 'NONE',
    "licenseNumber" TEXT,
    "role" "Role" NOT NULL DEFAULT 'GUEST',

    CONSTRAINT "User_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "CalTool" (
    "id" SERIAL NOT NULL,
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" TIMESTAMP(3) NOT NULL,
    "companyToolId" TEXT,
    "toolPn" TEXT,
    "toolSn" TEXT,
    "toolName" TEXT NOT NULL,
    "calDate" DATE,
    "expDate" DATE,
    "certificateUrl" TEXT,
    "toolLocation" TEXT,
    "details" TEXT,
    "toolType" "ToolType" NOT NULL DEFAULT 'TOOL',
    "toolStatus" "ToolStatus" NOT NULL DEFAULT 'UNSERVICABLE',

    CONSTRAINT "CalTool_pkey" PRIMARY KEY ("id")
);
