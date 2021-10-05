/* mylib database */

CREATE DATABASE mylib;

CREATE TABLE books(
	book_id SERIAL NOT NULL PRIMARY KEY,
	bookname VARCHAR(100) NOT NULL,
	au_fname VARCHAR(75),
	au_lname VARCHAR(75),
	page INT,
	publisher VARCHAR(75),
	lang VARCHAR(40),
	genre VARCHAR(40)
);

INSERT INTO books(bookname,au_fname,au_lname,page,publisher,lang,genre)
VALUES ('Kara Kutu: Yüzleşme Vakti','Soner','Yalçın',577,'Kırmızı Kedi','Turkish','Research');

ALTER TABLE books
CONSTRAINT un UNIQUE (books)
