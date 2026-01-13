namespace D365.Presentation.Device.Core.Records;

public record D365RequestTransferOrderRecord(
    [property: JsonPropertyName("_request")] D365TransferOrderRecord? Request = null
);

public record D365TransferOrderRecord(
    [property: JsonPropertyName("DataAreaId")] string? DataAreaId = null,
    [property: JsonPropertyName("transferOrderID")] string? TransferOrderId = null,
    [property: JsonPropertyName("transferLineNum")] int?[]? TransferLineNum = null,
    [property: JsonPropertyName("transferQTY")] int?[]? TransferQty = null
);

public record D365ResponseTransferOrderRecord(
    [property: JsonPropertyName("$id")] string? Id = null,
    [property: JsonPropertyName("ErrorMessage")] string? ErrorMessage = null,
    [property: JsonPropertyName("Success")] bool? Success = null,
    [property: JsonPropertyName("DebugMessage")] string? DebugMessage = null
);

public enum RequestTypeEnum : byte
{
    Ship,
    Receive
}