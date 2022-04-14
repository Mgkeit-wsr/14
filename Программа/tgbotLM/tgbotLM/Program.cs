using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;


namespace tgbotLM
{
    internal class Program
    {
        private static string token { get; } = "5378804445:AAGFQKDx0QKErv3XCjqT-Wt0yz4JNoENLNM";
        private static TelegramBotClient bot;
        private static LeisureFile[][] LeisureFileArray;
        static void Main()
        {
            LeisureFileArray = new LeisureFile[5][];

            LeisureFileArray[0] = new LeisureFile[2]
            {
                new LeisureFile(@"..\Files\Walks\Parks.txt"),
                new LeisureFile(@"..\Files\Walks\Streets.txt")
            };

            LeisureFileArray[1] = new LeisureFile[2]
            {
                new LeisureFile(@"..\Files\Culture\theatre.txt"),
                new LeisureFile(@"..\Files\Culture\museums.txt")
            };

            LeisureFileArray[2] = new LeisureFile[2]
            {
                new LeisureFile(@"..\Files\Entertaionment\attractions.txt"),
                new LeisureFile(@"..\Files\Entertaionment\water_parks.txt")
            };

            LeisureFileArray[3] = new LeisureFile[2]
            {
                new LeisureFile(@"..\Files\Education\lectures.txt"),
                new LeisureFile(@"..\Files\Education\historical.txt")
            };

            LeisureFileArray[4] = new LeisureFile[2]
            {
                new LeisureFile(@"..\Files\Catering\restaurants.txt"),
                new LeisureFile(@"..\Files\Catering\cafe.txt")
            };

            bot = new TelegramBotClient(token);
            bot.StartReceiving();

            bot.OnMessage += OnMessageHandler;

            Console.ReadLine();
            bot.StopReceiving();
        }

        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            Console.WriteLine($"{e.Message.Chat.Id} - {e.Message.Text}");
            int x = 0;
            string txt = e.Message.Text;
            if (txt == null) { Console.WriteLine("Отсутствует текст"); return; }

