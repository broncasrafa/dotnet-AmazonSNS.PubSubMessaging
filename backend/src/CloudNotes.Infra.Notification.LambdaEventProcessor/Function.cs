using CloudNotes.Infra.Notification.LambdaEventProcessor.Models;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Newtonsoft.Json;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace CloudNotes.Infra.Notification.LambdaEventProcessor;

public class Function
{
    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>
    public Function()
    {

    }


    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an SNS event object and can be used 
    /// to respond to SNS messages.
    /// </summary>
    /// <param name="evnt">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public async Task FunctionHandler(SNSEvent snsEvent, ILambdaContext context)
    {
        context.Logger.LogLine($"Processing event: {JsonConvert.SerializeObject(snsEvent)}");

        foreach (var record in snsEvent.Records)
        {
            await ProcessRecordAsync(record, context);
        }
    }

    private async Task ProcessRecordAsync(SNSEvent.SNSRecord record, ILambdaContext context)
    {
        context.Logger.LogInformation($"Processed record {record.Sns.Message}");

        Event cloudNotesEvent = JsonConvert.DeserializeObject<Event>(record.Sns.Message);

        // TODO: Do interesting work based on the new message

        context.Logger.LogLine($"Processed record for note {cloudNotesEvent.NoteId}");

        await Task.CompletedTask;
    }
}