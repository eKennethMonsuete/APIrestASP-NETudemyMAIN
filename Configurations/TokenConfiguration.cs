namespace APIrestASP_NETudemy.Configurations
{
    public class TokenConfiguration
    {

        public string Audience
        {
        get; set; }
        public string Issuer
        {
        get; set; }
        public string Secret
        {
        get; set; }
        public string Minutes
        {
        get; set; }
        
        public string DaysToExpire
        {
        get; set; }
    }
}
