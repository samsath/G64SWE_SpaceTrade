CREATE TABLE Users (
	Users_id INTEGER PRIMARY KEY AUTOINCREMENT,
	Name VARCHAR(255) NOT NULL
);

CREATE TABLE Sessions (
	Session_id INTEGER PRIMARY KEY AUTOINCREMENT,
	Users_id INTEGER,
	Money INTEGER,
	CONSTRAINT fk_Session_Users FOREIGN KEY (Users_id) REFERENCES Users (Users_id)
);
	
CREATE TABLE HightScore (
	HightScore_id INTEGER PRIMARY KEY AUTOINCREMENT,
	Users_id INTEGER,
	Score INTEGER,
	CONSTRAINT fk_High_Users FOREIGN KEY (Users_id) REFERENCES Users (Users_id)
);

CREATE TABLE Ship (
	Ship_id INTEGER PRIMARY KEY AUTOINCREMENT,
	Model INTEGER,
	Ammo_Level INTEGER,
	Health_Level INTEGER,
	Cargo_Level INTEGER,
	Fuel_Level INTEGER,
	Owner INTEGER,
	Extenstions VARCHAR(255),
	x_loc INTEGER,
	y_loc INTEGER,
	CONSTRAINT fk_ship_Users FOREIGN KEY (Owner) REFERENCES Users (Users_id)
);

CREATE TABLE Resources (
	Resources_id INTEGER PRIMARY KEY AUTOINCREMENT,
	Name VARCHAR(255) NOT NULL,
	Initial_Price INTEGER,
	Description VARCHAR(255)
);

CREATE TABLE Planet (
	Planet_id INTEGER PRIMARY KEY AUTOINCREMENT,
	Title VARCHAR(255),
	X_loc INTEGER,
	Y_loc INTEGER,
	Diameter INTEGER
);

CREATE TABLE Media (
	Media_id INTEGER PRIMARY KEY AUTOINCREMENT,
	X_size INTEGER,
	Y_size INTEGER,
	length INTEGER,
	file_Loc VARCHAR(255),
	Media_type INTEGER
);


CREATE TABLE PlanetResources (
	PR_id INTEGER PRIMARY KEY AUTOINCREMENT,
	Planet_id INTEGER,
	Resources_id INTEGER,
	Amount INTEGER,
	Price INTEGER,
	CONSTRAINT fk_PR_Pl FOREIGN KEY (Planet_id) REFERENCES Planet (Planet_id),
	CONSTRAINT fk_PR_RS FOREIGN KEY (Resources_id) REFERENCES Resources (Resources_id)
);

CREATE TABLE ShipResource (
	SR_id INTEGER PRIMARY KEY AUTOINCREMENT,
	Ship_id INTEGER,
	Resources_id INTEGER,
	Amount INTEGER,
	Bought_Price INTEGER,
	CONSTRAINT fk_SR_Sh FOREIGN KEY (Ship_id) REFERENCES Ship (Ship_id),
	CONSTRAINT fk_SR_Rs FOREIGN KEY (Resources_id) REFERENCES Resources (Resources_id)
);

CREATE TABLE ResourcesMedia (
	RM_id INTEGER PRIMARY KEY AUTOINCREMENT,
	Resources_id INTEGER,
	Media_id INTEGER,
	CONSTRAINT fk_RM_R FOREIGN KEY (Resources_id) REFERENCES Resources (Resources_id),
	CONSTRAINT fk_RM_M FOREIGN KEY (Media_id) REFERENCES Media (Media_id)
);

CREATE TABLE ShipMedia (
	SM_id INTEGER PRIMARY KEY AUTOINCREMENT,
	Ship_id INTEGER,
	Media_id INTEGER,
	Reason INTEGER,
	CONSTRAINT fk_SM_R FOREIGN KEY (Ship_id) REFERENCES Ship (Ship_id),
	CONSTRAINT fk_SM_M FOREIGN KEY (Media_id) REFERENCES Media (Media_id)
);

CREATE TABLE PlanetMedia (
	PM_id INTEGER PRIMARY KEY AUTOINCREMENT,
	Planet_id INTEGER,
	Media_id INTEGER,
	CONSTRAINT fk_PM_R FOREIGN KEY (Planet_id) REFERENCES Planet (Planet_id),
	CONSTRAINT fk_PM_M FOREIGN KEY (Media_id) REFERENCES Media (Media_id)
);

CREATE TABLE SessionToPlanet (
	Ses_to_Plan_id INTEGER PRIMARY KEY,
	Session_id INTEGER,
	Planet_id INTEGER,
	CONSTRAINT fk_StP_Se FOREIGN KEY (Session_id) REFERENCES Sessions(Session_id),
	CONSTRAINT fk_StP_Pl FOREIGN KEY (Planet_id) REFERENCES Planet(Planet_id)
);

CREATE TABLE SessionToShip (
	Ses_to_Ship_id INTEGER PRIMARY KEY,
	Session_id INTEGER,
	Ship_id INTEGER,
	CONSTRAINT fk_StS_Session FOREIGN KEY (Session_id) REFERENCES Sessions(Session_id),
	CONSTRAINT fk_StS_Ship FOREIGN KEY (Ship_id) REFERENCES Ship(Ship_id)
);

CREATE TABLE SessionToResources (
	Ses_to_Res_id INTEGER PRIMARY KEY,
	Session_id INTEGER,
	Resource_id INTEGER,
	CONSTRAINT fk_StR_Ses FOREIGN KEY (Session_id) REFERENCES Sessions(Session_id),
	CONSTRAINT fk_StR_Res FOREIGN KEY (Resource_id) REFERENCES Resources(REsourcs_id)
);