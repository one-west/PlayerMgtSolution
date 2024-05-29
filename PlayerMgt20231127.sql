-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: localhost    Database: playermgtdb
-- ------------------------------------------------------
-- Server version	8.0.33

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `clubs`
--

DROP TABLE IF EXISTS `clubs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clubs` (
  `cId` int NOT NULL,
  `cImage` varchar(30) DEFAULT NULL,
  `cName` varchar(30) NOT NULL,
  `cStadium` varchar(30) NOT NULL,
  `cPlayed` int NOT NULL,
  `cPoints` int DEFAULT '0',
  `cWon` int DEFAULT '0',
  `cDraw` int DEFAULT '0',
  `cLoss` int DEFAULT '0',
  `cGF` int DEFAULT '0',
  `cGA` int DEFAULT '0',
  PRIMARY KEY (`cId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clubs`
--

LOCK TABLES `clubs` WRITE;
/*!40000 ALTER TABLE `clubs` DISABLE KEYS */;
INSERT INTO `clubs` VALUES (1,'BUSAN_IPARK.png','부산아이파크','부산아시아드주경기장',34,69,20,9,5,49,25),(2,'GIMCHEON_SANGMU.png','김천상무','김천종합운동장',34,67,21,4,9,69,36),(3,'GIMPO_FC.png','김포FC','김포솔터축구장',34,59,16,11,7,40,24),(4,'BUCHEON_FC1995.png','부천FC1995','부천종합운동장',35,54,15,9,11,41,34),(5,'GYEONGNAM_FC.png','경남FC','창원축구센터',34,53,14,11,9,52,41),(6,'JEONNAM_DRAGONS_FC.png','전남드래곤즈','광양축구전용구장',34,50,15,5,14,51,52),(7,'ANYANG_FC.png','FC안양','부산아시아드주경기장',34,48,13,9,12,53,48),(8,'CHEONJU_FC.png','충북청주FC','청주종합경기장',34,48,12,12,10,35,41),(9,'ASAN_FC.png','충남아산FC','아산이순신종합운동장',35,42,12,6,17,39,45),(10,'SEONGNAM_FC.png','성남FC','탄천종합운동장',34,41,10,11,13,41,48),(11,'SEOUL_ELAND.png','서울이랜드FC','목동종합운동장',34,35,10,5,19,36,51),(12,'CHEONAN_FC.png','천안시티FC','천안종합운동장',34,24,5,9,20,32,60),(13,'ANSAN_FC.png','안산그리너스FC','안산와~스타디움',34,22,5,7,22,36,69);
/*!40000 ALTER TABLE `clubs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fomation`
--

DROP TABLE IF EXISTS `fomation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fomation` (
  `pId` int NOT NULL,
  `fmX` int DEFAULT NULL,
  `fmY` int DEFAULT NULL,
  PRIMARY KEY (`pId`),
  CONSTRAINT `playerID` FOREIGN KEY (`pId`) REFERENCES `player` (`pId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fomation`
--

LOCK TABLES `fomation` WRITE;
/*!40000 ALTER TABLE `fomation` DISABLE KEYS */;
/*!40000 ALTER TABLE `fomation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `player`
--

DROP TABLE IF EXISTS `player`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `player` (
  `pId` int NOT NULL AUTO_INCREMENT,
  `pState` varchar(20) NOT NULL,
  `pName` varchar(30) NOT NULL,
  `pPosition` varchar(20) NOT NULL,
  `pNum` int NOT NULL,
  `pNat` varchar(20) NOT NULL,
  `pHeight` double NOT NULL,
  `pWeight` double NOT NULL,
  `pAge` date NOT NULL,
  `pReport` text,
  `pImage` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`pId`)
) ENGINE=InnoDB AUTO_INCREMENT=46 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `player`
--

LOCK TABLES `player` WRITE;
/*!40000 ALTER TABLE `player` DISABLE KEYS */;
INSERT INTO `player` VALUES (1,'선발','박성수','GK',1,'대한민국',192,83,'1996-05-12','반사신경이 좋음','TPlayer_박성수_GK.png'),(2,'선발','정준연','DF',2,'대한민국',178,70,'1989-04-30',NULL,'TPlayer_정준연_DF.png'),(3,'선발','박경빈','DF',3,'대한민국',190,83,'2002-07-22',NULL,'TPlayer_박경빈_DF.png'),(4,'선발','이창용','DF',4,'대한민국',180,76,'1990-08-27',NULL,'TPlayer_이창용_DF.png'),(5,'선발','박종현','DF',5,'대한민국',185,75,'2000-11-24','','TPlayer_박종현_DF.png'),(6,'선발','김정현','MF',6,'대한민국',185,74,'1993-06-01',NULL,'TPlayer_김정현_MF.png'),(7,'선발','안용우','FW',7,'대한민국',176,69,'1991-08-10',NULL,'TPlayer_안용우_FW.png'),(8,'선발','황기욱','MF',8,'대한민국',185,77,'1996-06-10',NULL,'TPlayer_황기욱_MF.png'),(9,'선발','브루노','FW',9,'브라질',190,83,'1994-07-20',NULL,'TPlayer_브루노_FW.png'),(10,'선발','라에르시오','FW',10,'브라질',179,82,'1998-11-17',NULL,'TPlayer_라에르시오_FW.png'),(11,'선발','조성준','FW',11,'대한민국',176,72,'1990-11-27',NULL,'TPlayer_조성준_FW.png'),(12,'선발','이재용','FW',13,'대한민국',177,68,'2002-09-20',NULL,'TPlayer_이재용_FW.png'),(13,'선발','홍창범','MF',14,'대한민국',170,68,'1998-10-22',NULL,'TPlayer_홍창범_MF.png'),(14,'선발','김형진','DF',15,'대한민국',185,72,'1993-12-20',NULL,'TPlayer_김형진_DF.png'),(15,'선발','류승우','FW',17,'대한민국',174,68,'1993-12-17',NULL,'TPlayer_류승우_FW.png'),(16,'선발','김륜도','FW',18,'대한민국',187,74,'1991-07-09',NULL,'TPlayer_김륜도_FW.png'),(17,'선발','양정운','FW',19,'대한민국',176,68,'2001-05-14',NULL,'TPlayer_양정운_FW.png'),(18,'선발','이동수','MF',20,'대한민국',185,72,'1997-06-03',NULL,'TPlayer_이동수_MF.png'),(19,'후보','김태훈','GK',21,'대한민국',189,82,'1997-04-24',NULL,'TPlayer_김태훈_GK.png'),(20,'후보','김동진','DF',22,'대한민국',177,74,'1992-12-28',NULL,'TPlayer_김동진_DF.png'),(21,'후보','김성동','GK',23,'대한민국',190,80,'2002-02-23',NULL,'TPlayer_김성동_GK.png'),(22,'후보','최성범','FW',24,'대한민국',173,68,'2001-12-24',NULL,'TPlayer_최성범_FW.png'),(23,'후보','김하준','DF',25,'대한민국',188,78,'2002-07-17',NULL,'TPlayer_김하준_DF.png'),(24,'후보','김정민','MF',26,'대한민국',182,80,'1999-11-13',NULL,'TPlayer_김정민_MF.png'),(25,'후보','홍현호','FW',27,'대한민국',174,70,'2002-06-11',NULL,'TPlayer_홍현호_FW.png'),(26,'후보','문성우','MF',28,'대한민국',183,72,'2003-05-15',NULL,'TPlayer_문성우_MF.png'),(27,'후보','백동규','DF',30,'대한민국',185,79,'1991-05-30',NULL,'TPlayer_백동규_DF.png'),(28,'후보','이태희','DF',32,'대한민국',181,66,'1992-06-16',NULL,'TPlayer_이태희_DF.png'),(29,'후보','연제민','DF',40,'대한민국',187,82,'1993-05-28',NULL,'TPlayer_연제민_DF.png'),(30,'후보','공민현','FW',81,'대한민국',182,70,'1990-01-19',NULL,'TPlayer_공민현_FW.png'),(31,'후보','윤준성','DF',83,'대한민국',188,81,'1989-09-28',NULL,'TPlayer_윤준성_DF.png'),(32,'후보','구대영','DF',90,'대한민국',178,73,'1992-05-09',NULL,'TPlayer_구대영_DF.png'),(33,'후보','야고','FW',97,'브라질',170,65,'1997-05-26',NULL,'TPlayer_야고_FW.png'),(34,'후보','주현우','MF',99,'대한민국',173,67,'1990-09-12',NULL,'TPlayer_주현우_MF.png');
/*!40000 ALTER TABLE `player` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `playerrecord`
--

DROP TABLE IF EXISTS `playerrecord`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `playerrecord` (
  `rId` int NOT NULL AUTO_INCREMENT,
  `rPlayed` int DEFAULT '0',
  `rGoal` int DEFAULT '0',
  `rAssist` int DEFAULT '0',
  `rShots` int DEFAULT '0',
  `rShotsT` int DEFAULT '0',
  `rfoul` int DEFAULT '0',
  `rYellow` int DEFAULT '0',
  `rRed` int DEFAULT '0',
  `rCornerKick` int DEFAULT '0',
  `rFleeKick` int DEFAULT '0',
  `rCatch` int DEFAULT '0',
  `rPunch` int DEFAULT '0',
  `rAerialBall` int DEFAULT '0',
  PRIMARY KEY (`rId`),
  CONSTRAINT `rID` FOREIGN KEY (`rId`) REFERENCES `player` (`pId`)
) ENGINE=InnoDB AUTO_INCREMENT=46 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `playerrecord`
--

LOCK TABLES `playerrecord` WRITE;
/*!40000 ALTER TABLE `playerrecord` DISABLE KEYS */;
INSERT INTO `playerrecord` VALUES (1,25,0,0,0,0,0,1,0,0,68,24,22,31),(2,3,0,0,0,0,1,0,0,0,2,24,22,31),(3,0,0,0,0,0,0,0,0,0,0,0,0,0),(4,20,0,2,8,3,17,3,0,0,0,0,0,0),(5,30,0,0,5,2,29,5,0,0,0,0,0,0),(6,20,4,1,25,13,41,2,0,0,0,0,0,0),(7,15,1,1,13,3,6,0,0,20,0,0,0,0),(8,29,0,0,13,6,37,3,0,0,0,0,0,0),(9,12,2,2,22,6,23,2,1,0,0,0,0,0),(10,10,2,0,11,5,0,1,0,0,0,0,0,0),(11,27,3,3,21,13,13,1,0,0,0,0,0,0),(12,5,0,0,2,1,0,1,0,0,0,0,0,0),(13,12,2,1,10,6,8,0,0,0,0,0,0,0),(14,28,2,0,12,5,40,8,0,0,0,0,0,0),(15,3,0,0,0,0,2,0,0,1,1,0,0,0),(16,5,0,0,1,1,0,1,0,0,0,0,0,0),(17,8,0,0,3,1,5,1,0,0,0,0,0,0),(18,15,2,0,6,14,1,0,0,7,0,0,0,0),(19,7,0,0,0,0,0,1,0,0,18,4,3,9),(20,24,5,2,23,14,30,3,0,22,0,0,0,0),(21,3,0,0,0,0,0,1,0,0,6,4,5,1),(22,10,0,1,1,1,6,0,0,0,0,0,0,0),(23,16,1,0,2,2,1,0,0,0,0,0,0,0),(24,6,0,0,6,3,3,0,0,1,0,0,0,0),(25,6,0,1,3,1,1,1,0,0,0,0,0,0),(26,30,3,0,9,6,26,2,0,0,0,0,0,0),(27,33,0,0,9,2,33,5,0,0,0,0,0,0),(28,16,0,2,5,4,24,1,0,0,0,0,0,0),(29,5,0,0,0,0,4,1,0,0,0,0,0,0),(30,4,1,1,5,1,5,1,0,0,0,0,0,0),(31,5,0,0,2,2,7,1,0,0,0,0,0,0),(32,15,0,0,5,1,9,2,0,0,0,0,0,0),(33,30,5,7,58,37,21,3,0,22,1,0,0,0),(34,35,3,8,16,7,20,1,0,53,0,0,0,0);
/*!40000 ALTER TABLE `playerrecord` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `playerstat`
--

DROP TABLE IF EXISTS `playerstat`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `playerstat` (
  `sId` int NOT NULL AUTO_INCREMENT,
  `skill` int DEFAULT '0',
  `finish` int DEFAULT '0',
  `dribbling` int DEFAULT '0',
  `pass` int DEFAULT '0',
  `firstTouch` int DEFAULT '0',
  `corners` int DEFAULT '0',
  `crossing` int DEFAULT '0',
  `freeKick` int DEFAULT '0',
  `penaltyKick` int DEFAULT '0',
  `ofTheBall` int DEFAULT '0',
  `vision` int DEFAULT '0',
  `strength` int DEFAULT '0',
  `pace` int DEFAULT '0',
  `stamina` int DEFAULT '0',
  `leadership` int DEFAULT '0',
  `heading` int DEFAULT '0',
  `communication` int DEFAULT '0',
  `marking` int DEFAULT '0',
  `positioning` int DEFAULT '0',
  `GK_Handling` int DEFAULT '0',
  `GK_Reflexes` int DEFAULT '0',
  `GK_Kick` int DEFAULT '0',
  `GK_oneOnOnes` int DEFAULT '0',
  PRIMARY KEY (`sId`),
  CONSTRAINT `player` FOREIGN KEY (`sId`) REFERENCES `player` (`pId`)
) ENGINE=InnoDB AUTO_INCREMENT=46 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `playerstat`
--

LOCK TABLES `playerstat` WRITE;
/*!40000 ALTER TABLE `playerstat` DISABLE KEYS */;
INSERT INTO `playerstat` VALUES (1,0,0,0,18,14,11,11,13,15,9,35,54,30,22,50,11,50,50,40,55,58,50,54),(2,53,35,57,56,53,53,52,51,27,50,47,65,70,71,60,44,54,58,51,0,0,0,0),(3,44,25,29,32,31,26,25,25,31,26,29,66,60,59,21,43,23,47,30,0,0,0,0),(4,66,30,53,59,54,57,40,29,36,65,54,68,54,71,70,58,50,54,33,0,0,0,0),(5,30,24,25,53,29,39,30,25,34,34,30,58,55,62,56,54,55,58,60,0,0,0,0),(6,59,44,51,63,59,61,47,50,50,45,58,65,47,69,56,60,59,56,62,0,0,0,0),(7,59,51,59,55,59,56,61,50,47,58,54,61,71,62,70,40,64,56,49,0,0,0,0),(8,51,37,58,59,59,59,34,48,32,53,59,67,60,67,62,54,53,53,59,0,0,0,0),(9,70,60,65,50,57,45,46,51,48,60,50,57,74,59,24,52,28,20,46,0,0,0,0),(10,70,58,63,46,54,40,49,53,52,57,50,57,72,58,26,43,26,26,57,0,0,0,0),(11,60,55,62,54,60,51,54,46,54,59,49,46,77,60,52,33,51,35,29,0,0,0,0),(12,54,48,43,40,48,35,39,30,49,44,44,40,69,45,48,35,39,21,25,0,0,0,0),(13,69,48,60,58,59,54,43,29,51,50,57,39,72,66,47,30,51,37,37,0,0,0,0),(14,39,22,21,32,32,20,24,26,31,27,33,68,51,56,23,49,27,51,51,0,0,0,0),(15,75,46,56,52,53,49,49,43,51,53,51,33,66,48,48,38,50,36,36,0,0,0,0),(16,54,61,51,50,49,46,43,35,51,67,49,66,67,72,56,61,46,39,47,0,0,0,0),(17,54,50,54,41,47,35,46,61,45,50,45,43,63,43,47,36,41,23,25,0,0,0,0),(18,51,31,54,59,59,58,35,32,35,51,60,48,50,65,41,49,52,59,55,0,0,0,0),(19,0,0,0,26,11,25,14,14,16,11,29,48,27,21,52,16,51,50,28,52,54,51,48),(20,57,23,35,37,57,36,45,47,36,46,35,71,67,68,25,56,38,56,60,0,0,0,0),(21,0,0,0,21,19,19,13,11,18,8,21,55,24,19,7,11,12,50,13,43,49,53,41),(22,58,55,43,42,43,33,41,37,54,48,44,43,51,43,38,43,23,23,22,0,0,0,0),(23,43,19,22,26,27,27,26,27,32,26,28,58,51,49,47,45,43,47,49,0,0,0,0),(24,54,48,50,56,53,53,53,53,49,44,55,57,51,51,48,44,50,48,51,0,0,0,0),(25,53,49,43,41,44,36,39,36,53,51,43,45,56,44,69,40,41,24,24,0,0,0,0),(26,58,30,47,54,52,50,35,29,36,43,52,50,57,56,58,46,55,40,49,0,0,0,0),(27,35,35,42,50,40,50,27,22,27,48,46,66,49,70,61,53,48,53,59,0,0,0,0),(28,60,36,53,57,51,53,51,27,34,60,47,61,67,63,69,55,49,52,59,0,0,0,0),(29,67,24,33,41,42,26,26,26,37,24,56,66,60,62,52,61,55,55,54,0,0,0,0),(30,58,57,59,52,55,34,45,35,52,60,45,54,68,62,51,54,58,14,14,0,0,0,0),(31,60,40,52,39,58,35,27,26,31,35,27,71,62,60,38,59,50,55,28,0,0,0,0),(32,71,35,54,50,58,48,42,39,36,53,43,60,67,65,67,43,56,54,53,0,0,0,0),(33,79,62,68,58,64,60,51,52,64,53,59,42,84,62,51,52,57,35,22,0,0,0,0),(34,71,50,50,57,56,54,47,47,43,52,51,54,72,68,73,36,54,45,46,0,0,0,0);
/*!40000 ALTER TABLE `playerstat` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `schedule`
--

DROP TABLE IF EXISTS `schedule`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `schedule` (
  `gSeq` int NOT NULL AUTO_INCREMENT,
  `gDate` datetime NOT NULL,
  `gStadium` varchar(30) NOT NULL,
  `gContent` varchar(10) NOT NULL,
  `gOppTeam` int NOT NULL,
  `gResult` int DEFAULT NULL,
  PRIMARY KEY (`gSeq`),
  KEY `FK_idx` (`gOppTeam`),
  CONSTRAINT `FK` FOREIGN KEY (`gOppTeam`) REFERENCES `clubs` (`cId`)
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `schedule`
--

LOCK TABLES `schedule` WRITE;
/*!40000 ALTER TABLE `schedule` DISABLE KEYS */;
INSERT INTO `schedule` VALUES (1,'2023-03-01 13:30:00','광양축구전용구장','1 VS 0',6,0),(2,'2023-03-05 16:00:00','안양종합운동장','1 VS 1',11,2),(3,'2023-03-12 13:30:00','안산와~스타디움','1 VS 1',13,2),(4,'2023-03-19 16:00:00','안양종합운동장','2 VS 1',10,0),(5,'2023-04-01 13:30:00','안양종합운동장','3 VS 0',9,0),(6,'2023-04-08 16:00:00','부천종합운동장','4 VS 2',4,0),(7,'2023-04-18 19:30:00','창원축구센터','2 VS 3',5,1),(8,'2023-04-22 13:30:00','안양종합운동장','1 VS 2',3,1),(9,'2023-04-29 18:30:00','안양종합운동장','1 VS 0',8,0),(10,'2023-05-02 19:00:00','천안종합운동장','4 VS 0',12,0),(11,'2023-05-07 16:00:00','안양종합운동장','0 VS 3',1,1),(12,'2023-05-13 16:00:00','김천종합운동장','0 VS 0',2,2),(13,'2023-05-20 18:30:00','안양종합운동장','2 VS 0',6,0),(14,'2023-05-27 18:30:00','탄천종합운동장','2 VS 1',10,0),(15,'2023-06-03 20:00:00','안양종합운동장','2 VS 2',4,2),(16,'2023-06-25 18:30:00','목동종합운동장','2 VS 1',11,0),(17,'2023-07-02 18:30:00','안양종합운동장','2 VS 4',5,1),(18,'2023-07-08 18:00:00','청주종합경기장','1 VS 2',8,1),(19,'2023-07-15 20:00:00','아산이순신종합운동장','3 VS 2',9,0),(20,'2023-07-18 19:30:00','안양종합운동장','1 VS 1',12,2),(21,'2023-07-24 19:30:00','부산아시아드주경기장','1 VS 2',1,1),(22,'2023-07-31 19:30:00','안양종합운동장','2 VS 0',2,0),(23,'2023-08-05 20:00:00','김포솔터축구장','0 VS 1',3,1),(24,'2023-08-12 19:00:00','안양종합운동장','1 VS 1',13,2),(25,'2023-08-27 19:00:00','안양종합운동장','3 VS 1',6,0),(26,'2023-08-30 19:30:00','청주종합경기장','1 VS 2',8,1),(27,'2023-09-03 20:00:00','안양종합운동장','0 VS 1',1,1),(28,'2023-09-17 16:00:00','아산이순신종합운동장','3 VS 4',9,1),(29,'2023-09-20 19:00:00','안양종합운동장','1 VS 1',5,2),(30,'2023-09-23 16:00:00','김천종합운동장','1 VS 4',2,1),(31,'2023-09-30 13:30:00','안양종합운동장','1 VS 1',10,2),(32,'2023-10-07 18:30:00','김포솔터축구장','0 VS 3',3,1),(33,'2023-10-21 16:00:00','부천종합운동장','1 VS 1',4,2),(34,'2023-10-28 18:30:00','안양종합운동장','3 VS 0',11,1),(35,'2023-11-12 13:30:00','안산와~스타디움','0 VS 0',13,NULL),(36,'2023-11-26 15:00:00','안양종합운동장','0 VS 0',12,NULL);
/*!40000 ALTER TABLE `schedule` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-11-27 13:32:51
