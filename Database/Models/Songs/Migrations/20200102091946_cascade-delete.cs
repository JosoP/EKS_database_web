using Microsoft.EntityFrameworkCore.Migrations;

namespace EKS_database_web.Data.Songs.Migrations
{
    public partial class cascadedelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable( // rename old tables
                "song_category",
                null,
                "_song_category_old"
            );
            migrationBuilder.RenameTable(
                "song_playlist",
                null,
                "_song_playlist_old"
            );

            migrationBuilder.CreateTable( // create new tables with on delete cascade
                name: "song_category",
                columns: table => new
                {
                    song = table.Column<long>(nullable: false),
                    category = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_song_category", x => new {x.song, x.category});
                    table.ForeignKey(
                        name: "FK_song_category_categories_category",
                        column: x => x.category,
                        principalTable: "categories",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_song_category_songs_song",
                        column: x => x.song,
                        principalTable: "songs",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.PrimaryKey("PK_song_playlist", x => new {x.song, x.playlist});
                    table.ForeignKey(
                        name: "FK_song_playlist_playlists_playlist",
                        column: x => x.playlist,
                        principalTable: "playlists",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_song_playlist_songs_song",
                        column: x => x.song,
                        principalTable: "songs",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.Sql(
                "INSERT INTO song_category SELECT * FROM _song_category_old;"); // migrate data to new table
            migrationBuilder.Sql("INSERT INTO song_playlist SELECT * FROM _song_playlist_old;");

            migrationBuilder.DropTable("_song_category_old"); // remove old tables
            migrationBuilder.DropTable("_song_playlist_old");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable( // rename old tables
                "song_category",
                null,
                "_song_category_old"
            );
            migrationBuilder.RenameTable(
                "song_playlist",
                null,
                "_song_playlist_old"
            );

            migrationBuilder.CreateTable( // create new tables with on delete Restrict
                name: "song_category",
                columns: table => new
                {
                    song = table.Column<long>(nullable: false),
                    category = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_song_category", x => new {x.song, x.category});
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
                    table.PrimaryKey("PK_song_playlist", x => new {x.song, x.playlist});
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

            migrationBuilder.Sql(
                "INSERT INTO song_category SELECT * FROM _song_category_old;"); // migrate data to new table
            migrationBuilder.Sql("INSERT INTO song_playlist SELECT * FROM _song_playlist_old;");

            migrationBuilder.DropTable("_song_category_old"); // remove old tables
            migrationBuilder.DropTable("_song_playlist_old");
        }
    }
}