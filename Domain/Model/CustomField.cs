namespace Domain.Model {
    public class CustomField
    {
        public string Label { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{nameof(Label)}: {Label}, {nameof(Value)}: {Value}";
        }
    }

    
}