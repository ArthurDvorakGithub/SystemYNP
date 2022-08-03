namespace SystemYNP.Models
{
    public class YNP
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsExistInLocalDb { get; set; } = true;
        public bool ExistInExternalApi { get; set; }
    }
}
