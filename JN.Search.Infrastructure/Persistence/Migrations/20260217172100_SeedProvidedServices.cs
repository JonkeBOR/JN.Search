using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JN.Search.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedProvidedServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProvidedServices",
                columns: ["Id", "Latitude", "Longitude", "Name"],
                values: new object[,]
                {
                    { 1, 59.3166428, 18.0561182999999, "Massage" },
                    { 2, 59.3320299, 18.023149800000056, "Salongens massage" },
                    { 3, 59.315887, 18.081163800000013, "Massör" },
                    { 4, 59.3433317, 18.090476800000033, "Svensk massage" },
                    { 5, 59.31952889999999, 18.062400900000057, "Thaimassage" },
                    { 6, 59.34411099999999, 18.049118499999963, "LPG-massage" },
                    { 7, 59.44411099999999, 18.049118499999963, "Massage 30 min" },
                    { 8, 59.44411099999999, 18.149118499999963, "Ansiktsmassage" },
                    { 9, 59.40411099999999, 18.109118499999963, "Massage" },
                    { 10, 59.40211099999999, 18.105118499999963, "Härlig massage" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProvidedServices",
                keyColumn: "Id",
                keyValues: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
        }
    }
}
