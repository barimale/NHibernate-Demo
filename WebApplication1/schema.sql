
    drop table ProductType cascade constraints

    drop table Product cascade constraints

    create table ProductType (
        Id NUMBER(10,0) not null,
       Description VARCHAR2(50),
       primary key (Id)
    )

    create table Product (
        Id NUMBER(10,0) not null,
       Name VARCHAR2(50),
       Category VARCHAR2(50),
       Discontinued NUMBER(1,0),
       primary key (Id)
    )
