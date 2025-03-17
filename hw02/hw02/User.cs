namespace hw02
{
    public class User
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        private static readonly string FilePath = "users.txt";
        public static void SaveUser(User user)
        {
            File.AppendAllText(FilePath, $"{user.Name};{user.Login};{user.Password}\n");
        }

        public static List<User> GetAllUsers()
        {
            if (!File.Exists(FilePath)) return new List<User>();

            return File.ReadAllLines(FilePath)
                .Select(line => line.Split(';'))
                .Where(part => part.Length == 3)
                .Select(part => new User { Name = part[0], Login = part[1], Password = part[2] })
                .ToList();
        }
        public static bool ValidateUser(string login, string password)
        {
            return GetAllUsers().Any(u => u.Login == login && u.Password == password);
        }
    }
}
