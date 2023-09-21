namespace Common.DbPromotion
{
    public class PromotionMessage
    {
        /// <summary>
        /// ServiceId of the service that sent the message.
        /// </summary>
        public string? ServiceId { get; set; }

        /// <summary>
        /// Name of the service that sent the message.
        /// </summary>
        public string? ServiceName { get; set; }

        /// <summary>
        /// Date when the message was sent.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The message that was sent.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// ID of the addressee to whom the message was sent.
        /// </summary>
        public string? AddresseeId { get; set; }

        /// <summary>
        /// Name of the addressee to whom the message was sent.
        /// </summary>
        public string? AddresseeName { get; set; }

        /// <summary>
        /// ID of the advertising campaign.
        /// </summary>
        public string? IdAdvertisingCampaign { get; set; }
    }
}
