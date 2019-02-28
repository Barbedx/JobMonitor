using Microsoft.EntityFrameworkCore.Migrations;

namespace AspCoreAngular.Migrations
{
    public partial class addsociallinksmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51aec68f-691f-4085-a954-fb9534769cee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de734578-e029-4cfd-a85f-eb728302c430");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4c77542d-7ba1-4eec-8a70-d64624859e54");

            migrationBuilder.AddColumn<int>(
                name: "FacebookId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GoogleID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0b2bd70b-6159-42d9-a179-7b27d6fc4ac0", "7279be47-97b8-4894-8cff-8d839b8a5db1", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d0a89b7c-1bcc-4769-bb58-f4e8219eb594", "20b2eef8-f354-480b-b1d2-caa2db6308eb", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FacebookId", "GoogleID", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "092ea858-51c8-487b-a0bc-7364f95b9be1", 0, "f5d9b3a2-ec29-45d9-bc87-3a5cf702e4ef", "my@email.com", false, null, null, false, null, null, null, null, null, false, "05b6fb72-253f-43e8-9dcc-49ce2a2894db", false, "myname" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b2bd70b-6159-42d9-a179-7b27d6fc4ac0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0a89b7c-1bcc-4769-bb58-f4e8219eb594");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "092ea858-51c8-487b-a0bc-7364f95b9be1");

            migrationBuilder.DropColumn(
                name: "FacebookId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GoogleID",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "51aec68f-691f-4085-a954-fb9534769cee", "9bf82fdc-858d-4a36-b18a-7aada441f2e3", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "de734578-e029-4cfd-a85f-eb728302c430", "1a2a9a27-4844-4413-a895-0717c6958644", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4c77542d-7ba1-4eec-8a70-d64624859e54", 0, "077f4452-e1e5-4ee2-af21-2de462c295ad", "my@email.com", false, false, null, null, null, null, null, false, "216aa21b-8e15-4b78-ace7-4d1659238252", false, "myname" });
        }
    }
}
