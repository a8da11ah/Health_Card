create table Servants
(
    ServantID                int identity
        primary key,
    BirthDate                date,
    Gender                   varchar(50),
    MaritalStatus            varchar(50),
    BloodType                varchar(10),
    Height                   decimal(5, 2),
    Weight                   decimal(5, 2),
    EducationalQualification varchar(255),
    SmokingStatus            bit       default 0,
    DrugAllergies            text,
    CreatedAt                datetime2 default getdate(),
    UpdatedAt                datetime2 default getdate()
)
go

create table ChronicDiseases
(
    ChronicDiseaseID     int identity
        primary key,
    ServantID            int
        constraint FK_ChronicDiseases_Servants
            references Servants,
    DiseaseName          varchar(255) not null,
    Notes                text,
    DiseaseType          varchar(20) default 'PERSONAL'
        check ([DiseaseType] = 'FAMILY' OR [DiseaseType] = 'PERSONAL'),
    FamilyMemberRelation varchar(100),
    CreatedAt            datetime2   default getdate()
)
go

create index IX_ChronicDiseases_ServantID
    on ChronicDiseases (ServantID)
go

create table GeneralRemarks
(
    RemarkID   int identity
        primary key,
    ServantID  int
        constraint FK_GeneralRemarks_Servants
            references Servants,
    Remarks    text,
    OtherNotes text,
    CreatedBy  varchar(255),
    CreatedAt  datetime2 default getdate()
)
go

create index IX_GeneralRemarks_ServantID
    on GeneralRemarks (ServantID)
go

create table MedicalReferrals
(
    ReferralID       int identity
        primary key,
    ServantID        int
        constraint FK_MedicalReferrals_Servants
            references Servants,
    ReferralDate     date not null,
    MedicalDiagnosis text not null,
    LeaveType        varchar(255),
    LeaveDays        int,
    CreatedAt        datetime2 default getdate()
)
go

create index IX_MedicalReferrals_ServantID
    on MedicalReferrals (ServantID)
go

create table ServantChronicTreatments
(
    TreatmentID   int identity
        primary key,
    ServantID     int
        constraint FK_ServantChronicTreatments_Servants
            references Servants,
    TreatmentName varchar(255) not null,
    Notes         text,
    CreatedAt     datetime2 default getdate()
)
go

create index IX_ServantChronicTreatments_ServantID
    on ServantChronicTreatments (ServantID)
go

create table ServantMedicalReviews
(
    ReviewID         int identity
        primary key,
    ServantID        int
        constraint FK_ServantMedicalReviews_Servants
            references Servants,
    ReviewDate       date not null,
    ReviewType       varchar(100),
    MedicalDiagnosis text,
    Notes            text,
    CreatedAt        datetime2 default getdate()
)
go

create index IX_ServantMedicalReviews_ServantID
    on ServantMedicalReviews (ServantID)
go

CREATE TRIGGER TR_Servants_UpdatedAt
    ON Servants
    AFTER UPDATE
    AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Servants
    SET UpdatedAt = GETDATE()
    FROM Servants s
             INNER JOIN inserted i ON s.ServantID = i.ServantID;
END;
go

create table SurgicalOperations
(
    OperationID   int identity
        primary key,
    ServantID     int
        constraint FK_SurgicalOperations_Servants
            references Servants,
    OperationDate date         not null,
    OperationType varchar(255) not null,
    HospitalName  varchar(255),
    Notes         text,
    CreatedAt     datetime2 default getdate()
)
go

create index IX_SurgicalOperations_ServantID
    on SurgicalOperations (ServantID)
go

create table Vaccinations
(
    VaccinationID       int identity
        primary key,
    ServantID           int
        constraint FK_Vaccinations_Servants
            references Servants,
    VaccinationDate     date         not null,
    VaccinationType     varchar(255) not null,
    VaccinationLocation varchar(255),
    Notes               text,
    CreatedAt           datetime2 default getdate()
)
go

