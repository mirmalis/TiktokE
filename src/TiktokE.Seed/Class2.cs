using System;
using System.Collections.Generic;
using TiktokE.Seed.Reimplementations;

namespace TiktokE.Seed
{
  internal static class Class2
  {
    public static DataStructure Data = new DataStructure();
    static Class2()
    {
      M1();
    }
    private static void M1()
    {
      var letwins = new Channel("letwins") { ID = new Guid("6412ddfb-d8db-40ce-bd2c-a3beb6bb8a2e")};
      var egleblozienetupperware = new Channel("egleblozienetupperware") { ID = new Guid("cc6641e6-78a8-4f27-aa99-30d60294034d")};
      var homm9k = new Channel("homm9k") { ID = new Guid("03f1e2bd-620c-41fd-8e5d-51cba8676b79") };
      var paulytepouu = new Channel("paulytepouu") { ID = new Guid("44a9abd8-64db-4c87-8c3c-1d1a21e1be2d")};

      var me = new User("Aš") { ID = new Guid("E8A2311F-04E6-4FF5-9B57-F5FAC1F83B44") };
      me.AddUplaoderPreference(Core.PreferenceType.Ignore, letwins, egleblozienetupperware, homm9k, paulytepouu);
      me.AddTagPreference(Core.PreferenceType.Ignore, new Tag("geradovana"));
      me.AddVideoInterraction(Core.VideoInteraction.MarkedToDownload,
        "https://www.tiktok.com/@meredithduxbury/video/6950377751517891846"
        , "https://www.tiktok.com/@llaurengibson/video/6948222706932370694"
        , "https://www.tiktok.com/@memas420/video/6946165000167836934?lang=en&is_copy_url=0&is_from_webapp=v1&sender_device=pc&sender_web_id=6940132320322225670"
        , "https://www.tiktok.com/@karolinasla/video/6949139344380775686?lang=en&is_copy_url=0&is_from_webapp=v1&sender_device=pc&sender_web_id=6940132320322225670"
      );
      Data.AddRange(
        new Channel("meredithduxbury") { ID = new Guid("c0bd26a0-2712-45e6-8af8-3aa32e5b62b5")}, 
        new Channel("llaurengibson") { ID = new Guid("ee598a87-1924-49e9-9c48-b64c316ebd9b")}, 
        new Channel("karolinasla") { ID = new Guid("db7b80bc-0ba9-407e-8877-a1d496c76f3c") }, 
        new Channel("memas420") { ID = new Guid("65571449-4b02-4f90-b2ce-e717f3c2ba71") }
      );
      Data.AddRange(me);
    }
  }
}