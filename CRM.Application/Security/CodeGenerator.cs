namespace CRM.Application.Security
{
    public class CodeGenerator
    {
        public static string GenerateUniqCode()
        {
            return Guid.NewGuid().ToString("N");
        }
    }

}