create index IX_Vaccinations_ServantID
    on Vaccinations (ServantID)
go

create table WorkInjuries
(
    InjuryID           int identity
        primary key,
    ServantID          int
        constraint FK_WorkInjuries_Servants
            references Servants,
    InjuryDate         date         not null,
    InjuryType         varchar(255) not null,
    DepartmentOfInjury varchar(255),
    Description        text,
    CreatedAt          datetime2 default getdate()
)
go

create index IX_WorkInjuries_ServantID
    on WorkInjuries (ServantID)
go

create table sysdiagrams
(
    name         sysname not null,
    principal_id int     not null,
    diagram_id   int identity
        primary key,
    version      int,
    definition   varbinary(max),
    constraint UK_principal_name
        unique (principal_id, name)
)
go

CREATE VIEW vwChronicDiseases AS
SELECT
    ChronicDiseaseID,
    ServantID,
    DiseaseName,
    Notes,
    DiseaseType,
    FamilyMemberRelation,
    CreatedAt
FROM ChronicDiseases
go

CREATE VIEW vwGeneralRemarks AS
SELECT
    RemarkID,
    ServantID,
    Remarks,
    OtherNotes,
    CreatedBy,
    CreatedAt
FROM GeneralRemarks
go

CREATE VIEW vwMedicalReferrals AS
SELECT
    ReferralID,
    ServantID,
    ReferralDate,
    MedicalDiagnosis,
    LeaveType,
    LeaveDays,
    CreatedAt
FROM MedicalReferrals
go

CREATE VIEW vwServantChronicTreatments AS
SELECT
    TreatmentID,
    ServantID,
    TreatmentName,
    Notes,
    CreatedAt
FROM ServantChronicTreatments
go

CREATE VIEW vwServantMedicalReviews AS
SELECT
    ReviewID,
    ServantID,
    ReviewDate,
    ReviewType,
    MedicalDiagnosis,
    Notes,
    CreatedAt
FROM ServantMedicalReviews
go

CREATE VIEW vwServants AS
SELECT
    ServantID,
    BirthDate,
    Gender,
    MaritalStatus,
    BloodType,
    Height,
    Weight,
    EducationalQualification,
    SmokingStatus,
    DrugAllergies,
    CreatedAt,
    UpdatedAt
FROM Servants
go

CREATE VIEW vwSurgicalOperations AS
SELECT
    OperationID,
    ServantID,
    OperationDate,
    OperationType,
    HospitalName,
    Notes,
    CreatedAt
FROM SurgicalOperations
go

CREATE VIEW vwVaccinations AS
SELECT
    VaccinationID,
    ServantID,
    VaccinationDate,
    VaccinationType,
    VaccinationLocation,
    Notes,
    CreatedAt
FROM Vaccinations
go

CREATE VIEW vwWorkInjuries AS
SELECT
    InjuryID,
    ServantID,
    InjuryDate,
    InjuryType,
    DepartmentOfInjury,
    Description,
    CreatedAt
