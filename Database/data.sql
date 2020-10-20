INSERT INTO Job (Status, CreatedDate, UpdatedDate)
VALUES ('pending', '2020-07-29 15:10:58.000', null);

INSERT INTO Location (Name, PositionX, PositionY, PositionZ, OrientationX, OrientationY,
                      OrientationZ, OrientationW)
VALUES ('A', -0.7045400, -2.7252000, 0.0000000, 0.0000000, 0.0000000, -0.7023200, 0.7088800),
       ('B', 0.5186200, 1.4819400, 0.0000000, 0.0000000, 0.0000000, 0.0969700, 0.9952800);

INSERT INTO Goal (JobId, LocationId, Status)
VALUES (1, 1, 'pending'),
       (1, 2, 'pending');

INSERT INTO Users (FirstName, LastName, Role, Username, Password)
VALUES ('Administrator', '1', 'Admin', 'admin', '1234')