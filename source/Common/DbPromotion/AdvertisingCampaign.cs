namespace Common.DbPromotion
{
    internal class AdvertisingCampaign
    {
        /// <summary>
        /// ID of the advertising campaign.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Name of the advertising campaign.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Description of the advertising campaign.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Building block set with which the campaign messages were assembled.
        /// </summary>
        public string? BuildingBlockSetId { get; set; }
    }
}