FROM WorkInjuries
go


	CREATE FUNCTION dbo.fn_diagramobjects() 
	RETURNS int
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		declare @id_upgraddiagrams		int
		declare @id_sysdiagrams			int
		declare @id_helpdiagrams		int
		declare @id_helpdiagramdefinition	int
		declare @id_creatediagram	int
		declare @id_renamediagram	int
		declare @id_alterdiagram 	int 
		declare @id_dropdiagram		int
		declare @InstalledObjects	int

		select @InstalledObjects = 0

		select 	@id_upgraddiagrams = object_id(N'dbo.sp_upgraddiagrams'),
			@id_sysdiagrams = object_id(N'dbo.sysdiagrams'),
			@id_helpdiagrams = object_id(N'dbo.sp_helpdiagrams'),
			@id_helpdiagramdefinition = object_id(N'dbo.sp_helpdiagramdefinition'),
			@id_creatediagram = object_id(N'dbo.sp_creatediagram'),
			@id_renamediagram = object_id(N'dbo.sp_renamediagram'),
			@id_alterdiagram = object_id(N'dbo.sp_alterdiagram'), 
			@id_dropdiagram = object_id(N'dbo.sp_dropdiagram')

		if @id_upgraddiagrams is not null
			select @InstalledObjects = @InstalledObjects + 1
		if @id_sysdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 2
		if @id_helpdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 4
		if @id_helpdiagramdefinition is not null
			select @InstalledObjects = @InstalledObjects + 8
		if @id_creatediagram is not null
			select @InstalledObjects = @InstalledObjects + 16
		if @id_renamediagram is not null
			select @InstalledObjects = @InstalledObjects + 32
		if @id_alterdiagram  is not null
			select @InstalledObjects = @InstalledObjects + 64
		if @id_dropdiagram is not null
			select @InstalledObjects = @InstalledObjects + 128
		
		return @InstalledObjects 
	END
go

deny execute on fn_diagramobjects to guest
go

grant execute on fn_diagramobjects to [public]
go

CREATE PROCEDURE spCreateChronicDisease
    @ServantID INT,
    @DiseaseName NVARCHAR(255),
    @Notes NVARCHAR(MAX) = NULL,
    @DiseaseType INT = 0, -- default PERSONAL
    @FamilyMemberRelation NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO ChronicDiseases
    (
        ServantID,
        DiseaseName,
        Notes,
        DiseaseType,
        FamilyMemberRelation,
        CreatedAt
    )
    VALUES
        (
            @ServantID,
            @DiseaseName,
            @Notes,
            @DiseaseType,
            @FamilyMemberRelation,
            SYSUTCDATETIME()
        );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS ChronicDiseaseID;
END
go

CREATE PROCEDURE spCreateGeneralRemark
    @ServantID INT,
    @Remarks NVARCHAR(MAX) = NULL,
    @OtherNotes NVARCHAR(MAX) = NULL,
    @CreatedBy NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO GeneralRemarks
    (
        ServantID,
        Remarks,
        OtherNotes,
        CreatedBy,
        CreatedAt
    )
    VALUES
        (
            @ServantID,
            @Remarks,
            @OtherNotes,
            @CreatedBy,
            SYSUTCDATETIME()
        );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS RemarkID;
END
go

CREATE PROCEDURE spCreateMedicalReferral
    @ServantID INT,
    @ReferralDate DATE, -- Assuming DATE type for date-only field
    @MedicalDiagnosis NVARCHAR(MAX), -- Assuming string without length means MAX
    @LeaveType NVARCHAR(255) = NULL,
    @LeaveDays INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO MedicalReferrals
    (
        ServantID,
        ReferralDate,
        MedicalDiagnosis,
        LeaveType,
        LeaveDays,
        CreatedAt
    )
    VALUES
        (
            @ServantID,
            @ReferralDate,
            @MedicalDiagnosis,
            @LeaveType,
            @LeaveDays,
            SYSUTCDATETIME()
        );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS ReferralID;
END
go

CREATE PROCEDURE spCreateServant
    @BirthDate DATE = NULL,
    @Gender NVARCHAR(50) = NULL,
    @MaritalStatus NVARCHAR(50) = NULL,
    @BloodType NVARCHAR(10) = NULL,
    @Height DECIMAL(5,2) = NULL,
    @Weight DECIMAL(5,2) = NULL,
    @EducationalQualification NVARCHAR(255) = NULL,
    @SmokingStatus BIT = 0,
    @DrugAllergies NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Servants
    (
        BirthDate,
        Gender,
        MaritalStatus,
        BloodType,
        Height,
        Weight,
        EducationalQualification,
        SmokingStatus,
        DrugAllergies,
        CreatedAt,
        UpdatedAt
    )
    VALUES
        (
            @BirthDate,
            @Gender,
            @MaritalStatus,
            @BloodType,
            @Height,
            @Weight,
            @EducationalQualification,
            @SmokingStatus,
            @DrugAllergies,
            SYSUTCDATETIME(),
            SYSUTCDATETIME()
        );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS ServantID;
