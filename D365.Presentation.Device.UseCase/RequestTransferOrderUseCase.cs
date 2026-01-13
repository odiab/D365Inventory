namespace D365.Presentation.Device.UseCase;

public class RequestTransferOrderUseCase(
    IMapper mapper,
    ID365Repository repository,
    ITransferOrderRepository transferOrderRepository) : IRequestTransferOrderUseCase
{
    public async Task<string?> ExecuteAsync(string baseUrl, Guid? id = null, string? transferOrderNumber = null, string? dataAreaId = null, RequestTypeEnum requestType = RequestTypeEnum.Ship)
    {
        D365RequestTransferOrderRecord? request = new();
        switch (requestType)
        {
            case RequestTypeEnum.Ship:
                {
                    var headerResult = await transferOrderRepository.SelectAsync(id);
                    if (headerResult is null)
                    {
                        return $"Can not load transfer order.";
                    }

                    request = mapper.Map<D365RequestTransferOrderRecord>(headerResult);
                    break;
                }
            case RequestTypeEnum.Receive:
                {
                    var result = await repository.GetTransferOrderAsync(baseUrl, dataAreaId, transferOrderNumber);
                    if (!result.Succeeded)
                    {
                        return
                            $"The transfer order with number #{transferOrderNumber} could not be found. Please check the order number and try again. If the issue persists, contact support for further assistance.";
                    }

                    request = mapper.Map<D365RequestTransferOrderRecord>(result.Data);
                    break;
                }
        }

        var requestResult = await repository.OnPostRequestTransferOrderAsync(baseUrl, request, requestType);
        if (requestResult is { Succeeded: true })
        {
            return requestResult.Data == null
                ? "An error occurred while processing your order. Please try again later. If the issue persists, contact support for further assistance."
                : requestResult.Data.Success.GetValueOrDefault(false)
                    ? null
                    : requestResult.Data.ErrorMessage;
        }

        if (requestResult.Errors != null)
        {
            return string.Join(Environment.NewLine, requestResult.Errors);
        }

        return requestResult.Data == null
            ? "An error occurred while processing your order. Please try again later. If the issue persists, contact support for further assistance."
            : requestResult.Data.Success.GetValueOrDefault(false)
                ? null
                : requestResult.Data.ErrorMessage;
    }
}