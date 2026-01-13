namespace D365.Presentation.Device.Models;

public class OrderChangedMessage(OrderMessageModel value) : ValueChangedMessage<OrderMessageModel>(value)
{
    public static OrderChangedMessage Create(OrderMessageModel model)
        => new(model);
}
