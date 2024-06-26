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
using System.Runtime.Intrinsics.Arm;
using Newtonsoft.Json.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

    private static async Task UpdateHandler(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
    {
        //await ButotonUp(botClient,update,cancellationToken);
        await CallBack(botClient, update, cancellationToken);
        await MarketHandler(botClient, update, cancellationToken);
        await MessageHadler(botClient, update, cancellationToken);
        await Telegramaccount(botClient, update, cancellationToken);


    }
    async static Task MessageHadler(ITelegramBotClient botClient, Telegram.Bot.Types.   Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;

        //var user = message.From;


        if (message != null && message.Text != null)
        {
            if (message.Text == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Прветветвую тебя в магазине где ты можешь легко купить аккаунты для игр и для соцсетей ");
                var keyboardInlineMenu = new InlineKeyboardMarkup(new[]
                            {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("🛒Магазин ",callbackData:"buttonMarket"),
                            InlineKeyboardButton.WithCallbackData("🗃️История Сделок",callbackData:"buttonTransactionHistory"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("⚙️ Настройки  ",callbackData:"buttonSettings"),

                            InlineKeyboardButton.WithCallbackData("🛠 ️Поддержка", callbackData: "buttonSupport"),
                             InlineKeyboardButton.WithCallbackData("📜 Правила", callbackData: "buttonRules"),
                        }

                    });

                 await botClient.SendTextMessageAsync(update.Message.Chat.Id, "⠀⠀⠀⠀⠀⠀⠀⠀⠀Главное меню⠀⠀⠀⠀⠀⠀⠀⠀⠀", replyMarkup: keyboardInlineMenu, cancellationToken: cancellationToken);
                return;
                //await InlineButtonMainMenu(botClient,update.Message.Chat.Id, cancellationToken);
            }
        }

        return;

    }

    public static async Task InlineButtonMainMenu(ITelegramBotClient botClient, long? ChatId, CancellationToken cancellationToken)
    {

        // var message = update.Message;
        var keyboardInlineMenu = new InlineKeyboardMarkup(new[]
                          {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("🛒Магазин ",callbackData:"buttonMarket"),
                            InlineKeyboardButton.WithCallbackData("🗃️История Сделок",callbackData:"buttonTransactionHistory"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("⚙️ Настройки  ",callbackData:"buttonSettings"),

                            InlineKeyboardButton.WithCallbackData("🛠 ️Поддержка", callbackData: "buttonSupport"),
                             InlineKeyboardButton.WithCallbackData("📜 Правила", callbackData: "buttonRules"),
                        }

                    });

      // await botClient.SendTextMessageAsync(ChatId, "⠀⠀⠀⠀⠀⠀⠀⠀⠀Главное меню⠀⠀⠀⠀⠀⠀⠀⠀⠀", replyMarkup: keyboardInlineMenu, cancellationToken: cancellationToken);
        return;


    }
    private static async Task CallBack(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
    {

        //вывод в зависимости от выбранной кнопки
        Message message = update.Message;
        if (update != null && update.CallbackQuery != null)
        {
            string answer = update.CallbackQuery.Data;
            switch (answer)
            {
                case "buttonMarket":
                    await Market(botClient,update,cancellationToken);
                    break;
                case "buttonTransactionHistory":
                   await TransactionHistory(botClient,update,cancellationToken);
                    break;
                case "buttonSettings":
                    await Settigs(botClient,update, cancellationToken);
                    break;
                case "buttonSupport":
                    await Support(botClient, update);
                    break;
                case "buttonRules":
                    //await Rules(botClient,update, cancellationToken);
                    await Rules(botClient, update, cancellationToken);
                    break;
                case "buttonmain":
                    await InlineButtonMainMenu(botClient, update.CallbackQuery.Message.Chat.Id, cancellationToken);
                    break;
                case "buttonBack":
                     await botClient.DeleteMessageAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, cancellationToken: cancellationToken);
                    break;
            }


        }
    }
    

    // button
    public static async Task Support(ITelegramBotClient botClient, Telegram.Bot.Types.Update update)
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
    public static async Task Settigs(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
    {
        var keyboardInlineBack = new InlineKeyboardMarkup(new[]
                        {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("⬅️ Назад",callbackData:"buttonBack"),
                        }
                    });

        await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat, $"Настройки \n  твой никней  \n Баланс ", replyMarkup: keyboardInlineBack , cancellationToken: cancellationToken) ;
    }
    public static async Task Rules(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)

    { string messageRules = "1️⃣ Мы ничего не несем  .\n 2️⃣ Базар как вода и то и то нужно фильтроват иначе почкам конец .\n 3️⃣ Шкаф не тумба - тимон и пумба .\n 4️⃣ Знаешь красное море ? - я покрасил . \n 5️⃣ Мало ешь больше кушай.\n 6️⃣  Джони но не Деп";
        var message = update.Message;
        var keyboardInlineBack = new InlineKeyboardMarkup(new[]
                        {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("⬅️ Назад",callbackData:"buttonBack"),
                        }
                    });


        //await botClient.EditMessageTextAsync(update.Message.Chat.Id,messageId, "Привет");

       
        await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat, $"Правила \n {messageRules}", replyMarkup: keyboardInlineBack, cancellationToken: cancellationToken);
        // await botClient.SendTextMessageAsync(message.Chat.Id, messageRules, replyMarkup: keyboardInlineBack);
        return;




    }
    public static async Task TransactionHistory(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
    {

    }
    // 

    public static async Task Market(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
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


    public static async Task MarketHandler(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;
        //вывод в зависимости от выбранной кнопки
        if (update != null && update.CallbackQuery != null)
        {
            string answer = update.CallbackQuery.Data;
            switch (answer)
            {
                case "buttonAccountTelegarm":
                    var keyboardInlineMenuTelegram = new InlineKeyboardMarkup(new[]
                     {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Чистый  Аккаунт",callbackData:"ButtonTelegramNew"),
                            InlineKeyboardButton.WithCallbackData("Аккаунт с премиум ",callbackData:"buttonTelegramPremium"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Авторизованные акк",callbackData:"buttonTelegramauthorized"),

                            InlineKeyboardButton.WithCallbackData("Случайный акк", callbackData: "buttonTelegramRandomAccount"),

                        },
                        new []
                        {
                             InlineKeyboardButton.WithCallbackData("⬅️ Назад", callbackData: "buttonBackMenu"),
                        }
                    });
                    await botClient.EditMessageReplyMarkupAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, replyMarkup: keyboardInlineMenuTelegram);
                    //await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "⠀⠀⠀⠀⠀⠀⠀⠀Аккаунт Телеграмм⠀⠀⠀⠀⠀⠀⠀⠀", replyMarkup: keyboardInlineMenu, cancellationToken: cancellationToken);
                    break;
                case "buttonAccountVK":
                    var keyboardInlineMenuVk = new InlineKeyboardMarkup(new[]
                      {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Чистый  Аккаунт",callbackData:"ButtonVKNew"),
                            InlineKeyboardButton.WithCallbackData("С потвержденным телефоном",callbackData:"buttonVKconfirmed"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Аккаунт с панели(взломаный)",callbackData:"buttonVKvzlom"),

                            InlineKeyboardButton.WithCallbackData("Случайный акк", callbackData: "buttonVkRandomAccount"),

                        },
                        new []
                        {
                             InlineKeyboardButton.WithCallbackData("⬅️ Назад", callbackData: "buttonBackMenu"),
                        }
                     
                    });
                    await botClient.EditMessageReplyMarkupAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, replyMarkup: keyboardInlineMenuVk);
                    break;
                case "buttonAccountSteam":
                    var keyboardInlineMenuSteam = new InlineKeyboardMarkup(new[]
                     {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Чистый  Аккаунт",callbackData:"ButtonSteamNew"),
                            InlineKeyboardButton.WithCallbackData("С потвержденнием",callbackData:"buttonSteamconfirmed"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("С Steam Guard",callbackData:"buttonSteamGuard"),

                            InlineKeyboardButton.WithCallbackData("Случайный акк", callbackData: "buttonSteamRandomAccount"),

                        },
                        new []
                        {
                             InlineKeyboardButton.WithCallbackData("⬅️ Назад", callbackData: "buttonBackMenu"),
                        }

                    });
                    await botClient.EditMessageReplyMarkupAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, replyMarkup: keyboardInlineMenuSteam);
                    break;
                case "buttonAccountEA":
                    var keyboardInlineMenuEA = new InlineKeyboardMarkup(new[]
                     {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Чистый  Аккаунт",callbackData:"ButtonVKNew"),
                            InlineKeyboardButton.WithCallbackData("С потвержденным телефоном",callbackData:"buttonVKconfirmed"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Аккаунт с панели(взломаный)",callbackData:"buttonVKvzlom"),

                            InlineKeyboardButton.WithCallbackData("Случайный акк", callbackData: "buttonVkRandomAccount"),

                        },
                        new []
                        {
                             InlineKeyboardButton.WithCallbackData("⬅️ Назад", callbackData: "buttonBackMenu"),
                        }

                    });
                    await botClient.EditMessageReplyMarkupAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, replyMarkup: keyboardInlineMenuEA);
                    break;
                case "buttonAccountEGS":
                    var keyboardInlineMenuEGS = new InlineKeyboardMarkup(new[]
                     {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Чистый  Аккаунт",callbackData:"ButtonVKNew"),
                            InlineKeyboardButton.WithCallbackData("C случайными играми",callbackData:"buttonVKconfirmed"),
                        },
                        new []
                        {

                            InlineKeyboardButton.WithCallbackData("Случайный акк", callbackData: "buttonVkRandomAccount"),

                        },
                        new []
                        {
                             InlineKeyboardButton.WithCallbackData("⬅️ Назад", callbackData: "buttonBackMenu"),
                        }

                    });
                    await botClient.EditMessageReplyMarkupAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, replyMarkup: keyboardInlineMenuEGS);
                    break;
                case "buttonBackMenu":
                   
                   // await InlineButtonMainMenu(botClient, update.CallbackQuery.Message.Chat.Id, cancellationToken);

                    break;
            }


        }
    }

    // Account 
    public static async Task Telegramaccount(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
    {


        var message = update.Message;


        if (update != null && update.CallbackQuery != null)
        {
            string answer = update.CallbackQuery.Data;
            switch (answer)
            {
                case "ButtonTelegramNew":
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Ты выбрал новый   телеграмм");





                    break;
                case "buttonTelegramPremium":
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Ты выбрал новый  акк для доты  ");

                    break;
                case "buttonTelegramauthorized":
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Ты выбрал новый   авторизованый акк");
                    break;
                case "buttonTelegramRandomAccount":
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Ты выбрал случайунй акк");
                    break;

                case "buttonBackMenu":
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
                    await botClient.EditMessageReplyMarkupAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, replyMarkup: keyboardInlineMarketAccount, cancellationToken: cancellationToken);
                    break;
            }


        }
    }
    public static async Task VkAccount(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;


        if (update != null && update.CallbackQuery != null)
        {
            string answer = update.CallbackQuery.Data;
            switch (answer)
            {
                case "ButtonTelegramNew":
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Ты выбрал новый   телеграмм");





                    break;
                case "buttonTelegramPremium":
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Ты выбрал новый  акк для доты  ");

                    break;
                case "buttonTelegramauthorized":
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Ты выбрал новый   авторизованый акк");
                    break;
                case "buttonTelegramRandomAccount":
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Ты выбрал случайунй акк");
                    break;

                case "buttonBackMenu":
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
                    await botClient.EditMessageReplyMarkupAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, replyMarkup: keyboardInlineMarketAccount, cancellationToken: cancellationToken);
                    break;
            }


        }
    }

    public static async Task SteamAccount(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
    {

    }
    public static async Task EaAccount(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
    {

    }
    public static async Task EpicGameStoreAccount(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
    {
    }

}
   


