CREATE TABLE WarningData (
	HotelId int NOT NULL,
	Warning nvarchar(256) NOT NULL,
	Date datetime NOT NULL )

CREATE INDEX ix_date ON WarningData(Date);
CREATE INDEX ix_hotelid ON WarningData(HotelId);