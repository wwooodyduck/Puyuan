using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuyuanDotNet8.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Uuid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Fb_Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                    table.UniqueConstraint("AK_UserProfile_Uuid", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "BloodPressure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Systolic = table.Column<double>(type: "float", maxLength: 3, nullable: true, defaultValue: 0.0),
                    Diastolic = table.Column<double>(type: "float", maxLength: 3, nullable: true, defaultValue: 0.0),
                    Pulse = table.Column<int>(type: "int", maxLength: 3, nullable: true, defaultValue: 0),
                    Recorded_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodPressure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodPressure_UserProfile_Uuid",
                        column: x => x.Uuid,
                        principalTable: "UserProfile",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloodSugar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sugar = table.Column<int>(type: "int", maxLength: 3, nullable: true, defaultValue: 0),
                    Timeperiod = table.Column<int>(type: "int", maxLength: 3, nullable: true, defaultValue: 0),
                    Recorded_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodSugar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodSugar_UserProfile_Uuid",
                        column: x => x.Uuid,
                        principalTable: "UserProfile",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Default",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Suger_Delta_Max = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Suger_Delta_Min = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Suger_Morning_Max = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Suger_Morning_Min = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Suger_Evening_Max = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Suger_Evening_Min = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Suger_Before_Max = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Suger_Before_Min = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Suger_After_Max = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Suger_After_Min = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Systolic_Max = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Systolic_Min = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Diastolic_Max = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Diastolic_Min = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Pulse_Max = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Pulse_Min = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Weight_Max = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Weight_Min = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Bmi_Max = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Bmi_Min = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Body_Fat_Max = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Body_Fat_Min = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Default", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Default_UserProfile_Uuid",
                        column: x => x.Uuid,
                        principalTable: "UserProfile",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiaryDiet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Meal = table.Column<int>(type: "int", maxLength: 5, nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageCount = table.Column<int>(type: "int", nullable: true),
                    Lat = table.Column<double>(type: "float", maxLength: 100, nullable: true),
                    Lng = table.Column<double>(type: "float", maxLength: 100, nullable: true),
                    Recorded_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryDiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaryDiet_UserProfile_Uuid",
                        column: x => x.Uuid,
                        principalTable: "UserProfile",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrugInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Drug_Type = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Recorded_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugInformation_UserProfile_Uuid",
                        column: x => x.Uuid,
                        principalTable: "UserProfile",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Relation_Id = table.Column<int>(type: "int", nullable: false),
                    Friend_Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Read = table.Column<bool>(type: "bit", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friend", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friend_UserProfile_User_Id",
                        column: x => x.User_Id,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HbA1c",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    A1c = table.Column<double>(type: "float", maxLength: 20, nullable: true),
                    Recorded_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HbA1c", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HbA1c_UserProfile_Uuid",
                        column: x => x.Uuid,
                        principalTable: "UserProfile",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Diabetes_Type = table.Column<int>(type: "int", nullable: true),
                    Oad = table.Column<bool>(type: "bit", nullable: true),
                    Insulin = table.Column<bool>(type: "bit", nullable: true),
                    Anti_Hypertensives = table.Column<bool>(type: "bit", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalInformation_UserProfile_Uuid",
                        column: x => x.Uuid,
                        principalTable: "UserProfile",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: true),
                    Member_Id = table.Column<int>(type: "int", nullable: true),
                    Reply_Id = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_UserProfile_Uuid",
                        column: x => x.Uuid,
                        principalTable: "UserProfile",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    After_Recording = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    No_Recording_For_A_Day = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Over_Max_Or_Under_Min = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    After_Meal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Unit_Of_Sugar = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Unit_Of_Weight = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Unit_Of_Height = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setting_UserProfile_Uuid",
                        column: x => x.Uuid,
                        principalTable: "UserProfile",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Share",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Fid = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Data_Type = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Relation_Type = table.Column<int>(type: "int", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Share", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Share_UserProfile_Uid",
                        column: x => x.Uid,
                        principalTable: "UserProfile",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: true),
                    Member_Id = table.Column<int>(type: "int", nullable: true),
                    Reply_Id = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCare", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCare_UserProfile_Uuid",
                        column: x => x.Uuid,
                        principalTable: "UserProfile",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSet",
                columns: table => new
                {
                    Uuid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "Normal"),
                    Group = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Height = table.Column<int>(type: "int", maxLength: 10, precision: 5, nullable: true),
                    Weight = table.Column<int>(type: "int", maxLength: 10, precision: 5, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Invite_Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UnreadRecordsOne = table.Column<int>(type: "int", maxLength: 10, nullable: false, defaultValue: 0),
                    UnreadRecordsTwo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "0"),
                    UnreadRecordsThree = table.Column<int>(type: "int", maxLength: 10, nullable: false, defaultValue: 0),
                    Verified = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Privacy_Policy = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Must_Change_Password = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Fcm_Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Badge = table.Column<int>(type: "int", maxLength: 10, nullable: false, defaultValue: 0),
                    Login_Times = table.Column<int>(type: "int", maxLength: 20, nullable: false, defaultValue: 0),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSet", x => x.Uuid);
                    table.ForeignKey(
                        name: "FK_UserSet_UserProfile_Uuid",
                        column: x => x.Uuid,
                        principalTable: "UserProfile",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Verification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VerifictionCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Verification_UserProfile_Uuid",
                        column: x => x.Uuid,
                        principalTable: "UserProfile",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Weight = table.Column<double>(type: "float", maxLength: 3, nullable: true, defaultValue: 0.0),
                    Body_Fat = table.Column<double>(type: "float", maxLength: 3, nullable: true, defaultValue: 0.0),
                    Bmi = table.Column<double>(type: "float", maxLength: 3, nullable: true, defaultValue: 0.0),
                    Recorded_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weight_UserProfile_Uuid",
                        column: x => x.Uuid,
                        principalTable: "UserProfile",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BloodPressure_Uuid",
                table: "BloodPressure",
                column: "Uuid");

            migrationBuilder.CreateIndex(
                name: "IX_BloodSugar_Uuid",
                table: "BloodSugar",
                column: "Uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Default_Uuid",
                table: "Default",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiaryDiet_Uuid",
                table: "DiaryDiet",
                column: "Uuid");

            migrationBuilder.CreateIndex(
                name: "IX_DrugInformation_Uuid",
                table: "DrugInformation",
                column: "Uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_User_Id",
                table: "Friend",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_HbA1c_Uuid",
                table: "HbA1c",
                column: "Uuid");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInformation_Uuid",
                table: "MedicalInformation",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Uuid",
                table: "Notification",
                column: "Uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Setting_Uuid",
                table: "Setting",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Share_Uid",
                table: "Share",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_UserCare_Uuid",
                table: "UserCare",
                column: "Uuid");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_Username",
                table: "UserProfile",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Verification_Uuid",
                table: "Verification",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weight_Uuid",
                table: "Weight",
                column: "Uuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodPressure");

            migrationBuilder.DropTable(
                name: "BloodSugar");

            migrationBuilder.DropTable(
                name: "Default");

            migrationBuilder.DropTable(
                name: "DiaryDiet");

            migrationBuilder.DropTable(
                name: "DrugInformation");

            migrationBuilder.DropTable(
                name: "Friend");

            migrationBuilder.DropTable(
                name: "HbA1c");

            migrationBuilder.DropTable(
                name: "MedicalInformation");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "Share");

            migrationBuilder.DropTable(
                name: "UserCare");

            migrationBuilder.DropTable(
                name: "UserSet");

            migrationBuilder.DropTable(
                name: "Verification");

            migrationBuilder.DropTable(
                name: "Weight");

            migrationBuilder.DropTable(
                name: "UserProfile");
        }
    }
}
