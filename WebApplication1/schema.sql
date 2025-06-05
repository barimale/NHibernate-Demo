
    drop table tbl_ProductType cascade constraints;

    drop table tbl_Product cascade constraints;

    drop table tbl_Address cascade constraints;

    drop table tbl_Company cascade constraints;

    drop table tbl_AddressCompany cascade constraints;

    drop sequence hibernate_sequence;

    create table tbl_ProductType (
        Id NUMBER(10,0) not null,
       Description VARCHAR2(50),
       primary key (Id)
    );

    create table tbl_Product (
        Id NUMBER(10,0) not null,
       Name VARCHAR2(50),
       Category VARCHAR2(50),
       Discontinued NUMBER(1,0),
       primary key (Id)
    );

    create table tbl_Address (
        Id NUMBER(10,0) not null,
       Street VARCHAR2(100),
       City VARCHAR2(50),
       State VARCHAR2(50),
       ZipCode VARCHAR2(20),
       Country VARCHAR2(50),
       CompanyId NUMBER(10,0),
       primary key (Id)
    );

    create table tbl_Company (
        Id NUMBER(10,0) not null,
       AddressId NUMBER(10,0),
       primary key (Id)
    );

    create table tbl_AddressCompany (
        Id NUMBER(10,0) not null,
       Description VARCHAR2(200),
       CreationDate TIMESTAMP(7) not null,
       AddressId NUMBER(10,0) not null,
       CompanyId NUMBER(10,0) not null,
       primary key (Id)
    );

    alter table tbl_Address 
        add constraint FK_D40DEC38 
        foreign key (CompanyId) 
        references tbl_Company;

    alter table tbl_Company 
        add constraint FK_EEC0A3B6 
        foreign key (AddressId) 
        references tbl_Address;

    alter table tbl_AddressCompany 
        add constraint FK_D00665CE 
        foreign key (AddressId) 
        references tbl_Address;

    alter table tbl_AddressCompany 
        add constraint FK_34AF3329 
        foreign key (CompanyId) 
        references tbl_Company;

    create sequence hibernate_sequence;
