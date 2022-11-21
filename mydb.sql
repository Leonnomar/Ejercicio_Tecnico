-- phpMyAdmin SQL Dump
-- version 4.9.1
-- https://www.phpmyadmin.net/
--
-- Servidor: localhost
-- Tiempo de generación: 21-11-2022 a las 20:44:34
-- Versión del servidor: 8.0.17
-- Versión de PHP: 7.3.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `mydb`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `clases`
--

CREATE TABLE `clases` (
  `Cod_Cla` int(2) NOT NULL,
  `Nom_Cla` varchar(45) NOT NULL,
  `Departamentos_Cod_Dep` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `clases`
--

INSERT INTO `clases` (`Cod_Cla`, `Nom_Cla`, `Departamentos_Cod_Dep`) VALUES
(1, 'Comestibles', 1),
(2, 'Licuadoras', 1),
(3, 'Batidoras', 1),
(4, 'Cafeteras', 1),
(21, 'Amplificadores Car Audio', 2),
(22, 'Auto Stereos', 2),
(41, 'Colchon', 3),
(42, 'Juego Box', 3),
(61, 'Salas', 4),
(62, 'Complementos para Salas', 4),
(63, 'Sofas Cama', 4);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `departamentos`
--

CREATE TABLE `departamentos` (
  `Cod_Dep` int(1) NOT NULL,
  `Nom_Dep` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `departamentos`
--

INSERT INTO `departamentos` (`Cod_Dep`, `Nom_Dep`) VALUES
(1, 'Domésticos'),
(2, 'Electrónica'),
(3, 'Mueble Suelto'),
(4, 'Salas, Recámaras, Comedores');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `familias`
--

CREATE TABLE `familias` (
  `Cod_Fam` int(3) NOT NULL,
  `Nom_Fam` varchar(45) NOT NULL,
  `Clases_Cod_Cla` int(2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `familias`
--

INSERT INTO `familias` (`Cod_Fam`, `Nom_Fam`, `Clases_Cod_Cla`) VALUES
(0, 'Sin nombre', 1),
(1, 'Licuadoras', 2),
(2, 'Exclusivo COPPEL.COM', 2),
(51, 'Batidora Manual', 3),
(52, 'Procesador', 3),
(53, 'Picadora', 3),
(54, 'Batidora Pedestal', 3),
(55, 'Batidora Fuente de Soda', 3),
(56, 'Multipracticos', 3),
(57, 'Exclusivos COPPEL.C0M', 3),
(101, 'Cafeteras', 4),
(102, 'Percoladoras', 4),
(151, 'Amplificador/Receptor', 21),
(152, 'Kit de Intalacion', 21),
(153, 'Amplificadores COPPEL.COM', 21),
(201, 'Autoestereo CD c/Bocinas', 22),
(202, 'Accesorios Car Audio', 22),
(203, 'Amplificadores', 22),
(204, 'Alarma Auto/Casa/Oficina', 22),
(205, 'Sin Mecanismo', 22),
(206, 'Con CD', 22),
(207, 'Multimedia', 22),
(208, 'Paquete sin Mecanismo', 22),
(209, 'Paquete con CD', 22),
(251, 'Pillow Top KS', 41),
(252, 'Pillow Top Doble KS', 41),
(253, 'Hule Espuma KS', 41),
(301, 'Estandar Individual', 42),
(302, 'Pillow Top Individual', 42),
(303, 'Pillow Top Doble Individual', 42),
(351, 'Esquineras Superior', 61),
(352, 'Tipo L Seccional', 61),
(401, 'Sillon Ocasional', 62),
(402, 'Puff', 62),
(403, 'Baul', 62),
(404, 'Taburete', 62),
(451, 'Sofa Cama Tapizado Contemp', 63),
(452, 'Sofa cama clasico', 63),
(453, 'Estudio', 63);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `skus`
--

CREATE TABLE `skus` (
  `SKU` varchar(6) NOT NULL,
  `Artículo` varchar(15) NOT NULL,
  `Marca` varchar(15) NOT NULL,
  `Modelo` varchar(20) NOT NULL,
  `Stock` int(9) NOT NULL,
  `Cantidad` int(9) NOT NULL,
  `Fec_Alt` date NOT NULL,
  `Fec_Baj` date NOT NULL,
  `Departamentos_Cod_Dep` int(11) NOT NULL,
  `Clases_Cod_Cla` int(2) NOT NULL,
  `Familias_Cod_Fam` int(3) NOT NULL,
  `Descontinuado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `clases`
--
ALTER TABLE `clases`
  ADD PRIMARY KEY (`Cod_Cla`,`Departamentos_Cod_Dep`),
  ADD KEY `fk_Clases_Departamentos_idx` (`Departamentos_Cod_Dep`);

--
-- Indices de la tabla `departamentos`
--
ALTER TABLE `departamentos`
  ADD PRIMARY KEY (`Cod_Dep`);

--
-- Indices de la tabla `familias`
--
ALTER TABLE `familias`
  ADD PRIMARY KEY (`Cod_Fam`,`Clases_Cod_Cla`),
  ADD KEY `fk_Familias_Clases_idx` (`Clases_Cod_Cla`);

--
-- Indices de la tabla `skus`
--
ALTER TABLE `skus`
  ADD PRIMARY KEY (`SKU`,`Departamentos_Cod_Dep`,`Clases_Cod_Cla`,`Familias_Cod_Fam`),
  ADD KEY `fk_SKUS_Departamentos_idx` (`Departamentos_Cod_Dep`),
  ADD KEY `fk_SKUS_Clases_idx` (`Clases_Cod_Cla`),
  ADD KEY `fk_SKUS_Familias_idx` (`Familias_Cod_Fam`);

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `clases`
--
ALTER TABLE `clases`
  ADD CONSTRAINT `fk_Clases_Departamentos` FOREIGN KEY (`Departamentos_Cod_Dep`) REFERENCES `departamentos` (`Cod_Dep`);

--
-- Filtros para la tabla `familias`
--
ALTER TABLE `familias`
  ADD CONSTRAINT `fk_Familias_Clases` FOREIGN KEY (`Clases_Cod_Cla`) REFERENCES `clases` (`Cod_Cla`);

--
-- Filtros para la tabla `skus`
--
ALTER TABLE `skus`
  ADD CONSTRAINT `fk_SKUS_Clases` FOREIGN KEY (`Clases_Cod_Cla`) REFERENCES `clases` (`Cod_Cla`),
  ADD CONSTRAINT `fk_SKUS_Departamentos` FOREIGN KEY (`Departamentos_Cod_Dep`) REFERENCES `departamentos` (`Cod_Dep`),
  ADD CONSTRAINT `fk_SKUS_Familias` FOREIGN KEY (`Familias_Cod_Fam`) REFERENCES `familias` (`Cod_Fam`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
