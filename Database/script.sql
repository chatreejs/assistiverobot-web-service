create database AssistiveRobot

go

use AssistiveRobot

drop table if exists Goal
drop table if exists Location
drop table if exists Job
drop table if exists Users
drop table if exists UserToken

create table Job
(
    JobId bigint identity (1, 1) not null,
    Status varchar(45) null,
    CreatedDate datetime null,
    UpdatedDate datetime null,
    primary key (JobId)
)

go

create table Location
(
    LocationId bigint identity (1, 1) not null,
    Name varchar(255) null,
    PositionX decimal(10, 7) null,
    PositionY decimal(10, 7) null,
    PositionZ decimal(10, 7) null,
    OrientationX decimal(10, 7) null,
    OrientationY decimal(10, 7) null,
    OrientationZ decimal(10, 7) null,
    OrientationW decimal(10, 7) null,
    primary key (LocationId)
)

go

create table Goal
(
    GoalId bigint identity (1, 1) not null,
    JobId bigint not null,
    LocationId bigint not null,
    Status varchar(45) null,
    primary key (GoalId),
    foreign key (JobId) references Job (JobId),
    foreign key (LocationId) references Location (LocationId)
)

go

create table Users
(
    UserId bigint identity (1, 1) not null,
    FirstName varchar(50) null,
    LastName varchar(50) null,
    Role varchar(10) not null,
    Username varchar(50) not null,
    Password varchar(50) not null,
    primary key (UserId)
)

create table UserToken
(
    UserId bigint identity (1, 1) not null,
    Issued datetime null,
    Expires datetime null,
    RefreshToken varchar(max) null,
    FcmToken varchar(max) null,
    ClientId varchar(50) null,
    Nonce varchar(max) null,
    CheckSum varchar(max) null
)

go

