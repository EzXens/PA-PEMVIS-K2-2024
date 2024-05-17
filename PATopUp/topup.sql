-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 17 Bulan Mei 2024 pada 15.29
-- Versi server: 10.4.28-MariaDB
-- Versi PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `topup`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `game`
--

CREATE TABLE `game` (
  `game_id` int(11) NOT NULL,
  `nama_game` varchar(100) NOT NULL,
  `gambar` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `game`
--

INSERT INTO `game` (`game_id`, `nama_game`, `gambar`) VALUES
(22, 'GENSHIN IMPACT', 'D:\\ICON\\GENSIHN.png'),
(23, 'HONKAI STAR RAIL', 'D:\\ICON\\HSR.png'),
(24, 'ARENA BREAKOUT', 'D:\\ICON\\ARENA.png'),
(25, 'ARKNIGHTS', 'D:\\ICON\\ARKNIGHTS.png'),
(26, 'BLUE ARCHIVE', 'D:\\ICON\\BLUE.png'),
(27, 'CLASH OF CLANS', 'D:\\ICON\\COC.png'),
(28, 'CALL OF DUTY MOBILE', 'D:\\ICON\\CODM.png'),
(29, 'FREE FIRE', 'D:\\ICON\\FF.png'),
(30, 'HONOR OF KINGS', 'D:\\ICON\\HOK.png'),
(40, 'Valorant', 'D:\\ICON\\VALO.png');

-- --------------------------------------------------------

--
-- Struktur dari tabel `matauanggame`
--

CREATE TABLE `matauanggame` (
  `mata_uang_id` int(11) NOT NULL,
  `game_id` int(11) NOT NULL,
  `nama_mata_uang` varchar(50) NOT NULL,
  `jumlah` int(11) DEFAULT NULL,
  `harga_per_unit` decimal(10,2) NOT NULL,
  `gambar` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `matauanggame`
--

INSERT INTO `matauanggame` (`mata_uang_id`, `game_id`, `nama_mata_uang`, `jumlah`, `harga_per_unit`, `gambar`) VALUES
(13, 22, 'genesis crystal', 200, 20000.00, 'D:\\ICON\\matauang\\Genesis Crystal_icon.png'),
(14, 22, 'Genesis Crystal', 2240, 419719.00, 'D:\\ICON\\matauang\\Genesis Crystal_icon.png'),
(15, 22, 'Genesis Crystal', 3880, 634802.00, 'D:\\ICON\\matauang\\Genesis Crystal_icon.png'),
(16, 23, 'Oneiric Shard', 60, 15000.00, 'D:\\ICON\\matauang\\download.jpeg'),
(17, 23, 'Oneiric Shard', 330, 63000.00, 'D:\\ICON\\matauang\\download.jpeg'),
(18, 23, 'Oneiric Shard', 1090, 190000.00, 'D:\\ICON\\matauang\\download.jpeg'),
(19, 23, 'Oneiric Shard', 2240, 413000.00, 'D:\\ICON\\matauang\\download.jpeg'),
(20, 23, 'Oneiric Shard', 3880, 636000.00, 'D:\\ICON\\matauang\\download.jpeg'),
(21, 23, 'Oneiric Shard', 8080, 1272000.00, 'D:\\ICON\\matauang\\download.jpeg'),
(23, 22, 'Genesis Crystal', 8080, 1267000.00, 'D:\\ICON\\matauang\\Genesis Crystal_icon.png'),
(24, 22, 'Blessing of Welkin Moon', 1, 65000.00, 'D:\\ICON\\matauang\\download (2).jpeg'),
(25, 24, 'Bonds', 66, 13000.00, 'D:\\ICON\\matauang\\download (3).jpeg'),
(26, 24, 'Bonds', 335, 68000.00, 'D:\\ICON\\matauang\\download (3).jpeg'),
(27, 24, 'Bonds', 675, 136000.00, 'D:\\ICON\\matauang\\download (3).jpeg'),
(28, 24, 'Bonds', 1690, 342000.00, 'D:\\ICON\\matauang\\download (3).jpeg'),
(29, 24, 'Bonds', 3400, 689000.00, 'D:\\ICON\\matauang\\download (3).jpeg'),
(30, 24, 'Bonds', 6820, 1368000.00, 'D:\\ICON\\matauang\\download (3).jpeg');

-- --------------------------------------------------------

--
-- Struktur dari tabel `transaksi`
--

CREATE TABLE `transaksi` (
  `transaksi_id` int(11) NOT NULL,
  `user_id` int(11) DEFAULT NULL,
  `game_id` int(11) DEFAULT NULL,
  `jumlah_topup` decimal(10,2) NOT NULL,
  `tanggal_transaksi` timestamp NOT NULL DEFAULT current_timestamp(),
  `uid` varchar(255) DEFAULT NULL,
  `status` varchar(50) DEFAULT NULL,
  `mata_uang_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `transaksi`
--

INSERT INTO `transaksi` (`transaksi_id`, `user_id`, `game_id`, `jumlah_topup`, `tanggal_transaksi`, `uid`, `status`, `mata_uang_id`) VALUES
(9, 4, 22, 634802.00, '2024-05-15 10:42:29', '30030303', 'Berhasil', 15),
(10, 4, 22, 634802.00, '2024-05-15 10:50:18', '3243567', 'Berhasil', 15),
(11, 4, 22, 634802.00, '2024-05-15 10:53:55', '432432567', 'Berhasil', 15),
(12, 4, 22, 634802.00, '2024-05-15 11:13:22', '23023032023', 'Berhasil', 15),
(13, 4, 22, 634802.00, '2024-05-15 13:38:42', '345643', 'Berhasil', 15),
(14, 4, 23, 1272000.00, '2024-05-16 10:38:58', '801309964', 'Berhasil', 21),
(15, 4, 23, 15000.00, '2024-05-16 18:59:57', '2332', 'Berhasil', 16),
(16, 4, 24, 1368000.00, '2024-05-16 19:05:01', '3232', 'Berhasil', 30),
(17, 6, 23, 1272000.00, '2024-05-17 02:20:41', '20003230', 'Berhasil', 21),
(18, 7, 24, 1368000.00, '2024-05-17 03:30:36', '211212', 'Berhasil', 30),
(19, 4, 22, 419719.00, '2024-05-17 04:04:08', '7777', 'Berhasil', 14),
(20, 8, 22, 1267000.00, '2024-05-17 07:17:56', '2009404923424', 'Berhasil', 23),
(21, 8, 22, 193556.00, '2024-05-17 07:54:01', '2003232349123', 'Berhasil', 13),
(22, 8, 22, 1267000.00, '2024-05-17 07:58:09', '3923923030192', 'Berhasil', 23),
(23, 9, 22, 65000.00, '2024-05-17 08:17:23', '444444444', 'Berhasil', 24),
(24, 9, 22, 65000.00, '2024-05-17 08:21:05', '999999999', 'Berhasil', 24),
(25, 10, 22, 1267000.00, '2024-05-17 10:01:24', '123456789', 'Berhasil', 23),
(26, 13, 22, 65000.00, '2024-05-17 13:00:27', '123456789', 'Berhasil', 24),
(27, 8, 22, 20000.00, '2024-05-17 13:27:30', '2323323232323', 'Berhasil', 13);

-- --------------------------------------------------------

--
-- Struktur dari tabel `user`
--

CREATE TABLE `user` (
  `user_id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(100) NOT NULL,
  `saldo` decimal(10,2) DEFAULT 0.00
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `user`
--

INSERT INTO `user` (`user_id`, `username`, `password`, `saldo`) VALUES
(1, 'ASEP12345', '123456789', 27504.00),
(2, 'DUSTIN12345', '123456789', 18252.00),
(4, 'asepkopling', '12345678', 44354325.00),
(6, 'reyhankopling', 'sembarang123', 38728000.00),
(7, 'DUSTIN123456', '123456789', 1712000.00),
(8, 'kocak123', 'ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f', 47252444.00),
(9, 'ASEP123456', '15e2b0d3c33891ebb0f1ef609ec419420c20e320ce94c65fbc8c3312448eb225', 5870000.00),
(10, 'natanbebek', 'dd3fdb003083f060a282a27cc03226c48a7a81781bc310ec8a032da1a9f2e5e9', 8753000.00),
(11, 'adipermana1123', 'ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f', 0.00),
(12, 'nagakocak', 'ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f', 0.00),
(13, 'ASHADI12345', '15e2b0d3c33891ebb0f1ef609ec419420c20e320ce94c65fbc8c3312448eb225', 5000.00);

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `game`
--
ALTER TABLE `game`
  ADD PRIMARY KEY (`game_id`),
  ADD UNIQUE KEY `nama_game` (`nama_game`);

--
-- Indeks untuk tabel `matauanggame`
--
ALTER TABLE `matauanggame`
  ADD PRIMARY KEY (`mata_uang_id`),
  ADD KEY `fk_game_id` (`game_id`);

--
-- Indeks untuk tabel `transaksi`
--
ALTER TABLE `transaksi`
  ADD PRIMARY KEY (`transaksi_id`),
  ADD KEY `user_id` (`user_id`),
  ADD KEY `game_id` (`game_id`),
  ADD KEY `mata_uang_id` (`mata_uang_id`);

--
-- Indeks untuk tabel `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT untuk tabel yang dibuang
--

--
-- AUTO_INCREMENT untuk tabel `game`
--
ALTER TABLE `game`
  MODIFY `game_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=43;

--
-- AUTO_INCREMENT untuk tabel `matauanggame`
--
ALTER TABLE `matauanggame`
  MODIFY `mata_uang_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=40;

--
-- AUTO_INCREMENT untuk tabel `transaksi`
--
ALTER TABLE `transaksi`
  MODIFY `transaksi_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT untuk tabel `user`
--
ALTER TABLE `user`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- Ketidakleluasaan untuk tabel pelimpahan (Dumped Tables)
--

--
-- Ketidakleluasaan untuk tabel `matauanggame`
--
ALTER TABLE `matauanggame`
  ADD CONSTRAINT `fk_game_id` FOREIGN KEY (`game_id`) REFERENCES `game` (`game_id`) ON DELETE CASCADE;

--
-- Ketidakleluasaan untuk tabel `transaksi`
--
ALTER TABLE `transaksi`
  ADD CONSTRAINT `fk_game_id_transaksi` FOREIGN KEY (`game_id`) REFERENCES `game` (`game_id`),
  ADD CONSTRAINT `fk_mata_uang` FOREIGN KEY (`mata_uang_id`) REFERENCES `matauanggame` (`mata_uang_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `transaksi_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
