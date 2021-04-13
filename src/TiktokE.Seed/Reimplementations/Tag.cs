namespace TiktokE.Seed.Reimplementations
{
  public class Tag : Core.TT.Tag
  {
    #region Constructors
    public Tag() { }
    public Tag(string name) : base()
    {
      this.ID = name;
    }
    #endregion
  }
}