END
go

CREATE PROCEDURE spCreateServantChronicTreatment
    @ServantID INT,
    @TreatmentName NVARCHAR(255),
    @Notes NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO ServantChronicTreatments
    (
        ServantID,
        TreatmentName,
        Notes,
        CreatedAt
    )
    VALUES
        (
            @ServantID,
            @TreatmentName,
            @Notes,
            SYSUTCDATETIME()
        );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS TreatmentID;
END
go

CREATE PROCEDURE spCreateServantMedicalReview
    @ServantID INT,
    @ReviewDate DATE,
    @ReviewType NVARCHAR(100) = NULL,
    @MedicalDiagnosis NVARCHAR(MAX) = NULL,
    @Notes NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO ServantMedicalReviews
    (
        ServantID,
        ReviewDate,
        ReviewType,
        MedicalDiagnosis,
        Notes,
        CreatedAt
    )
    VALUES
        (
            @ServantID,
            @ReviewDate,
            @ReviewType,
            @MedicalDiagnosis,
            @Notes,
            SYSUTCDATETIME()
        );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS ReviewID;
END
go

CREATE PROCEDURE spCreateSurgicalOperation
    @ServantID INT,
    @OperationDate DATE,
    @OperationType NVARCHAR(255),
    @HospitalName NVARCHAR(255) = NULL,
    @Notes NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO SurgicalOperations
    (
        ServantID,
        OperationDate,
        OperationType,
        HospitalName,
        Notes,
        CreatedAt
    )
    VALUES
        (
            @ServantID,
            @OperationDate,
            @OperationType,
            @HospitalName,
            @Notes,
            SYSUTCDATETIME()
        );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS OperationID;
END
go

CREATE PROCEDURE spCreateVaccination
    @ServantID INT,
    @VaccinationDate DATE,
    @VaccinationType NVARCHAR(255),
    @VaccinationLocation NVARCHAR(255) = NULL,
    @Notes NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Vaccinations
    (
        ServantID,
        VaccinationDate,
        VaccinationType,
        VaccinationLocation,
        Notes,
        CreatedAt
    )
    VALUES
        (
            @ServantID,
            @VaccinationDate,
            @VaccinationType,
            @VaccinationLocation,
            @Notes,
            SYSUTCDATETIME()
        );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS VaccinationID;
END
go

CREATE PROCEDURE spCreateWorkInjury
    @ServantID INT,
    @InjuryDate DATE,
    @InjuryType NVARCHAR(255),
    @DepartmentOfInjury NVARCHAR(255) = NULL,
    @Description NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO WorkInjuries
    (
        ServantID,
        InjuryDate,
        InjuryType,
        DepartmentOfInjury,
        Description,
        CreatedAt
    )
    VALUES
        (
            @ServantID,
            @InjuryDate,
            @InjuryType,
            @DepartmentOfInjury,
            @Description,
            SYSUTCDATETIME()
        );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS InjuryID;
END
go

CREATE PROCEDURE spDeleteChronicDisease
@ChronicDiseaseID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM ChronicDiseases
    WHERE ChronicDiseaseID = @ChronicDiseaseID;
END
go

CREATE PROCEDURE spDeleteGeneralRemark
@RemarkID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM GeneralRemarks
    WHERE RemarkID = @RemarkID;
END
go

CREATE PROCEDURE spDeleteMedicalReferral
@ReferralID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM MedicalReferrals
    WHERE ReferralID = @ReferralID;
END
go

CREATE PROCEDURE spDeleteServant
@ServantID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Servants WHERE ServantID = @ServantID;
END
go

