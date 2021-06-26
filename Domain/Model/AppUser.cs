namespace Domain.Model {
    public class AppUser {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return $"{nameof(UserId)}: {UserId}, {nameof(Username)}: {Username}, {nameof(DisplayName)}: {DisplayName}, {nameof(Name)}: {Name}, {nameof(LastName)}: {LastName}";
        }
    }
}