            switch (txt.ToLower()) //""
            {
                case "/start":
                    goto case "привет";
                //x = 0;
                //await bot.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: "Здравствуйте! Я бот для предоставления Вам досуга в Москве. Какой досуг вы хотите провести? Выберите из представленых:", replyMarkup: GetButtons(x));
                //break;
                case "привет":
                    x = 0;
                    await bot.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: "Здравствуйте! Я бот для предоставления Вам досуга в Москве. Какой досуг вы хотите провести? Выберите из представленых:", replyMarkup: GetButtons(x));
                    break;
                case "в начало":
                    x = 0;
                    await bot.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: "Cписок досуга:", replyMarkup: GetButtons(x));
                    break;
                case "список досуга":
                    x = 0;
                    await bot.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: "Список досуга:", replyMarkup: GetButtons(x));
                    break;
                case "прогулка":
                    x = 2;
                    await bot.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: "Прогулки это хорошо! Приятный воздух, красивые парки и улицы! Куда Вы хотите сходить? Парки или красивые улицы:", replyMarkup: GetButtons(x));
                    break;
                case "культура":
                    x = 3;
                    await bot.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: "Посмотреть выступление, изучить старые картины и историю России. Куда Вы пойдёте? Театры или Музеи?", replyMarkup: GetButtons(x));
                    break;
                case "развлечение":
                    x = 4;
                    await bot.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: "Не когда не поздно покататься на аттракционах, сходить в аквапарк! Так куда Вы хотите? Аттракционы или аквапарки:", replyMarkup: GetButtons(x));
                    break;
                case "образование":
                    x = 5;
                    await bot.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: "Всегда найдётся чему научиться и кто научит! Выбирайте! Лекции или Библиотеки:", replyMarkup: GetButtons(x));
                    break;
                case "общепит":
                    x = 6;
                    await bot.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: "После хорошей прогулки хорошо перекусить! Рестораны или Кафе:", replyMarkup: GetButtons(x));
                    break;
                case "парки":
                    SendMess(e.Message.Chat.Id, LeisureFileArray[0][0].GetRandomLeisure());
                    break;
                case "красочные улицы":
                    SendMess(e.Message.Chat.Id, LeisureFileArray[0][1].GetRandomLeisure());
                    break;
                case "театры":
                    SendMess(e.Message.Chat.Id, LeisureFileArray[1][0].GetRandomLeisure());
                    break;
                case "музеи":
                    SendMess(e.Message.Chat.Id, LeisureFileArray[1][1].GetRandomLeisure());
                    break;
                case "аттракционы":
                    SendMess(e.Message.Chat.Id, LeisureFileArray[2][0].GetRandomLeisure());
                    break;
                case "аквапарки":
                    SendMess(e.Message.Chat.Id, LeisureFileArray[2][1].GetRandomLeisure());
                    break;
                case "открытые лекции":
                    SendMess(e.Message.Chat.Id, LeisureFileArray[3][0].GetRandomLeisure());
                    break;
                case "библиотеки":
                    SendMess(e.Message.Chat.Id, LeisureFileArray[3][1].GetRandomLeisure());
                    break;
                case "рестораны":
                    SendMess(e.Message.Chat.Id, LeisureFileArray[4][0].GetRandomLeisure());
                    break;
                case "кафе":
                    SendMess(e.Message.Chat.Id, LeisureFileArray[4][1].GetRandomLeisure());
                    break;
                default:
                    x = 0;
                    await bot.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: "Список досуга", replyMarkup: GetButtons(x));
                    break;
            }

        }

        private static async void SendMess(long id,string s)
        {
            await bot.SendTextMessageAsync(chatId: id, text: s);

        }
        private static IReplyMarkup GetButtons(int x)
        {
            ReplyKeyboardMarkup a;
            switch (x) 
            {
                case 0:
                    a = new ReplyKeyboardMarkup
                    {
                        Keyboard = new List<List<KeyboardButton>>
                        {
                            new List<KeyboardButton> { new KeyboardButton { Text = "Прогулка" }, new KeyboardButton { Text = "Культура" },new KeyboardButton { Text = "Общепит" } },
                            new List<KeyboardButton> { new KeyboardButton { Text = "Образование" }, new KeyboardButton { Text = "Развлечение" } }
                        }
                    };
                    break;
                case 2:
                    a = new ReplyKeyboardMarkup
                    {
                        Keyboard = new List<List<KeyboardButton>>
                        {
                            new List<KeyboardButton>{ new KeyboardButton {Text="Парки"}, new KeyboardButton {Text="Красивые улицы"}, new KeyboardButton { Text = "В начало" } }
                        }
                    };
                    break;
                case 3:
                    a = new ReplyKeyboardMarkup
                    {
                        Keyboard = new List<List<KeyboardButton>>
                        {
                            new List<KeyboardButton>{ new KeyboardButton {Text="Театры"}, new KeyboardButton {Text="Музеи"}, new KeyboardButton { Text = "В начало" } }
                        }
                    };
                    break;
                case 4:
                    a = new ReplyKeyboardMarkup
                    {
                        Keyboard = new List<List<KeyboardButton>>
                        {
                            new List<KeyboardButton>{ new KeyboardButton {Text="Аттракционы"}, new KeyboardButton {Text="Аквапарки"}, new KeyboardButton { Text = "В начало" } }
                        }
                    };
                    break;
                case 5:
                    a = new ReplyKeyboardMarkup
                    {
                        Keyboard = new List<List<KeyboardButton>>
                        {
                            new List<KeyboardButton>{ new KeyboardButton {Text="Открытые лекции"}, new KeyboardButton {Text="Библиотеки"}, new KeyboardButton { Text = "В начало" } }
                        }
                    };
                    break;
                case 6:
                    a = new ReplyKeyboardMarkup
                    {
                        Keyboard = new List<List<KeyboardButton>>
                        {
                            new List<KeyboardButton>{ new KeyboardButton {Text="Рестораны"}, new KeyboardButton {Text="Кафе"}, new KeyboardButton { Text = "В начало" } }
                        }
                    };
                    break;
                default:
                    a = new ReplyKeyboardMarkup
                    {
                        Keyboard = new List<List<KeyboardButton>>
                        {
                            new List<KeyboardButton>{ new KeyboardButton {Text="Привет"}, new KeyboardButton {Text="Список досуга"}, new KeyboardButton { Text = "В начало" } }
                        }
                    };
                    break;
            }

            



            return a;
        }
    }
}
