CREATE TABLE Users (
	User_ID INT NOT NULL,
	Name VARCHAR(255) NOT NULL,
	Money INT,
	CONSTRAINT pk_User PRIMARY KEY (User_ID)
);

CREATE TABLE HightScore (
	HightScore_Id INT NOT NULL,
	User_ID INT,
	Score INT,
	CONSTRAINT pk_High PRIMARY KEY (HightScore_Id),
	CONSTRAINT fk_High_User FOREIGN KEY (User_ID) REFERENCES User (User_ID)
) ;

CREATE TABLE Ship (
	Ship_ID INT NOT NULL,
	Model INT,
	Ammo_Level INT,
	Health_Level INT,
	Cargo_Level INT,
	Fuel_Level INT,
	Owner INT,
	Extenstions VARCHAR(255),
	CONSTRAINT pk_Ship PRIMARY KEY (Ship_ID),
	CONSTRAINT fk_ship_user FOREIGN KEY (Owner) REFERENCES User (User_ID)
);

CREATE TABLE Resources (
	Resources_ID INT NOT NULL,
	Name VARCHAR(255) NOT NULL,
	Initial_Price INT,
	Description VARCHAR(255),
	CONSTRAINT pk_Resources PRIMARY KEY (Resources_ID)
);

CREATE TABLE Planet (
	Planet_ID INT NOT NULL,
	Title VARCHAR(255),
	X_loc INT,
	Y_loc INT,
	Diameter INT,
	CONSTRAINT pk_Planet PRIMARY KEY(Planet_ID)
);

CREATE TABLE Media (
	Media_ID INT NOT NULL,
	X_size INT,
	Y_size INT,
	length INT,
	file_Loc VARCHAR(255),
	Media_type INT,
	CONSTRAINT pk_Media PRIMARY KEY (Media_ID)
);

CREATE TABLE PlanetResources (
	PR_ID INT NOT NULL,
	Planet_ID INT,
	Resources_ID INT,
	Price INT,
	CONSTRAINT pk_PR PRIMARY KEY (PR_ID),
	CONSTRAINT fk_PR_Pl FOREIGN KEY (Planet_ID) REFERENCES Planet (Planet_ID),
	CONSTRAINT fk_PR_RS FOREIGN KEY (Resources_ID) REFERENCES Resources (Resources_ID)
);

CREATE TABLE ShipResource (
	SR_ID INT NOT NULL,
	Ship_ID INT,
	Resources_ID INT,
	Amount INT,
	Bought_Price INT,
	CONSTRAINT pk_SR PRIMARY KEY (SR_ID),
	CONSTRAINT fk_SR_Sh FOREIGN KEY (Ship_ID) REFERENCES Ship (Ship_ID),
	CONSTRAINT fk_SR_Rs FOREIGN KEY (Resources_ID) REFERENCES Resources (Resources_ID)
);

CREATE TABLE ResourcesMedia (
	RM_ID INT NOT NULL,
	Resources_ID INT,
	Media_ID INT,
	CONSTRAINT pk_RM PRIMARY KEY (RM_ID),
	CONSTRAINT fk_RM_R FOREIGN KEY (Resources_ID) REFERENCES Resources (Resources_ID),
	CONSTRAINT fk_RM_M FOREIGN KEY (Media_ID) REFERENCES Media (Media_ID)
);

CREATE TABLE ShipMedia (
	SM_ID INT NOT NULL,
	Ship_ID INT,
	Media_ID INT,
	Reason INT,
	CONSTRAINT pk_SM PRIMARY KEY (SM_ID),
	CONSTRAINT fk_SM_R FOREIGN KEY (Ship_ID) REFERENCES Ship (Ship_ID),
	CONSTRAINT fk_SM_M FOREIGN KEY (Media_ID) REFERENCES Media (Media_ID)
);

CREATE TABLE PlanetMedia (
	PM_ID INT NOT NULL,
	Planet_ID INT,
	Media_ID INT,
	CONSTRAINT pk_PM PRIMARY KEY (PM_ID),
	CONSTRAINT fk_PM_R FOREIGN KEY (Planet_ID) REFERENCES Planet (Planet_ID),
	CONSTRAINT fk_PM_M FOREIGN KEY (Media_ID) REFERENCES Media (Media_ID)
);
