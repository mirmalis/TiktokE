using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TiktokE.Db.Migrations
{
    public partial class Initialcommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audios",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    TTID = table.Column<ulong>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audios", x => x.ID);
                    table.UniqueConstraint("AK_Audios_Name_TTID", x => new { x.Name, x.TTID });
                });

            migrationBuilder.CreateTable(
                name: "Handles",
                columns: table => new
                {
                    ID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Verified = table.Column<bool>(type: "INTEGER", nullable: true),
                    ActiveHandleID = table.Column<string>(type: "TEXT", nullable: true),
                    NeedsChecking = table.Column<bool>(type: "INTEGER", nullable: false),
                    Seen = table.Column<bool>(type: "INTEGER", nullable: false),
                    TTID = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Until = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Recorded = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastSeen = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.ID);
                    table.UniqueConstraint("AK_Channels_TTID", x => x.TTID);
                    table.ForeignKey(
                        name: "FK_Channels_Handles_ActiveHandleID",
                        column: x => x.ActiveHandleID,
                        principalTable: "Handles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Channel_Handle",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChannelID = table.Column<Guid>(type: "TEXT", nullable: false),
                    HandleID = table.Column<string>(type: "TEXT", nullable: true),
                    Since = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Until = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Recorded = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastSeen = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel_Handle", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Channel_Handle_Channels_ChannelID",
                        column: x => x.ChannelID,
                        principalTable: "Channels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Channel_Handle_Handles_HandleID",
                        column: x => x.HandleID,
                        principalTable: "Handles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UploaderPreferences",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChannelID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploaderPreferences", x => x.ID);
                    table.UniqueConstraint("AK_UploaderPreferences_UserID_ChannelID", x => new { x.UserID, x.ChannelID });
                    table.ForeignKey(
                        name: "FK_UploaderPreferences_Channels_ChannelID",
                        column: x => x.ChannelID,
                        principalTable: "Channels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UploaderPreferences_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChannelID = table.Column<Guid>(type: "TEXT", nullable: true),
                    HandleID = table.Column<string>(type: "TEXT", nullable: false),
                    AudioID = table.Column<Guid>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    TTID = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Until = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Recorded = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastSeen = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.ID);
                    table.UniqueConstraint("AK_Videos_TTID_HandleID", x => new { x.TTID, x.HandleID });
                    table.ForeignKey(
                        name: "FK_Videos_Audios_AudioID",
                        column: x => x.AudioID,
                        principalTable: "Audios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Videos_Channels_ChannelID",
                        column: x => x.ChannelID,
                        principalTable: "Channels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Videos_Handles_HandleID",
                        column: x => x.HandleID,
                        principalTable: "Handles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVideoInteractions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    VideoID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVideoInteractions", x => x.ID);
                    table.UniqueConstraint("AK_UserVideoInteractions_UserID_VideoID", x => new { x.UserID, x.VideoID });
                    table.ForeignKey(
                        name: "FK_UserVideoInteractions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVideoInteractions_Videos_VideoID",
                        column: x => x.VideoID,
                        principalTable: "Videos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Channel_Handle_ChannelID",
                table: "Channel_Handle",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_Channel_Handle_HandleID",
                table: "Channel_Handle",
                column: "HandleID");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_ActiveHandleID",
                table: "Channels",
                column: "ActiveHandleID");

            migrationBuilder.CreateIndex(
                name: "IX_UploaderPreferences_ChannelID",
                table: "UploaderPreferences",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_UserVideoInteractions_VideoID",
                table: "UserVideoInteractions",
                column: "VideoID");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_AudioID",
                table: "Videos",
                column: "AudioID");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ChannelID",
                table: "Videos",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_HandleID",
                table: "Videos",
                column: "HandleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Channel_Handle");

            migrationBuilder.DropTable(
                name: "UploaderPreferences");

            migrationBuilder.DropTable(
                name: "UserVideoInteractions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Audios");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "Handles");
        }
    }
}
