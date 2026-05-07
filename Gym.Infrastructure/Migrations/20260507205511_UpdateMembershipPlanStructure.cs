using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMembershipPlanStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_MembershipPlans_MembershipPlanId",
                table: "Members");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MembershipPlans",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IncludesPersonalTrainer",
                table: "MembershipPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MembershipPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "MembershipPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_MembershipPlans_MembershipPlanId",
                table: "Members",
                column: "MembershipPlanId",
                principalTable: "MembershipPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_MembershipPlans_MembershipPlanId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "MembershipPlans");

            migrationBuilder.DropColumn(
                name: "IncludesPersonalTrainer",
                table: "MembershipPlans");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MembershipPlans");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "MembershipPlans");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_MembershipPlans_MembershipPlanId",
                table: "Members",
                column: "MembershipPlanId",
                principalTable: "MembershipPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
