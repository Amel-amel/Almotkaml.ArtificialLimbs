namespace Almotkaml.ArtificialLimbs.Global.Herbler
{
    public interface ISetting
    {
        string ShortName { get; }
        string DivisonInGovernment { get; }
        string LongName { get; }
        string EnglishName { get; }
        string Phone { get; }
        string Mobile { get; }
        string Email { get; }
        string Website { get; }
        string Address { get; }
        string LogoPath { get; }
       
    }
}