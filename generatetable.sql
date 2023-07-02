create table library_app.books
(
    book_id       serial
        constraint books_pk
            primary key,
    book_title    text,
    publisher     text,
    author        text,
    cover_img_url text
);

alter table library_app.books
    owner to pgbqmord;

