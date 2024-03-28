﻿using System;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Channels;
using System.Diagnostics.Contracts;

internal class Program
{
    private static ReceiverOptions _receiverOptions;
    private static void Main(string[] args)
    {
        Start();
        Console.ReadLine();
    }
    public static async Task Start()
    {
        CancellationTokenSource cts = new();
        _receiverOptions = new();
        var Botclient = new TelegramBotClient("7082038598:AAHqMCO_r1LPZlid_Bv09XWoKgovjPFiPME");
        Botclient.StartReceiving(
     updateHandler: UpdateHandler,
     pollingErrorHandler: ErrorHandler,
     receiverOptions: _receiverOptions,
     cancellationToken: cts.Token);
    }

    private static Task ErrorHandler(ITelegramBotClient botClient, Exception arg2, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        await MessageHadler(botClient, update, cancellationToken);
    }
    async static Task MessageHadler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;
       
        //var user = message.From;


        if (message != null && message.Text != null)
        {
            if (message.Text == "/start")
            {
                await InlineButtonMainMenu(botClient,update.Message.Chat.Id, cancellationToken);
            }
        }
                 await CallBack(botClient, update,cancellationToken);
                 await MarketHandler(botClient,update,cancellationToken);
        return;

    }
    public static async Task InlineButtonMainMenu(ITelegramBotClient botClient,long? ChatId, CancellationToken cancellationToken)
    {

       // var message = update.Message;
        var keyboardInline = new InlineKeyboardMarkup(new[]
                          {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("🛒Магазин ",callbackData:"buttonMarket"),
                            InlineKeyboardButton.WithCallbackData("🗃️История Сделок",callbackData:"buttonTransactionHistory"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("⚙️Настройки  ",callbackData:"buttonSettings"),

                            InlineKeyboardButton.WithCallbackData("🛠️Поддержка", callbackData: "buttonSupport"),
                             InlineKeyboardButton.WithCallbackData("📜Правила", callbackData: "buttonRules"),
                        }

                    });

        await botClient.SendTextMessageAsync(ChatId, "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀Главное меню ", replyMarkup: keyboardInline, cancellationToken: cancellationToken);
        return;
        

    }
    private static async Task CallBack(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        //вывод в зависимости от выбранной кнопки
                    Message message = update.Message;
        if (update != null && update.CallbackQuery != null)
        {
            string answer = update.CallbackQuery.Data;
            switch (answer)
            {
                case "buttonMarket":
                    await Market(botClient, update, cancellationToken);
                    break;
                case "buttonTransactionHistory":
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, answer);
                    break;
                case "buttonSettings":
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, answer);
                    break;
                case "buttonSupport":
                    await Support(botClient, update);
                    break;
                case "buttonRules":
                    await Rules(botClient,update, cancellationToken);
                    break;
                case "buttonBack":
                   // await botClient.DeleteMessageAsync(update.CallbackQuery.Message.Chat.Id, messageId: message.MessageId - 1, cancellationToken: cancellationToken);
                    await InlineButtonMainMenu(botClient,update.CallbackQuery.Message.Chat.Id, cancellationToken);
                    
                    break;
            }


        }
    }
    public static async Task Support(ITelegramBotClient botClient, Update update)
    {
        var keyboardInlineSupportBack = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                            InlineKeyboardButton.WithCallbackData("⬅️ Назад",callbackData:"buttonBack"),
                        }
                    });
        await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat, $"Для написания жадолы напишите нашему боту @botReport", replyMarkup: keyboardInlineSupportBack);

    }
    public  static async Task Settigs(ITelegramBotClient botClient, Update update)
    {
        await botClient.SendTextMessageAsync(update.Message.Chat.Id,$"Настройки \n  твой никней {update.Message.From} \n Баланс ");
    }
    public static async Task Rules(ITelegramBotClient botClient, Update update,CancellationToken cancellationToken)

    {   string messageRules = "1️⃣ Мы ничего не несем  .\n 2️⃣ Базар как вода и то и то нужно фильтроват иначе почкам конец .\n 3️⃣ Шкаф не тумба - тимон и пумба .\n 4️⃣ Знаешь красное море ? - я покрасил . \n 5️⃣ Мало ешь больше кушай.\n 6️⃣  Джони но не Деп";
        var message = update.Message;
        var keyboardInlineBack = new InlineKeyboardMarkup(new[]
                        {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("⬅️ Назад",callbackData:"buttonBack"),
                        }
                    });


        //await botClient.EditMessageTextAsync(update.Message.Chat.Id,messageId, "Привет");

        Console.WriteLine("ну привет");

        await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat,$"Правила \n {messageRules}", replyMarkup: keyboardInlineBack, cancellationToken: cancellationToken);
        // await botClient.SendTextMessageAsync(message.Chat.Id, messageRules, replyMarkup: keyboardInlineBack);
        return;


        

    }
    
   
    public static async Task MarketHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        //вывод в зависимости от выбранной кнопки
        if (update != null && update.CallbackQuery != null)
        {
            string answer = update.CallbackQuery.Data;
            switch (answer)
            {
                case "buttonAccountTelegarm":
                    ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                                   {
                            new KeyboardButton[] { "🛒Купить" },
                            new KeyboardButton[] { "⭐В изранное" },
                            new KeyboardButton[] { "Дальше" },
  
                            });
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Данные о аккаунте ", replyMarkup: replyKeyboardMarkup );
                    return;

                    break;
                case "buttonAccountVK":

                    break;
                case "buttonAccountSteam":
                    
                    break;
                case "buttonAccountEA":
                    break;
                case "buttonAccountEGS":
                   
                    break;
                case "buttonBack":
                    await InlineButtonMainMenu(botClient, update.CallbackQuery.Message.Chat.Id, cancellationToken);

                    break;
            }


        }
    }
    public static async Task Market(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var keyboardInlineMarketAccount = new InlineKeyboardMarkup(new[]
                       {
        new[]
        {
         InlineKeyboardButton.WithCallbackData("Аккаунт Телеграмм",callbackData:"buttonAccountTelegarm")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData("Аккакнт VK",callbackData: "buttonAccountVK"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData("Аккакнт Steam",callbackData: "buttonAccountSteam"),

        },
        new []
        {
             InlineKeyboardButton.WithCallbackData("Аккакнт EA",callbackData: "buttonAccountEA"),
        },
        new[]
        {
              InlineKeyboardButton.WithCallbackData("Аккакнт Epic Game Store",callbackData: "buttonAccountEGS"),

        },
        new []
        {
             InlineKeyboardButton.WithCallbackData("⬅️ Назад",callbackData: "buttonBack"),
        },
        });
        await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat, "Аккаунты👇", replyMarkup: keyboardInlineMarketAccount);


    }
    
}
