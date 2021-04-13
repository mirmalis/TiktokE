using System;

namespace TiktokE.Seed
{
  class Program
  {
    static void Main()
    {
      Console.WriteLine("Hello World!");
      var context = new Db.TiktokEContext();
      Class2.Data.Inject(context, true);
      Console.WriteLine("Goodbye World!");
    }
  }
}
