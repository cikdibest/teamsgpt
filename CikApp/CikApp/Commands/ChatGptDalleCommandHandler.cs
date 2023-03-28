//using CikApp.Models;
//using AdaptiveCards.Templating;
//using Microsoft.Bot.Builder;
//using Microsoft.Bot.Schema;
//using Microsoft.TeamsFx.Conversation;
//using Newtonsoft.Json;
//using OpenAI.GPT3.Managers;
//using OpenAI.GPT3;
//using OpenAI.GPT3.ObjectModels.RequestModels;
//using OpenAI.GPT3.ObjectModels;

//namespace CikApp.Commands
//{
//    /// <summary>
//    /// The <see cref="HelloWorldCommandHandler"/> registers a pattern with the <see cref="ITeamsCommandHandler"/> and 
//    /// responds with an Adaptive Card if the user types the <see cref="TriggerPatterns"/>.
//    /// </summary>
//    public class ChatGptDalleCommandHandler : ITeamsCommandHandler
//    {
//        private readonly ILogger<ChatGptDalleCommandHandler> _logger;
//        private readonly string _adaptiveCardFilePath = Path.Combine(".", "Resources", "GptCompletionCard.json");

//        public IEnumerable<ITriggerPattern> TriggerPatterns => new List<ITriggerPattern>
//        {
//            // Used to trigger the command handler if the command text contains 'gptcompletion'
//            new RegExpTrigger("gptdalle")
//        };

//        public ChatGptDalleCommandHandler(ILogger<ChatGptDalleCommandHandler> logger)
//        {
//            _logger = logger;
//        }

//        public async Task<ICommandResponse> HandleCommandAsync(ITurnContext turnContext, CommandMessage message, CancellationToken cancellationToken = default)
//        {
//            var openAiService = new OpenAIService(new OpenAiOptions()
//            {
//                ApiKey = "{APIKEY GOES HERE}"
//            });
//            var imageResult = await openAiService.Image.CreateImage(new ImageCreateRequest
//            {
//                Prompt = message.Text,
//                N = 2,
//                Size = StaticValues.ImageStatics.Size.Size256,
//                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
//            });


//            if (imageResult.Successful)
//            {
//                Console.WriteLine(string.Join("\n", imageResult.Results.Select(r => r.Url)));
//            }

//            _logger?.LogInformation($"Bot received message: {message.Text}");

//            // Read adaptive card template
//            var cardTemplate = await File.ReadAllTextAsync(_adaptiveCardFilePath, cancellationToken);

//            // Render adaptive card content
//            var cardContent = new AdaptiveCardTemplate(cardTemplate).Expand
//            (
//                new GptModel
//                {
//                    Title = "Your Gpt Bot is Running",
//                    Body = string.Join("\r\n", imageResult.Results.Select(r => r.Url))
//                }
//            );

//            // Build attachment
//            var activity = MessageFactory.Attachment
//            (
//                new Attachment
//                {
//                    ContentType = "application/vnd.microsoft.card.adaptive",
//                    Content = JsonConvert.DeserializeObject(cardContent),
//                }
//            );

//            // send response
//            return new ActivityCommandResponse(activity);
//        }
//    }
//}
