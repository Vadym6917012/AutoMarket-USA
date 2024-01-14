using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AutoMarket.Server.Core.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BodyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriveTrains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriveTrains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FuelTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GearBoxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearBoxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Generations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearFrom = table.Column<int>(type: "int", nullable: false),
                    YearTo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProducingCountries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProducingCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateCreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateExpiresUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Makes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProducingCountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Makes_ProducingCountries_ProducingCountryId",
                        column: x => x.ProducingCountryId,
                        principalTable: "ProducingCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MakeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Makes_MakeId",
                        column: x => x.MakeId,
                        principalTable: "Makes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModelGeneration",
                columns: table => new
                {
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    GenerationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelGeneration", x => new { x.ModelId, x.GenerationId });
                    table.ForeignKey(
                        name: "FK_ModelGeneration_Generations_GenerationId",
                        column: x => x.GenerationId,
                        principalTable: "Generations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModelGeneration_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modifications_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    GenerationId = table.Column<int>(type: "int", nullable: false),
                    ModificationId = table.Column<int>(type: "int", nullable: false),
                    VIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BodyTypeId = table.Column<int>(type: "int", nullable: false),
                    GearBoxTypeId = table.Column<int>(type: "int", nullable: false),
                    DriveTrainId = table.Column<int>(type: "int", nullable: false),
                    FuelTypeId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnicalConditionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsAdvertisementApproved = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_BodyTypes_BodyTypeId",
                        column: x => x.BodyTypeId,
                        principalTable: "BodyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_DriveTrains_DriveTrainId",
                        column: x => x.DriveTrainId,
                        principalTable: "DriveTrains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_FuelTypes_FuelTypeId",
                        column: x => x.FuelTypeId,
                        principalTable: "FuelTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_GearBoxes_GearBoxTypeId",
                        column: x => x.GearBoxTypeId,
                        principalTable: "GearBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Generations_GenerationId",
                        column: x => x.GenerationId,
                        principalTable: "Generations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Modifications_ModificationId",
                        column: x => x.ModificationId,
                        principalTable: "Modifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_TechnicalConditions_TechnicalConditionId",
                        column: x => x.TechnicalConditionId,
                        principalTable: "TechnicalConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePathToDisplay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BodyTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Седан" },
                    { 2, "Хетчбек" },
                    { 3, "Мінівен" },
                    { 4, "Купе" },
                    { 5, "Позашляховик / Кросовер" }
                });

            migrationBuilder.InsertData(
                table: "DriveTrains",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Передній" },
                    { 2, "Задній" },
                    { 3, "Повний" }
                });

            migrationBuilder.InsertData(
                table: "FuelTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Бензин" },
                    { 2, "Газ" },
                    { 3, "Газ метан / Бензин" },
                    { 4, "Газ пропан-бутан / Бензин" },
                    { 5, "Гібрид (HEV)" },
                    { 6, "Гібрид (PHEV)" },
                    { 7, "Дизель" },
                    { 8, "Електро" }
                });

            migrationBuilder.InsertData(
                table: "GearBoxes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ручна / Механіка" },
                    { 2, "Автомат" },
                    { 3, "Типтронік" },
                    { 4, "Робот" },
                    { 5, "Варіатор" }
                });

            migrationBuilder.InsertData(
                table: "Generations",
                columns: new[] { "Id", "Name", "YearFrom", "YearTo" },
                values: new object[,]
                {
                    { 1, "E39", 1995, 2000 },
                    { 2, "E39 [restyling]", 2000, 2004 },
                    { 3, "G15 (FL)", 2022, 2023 },
                    { 4, "G15", 2018, 2023 },
                    { 5, "E31", 1987, 1999 },
                    { 6, "G05 (FL)", 2023, 2023 },
                    { 7, "G05", 2018, 2023 },
                    { 8, "F15", 2013, 2018 },
                    { 9, "E70 (FL)", 2010, 2013 },
                    { 10, "E70", 2006, 2010 },
                    { 11, "E53 (FL)", 2003, 2006 },
                    { 12, "E53", 2000, 2003 },
                    { 13, "E36/7 (FL)", 1999, 2002 },
                    { 14, "E36/7", 1995, 1999 }
                });

            migrationBuilder.InsertData(
                table: "ProducingCountries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Німеччина" },
                    { 2, "Японія" },
                    { 3, "Франція" },
                    { 4, "США" },
                    { 5, "Корея" },
                    { 6, "Чехія" },
                    { 7, "Італія" },
                    { 8, "Швеція" },
                    { 9, "Англія" },
                    { 10, "Іспанія" }
                });

            migrationBuilder.InsertData(
                table: "TechnicalConditions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Пошкодження або раніше відремонтовані пошкодження відсутні", "Повністю непошкоджене" },
                    { 2, "Пошкодження усунуті, не потребує ремонту", "Професійно відремонтовані пошкодження" },
                    { 3, "Після ДТП, сліди граду, пошкодження кузова, несправність рульового управління, коробки передач, осей і т.д.", "Не відремонтовані пошкодження" },
                    { 4, "Через ДТП, пожежу, несправності двигуна і т.д.", "Не на ходу / На запчастини" }
                });

            migrationBuilder.InsertData(
                table: "Makes",
                columns: new[] { "Id", "ImagePath", "Name", "ProducingCountryId" },
                values: new object[,]
                {
                    { 1, "https://localhost:7119/MakeIcons\\bmw.svg", "BMW", 1 },
                    { 2, "https://localhost:7119/MakeIcons\\toyota.svg", "Toyota", 2 },
                    { 3, "https://localhost:7119/MakeIcons\\audi.svg", "Audi", 1 },
                    { 4, "https://localhost:7119/MakeIcons\\maybach.svg", "Maybach", 1 },
                    { 5, "https://localhost:7119/MakeIcons\\mercedes benz.svg", "Mercedes-Benz", 1 },
                    { 6, "https://localhost:7119/MakeIcons\\opel.svg", "Opel", 1 },
                    { 7, "https://localhost:7119/MakeIcons\\porsche.svg", "Porsche", 1 },
                    { 8, "https://localhost:7119/MakeIcons\\smart.svg", "Smart", 1 },
                    { 9, "https://localhost:7119/MakeIcons\\volkswagen.svg", "Volkswagen", 1 },
                    { 10, "https://localhost:7119/MakeIcons\\bmw.svg", "BMW-Alpina", 1 },
                    { 11, "https://localhost:7119/MakeIcons\\daihatsu.svg", "Daihatsu", 2 },
                    { 12, "https://localhost:7119/MakeIcons\\honda.svg", "Honda", 2 },
                    { 13, "https://localhost:7119/MakeIcons\\isuzu.svg", "Isuzu", 2 },
                    { 14, "https://localhost:7119/MakeIcons\\lexus.svg", "Lexus", 2 },
                    { 15, "https://localhost:7119/MakeIcons\\mazda.svg", "Mazda", 2 },
                    { 16, "https://localhost:7119/MakeIcons\\mitsubishi.svg", "Mitsubishi", 2 },
                    { 17, "https://localhost:7119/MakeIcons\\nissan.svg", "Nissan", 2 },
                    { 18, "https://localhost:7119/MakeIcons\\subaru.svg", "Subaru", 2 },
                    { 19, "https://localhost:7119/MakeIcons\\suzuki.svg", "Suzuki", 2 },
                    { 20, "https://localhost:7119/MakeIcons\\acura.svg", "Acura", 2 },
                    { 21, "https://localhost:7119/MakeIcons\\infiniti.svg", "Infiniti", 2 },
                    { 22, "https://localhost:7119/MakeIcons\\scion.svg", "Scion", 2 },
                    { 23, "https://localhost:7119/MakeIcons\\aixam.svg", "Aixam", 3 },
                    { 24, "https://localhost:7119/MakeIcons\\citroen.svg", "Citroen", 3 },
                    { 25, "https://localhost:7119/MakeIcons\\peugeot.svg", "Peugeot", 3 },
                    { 26, "https://localhost:7119/MakeIcons\\renault.svg", "Renault", 3 },
                    { 27, "https://localhost:7119/MakeIcons\\alpine.svg", "Alpine", 3 },
                    { 28, "https://localhost:7119/MakeIcons\\bugatti.svg", "Bugatti", 3 },
                    { 29, "https://localhost:7119/MakeIcons\\cadillac.svg", "Cadillac", 4 },
                    { 30, "https://localhost:7119/MakeIcons\\chevrolet.svg", "Chevrolet", 4 },
                    { 31, "https://localhost:7119/MakeIcons\\chrysler.svg", "Chrysler", 4 },
                    { 32, "https://localhost:7119/MakeIcons\\ford.svg", "Ford", 4 },
                    { 33, "https://localhost:7119/MakeIcons\\jeep.svg", "Jeep", 4 },
                    { 34, "https://localhost:7119/MakeIcons\\buick.svg", "Buick", 4 },
                    { 35, "https://localhost:7119/MakeIcons\\dodge.svg", "Dodge", 4 },
                    { 36, "https://localhost:7119/MakeIcons\\eagle.svg", "Eagle", 4 },
                    { 37, "https://localhost:7119/MakeIcons\\gmc.svg", "GMC", 4 },
                    { 38, "https://localhost:7119/MakeIcons\\hummer.svg", "Hummer", 4 },
                    { 39, "https://localhost:7119/MakeIcons\\lincoln.svg", "Lincoln", 4 },
                    { 40, "https://localhost:7119/MakeIcons\\mercury.svg", "Mercury", 4 },
                    { 41, "https://localhost:7119/MakeIcons\\oldsmobile.svg", "Oldsmobile", 4 },
                    { 42, "https://localhost:7119/MakeIcons\\pontiac.svg", "Pontiac", 4 },
                    { 43, "https://localhost:7119/MakeIcons\\plymouth.svg", "Plymouth", 4 },
                    { 44, "https://localhost:7119/MakeIcons\\saturn.svg", "Saturn", 4 },
                    { 45, "https://localhost:7119/MakeIcons\\tesla.svg", "Tesla", 4 },
                    { 46, "https://localhost:7119/MakeIcons\\fisker.svg", "Fisker", 4 },
                    { 47, "https://localhost:7119/MakeIcons\\ram.svg", "Ram", 4 },
                    { 48, "https://localhost:7119/MakeIcons\\daewoo.svg", "Daewoo", 5 },
                    { 49, "https://localhost:7119/MakeIcons\\hyundai.svg", "Hyundai", 5 },
                    { 50, "https://localhost:7119/MakeIcons\\kia.svg", "Kia", 5 },
                    { 51, "https://localhost:7119/MakeIcons\\genesis.svg", "Genesis", 5 },
                    { 52, "https://localhost:7119/MakeIcons\\skoda.svg", "Skoda", 6 },
                    { 53, "https://localhost:7119/MakeIcons\\alfa romeo.svg", "Alfa Romeo", 7 },
                    { 54, "https://localhost:7119/MakeIcons\\ferrari.svg", "Ferrari", 7 },
                    { 55, "https://localhost:7119/MakeIcons\\fiat.svg", "Fiat", 7 },
                    { 56, "https://localhost:7119/MakeIcons\\lamborghini.svg", "Lamborghini", 7 },
                    { 57, "https://localhost:7119/MakeIcons\\maserati.svg", "Maserati", 7 },
                    { 58, "https://localhost:7119/MakeIcons\\saab.svg", "Saab", 8 },
                    { 59, "https://localhost:7119/MakeIcons\\volvo.svg", "Volvo", 8 },
                    { 60, "https://localhost:7119/MakeIcons\\aston martin.svg", "Aston Martin", 9 },
                    { 61, "https://localhost:7119/MakeIcons\\bentley.svg", "Bentley", 9 },
                    { 62, "https://localhost:7119/MakeIcons\\land rover.svg", "Land Rover", 9 },
                    { 63, "https://localhost:7119/MakeIcons\\rolls royce.svg", "Rolls Royce", 9 },
                    { 64, "https://localhost:7119/MakeIcons\\mini.svg", "Mini", 9 },
                    { 65, "https://localhost:7119/MakeIcons\\mclaren.svg", "McLaren", 9 },
                    { 66, "https://localhost:7119/MakeIcons\\seat.svg", "Seat", 10 }
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "MakeId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "5 Series" },
                    { 2, 2, "4Runner" },
                    { 3, 1, "8 Series" },
                    { 4, 1, "X5" },
                    { 5, 1, "Z1" },
                    { 6, 1, "Z3" },
                    { 7, 1, "Z4" },
                    { 8, 1, "X3" },
                    { 9, 1, "M Series" },
                    { 10, 1, "X6" },
                    { 11, 1, "1 Series" },
                    { 12, 1, "X5 M" },
                    { 13, 1, "M5" },
                    { 14, 1, "X6 M" },
                    { 15, 1, "M6" },
                    { 16, 1, "X1" },
                    { 17, 1, "7 Series" },
                    { 18, 1, "X Series" },
                    { 19, 1, "6 Series Gran Coupe" },
                    { 20, 1, "X2" },
                    { 21, 1, "Z Series" },
                    { 22, 1, "4 Series" },
                    { 23, 1, "2 Series" },
                    { 24, 1, "3 Series GT" },
                    { 25, 1, "X4" },
                    { 26, 1, "4 Series Gran Coupe" },
                    { 27, 1, "i8" },
                    { 28, 1, "2 Series Active Tourer" },
                    { 29, 1, "5 Series GT" },
                    { 30, 1, "I3" },
                    { 31, 1, "M2" },
                    { 32, 1, "M4" },
                    { 33, 1, "X7" },
                    { 34, 1, "6 Series GT" },
                    { 35, 1, "X3 M" },
                    { 36, 1, "X4 M" },
                    { 37, 1, "M8" },
                    { 38, 1, "8 Series Gran Coupe" },
                    { 39, 1, "2 Series Gran Coupe" },
                    { 40, 1, "M8 Gran Coupe" },
                    { 41, 1, "2 Series Gran Tourer" },
                    { 42, 1, "iX3" },
                    { 43, 1, "iX" },
                    { 44, 1, "i3S" },
                    { 45, 1, "i4" },
                    { 46, 1, "i7" },
                    { 47, 1, "1800" },
                    { 48, 1, "3 Series Compact" },
                    { 49, 1, "E9" },
                    { 50, 1, "E3" },
                    { 51, 1, "iX1" },
                    { 52, 1, "XM" },
                    { 53, 1, "i5" }
                });

            migrationBuilder.InsertData(
                table: "ModelGeneration",
                columns: new[] { "GenerationId", "ModelId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 3 },
                    { 4, 3 },
                    { 5, 3 },
                    { 6, 4 },
                    { 7, 4 },
                    { 8, 4 },
                    { 9, 4 },
                    { 10, 4 },
                    { 11, 4 },
                    { 12, 4 },
                    { 13, 6 },
                    { 14, 6 }
                });

            migrationBuilder.InsertData(
                table: "Modifications",
                columns: new[] { "Id", "ModelId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "520i AT (150 hp)" },
                    { 2, 1, "520i MT (150 hp)" },
                    { 3, 6, "2.8 MT(192 hp)" },
                    { 4, 6, "2.8 AT (192 hp)" },
                    { 5, 6, "3.0i MT (231 hp)" },
                    { 6, 6, "3.0i AT (231 hp)" },
                    { 7, 1, "3.2i MT (321 hp)" },
                    { 8, 6, "1.9 MT(140 hp)" },
                    { 9, 6, "1.9 AT (140 hp)" },
                    { 10, 6, "2.0 MT (150 hp)" },
                    { 11, 6, "2.2i MT (170 hp)" },
                    { 12, 6, "2.2i AT (170 hp)" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BodyTypeId",
                table: "Cars",
                column: "BodyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DriveTrainId",
                table: "Cars",
                column: "DriveTrainId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_FuelTypeId",
                table: "Cars",
                column: "FuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_GearBoxTypeId",
                table: "Cars",
                column: "GearBoxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_GenerationId",
                table: "Cars",
                column: "GenerationId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ModelId",
                table: "Cars",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ModificationId",
                table: "Cars",
                column: "ModificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_TechnicalConditionId",
                table: "Cars",
                column: "TechnicalConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_UserId",
                table: "Cars",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_CarId",
                table: "Images",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Makes_ProducingCountryId",
                table: "Makes",
                column: "ProducingCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelGeneration_GenerationId",
                table: "ModelGeneration",
                column: "GenerationId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_MakeId",
                table: "Models",
                column: "MakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Modifications_ModelId",
                table: "Modifications",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "ModelGeneration");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BodyTypes");

            migrationBuilder.DropTable(
                name: "DriveTrains");

            migrationBuilder.DropTable(
                name: "FuelTypes");

            migrationBuilder.DropTable(
                name: "GearBoxes");

            migrationBuilder.DropTable(
                name: "Generations");

            migrationBuilder.DropTable(
                name: "Modifications");

            migrationBuilder.DropTable(
                name: "TechnicalConditions");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Makes");

            migrationBuilder.DropTable(
                name: "ProducingCountries");
        }
    }
}
