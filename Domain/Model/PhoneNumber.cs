namespace Domain.Model {
    public class PhoneNumber {
        public string Label { get; set; }
        public string Number { get; set; }

        public override string ToString()
        {
            return $"{nameof(Label)}: {Label}, {nameof(Number)}: {Number}";
        }
    }
}