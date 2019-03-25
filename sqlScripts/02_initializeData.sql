INSERT INTO Customers
VALUES ('Bob', 'Spider', 'email@test.com', '099999888', '133 salas 601, Rio de Janeiro 22070-010. BRAZIL'),
('Science', 'Fiction', 'email@test2.com', '099999888', 'Mentone 3194. Victoria. AUSTRALIA')

INSERT INTO CreditCards
VALUES (1,	'33344455566',	'Bob Spider',	500.00, '2019-01-04 00:00:00.000'),
(2,	'444333222111',	'Science Fiction',	100.00, '2019-01-01 00:00:00.000'),
(1,	'555333222111',	'Other Fiction',	600.00, '2018-01-01 00:00:00.000')


INSERT INTO Transactions
VALUES (1, 1, 500.00, 0, 'success', '2019-01-03 00:00:00.000'),
(1, 2, 500.00, 1, 'insufficient funds', '2019-01-03 00:00:00.000'),
(2, 2, 500.00, 3, 'card expired', '2019-01-03 00:00:00.000')