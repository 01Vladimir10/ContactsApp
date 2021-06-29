namespace Domain.Model {
    public class Label
    {
        public string LabelId {get; set;}
        public string Color {get; set;}

        public string Description { get; set; }

        public override string ToString()
        {
            return $"{nameof(LabelId)}: {LabelId}, {nameof(Color)}: {Color}, {nameof(Description)}: {Description}";
        }
    }

}