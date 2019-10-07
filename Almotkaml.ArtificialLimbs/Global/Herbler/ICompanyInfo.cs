namespace Almotkaml.ArtificialLimbs.Global.Herbler
{
    public interface ICompanyInfo
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
        //add by ali alherbade 26-05-2019
        string PayrollUnit { get; set; }// وحدة المرتبات
        string References { get; set; }// المراجع 
        string FinancialAuditor { get; set; }// المراقب المالي
        string FinancialAffairs { get; set; }// الشئون المالية
        string Department { get; set; }// القسم


    }
}