CREATE PROCEDURE spDeleteServantChronicTreatment
@TreatmentID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM ServantChronicTreatments
    WHERE TreatmentID = @TreatmentID;
END
go

CREATE PROCEDURE spDeleteServantMedicalReview
@ReviewID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM ServantMedicalReviews
    WHERE ReviewID = @ReviewID;
END
go

CREATE PROCEDURE spDeleteSurgicalOperation
@OperationID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM SurgicalOperations
    WHERE OperationID = @OperationID;
END
go

CREATE PROCEDURE spDeleteVaccination
@VaccinationID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Vaccinations
    WHERE VaccinationID = @VaccinationID;
END
go

CREATE PROCEDURE spDeleteWorkInjury
@InjuryID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM WorkInjuries
    WHERE InjuryID = @InjuryID;
END
go

CREATE PROCEDURE spGetChronicDiseaseById
@ChronicDiseaseID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ChronicDiseaseID,
        ServantID,
        DiseaseName,
        Notes,
        DiseaseType,
        FamilyMemberRelation,
        CreatedAt
    FROM ChronicDiseases
    WHERE ChronicDiseaseID = @ChronicDiseaseID;
END
go

CREATE PROCEDURE spGetGeneralRemarkById
@RemarkID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        RemarkID,
        ServantID,
        Remarks,
        OtherNotes,
        CreatedBy,
        CreatedAt
    FROM GeneralRemarks
    WHERE RemarkID = @RemarkID;
END
go

CREATE PROCEDURE spGetMedicalReferralById
@ReferralID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ReferralID,
        ServantID,
        ReferralDate,
        MedicalDiagnosis,
        LeaveType,
        LeaveDays,
        CreatedAt
    FROM MedicalReferrals
    WHERE ReferralID = @ReferralID;
END
go

CREATE PROCEDURE spGetServantById
@ServantID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ServantID,
        BirthDate,
        Gender,
        MaritalStatus,
        BloodType,
        Height,
        Weight,
        EducationalQualification,
        SmokingStatus,
        DrugAllergies,
        CreatedAt,
        UpdatedAt
    FROM Servants
    WHERE ServantID = @ServantID;
END
go

CREATE PROCEDURE spGetServantChronicTreatmentById
@TreatmentID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        TreatmentID,
        ServantID,
        TreatmentName,
        Notes,
        CreatedAt
    FROM ServantChronicTreatments
    WHERE TreatmentID = @TreatmentID;
END
go

CREATE PROCEDURE spGetServantMedicalReviewById
@ReviewID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ReviewID,
        ServantID,
        ReviewDate,
        ReviewType,
        MedicalDiagnosis,
        Notes,
        CreatedAt
    FROM ServantMedicalReviews
    WHERE ReviewID = @ReviewID;
END
go

CREATE PROCEDURE spGetSurgicalOperationById
@OperationID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        OperationID,
        ServantID,
        OperationDate,
        OperationType,
        HospitalName,
        Notes,
        CreatedAt
    FROM SurgicalOperations
    WHERE OperationID = @OperationID;
END
go

CREATE PROCEDURE spGetVaccinationById
@VaccinationID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        VaccinationID,
        ServantID,
        VaccinationDate,
        VaccinationType,
        VaccinationLocation,
        Notes,
        CreatedAt
    FROM Vaccinations
    WHERE VaccinationID = @VaccinationID;
END
go

CREATE PROCEDURE spGetWorkInjuryById
@InjuryID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        InjuryID,
        ServantID,
        InjuryDate,
        InjuryType,
        DepartmentOfInjury,
        Description,
        CreatedAt
    FROM WorkInjuries
    WHERE InjuryID = @InjuryID;
END
go

