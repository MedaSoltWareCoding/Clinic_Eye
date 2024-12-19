-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 19, 2024 at 11:23 AM
-- Server version: 10.4.32-MariaDB-log
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `clinics`
--

-- --------------------------------------------------------

--
-- Table structure for table `appointment`
--

CREATE TABLE `appointment` (
  `id` int(100) NOT NULL,
  `id_patient` int(100) NOT NULL,
  `date` date NOT NULL,
  `state` int(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `doctors`
--

CREATE TABLE `doctors` (
  `id` int(100) NOT NULL,
  `name` varchar(100) NOT NULL,
  `fname` varchar(100) NOT NULL,
  `age` int(100) NOT NULL,
  `adress` varchar(100) NOT NULL,
  `phone` varchar(100) NOT NULL,
  `branch` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `doctors`
--

INSERT INTO `doctors` (`id`, `name`, `fname`, `age`, `adress`, `phone`, `branch`) VALUES
(1, 'عبد الصمد', 'قية', 25, '0636965863', 'قمار', 'شبكية العين');

-- --------------------------------------------------------

--
-- Table structure for table `examinations`
--

CREATE TABLE `examinations` (
  `id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `mendicant`
--

CREATE TABLE `mendicant` (
  `id` int(100) NOT NULL,
  `med_name` varchar(100) NOT NULL,
  `description` varchar(100) NOT NULL,
  `dosage` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `mendicant`
--

INSERT INTO `mendicant` (`id`, `med_name`, `description`, `dosage`) VALUES
(1, 'aspirin', 'vocale', '2/3 par jour'),
(2, 'aspigic', 'nazal', '99');

-- --------------------------------------------------------

--
-- Table structure for table `patient`
--

CREATE TABLE `patient` (
  `id` int(11) NOT NULL,
  `pat_name` varchar(100) NOT NULL,
  `pat_fname` varchar(100) NOT NULL,
  `pat_age` varchar(100) NOT NULL,
  `pat_gander` varchar(100) NOT NULL,
  `pat_birthday` date NOT NULL,
  `pat_city` varchar(100) NOT NULL,
  `pat_adress` varchar(100) NOT NULL,
  `pat_phone` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `patient`
--

INSERT INTO `patient` (`id`, `pat_name`, `pat_fname`, `pat_age`, `pat_gander`, `pat_birthday`, `pat_city`, `pat_adress`, `pat_phone`) VALUES
(1, 'rachid', 'medaoui', '29', 'Male', '1995-05-10', 'alger', 'eloued', '0696360185'),
(2, 'رشيد', 'مداوي', '29', 'Male', '1995-05-12', 'الوادي', 'الجزائر', '0216987497'),
(4, 'ياسين', 'سلطاني', '29', 'Male', '2024-12-04', 'الوادي', 'الجزائر', '022236597'),
(5, 'بوبكر', 'سلطاني', '31', 'Male', '1980-12-12', 'الوادي', 'الإستقلال', '0223264979'),
(6, 'fifo', 'شارف', '30', 'Male', '1994-12-12', 'الوادي', 'العاصمة', '02036479');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `doctors`
--
ALTER TABLE `doctors`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `mendicant`
--
ALTER TABLE `mendicant`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `patient`
--
ALTER TABLE `patient`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `doctors`
--
ALTER TABLE `doctors`
  MODIFY `id` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `mendicant`
--
ALTER TABLE `mendicant`
  MODIFY `id` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `patient`
--
ALTER TABLE `patient`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
