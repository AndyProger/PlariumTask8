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
            return Dictionary.Keys.SelectMany(u => u.Letters,
                                             (u, l) => new { User = u, Letter = l })
                                             .OrderBy(u => u.Letter.Text.Length)
                                             .Select(u => u.User).FirstOrDefault();
        }

        /// <summary>
        /// Информация о пользователях, а также количестве полученных и отправленных ими письмах
        /// </summary>
        public static string GetUsersInfo()
        {
            var usersInfo = Dictionary.Keys.Select(x => x + $"Sent letters: {x.LettersSent}\n" +
                    $"Received letters: {x.LettersReceived}\n\n");

            return string.Join(" ", usersInfo);
        }

        /// <summary>
        /// Информация о пользователях, которые получили хотя бы одно сообщение с заданной темой
        /// </summary>
        public static string GetUsersWithSuchTopic(string topic)
        {
            var users = Dictionary.Keys.SelectMany(u => u.Letters,
                                                (u, l) => new { User = u, Letter = l })
                                                .Where(u => u.Letter.Topic == topic)
                                                .Select(u => u.User);

            return string.Join(" ", users);
        }

        /// <summary>
        /// Информация о пользователях, которые не получали сообщения с заданной темой
        /// </summary>
        public static string GetUsersWithoutSuchTopic(string topic)
        {
            var users = Dictionary.Keys.Except(Dictionary.Keys.SelectMany(u => u.Letters,
                                                (u, l) => new { User = u, Letter = l })
                                                .Where(u => u.Letter.Topic == topic)
                                                .Select(u => u.User).Distinct());

            return string.Join(" ", users);
        }
    }
}