CREATE PROCEDURE spUpdateChronicDisease
    @ChronicDiseaseID INT,
    @ServantID INT,
    @DiseaseName NVARCHAR(255),
    @Notes NVARCHAR(MAX) = NULL,
    @DiseaseType INT,
    @FamilyMemberRelation NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE ChronicDiseases
    SET
        ServantID = @ServantID,
        DiseaseName = @DiseaseName,
        Notes = @Notes,
        DiseaseType = @DiseaseType,
        FamilyMemberRelation = @FamilyMemberRelation
    WHERE ChronicDiseaseID = @ChronicDiseaseID;
END
go

CREATE PROCEDURE spUpdateGeneralRemark
    @RemarkID INT,
    @ServantID INT,
    @Remarks NVARCHAR(MAX) = NULL,
    @OtherNotes NVARCHAR(MAX) = NULL,
    @CreatedBy NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE GeneralRemarks
    SET
        ServantID = @ServantID,
        Remarks = @Remarks,
        OtherNotes = @OtherNotes,
        CreatedBy = @CreatedBy
    WHERE RemarkID = @RemarkID;
END
go

CREATE PROCEDURE spUpdateMedicalReferral
    @ReferralID INT,
    @ServantID INT,
    @ReferralDate DATE,
    @MedicalDiagnosis NVARCHAR(MAX),
    @LeaveType NVARCHAR(255) = NULL,
    @LeaveDays INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE MedicalReferrals
    SET
        ServantID = @ServantID,
        ReferralDate = @ReferralDate,
        MedicalDiagnosis = @MedicalDiagnosis,
        LeaveType = @LeaveType,
        LeaveDays = @LeaveDays
    WHERE ReferralID = @ReferralID;
END
go

CREATE PROCEDURE spUpdateServant
    @ServantID INT,
    @BirthDate DATE = NULL,
    @Gender NVARCHAR(50) = NULL,
    @MaritalStatus NVARCHAR(50) = NULL,
    @BloodType NVARCHAR(10) = NULL,
    @Height DECIMAL(5,2) = NULL,
    @Weight DECIMAL(5,2) = NULL,
    @EducationalQualification NVARCHAR(255) = NULL,
    @SmokingStatus BIT,
    @DrugAllergies NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Servants
    SET
        BirthDate = @BirthDate,
        Gender = @Gender,
        MaritalStatus = @MaritalStatus,
        BloodType = @BloodType,
        Height = @Height,
        Weight = @Weight,
        EducationalQualification = @EducationalQualification,
        SmokingStatus = @SmokingStatus,
        DrugAllergies = @DrugAllergies,
        UpdatedAt = SYSUTCDATETIME()
    WHERE ServantID = @ServantID;
END
go

CREATE PROCEDURE spUpdateServantChronicTreatment
    @TreatmentID INT,
    @ServantID INT,
    @TreatmentName NVARCHAR(255),
    @Notes NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE ServantChronicTreatments
    SET
        ServantID = @ServantID,
        TreatmentName = @TreatmentName,
        Notes = @Notes
    WHERE TreatmentID = @TreatmentID;
END
go

CREATE PROCEDURE spUpdateServantMedicalReview
    @ReviewID INT,
    @ServantID INT,
    @ReviewDate DATE,
    @ReviewType NVARCHAR(100) = NULL,
    @MedicalDiagnosis NVARCHAR(MAX) = NULL,
    @Notes NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE ServantMedicalReviews
    SET
        ServantID = @ServantID,
        ReviewDate = @ReviewDate,
        ReviewType = @ReviewType,
        MedicalDiagnosis = @MedicalDiagnosis,
        Notes = @Notes
    WHERE ReviewID = @ReviewID;
END
go

CREATE PROCEDURE spUpdateSurgicalOperation
    @OperationID INT,
    @ServantID INT,
    @OperationDate DATE,
    @OperationType NVARCHAR(255),
    @HospitalName NVARCHAR(255) = NULL,
    @Notes NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE SurgicalOperations
    SET
        ServantID = @ServantID,
        OperationDate = @OperationDate,
        OperationType = @OperationType,
        HospitalName = @HospitalName,
        Notes = @Notes
    WHERE OperationID = @OperationID;
