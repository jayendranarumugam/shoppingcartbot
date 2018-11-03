using System;
using System.Configuration;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

// For more information about this template visit http://aka.ms/azurebots-csharp-luis
[Serializable]
public class BasicLuisDialog : LuisDialog<object>
{
    public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(
        ConfigurationManager.AppSettings["LuisAppId"], 
        ConfigurationManager.AppSettings["LuisAPIKey"], 
        domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
    {
    }

    [LuisIntent("")]
    public async Task NoneIntent(IDialogContext context, LuisResult result)
    {
        await this.ShowLuisResult(context, result);
    }

    // Go to https://luis.ai and create a new intent, then train/publish your luis app.
    // Finally replace "Greeting" with the name of your newly created intent in the following handler
    [LuisIntent("welcomeintent")]
    public async Task WelcomeIntent(IDialogContext context, LuisResult result)
    {
        await this.ShowLuisResult(context, result);
    }

    [LuisIntent("itembypricingintent")]
    public async Task ActionOnRepositoryIntent(IDialogContext context, LuisResult result)
    {
        await this.ShowLuisResult(context, result);
    }

    [LuisIntent("helpintent")]
    public async Task HelpIntent(IDialogContext context, LuisResult result)
    {
        await this.ShowLuisResult(context, result);
    }


       

    private async Task ShowLuisResult(IDialogContext context, LuisResult result) 
    {
        string detectedIntent = result.Intents[0].Intent;
        string messageToUser = string.Empty;
        switch(detectedIntent)
        {
            case "welcomeintent":
                messageToUser = $"Greeting from XYZ Shopping World, I am chat bot of XYZ Shopping world.";
                break;

            case "helpintent":
                messageToUser = $"I can help you with activties like helping with your shopping Experience . \n I can get the Items based on your budject!";
                break;         

            case "itembypricingintent":
                //API Call to fetch the Items List by the pricing
                messageToUser = $"I will bring you the {result.Entities[0].Entity} by the pricing {result.Entities[1].Entity}";                 
                break;

            case "":
                messageToUser= $"Sorry I couldn't fetch that for now. Could you try again with other request";
                break;
                
        }
        await context.PostAsync(messageToUser);
        context.Wait(MessageReceived);
    }
}

