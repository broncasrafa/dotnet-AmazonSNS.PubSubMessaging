using Amazon.SimpleNotificationService.Model;

namespace CloudNotes.Infra.Notification.SNS;

public class SNSMessageAttributeCollection : Dictionary<string, MessageAttributeValue>
{
    public void Add(string attributeName, string attributeValue)
    {
        this[attributeName] = new MessageAttributeValue
        {
            DataType = "String",
            StringValue = attributeValue
        };
    }

    public void Add(string attributeName, float attributeValue)
    {
        this[attributeName] = new MessageAttributeValue
        {
            DataType = "Number",
            StringValue = attributeValue.ToString()
        };
    }

    public void Add(string attributeName, double attributeValue)
    {
        this[attributeName] = new MessageAttributeValue
        {
            DataType = "Number",
            StringValue = attributeValue.ToString()
        };
    }

    public void Add(string attributeName, int attributeValue)
    {
        this[attributeName] = new MessageAttributeValue
        {
            DataType = "Number",
            StringValue = attributeValue.ToString()
        };
    }

    public void Add(string attributeName, List<string> attributeValue)
    {
        string valueString = "[\"" + string.Join("\", \"", attributeValue.ToArray()) + "\"]";
        this[attributeName] = new MessageAttributeValue
        {
            DataType = "String.Array",
            StringValue = valueString
        };
    }

    public void Add(string attributeName, List<float> attributeValue)
    {
        string valueString = "[" + string.Join(", ", attributeValue.ToArray()) + "]";
        this[attributeName] = new MessageAttributeValue
        {
            DataType = "String.Array",
            StringValue = valueString
        };
    }

    public void Add(string attributeName, List<int> attributeValue)
    {
        string valueString = "[" + string.Join(", ", attributeValue.ToArray()) + "]";
        this[attributeName] = new MessageAttributeValue
        {
            DataType = "String.Array",
            StringValue = valueString
        };
    }

    public void Add(string attributeName, List<bool> attributeValue)
    {
        string valueString = "[" + string.Join(", ", attributeValue.ToArray()) + "]";
        this[attributeName] = new MessageAttributeValue
        {
            DataType = "String.Array",
            StringValue = valueString
        };
    }
}
