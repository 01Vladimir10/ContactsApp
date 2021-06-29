namespace Domain.Model{
    public class Email {
        public string Label { get; set; }
        public string EmailAddress { get; set; }

        public override string ToString()
        {
            return $"{nameof(Label)}: {Label}, {nameof(EmailAddress)}: {EmailAddress}";
        }
    }
}