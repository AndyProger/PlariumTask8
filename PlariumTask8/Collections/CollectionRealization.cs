using System.Linq;
using System.Text;
using UserSpace;

namespace CollectionOfUsers
{
    partial class UsersCollection
    {
        /// <summary>
        /// Найти пользователя, длина писем которого наименьшая.
        /// </summary>
        public static User FindUserWithTheShortesLetter()
        {
            var users = Dictionary.Keys.SelectMany(u => u.Letters,
                                                (u, l) => new { User = u, Letter = l })
                                                .OrderBy(u => u.Letter.Text.Length)
                                                .Select(u => u.User).FirstOrDefault();

            return users;
        }

        /// <summary>
        /// Информация о пользователях, а также количестве полученных и отправленных ими письмах
        /// </summary>
        public static string GetUsersInfo()
        {
            StringBuilder info = new StringBuilder();

            var usersInfo = Dictionary.Keys.Select(x => x + $"Sent letters: {x.LettersSent}\n" +
                    $"Received letters: {x.LettersReceived}\n\n");

            foreach(var str in usersInfo)
            {
                info.Append(str);
            }

            return info.ToString();
        }

        /// <summary>
        /// Информация о пользователях, которые получили хотя бы одно сообщение с заданной темой
        /// </summary>
        public static string GetUsersWithSuchTopic(string topic)
        {
            StringBuilder info = new StringBuilder();

            var users = Dictionary.Keys.SelectMany(u => u.Letters,
                                                (u, l) => new { User = u, Letter = l })
                                                .Where(u => u.Letter.Topic == topic)
                                                .Select(u => u.User);

            foreach (var user in users)
            {
                info.Append(user + "\n");
            }

            return info.ToString();
        }

        /// <summary>
        /// Информация о пользователях, которые не получали сообщения с заданной темой
        /// </summary>
        public static string GetUsersWithoutSuchTopic(string topic)
        {
            StringBuilder info = new StringBuilder();

            var users = Dictionary.Keys.Except(Dictionary.Keys.SelectMany(u => u.Letters,
                                                (u, l) => new { User = u, Letter = l })
                                                .Where(u => u.Letter.Topic == topic)
                                                .Select(u => u.User).Distinct());

            foreach (var user in users)
            {
                info.Append(user + "\n");
            }

            return info.ToString();
        }
    }
}
