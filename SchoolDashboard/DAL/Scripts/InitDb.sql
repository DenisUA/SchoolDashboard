PRAGMA temp_store = 2;
CREATE TEMP TABLE _Variables(Name TEXT PRIMARY KEY, [Value] INTEGER);

INSERT INTO SchoolLevels ([Description]) VALUES ('1 клас');

INSERT INTO _Variables VALUES ('Id', (SELECT last_insert_rowid()));

INSERT INTO Lessons (SchoolLevel, StartTimeSeconds, EndTimeSeconds, [Description]) VALUES 
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 32400, 34500, 'Урок 1'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 35400, 37500, 'Урок 2'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 38700, 40800, 'Урок 3'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 42000, 44100, 'Урок 4'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 45000, 47100, 'Урок 5');

INSERT INTO SchoolLevels ([Description]) VALUES ('2-4 клас');

UPDATE _Variables SET [Value] = (SELECT last_insert_rowid()) WHERE Name = 'Id';

INSERT INTO Lessons (SchoolLevel, StartTimeSeconds, EndTimeSeconds, [Description]) VALUES 
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 32400, 34800, 'Урок 1'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 35400, 37800, 'Урок 2'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 39000, 41400, 'Урок 3'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 42600, 45000, 'Урок 4'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 45600, 48000, 'Урок 5'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 48600, 51000, 'Урок 6');

INSERT INTO SchoolLevels ([Description]) VALUES ('Старша школа');

UPDATE _Variables SET [Value] = (SELECT last_insert_rowid()) WHERE Name = 'Id';

INSERT INTO Lessons (SchoolLevel, StartTimeSeconds, EndTimeSeconds, [Description]) VALUES 
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 32400, 35100, 'Урок 1'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 35700, 38400, 'Урок 2'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 39000, 41700, 'Урок 3'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 42900, 45600, 'Урок 4'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 46800, 49500, 'Урок 5'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 50100, 52800, 'Урок 6'),
((SELECT [Value] FROM _Variables WHERE Name = 'Id'), 53400, 56100, 'Урок 7');

DROP TABLE _Variables;

---------------Test data

INSERT INTO Awards ([Type], Owner, Description) VALUES 
(1, 'Test1', 'Some text blah blah'),
(2, 'Test2', 'Some text blah blah'),
(3, 'Test3', 'Some text blah blah'),
(4, 'Test4', 'Some text blah blah'),
(5, 'Test5', 'Some text blah blah'),
(6, 'Test6', 'Some text blah blah'),
(7, 'Test7', '2 місце на олімпіаді з правознавства, 11 клас (Ющенко. О.В)'),
(8, 'Парій Максим', '3 місце на олімпіаді з біології'),
(9, 'Дублянко Марта', '2 місце на олімпіаді з правознавства, 11 клас (Ющенко. О.В)'),
(10, 'Кульчицький Сергій', '2 місце на олімпіаді з правознавства, 11 клас (Ющенко. О.В)');

INSERT INTO CalendarEvents (TimeBinary, HasTime, Description, Place) VALUES
(636428058000000000, 1, 'Майстер клас по аеробіці', 'Кабінет №14'),
(636428058000000000, 1, 'Майстер клас по аеробіці', 'Кабінет №14'),
(636428058000000000, 1, 'Майстер клас по аеробіці', 'Кабінет №14'),
(636428058000000000, 1, 'Майстер клас по аеробіці', 'Кабінет №14'),
(636428058000000000, 1, 'Майстер клас по аеробіці', 'Кабінет №14'),
(636400494000000000, 1, 'Турнір по футболу', 'Стадіон'),
(636398568000000000, 1, 'Перший дзвінок', 'Шкільний двір'),
(636426414000000000, 0, 'День вишиванки', 'Школа');

INSERT INTO Facts (FactText) VALUES
('Найбільшу кількість значень має англійське слово "set" (58 значень як іменник, 126 як дієслово, 10 як прикметник, утворений від дієприкметника).'),
('У 1982 році журнал Time присудив титул "Людина року" персональному комп`ютеру.'),
('За прогнозами лінгвістів, через 25 років міжнародним засобом спілкування стане китайська мова, на друге місце по популярності перемістяться мови хінді і урду.'),
('Англійський король Георг I, перший представник Ганноверської династії на королівському троні Великобританії, був німцем, і говорити на англійській так і не навчився.'),
('Кіт хлебче воду або молоко, закидаючи рідину в рот не верхньою, а нижньою поверхнею язика, загинаючи кінчик язика не вгору, а вниз.'),
('Найдовшим в світі словом-паліндромом є фінське слово “saippuakivikauppias”, що означає “торговець шовком”.'),
('Стародавні єгиптяни стверджували: боги не зараховують час життя, проведений на рибалці.'),
('18 лютого 1979 в пустелі Сахара йшов сніг.'),
('Рудоволосі жінки майже не лисіють.'),
('У 1841 році в Мексиці мило було в такому дефіциті і коштувало так дорого, що виконувало функцію грошей.'),
('У національного гімну Греції 158 версій. Жоден житель Греції не знає всіх 158 версій гімну своєї країни.'),
('Китайці, які носять прізвище Чан, найпоширеніше в цій країні, могли б скласти цілу державу з населенням понад сто мільйонів чоловік.'),
('Компанія McDonald`s в процесі створення Windows XP заробила на співробітниках Microsoft більше двох мільйонів доларів'),
('У Саудівській Аравії жінка може отримати розлучення, якщо чоловік не дає їй кави.'),
('Суахілі - це комбінація мов африканських племен, арабської мови і португальської мови.'),
('У грецькій мові крапка з комою грає роль знаку питання, а баскська мова генетично не пов`язана ні з однією відомою мовною сім`єю.'),
('Між плитами піраміди Хеопса неможливо просунути лезо.'),
('В англійській мові більше 600 000 слів.'),
('Автор першої Конституції в світі – український політичний і громадський діяч Пилип Орлик.');

INSERT INTO Holidays ([Day],[Month], Name, Description, Picture) VALUES
(1, 9, '1 Вересня', 'День знань був оголошений державним святом в 1980 році, але тільки у 1984 його масово відзначили у всіх радянських школах. Зробити першим навчальним днем саме 1 Вересня вирішили комуністи – в 1935 році вони зобов’язали всі навчальні заклади починати навчальний рік у перший день осені. До цього в країні не було єдиної дати початку шкільних занять. Зараз День знань не є навчальним днем. Ранок починається на шкільних подвір’ях традиційної лінійкою, першим дзвінком і морем квітів. Потім у класах проводять Урок Світу, після якого дітей чекають різноманітні розважальні заходи.', '1September.jpg');