create database AssistiveRobot

go

use AssistiveRobot

drop table if exists Goal
drop table if exists Location
drop table if exists Job

create table Job
(
    JobId       bigint identity (1, 1) not null,
    Status      varchar(45)            null,
    CreatedDate datetime               null,
    UpdatedDate datetime               null,
    primary key (JobId)
)

go

create table Location
(
    LocationId   bigint identity (1, 1) not null,
    Name         varchar(255)           null,
    PositionX    decimal(10, 7)         null,
    PositionY    decimal(10, 7)         null,
    PositionZ    decimal(10, 7)         null,
    OrientationX decimal(10, 7)         null,
    OrientationY decimal(10, 7)         null,
    OrientationZ decimal(10, 7)         null,
    OrientationW decimal(10, 7)         null,
    primary key (LocationId)
)

create table Goal
(
    GoalId     bigint identity (1, 1) not null,
    JobId      bigint                 not null,
    LocationId bigint                 not null,
    Status     varchar(45)            null,
    primary key (GoalId),
    foreign key (JobId) references Job (JobId),
    foreign key (LocationId) references Location (LocationId)
)

go

