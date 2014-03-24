//==========================================================================================================================================
Get stuff
{0} {1} for inserting of strings or ints.

// get the user info
SELECT * FROM Users WHERE Users_id = {0};

//get the ship info
SELECT * FROM Ship, Ship_to_Media, Media WHERE(
	Ship.Ship_id == Ship_to_Media.Ship_id AND
	Ship_to_Media.Media_id == Media.Media_id ) 
	AND Ship_id = {0};

//get the Planet info
SELECT * FROM Planet, Planet_to_Media, Media WHERE ( 
	Planet.Planet_id == Planet_to_Media.Planet_id AND
	Planet_to_Media.Planet_id == Media.Media_id
	) AND Planet_id = {0};
	
//get Resource info
SELECT * FROM Resources, Resources_to_Media, Media WHERE (
	Resources.Resource_id == Resources_to_Media.Resource_id AND 
	Resources_to_Media.Media_id == Media.Media_id) 
	AND Resources_id = {0};

//get the Media info
SELECT * FROM Media WHERE Media_id = {0};

//get hightscore
SELECT * FROM Users, HighScore WHERE Users.Users_id == HighScore.Users_id;

//populate the user ship with resources
SELECT * FROM Users, Ship, Ship_to_Resource, Resources, Ship_to_Media, Media WHERE ( 
	Users.Users_id == Ship.Owner AND 
	Ship.Ship_id == Ship_to_Resource.Ship_id AND 
	Ship_to_Resource.Resource_id == Resource.Resource_id AND
	Ship.Ship_id == Ship_to_Media.Ship_id AND
	Ship_to_Media.Media_id == Media.Media_id) 
	AND Users_id = {0};
	
// get planet resources with price
SELECT Resources.Name, Planet_to_Resource.Price FROM Planet, Planet_to_Resource, Resources WHERE (
	Resources.Resources_id == Planet_to_Resources.Resources_id AND
	Planet_to_Resources.Planet_id == Planet.Planet_id) 
	AND Planet.Planet_id = {0} OR Planet.Title = {0};
	
//==========================================================================================================================================
Insert Stuff
{0} {1} for inserting of strings or ints.

// insert users
INSERT INTO Users (Name, Money) VALUES({0},{1});

// insert hightscore
INSERT INTO HighScore (Users_id, Score) VALUES({0},{1});

// insert ships which has no current media
INSERT INTO Ship (types, Health_Level, Cargo_Level, Owner) VALUES ({0},100,{1},{2});
INSERT INTO Media (x_size, y_size,file_location,types) VALUES ({3},{4},{5},{6});
INSERT INTO Ship_to_Media (Ship_id, Media_id, Reason) VALUES ((SELECT Ship_id FROM Ship ORDER BY Ship_id DESC LIMIT 1), (SELECT Media_id FROM Media ORDER BY Media_id DESC LIMIT 1), {6});

//insert ship which has similar media already
INSERT INTO Ship (types, Health_Level, Cargo_Level, Owner) VALUES ({0},100,{1},{2});
INSERT INTO Ship_to_Media (Ship_id, Media_id, Reason) VALUES ((SELECT Ship_id FROM Ship ORDER BY Ship_id DESC LIMIT 1), (SELECT Media_id FROM Media, Ship_2_Resources, Ship WHERE
	(Ship.Ship_id == Ship_to_Media.Ship_id AND
	Ship_to_Media.Media_id == Media.Media_id) AND Ship.types = {3}), {4});

//insert Resources which has no media
INSERT INTO Resources (Name, Inital_Price, Description) VALUES ({0},{1},{2});
INSERT INTO Media (x_size, y_size,file_location,types) VALUES ({3},{4},{5},{6});
INSERT INTO Resources_to_Media (Resourcs_id, Media_id) VALUES ((SELECT Resources_id FROM Resources ORDER BY Resources_id DESC LIMIT 1), (SELECT Media_id FROM Media ORDER BY Media_id DESC LIMIT 1), {6});

//insert Resources which has media
INSERT INTO Resources (Name, Inital_Price, Description) VALUES ({0},{1},{2});
INSERT INTO Resources_to_Media(Resources_id,Media_id) VALUES ((SELECT Resources_id FROM Resources ORDER BY Resources_id DESC LIMIT 1),(SELECT Resources_to_Media.Media_id FROM Resources_to_Media, Resources WHERE
	Resources.Resources_id == Resources_to_Media.Resources_id AND (Resources.Resources_id = {3} OR Resources.Name = {3})));

//insert Planet which has no media
INSERT INTO Planet (Title, X_loc,Y_loc,Diameter) VALUES ({0},{1},{2},{3});
INSERT INTO Media (x_size, y_size,file_location,types) VALUES ({4},{5},{6},{7});
INSERT INTO Planet_to_Media(Planet_id,Media_id) VALUES ((SELECT Planet_id FROM Planet ORDER BY Planet_id DESC LIMIT 1), (SELECT Media_id FROM Media ORDER BY Media_id DESC LIMIT 1));

//insert Planet which has media
INSERT INTO Planet (Title, X_loc,Y_loc,Diameter) VALUES ({0},{1},{2},{3});
INSERT INTO Planet_to_Media(Planet_id,Media_id) VALUES ((SELECT Planet_id FROM Planet ORDER BY Planet_id DESC LIMIT 1), (SELECT Media_id FROM Media WHERE file_location = {4}));

//insert Resource to ship
INSERT INTO Ship_to_Resource (Ship_id, Resource_id, amount,Bought_Price) VALUES (
	(SELECT Ship_id FROM Ship WHERE (Owner = {1} AND Ship_id = {0})),
	(SELECT Resource_id FROM Resources WHERE Name = {2}),
	{3}, {4});
UPDATE Ship SET Ship.Cargo_Level = Ship.Cargo_Level - {3} WHERE (SELECT Ship_id FROM Ship WHERE (Owner = {1} AND Ship_id = {0}));

//Insert Resource to Planet
INSERT INTO Planet_to_Resource (Planet_id, Resource_id, Price) VALUES (
	(SELECT Planet_id FROM Planet WHERE (Planet.Title = {1} OR Planet.Planet_id ={2})),
	(SELECT Resource_id FROM Resources WHERE (Resources.Name = {3} OR Resources.Resources_id = {4})),{5});
	
//==========================================================================================================================================
Update entries

// user money 
UPDATE Users SET money = {0} WHERE Users_id = {1};

// update ship stats
UPDATE Ship SET Ammo_Level = {0}, Health_Level ={1}, Cargo_Level={2}, Fuel_Level={3} WHERE Owner = {4} AND Ship_id = {5};

//update ship cargo
UPDATE Ship_to_Resource SET amount = {0}, Bought_Price = {1} WHERE (Resource_id = {2} AND Ship_id = (SELECT Ship_id FROM Ship WHERE (Owner = {3} AND Ship_id = {4})));
UPDATE Ship SET Cargo_Level = Cargo_Level + {0} WHERE Ship_id = {4};

//update ship loc
UPDATE Ship SET x_loc = {0}, y_loc = {1} WHERE Ship_id = {2};

//update planet Resources
UPDATE Planet_to_Resource SET Price = {0} WHERE (SELECT Planet_id FROM Planet WHERE Title = {1}) AND (SELECT Resources_id FROM Resources WHERE Name = {2});