END
go

CREATE PROCEDURE spUpdateVaccination
    @VaccinationID INT,
    @ServantID INT,
    @VaccinationDate DATE,
    @VaccinationType NVARCHAR(255),
    @VaccinationLocation NVARCHAR(255) = NULL,
    @Notes NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Vaccinations
    SET
        ServantID = @ServantID,
        VaccinationDate = @VaccinationDate,
        VaccinationType = @VaccinationType,
        VaccinationLocation = @VaccinationLocation,
        Notes = @Notes
    WHERE VaccinationID = @VaccinationID;
END
go

CREATE PROCEDURE spUpdateWorkInjury
    @InjuryID INT,
    @ServantID INT,
    @InjuryDate DATE,
    @InjuryType NVARCHAR(255),
    @DepartmentOfInjury NVARCHAR(255) = NULL,
    @Description NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE WorkInjuries
    SET
        ServantID = @ServantID,
        InjuryDate = @InjuryDate,
        InjuryType = @InjuryType,
        DepartmentOfInjury = @DepartmentOfInjury,
        Description = @Description
    WHERE InjuryID = @InjuryID;
END
go


	CREATE PROCEDURE dbo.sp_alterdiagram
	(
		@diagramname 	sysname,
		@owner_id	int	= null,
		@version 	int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId 			int
		declare @retval 		int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @ShouldChangeUID	int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid ARG', 16, 1)
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();	 
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		revert;
	
		select @ShouldChangeUID = 0
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		
		if(@DiagId IS NULL or (@IsDbo = 0 and @theId <> @UIDFound))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end
	
		if(@IsDbo <> 0)
		begin
			if(@UIDFound is null or USER_NAME(@UIDFound) is null) -- invalid principal_id
			begin
				select @ShouldChangeUID = 1 ;
			end
		end

		-- update dds data			
		update dbo.sysdiagrams set definition = @definition where diagram_id = @DiagId ;

		-- change owner
		if(@ShouldChangeUID = 1)
			update dbo.sysdiagrams set principal_id = @theId where diagram_id = @DiagId ;

		-- update dds version
		if(@version is not null)
			update dbo.sysdiagrams set version = @version where diagram_id = @DiagId ;

		return 0
	END
go

deny execute on sp_alterdiagram to guest
go

grant execute on sp_alterdiagram to [public]
go


	CREATE PROCEDURE dbo.sp_dropdiagram
	(
		@diagramname 	sysname,
		@owner_id	int	= null
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT; 
		
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		delete from dbo.sysdiagrams where diagram_id = @DiagId;
	
		return 0;
	END
go

deny execute on sp_dropdiagram to guest
go

grant execute on sp_dropdiagram to [public]
go


	CREATE PROCEDURE dbo.sp_helpdiagramdefinition
	(
		@diagramname 	sysname,
		@owner_id	int	= null 		
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		set nocount on

		declare @theId 		int
		declare @IsDbo 		int
		declare @DiagId		int
		declare @UIDFound	int
	
		if(@diagramname is null)
		begin
			RAISERROR (N'E_INVALIDARG', 16, 1);
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner');
		if(@owner_id is null)
			select @owner_id = @theId;
		revert; 
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname;
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId ))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end

		select version, definition FROM dbo.sysdiagrams where diagram_id = @DiagId ; 
		return 0
	END
go

deny execute on sp_helpdiagramdefinition to guest
go

grant execute on sp_helpdiagramdefinition to [public]
go


	CREATE PROCEDURE dbo.sp_helpdiagrams
	(
		@diagramname sysname = NULL,
		@owner_id int = NULL
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		DECLARE @user sysname
		DECLARE @dboLogin bit
		EXECUTE AS CALLER;
			SET @user = USER_NAME();
			SET @dboLogin = CONVERT(bit,IS_MEMBER('db_owner'));
		REVERT;
		SELECT
			[Database] = DB_NAME(),
			[Name] = name,
			[ID] = diagram_id,
			[Owner] = USER_NAME(principal_id),
			[OwnerID] = principal_id
		FROM
			sysdiagrams
		WHERE
			(@dboLogin = 1 OR USER_NAME(principal_id) = @user) AND
			(@diagramname IS NULL OR name = @diagramname) AND
			(@owner_id IS NULL OR principal_id = @owner_id)
		ORDER BY
			4, 5, 1
	END
go

deny execute on sp_helpdiagrams to guest
go

grant execute on sp_helpdiagrams to [public]
go


	CREATE PROCEDURE dbo.sp_renamediagram
	(
		@diagramname 		sysname,
		@owner_id		int	= null,
		@new_diagramname	sysname
	
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @DiagIdTarg		int
		declare @u_name			sysname
		if((@diagramname is null) or (@new_diagramname is null))
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT;
	
		select @u_name = USER_NAME(@owner_id)
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		-- if((@u_name is not null) and (@new_diagramname = @diagramname))	-- nothing will change
		--	return 0;
	
		if(@u_name is null)
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @theId and name = @new_diagramname
		else
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @owner_id and name = @new_diagramname
	
		if((@DiagIdTarg is not null) and  @DiagId <> @DiagIdTarg)
		begin
			RAISERROR ('The name is already used.', 16, 1);
			return -2
		end		
	
		if(@u_name is null)
			update dbo.sysdiagrams set [name] = @new_diagramname, principal_id = @theId where diagram_id = @DiagId
		else
			update dbo.sysdiagrams set [name] = @new_diagramname where diagram_id = @DiagId
		return 0
	END
go

deny execute on sp_renamediagram to guest
go

grant execute on sp_renamediagram to [public]
go


	CREATE PROCEDURE dbo.sp_upgraddiagrams
	AS
	BEGIN
		IF OBJECT_ID(N'dbo.sysdiagrams') IS NOT NULL
			return 0;
	
		CREATE TABLE dbo.sysdiagrams
		(
			name sysname NOT NULL,
			principal_id int NOT NULL,	-- we may change it to varbinary(85)
			diagram_id int PRIMARY KEY IDENTITY,
			version int,
	
			definition varbinary(max)
			CONSTRAINT UK_principal_name UNIQUE
			(
				principal_id,
				name
			)
		);


		/* Add this if we need to have some form of extended properties for diagrams */
		/*
		IF OBJECT_ID(N'dbo.sysdiagram_properties') IS NULL
		BEGIN
			CREATE TABLE dbo.sysdiagram_properties
			(
				diagram_id int,
				name sysname,
				value varbinary(max) NOT NULL
			)
		END
		*/

		IF OBJECT_ID(N'dbo.dtproperties') IS NOT NULL
		begin
			insert into dbo.sysdiagrams
			(
				[name],
				[principal_id],
				[version],
				[definition]
			)
			select	 
				convert(sysname, dgnm.[uvalue]),
				DATABASE_PRINCIPAL_ID(N'dbo'),			-- will change to the sid of sa
				0,							-- zero for old format, dgdef.[version],
				dgdef.[lvalue]
			from dbo.[dtproperties] dgnm
				inner join dbo.[dtproperties] dggd on dggd.[property] = 'DtgSchemaGUID' and dggd.[objectid] = dgnm.[objectid]	
				inner join dbo.[dtproperties] dgdef on dgdef.[property] = 'DtgSchemaDATA' and dgdef.[objectid] = dgnm.[objectid]
				
			where dgnm.[property] = 'DtgSchemaNAME' and dggd.[uvalue] like N'_EA3E6268-D998-11CE-9454-00AA00A3F36E_' 
			return 2;
		end
		return 1;
	END
go


