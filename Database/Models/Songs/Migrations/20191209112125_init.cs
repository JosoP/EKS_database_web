using Microsoft.EntityFrameworkCore.Migrations;

namespace EKS_database_web.Data.Songs.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    _id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    lastModified = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x._id);
                });

            migrationBuilder.CreateTable(
                name: "playlists",
                columns: table => new
                {
                    _id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    lastModified = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playlists", x => x._id);
                });

            migrationBuilder.CreateTable(
                name: "songs",
                columns: table => new
                {
                    _id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(nullable: false),
                    number = table.Column<long>(nullable: true),
                    author = table.Column<string>(nullable: true),
                    lastModified = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_songs", x => x._id);
                });

            migrationBuilder.CreateTable(
                name: "song_category",
                columns: table => new
                {
                    song = table.Column<long>(nullable: false),
                    category = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_song_category", x => new { x.song, x.category });
                    table.ForeignKey(
                        name: "FK_song_category_categories_category",
                        column: x => x.category,
                        principalTable: "categories",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_song_category_songs_song",
                        column: x => x.song,
                        principalTable: "songs",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "song_playlist",
                columns: table => new
                {
                    song = table.Column<long>(nullable: false),
                    playlist = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_song_playlist", x => new { x.song, x.playlist });
                    table.ForeignKey(
                        name: "FK_song_playlist_playlists_playlist",
                        column: x => x.playlist,
                        principalTable: "playlists",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_song_playlist_songs_song",
                        column: x => x.song,
                        principalTable: "songs",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "verses",
                columns: table => new
                {
                    _id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    songId = table.Column<long>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_verses", x => x._id);
                    table.ForeignKey(
                        name: "FK_verses_songs_songId",
                        column: x => x.songId,
                        principalTable: "songs",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_song_category_category",
                table: "song_category",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "IX_song_playlist_playlist",
                table: "song_playlist",
                column: "playlist");

            migrationBuilder.CreateIndex(
                name: "IX_verses_songId",
                table: "verses",
                column: "songId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "song_category");

            migrationBuilder.DropTable(
                name: "song_playlist");

            migrationBuilder.DropTable(
                name: "verses");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "playlists");

            migrationBuilder.DropTable(
                name: "songs");
        }
    }
}
