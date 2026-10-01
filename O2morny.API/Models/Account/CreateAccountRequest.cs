namespace O2morny.API.Models.Account
{
    public class CreateAccountRequest
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }

        public bool IsAcceptTerms { get; set; }

        public bool IsAcceptPrivacy { get; set; }

        public IFormFile? ProfilePictureFile { get; set; }
    }
}
